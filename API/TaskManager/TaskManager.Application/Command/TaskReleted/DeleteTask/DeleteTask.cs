using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Wrappers;
using TaskManager.Domain.Intefaces;

namespace TaskManager.Application.Command.TaskReleted.DeleteTask
{
    public class DeleteTask : IConsumer<DeleteTaskCommand>
    {
        private readonly ILogger<DeleteTask> logger;
        private readonly ITaskService taskService;

        public DeleteTask(ILogger<DeleteTask> logger, ITaskService taskService)
        {
            this.logger = logger;
            this.taskService = taskService;
        }

        public async Task Consume(ConsumeContext<DeleteTaskCommand> context)
        {
            try
            {
                this.logger.LogInformation($"[DeleteTask] Received event");
                if (context.Message.Id == 0)
                {
                    this.logger.LogInformation($"[DeleteTask] Id cannot be empty");
                    await context.RespondAsync(ResponseWrapper<DeleteTaskResponse>.Fail("Id cannot be empty"));
                }

                var getTask = await this.taskService.GetTaskByIdAsync(context.Message.Id);

                if(getTask != null)
                {
                    this.logger.LogInformation($"[DeleteTask] TaskService addTask method call");
                    var deleteTask = await this.taskService.DeleteTask(context.Message.Id);

                    if (deleteTask)
                    {
                        this.logger.LogInformation($"[DeleteTask] Task delete successfully task id : {context.Message.Id}");

                        var response = new DeleteTaskResponse
                        {
                            IsDelete = deleteTask
                        };

                        await context.RespondAsync(ResponseWrapper<DeleteTaskResponse>.Success("Task delete successfully", response));

                    }
                    else
                    {
                        this.logger.LogInformation($"[DeleteTask] Failed to delete task id {context.Message.Id}");
                        await context.RespondAsync(ResponseWrapper<DeleteTaskResponse>.Fail("Failed to delete task."));

                    }
                }
                else
                {
                    this.logger.LogInformation($"[DeleteTask] Invalid Task Id {context.Message.Id}");
                    await context.RespondAsync(ResponseWrapper<DeleteTaskResponse>.Fail("Invalid Task Id."));
                }

                
            }
            catch (Exception ex)
            {
                this.logger.LogDebug(ex, $"[DeleteTask] id: {context.Message.Id} - exception occored. stacktrace: {ex.StackTrace}");
                await context.RespondAsync(ResponseWrapper<DeleteTaskResponse>.Fail(ex.Message));
            }
        }
    }
}
