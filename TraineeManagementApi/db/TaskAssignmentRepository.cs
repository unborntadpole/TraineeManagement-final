namespace TraineeManagementApi.db;
using TraineeManagementApi.DTO;
using TraineeManagementApi.Models;
public class TaskAssignmentRepository
{
    private readonly ApplicationDbContext _context;

    public TaskAssignmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<TaskAssignmentDTO>> GetAllAsync()
    {
        var taskAsgs = _context.TaskAssignments;
        List<TaskAssignmentDTO> taskAsgns2 = [];
        foreach (var taskAsg in taskAsgs)
        {
            TaskAssignmentDTO taskAsg2 = new TaskAssignmentDTO(taskAsg);
            taskAsgns2.Add(taskAsg2);
        }
        return taskAsgns2;
    }

    public async Task<TaskAssignment?> GetByIdAsync(long id)
    {
        return await _context.TaskAssignments.FindAsync(id);
    }

    public async Task AddAsync(TaskAssignment taskAssignment)
    {
        await _context.TaskAssignments.AddAsync(taskAssignment);
    }

    public async Task Update(TaskAssignment taskAssignment)
    {
        _context.TaskAssignments.Update(taskAssignment);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

}