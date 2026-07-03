namespace TraineeManagementApi.Services;

using FluentValidation;
using TraineeManagementApi.DTO;
using TraineeManagementApi.Constants;

// public enum ValidStatus
// {
//     Active,
//     Inactive,
//     Completed
// }

public class CreateTraineeRequestValidator : AbstractValidator<TraineeRequest>
{
    public CreateTraineeRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name must be smaller than 50 characters");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name must be smaller than 50 characters");
        
        RuleFor(x => x.Email)
            // .NotEmpty().WithMessage("Email is required")
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.Net4xRegex).WithMessage("Invalid Email");
        
        RuleFor(x => x.TechStack)
            .NotEmpty().WithMessage("TechStack is required");
        
        RuleFor(x => x.Status)
            // .NotEmpty().WithMessage("Status is required")
            .Must( status => Enum.IsDefined(typeof(StatusEnums.TraineeStatus),status)).WithMessage("Invalid status");
        
    }
}

public class UpdateTraineeRequestValidator : AbstractValidator<TraineeRequest>
{
    public UpdateTraineeRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required")
            .MaximumLength(50).WithMessage("First name must be smaller than 50 characters");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required")
            .MaximumLength(50).WithMessage("Last name must be smaller than 50 characters");
        
        RuleFor(x => x.Email)
            // .NotEmpty().WithMessage("Email is required")
            .EmailAddress(FluentValidation.Validators.EmailValidationMode.Net4xRegex).WithMessage("Invalid Email");
        
        RuleFor(x => x.TechStack)
            .NotEmpty().WithMessage("TechStack is required");
        
        RuleFor(x => x.Status)
            // .NotEmpty().WithMessage("Status is required")
            .Must( status => Enum.IsDefined(typeof(StatusEnums.TraineeStatus),status)).WithMessage("Invalid status");
        
    }
}