using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Wrappers;
using TaskManager.Domain.Intefaces;

namespace TaskManager.Application.Command.TaskReleted.CreateTask
{
    public class CreateTask : IConsumer<CreateTaskCommand>
    {
        private readonly ILogger<CreateTask> logger;
        private readonly ITaskService taskService;

        public CreateTask(ILogger<CreateTask> logger, ITaskService taskService)
        {
            this.logger = logger;
            this.taskService = taskService;
        }

        public async Task Consume(ConsumeContext<CreateTaskCommand> context)
        {
            try
            {
                this.logger.LogInformation($"[CreateTask] Received event");
                if (String.IsNullOrEmpty(context.Message.taskDetail.Title))
                {
                    this.logger.LogInformation($"[CreateTask] Title cannot be empty");
                    await context.RespondAsync(ResponseWrapper<CreateTaskResponse>.Fail("Title cannot be empty"));
                }

                if (String.IsNullOrEmpty(context.Message.taskDetail.Description))
                {
                    this.logger.LogInformation($"[CreateTask] Description cannot be empty");
                    await context.RespondAsync(ResponseWrapper<CreateTaskResponse>.Fail("Description cannot be empty"));
                }

                this.logger.LogInformation($"[CreateTask] TaskService addTask method call");
                var addedTaskDetail =  this.taskService.AddTask(context.Message.taskDetail);

                if (addedTaskDetail != null)
                {
                    this.logger.LogInformation($"[CreateTask] Task added successfully task id : {addedTaskDetail.Id} title : {addedTaskDetail.Title}");
                    
                    var response = new CreateTaskResponse
                    {
                        task = addedTaskDetail
                    };

                    await context.RespondAsync(ResponseWrapper<CreateTaskResponse>.Success("Task added successfully.", response));

                }
                else
                {
                    this.logger.LogInformation($"[CreateTask] Failed to Add task title {context.Message.taskDetail.Title}");
                    await context.RespondAsync(ResponseWrapper<CreateTaskResponse>.Fail("Failed to Add task."));
                   
                }
            }
            catch (Exception ex)
            {
                this.logger.LogDebug(ex, $"[CreateTask] Title: {context.Message.taskDetail.Title} - exception occored. stacktrace: {ex.StackTrace}");
                await context.RespondAsync(ResponseWrapper<CreateTaskResponse>.Fail(ex.Message));
            }
        }
    }
}
