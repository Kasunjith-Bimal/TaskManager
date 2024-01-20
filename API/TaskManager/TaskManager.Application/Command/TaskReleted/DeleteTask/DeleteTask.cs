﻿using MassTransit;
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

                this.logger.LogInformation($"[DeleteTask] taskService addTask method call");
                var deleteTask = await  this.taskService.DeleteTask(context.Message.Id);

                if (deleteTask)
                {
                    this.logger.LogInformation($"[DeleteTask] task delete successfuly task id : {context.Message.Id}");
                    
                    var response = new DeleteTaskResponse
                    {
                        IsDelete = deleteTask
                    };

                    await context.RespondAsync(ResponseWrapper<DeleteTaskResponse>.Success("Task delete successfuly", response));

                }
                else
                {
                    this.logger.LogInformation($"[DeleteTask] task delete fail");
                    await context.RespondAsync(ResponseWrapper<DeleteTaskResponse>.Fail("Task delete fail"));
                   
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