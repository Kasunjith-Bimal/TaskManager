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

                this.logger.LogInformation($"[CreateTask] taskService addTask method call");
                var addedTaskDetail =  this.taskService.AddTask(context.Message.taskDetail);

                if (addedTaskDetail != null)
                {
                    this.logger.LogInformation($"[CreateTask] task added successfuly task id : {addedTaskDetail.Id} title : {addedTaskDetail.Title}");
                    
                    var response = new CreateTaskResponse
                    {
                        taskDetail = addedTaskDetail
                    };

                    await context.RespondAsync(ResponseWrapper<CreateTaskResponse>.Success("Task added successfuly", response));

                }
                else
                {
                    this.logger.LogInformation($"[CreateTask] task added fail");
                    await context.RespondAsync(ResponseWrapper<CreateTaskResponse>.Fail("Task added fail"));
                   
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
