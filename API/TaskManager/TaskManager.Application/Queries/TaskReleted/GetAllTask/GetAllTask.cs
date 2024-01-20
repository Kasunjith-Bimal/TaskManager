using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Wrappers;
using TaskManager.Domain.Intefaces;

namespace TaskManager.Application.Queries.TaskReleted.GetAllTask
{
    public class GetAllTask : IConsumer<GetAllTaskQuery>
    {
        private readonly ILogger<GetAllTask> logger;
        private readonly ITaskService taskService;

        public GetAllTask(ILogger<GetAllTask> logger, ITaskService taskService)
        {
            this.logger = logger;
            this.taskService = taskService;
        }

        public async Task Consume(ConsumeContext<GetAllTaskQuery> context)
        {
            try
            {
                this.logger.LogInformation($"[GetAllTask] Received event");
               
                this.logger.LogInformation($"[GetAllTask] taskService GetAllTasksAsync method call");
                var allTasks =  await this.taskService.GetAllTasksAsync();

                if (allTasks != null)
                {
                    this.logger.LogInformation($"[GetAllTask] get successfuly");

                    var response = new GetAllTaskResponse
                    {
                        tasks = allTasks
                    };

                    await context.RespondAsync(ResponseWrapper<GetAllTaskResponse>.Success("successfuly get all tasks", response));

                }
                else
                {
                    this.logger.LogInformation($"[GetAllTask] get fail");
                    await context.RespondAsync(ResponseWrapper<GetAllTaskResponse>.Fail("fail to get all tasks"));
                   
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
