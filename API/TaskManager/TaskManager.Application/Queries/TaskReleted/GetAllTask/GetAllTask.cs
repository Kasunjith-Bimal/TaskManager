﻿using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Wrappers;
using TaskManager.Domain.Entities;
using TaskManager.Domain.Intefaces;

namespace TaskManager.Application.Queries.TaskReleted.GetAllTask
{
    public class GetAllTask : IConsumer<GetAllTaskQuery>
    {
        private readonly ILogger<GetAllTask> logger;
        private readonly ITaskService taskService;
        private readonly IConfiguration configuration;
        public GetAllTask(ILogger<GetAllTask> logger, ITaskService taskService, IConfiguration configuration)
        {
            this.logger = logger;
            this.taskService = taskService;
            this.configuration = configuration;
        }

        public async Task Consume(ConsumeContext<GetAllTaskQuery> context)
        {
            try
            {
                this.logger.LogInformation($"[GetAllTask] Received event");
               
                this.logger.LogInformation($"[GetAllTask] TaskService GetAllTasksAsync method call");
                var allTasks =  await this.taskService.GetAllTasksAsync();

                if (allTasks != null)
                {
                    this.logger.LogInformation($"[GetAllTask] Successfuly get all tasks");

                    var response = new GetAllTaskResponse
                    {
                        tasks = allTasks.OrderByDescending(x => x.DueDate).ToList()
                    };

                    await context.RespondAsync(ResponseWrapper<GetAllTaskResponse>.Success("Successfuly get all tasks", response));

                }
                else
                {
                    var response = new GetAllTaskResponse
                    {
                        tasks = new List<TaskDetail>()
                    };

                    await context.RespondAsync(ResponseWrapper<GetAllTaskResponse>.Success("Successfuly get all tasks", response));

                }
            }
            catch (Exception ex)
            {
                this.logger.LogDebug(ex, $"[GetAllTask] - exception occored. stacktrace: {ex.StackTrace}");
                await context.RespondAsync(ResponseWrapper<GetAllTaskResponse>.Fail(ex.Message));
            }
        }
    }
}
