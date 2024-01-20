using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;

namespace TaskManager.Domain.Intefaces.ITaskRepository
{
    public interface ITaskReadRepository
    {
        Task<TaskDetail> GetTaskByIdAsync(long taskId);
        Task<List<TaskDetail>> GetAllTasksAsync();
    }
}
