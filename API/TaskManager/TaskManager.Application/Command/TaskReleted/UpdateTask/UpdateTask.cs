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

                if (context.Message.taskDetail.Id == 0)
                {
                    this.logger.LogInformation($"[UpdateTask] Id cannot be empty");
                    await context.RespondAsync(ResponseWrapper<UpdateTaskResponse>.Fail("Id cannot be empty"));
                }

                this.logger.LogInformation($"[UpdateTask] taskService Update Task method call taskid : {context.Message.taskDetail.Id}");
                var updatedTaskDetail =  this.taskService.UpdateTask(context.Message.taskDetail);

                if (updatedTaskDetail != null)
                {
                    this.logger.LogInformation($"[UpdateTask] task update successfuly task id : {updatedTaskDetail.Id} title : {updatedTaskDetail.Title}");
                    
                    var response = new UpdateTaskResponse
                    {
                        taskDetail = updatedTaskDetail
                    };

                    await context.RespondAsync(ResponseWrapper<UpdateTaskResponse>.Success("Task update successfuly", response));

                }
                else
                {
                    this.logger.LogInformation($"[UpdateTask] task update fail id ; {context.Message.taskDetail.Id}");
                    await context.RespondAsync(ResponseWrapper<UpdateTaskResponse>.Fail("Task update fail"));
                   
                }
            }
            catch (Exception ex)
            {
                this.logger.LogDebug(ex, $"[UpdateTask] Title: {context.Message.taskDetail.Title} - exception occored. stacktrace: {ex.StackTrace}");
                await context.RespondAsync(ResponseWrapper<UpdateTaskResponse>.Fail(ex.Message));
            }
        }
    }
}
