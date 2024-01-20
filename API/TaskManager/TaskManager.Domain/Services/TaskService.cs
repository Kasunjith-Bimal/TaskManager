using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Intefaces;
using TaskManager.Domain.Intefaces.ITaskRepository;

namespace TaskManager.Domain.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskReadRepository _readRepository;
        private readonly ITaskWriteRepository _writeRepository;

        public TaskService(ITaskReadRepository readRepository, ITaskWriteRepository writeRepository)
        {
            _readRepository = readRepository;
            _writeRepository = writeRepository;
        }

        public Task<TaskDetail> GetTaskByIdAsync(long taskId)
        {
            return _readRepository.GetTaskByIdAsync(taskId);
        }

        public Task<List<TaskDetail>> GetAllTasksAsync()
        {
            return _readRepository.GetAllTasksAsync();
        }

        public TaskDetail AddTask(TaskDetail task)
        {
            return _writeRepository.AddTask(task);
        }

        public TaskDetail UpdateTask(TaskDetail task)
        {
            return _writeRepository.UpdateTask(task);
        }

        public Task<bool> DeleteTask(long taskId)
        {
            return _writeRepository.DeleteTask(taskId);
        }
    }
}
