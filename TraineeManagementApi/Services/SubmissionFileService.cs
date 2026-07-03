namespace TraineeManagementApi.Services;

using TraineeManagementApi.db;
using TraineeManagementApi.DTO;
using System.Security.Cryptography;
using TraineeManagementApi.Models;
using TraineeManagementApi.Constants;

public class SubmissionFileService
{
    private readonly ILogger<SubmissionFileService> _logger;
    private readonly SubmissionFileRepository _repository;
    private readonly RabbitMQProducer _producer;
    private readonly IFileStorageService _fileStorageService;

    private readonly ProcessingJobsRepository _jobRepo;
    private readonly string _queueName = QueueConstants.SubmissionQueueName;
    private readonly string exchangeAndRoutingKey = QueueConstants.SubmissionExchangeAndRoutingKey;

    public SubmissionFileService(
        SubmissionFileRepository repository, 
        ILogger<SubmissionFileService> logger, 
        RabbitMQProducer producer, 
        IFileStorageService fileStorageService,
        ProcessingJobsRepository jobRepo
    )
    {
        _repository = repository;
        _logger = logger;
        _producer = producer;
        _fileStorageService = fileStorageService;
        _jobRepo = jobRepo;
    }

    public async Task<string> GetChecksum(IFormFile file)
    {
        using var stream = file.OpenReadStream();
        using var sha256 = SHA256.Create();
        byte[] hashBytes = await sha256.ComputeHashAsync(stream);
        string calculatedChecksum = BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        return calculatedChecksum;
    }

    public async Task<Result<PostFileResponse>> PostFile(IFormFile file, string user, long submissionId)
    {
        // if (file == null || file.Length == 0)
        // {
        //     _logger.LogWarning("Save file failed: File is empty");
        //     return Result<PostFileResponse>.ServerError("File cannot be empty", 400);
        // }
        Result<string> result = await _fileStorageService.SaveAsync(file);
        _logger.LogInformation($"File stored with name {result.Value}.");
        if(result.IsSuccess)
        {
            string checksum = await GetChecksum(file);
            SubmissionFile saveFile = new SubmissionFile()
            {
                OriginalFileName = file.FileName,
                GeneratedStorageName = result.Value,
                Size = file.Length,
                ContentType = file.ContentType,
                Checksum = checksum,
                UploadedByUser = user,
                UploadedDate = DateTime.UtcNow,
                SubmissionId = submissionId
            };
            try
            {
                await _repository.AddAsync(saveFile);
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to save metadata:\n{ex}");
                return Result<PostFileResponse>.ServerError("Failed to save file..",500);
            }
            _logger.LogInformation($"File metadata saved with id {saveFile.Id}");
            PostFileResponse response = new PostFileResponse(saveFile);
            SubmissionProcessingRequested payload = new SubmissionProcessingRequested(
                MessageId: Guid.NewGuid(),
                CorrelationId: Guid.NewGuid(),
                SubmissionId: submissionId,
                FileId: saveFile.Id,
                RequestedAt: DateTimeOffset.UtcNow,
                ContractVersion: UploadFilesConstants.SubmissionProcessingRequestedVersion.ToString()
            );
            response.TrackingId = payload.CorrelationId.ToString();
            try
            {
                await _producer.PublishAsync(exchangeAndRoutingKey, _queueName ,payload);

                _logger.LogInformation(
                    "Published message successfully. MessageId: {MsgId}, CorrelationId: {CorrId}, SubmissionId: {SubId}",
                    payload.MessageId, payload.CorrelationId, payload.SubmissionId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Broker down. Failed to queue message with correlationId: {payload.CorrelationId}");
                return Result<PostFileResponse>.ServerError("Persistence succeeded but worker queue unavailable. Please retry later.", 500);
            }
            try
            {
                await _jobRepo.PostByIdAsync(payload.CorrelationId.ToString());
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to save the ProcessingJob info in database:\n{e.Message} \nWarning: Job has been queued");
            }
            return Result<PostFileResponse>.SuccessWithCode( response, 202);
        }
        else return Result<PostFileResponse>.ServerError(result.Error, result.ErrorCode);
    }

    public async Task<Result<DownloadFileDTO>> GetFile(long submissionFileId)
    {
        SubmissionFile? fileMetadata = await _repository.GetByIdAsync(submissionFileId);
        if (fileMetadata == null)
        {
            return Result<DownloadFileDTO>.ServerError($"File with id {submissionFileId} not found", 404);
        }
        var res = await _fileStorageService.OpenReadAsync(fileMetadata.GeneratedStorageName);
        if (!res.IsSuccess)
        {
            return Result<DownloadFileDTO>.ServerError(res.Error,res.ErrorCode);
        }
        return Result<DownloadFileDTO>.Success(new DownloadFileDTO(res.Value, fileMetadata.ContentType, fileMetadata.OriginalFileName));
    }

    public async Task<Result<string>> DeleteFile(long id)
    {
        SubmissionFile? fileMetadata = await _repository.GetByIdAsync(id);
        if (fileMetadata == null)
        {
            return Result<string>.ServerError($"File with id {id} not found", 404);
        }
        try
        {
            Result<string> res = await _fileStorageService.DeleteAsync(fileMetadata.GeneratedStorageName);
            if (res.IsSuccess)
            {
                string name = fileMetadata.OriginalFileName;
                await _repository.DeleteAsync(fileMetadata);
                return Result<string>.Success($"Deleted file {name} Successfully.");
            }
            else
            {
                return res;
            }
        }
        catch( Exception e)
        {
            return Result<string>.ServerError(e.Message, 500);
        }
    }
}