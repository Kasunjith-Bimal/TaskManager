using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Wrappers;
using TaskManager.Domain.Intefaces;

namespace TaskManager.Application.Command.TaskReleted.UpdateTask
{
    public class UpdateTask : IConsumer<UpdateTaskCommand>
    {
        private readonly ILogger<UpdateTask> logger;
        private readonly ITaskService taskService;

        public UpdateTask(ILogger<UpdateTask> logger, ITaskService taskService)
        {
            this.logger = logger;
            this.taskService = taskService;
        }

        public async Task Consume(ConsumeContext<UpdateTaskCommand> context)
        {
            try
            {
                this.logger.LogInformation($"[UpdateTask] Received event");
                if (String.IsNullOrEmpty(context.Message.taskDetail.Title))
                {
                    this.logger.LogInformation($"[UpdateTask] Title cannot be empty");
                    await context.RespondAsync(ResponseWrapper<UpdateTaskResponse>.Fail("Title cannot be empty"));
                }

                if (String.IsNullOrEmpty(context.Message.taskDetail.Description))
                {
                    this.logger.LogInformation($"[UpdateTask] Description cannot be empty");
                    await context.RespondAsync(ResponseWrapper<UpdateTaskResponse>.Fail("Description cannot be empty"));
                }

                if (context.Message.taskDetail.Id == 0)
                {
                    this.logger.LogInformation($"[UpdateTask] Id cannot be empty");
                    await context.RespondAsync(ResponseWrapper<UpdateTaskResponse>.Fail("Id cannot be empty"));
                }

                if (context.Message.taskDetail.Id != context.Message.Id)
                {
                    this.logger.LogInformation($"[UpdateTask] Id missmatch");
                    await context.RespondAsync(ResponseWrapper<UpdateTaskResponse>.Fail("Id missmatch"));
                }

                var getTask = await this.taskService.GetTaskByIdAsync(context.Message.Id);

                if(getTask != null)
                {
                    this.logger.LogInformation($"[UpdateTask] TaskService UpdateTask method call taskid : {context.Message.taskDetail.Id}");
                    var updatedTaskDetail = this.taskService.UpdateTask(context.Message.taskDetail);

                    if (updatedTaskDetail != null)
                    {
                        this.logger.LogInformation($"[UpdateTask] Task update successfully task id : {updatedTaskDetail.Id} title : {updatedTaskDetail.Title}");

                        var response = new UpdateTaskResponse
                        {
                            task = updatedTaskDetail
                        };

                        await context.RespondAsync(ResponseWrapper<UpdateTaskResponse>.Success("Task update successfully.", response));

                    }
                    else
                    {
                        this.logger.LogInformation($"[UpdateTask] Failed to update task id ; {context.Message.taskDetail.Id}");
                        await context.RespondAsync(ResponseWrapper<UpdateTaskResponse>.Fail("Failed to update task."));

                    }
                }
                else
                {
                    this.logger.LogInformation($"[UpdateTask] Invalid Task Id ; {context.Message.taskDetail.Id}");
                    await context.RespondAsync(ResponseWrapper<UpdateTaskResponse>.Fail("Invalid Task Id."));
                }

                
            }
            catch (Exception ex)
            {
                this.logger.LogDebug(ex, $"[UpdateTask] id : {context.Message.taskDetail.Id} Title: {context.Message.taskDetail.Title} - exception occored. stacktrace: {ex.StackTrace}");
                await context.RespondAsync(ResponseWrapper<UpdateTaskResponse>.Fail(ex.Message));
            }
        }
    }
}
