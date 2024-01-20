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
    public class TaskWriteRepository : ITaskWriteRepository
    {
        private readonly TaskManagerDbContext _dbContext;

        public TaskWriteRepository(TaskManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public TaskDetail AddTask(TaskDetail task)
        {
            _dbContext.TaskDetails.Add(task);
            _dbContext.SaveChanges();
            return task;
        }

        public TaskDetail UpdateTask(TaskDetail task)
        {
            _dbContext.TaskDetails.Update(task);
            _dbContext.SaveChanges();
            return task;
        }

        public async Task<bool> DeleteTask(long taskId)
        {
            var task = await _dbContext.TaskDetails.FindAsync(taskId);
            if (task == null)
                return false;

            _dbContext.TaskDetails.Remove(task);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
