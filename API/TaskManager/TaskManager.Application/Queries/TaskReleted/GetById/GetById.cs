using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Application.Wrappers;
using TaskManager.Domain.Intefaces;

namespace TaskManager.Application.Queries.TaskReleted.GetById
{
    public class GetById : IConsumer<GetByIdQuery>
    {
        private readonly ILogger<GetById> logger;
        private readonly ITaskService taskService;
        private readonly IConfiguration configuration;
        public GetById(ILogger<GetById> logger, ITaskService taskService, IConfiguration configuration)
        {
            this.logger = logger;
            this.taskService = taskService;
            this.configuration = configuration;
        }

        public async Task Consume(ConsumeContext<GetByIdQuery> context)
        {
            try
            {
                this.logger.LogInformation($"[GetById] Received event");
                if (context.Message.Id != 0)
                {
                    this.logger.LogInformation($"[GetById] TaskService GetTaskById method call");
                    var findTask = await this.taskService.GetTaskByIdAsync(context.Message.Id);

                    if (findTask != null)
                    {
                        this.logger.LogInformation($"[GetById] Successfuly get task id {context.Message.Id}");

                        var response = new GetByIdResponse
                        {
                            task = findTask
                        };

                        await context.RespondAsync(ResponseWrapper<GetByIdResponse>.Success("Successfuly get task", response));

                    }
                    else
                    {
                        this.logger.LogInformation($"[GetById] Failed to get task id {context.Message.Id}");
                        await context.RespondAsync(ResponseWrapper<GetByIdResponse>.Fail("Failed to get task Invalid Task Id"));

                    }
                }
                else
                {
                    this.logger.LogInformation($"[GetById] Invalid Task Id {context.Message.Id}");
                    await context.RespondAsync(ResponseWrapper<GetByIdResponse>.Fail("Invalid Task Id"));
                }
                
            }
            catch (Exception ex)
            {
                this.logger.LogDebug(ex, $"[GetById] id {context.Message.Id} - exception occored. stacktrace: {ex.StackTrace}");
                await context.RespondAsync(ResponseWrapper<GetByIdResponse>.Fail(ex.Message));
            }
        }
    }
}
