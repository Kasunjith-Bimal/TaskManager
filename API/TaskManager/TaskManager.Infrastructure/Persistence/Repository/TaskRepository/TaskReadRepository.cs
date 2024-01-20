using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Intefaces.ITaskRepository;
using TaskManager.Infrastructure.Persistence.EFCore;

namespace TaskManager.Infrastructure.Persistence.Repository.TaskRepository
{
    public class TaskReadRepository : ITaskReadRepository
    {
        private readonly TaskManagerDbContext _dbContext;

        public TaskReadRepository(TaskManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TaskDetail> GetTaskByIdAsync(long taskId)
        {
            return await _dbContext.TaskDetails.FindAsync(taskId);
        }

        public async Task<List<TaskDetail>> GetAllTasksAsync()
        {
            return await _dbContext.TaskDetails.ToListAsync();
        }
    }
}
