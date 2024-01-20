using MassTransit;
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

        public GetById(ILogger<GetById> logger, ITaskService taskService)
        {
            this.logger = logger;
            this.taskService = taskService;
        }

        public async Task Consume(ConsumeContext<GetByIdQuery> context)
        {
            try
            {
                this.logger.LogInformation($"[GetById] Received event");
               
                this.logger.LogInformation($"[GetById] taskService GetTaskById method call");
                var findTask =  await this.taskService.GetTaskByIdAsync(context.Message.Id);

                if (findTask != null)
                {
                    this.logger.LogInformation($"[GetById] task get by id successfuly");

                    var response = new GetByIdResponse
                    {
                        taskDetail = findTask
                    };

                    await context.RespondAsync(ResponseWrapper<GetByIdResponse>.Success("successfuly get task", response));

                }
                else
                {
                    this.logger.LogInformation($"[GetById] fail to get task");
                    await context.RespondAsync(ResponseWrapper<GetByIdResponse>.Fail("fail to get task"));
                   
                }
            }
            catch (Exception ex)
            {
                this.logger.LogDebug(ex, $"[GetById] - exception occored. stacktrace: {ex.StackTrace}");
                await context.RespondAsync(ResponseWrapper<GetByIdResponse>.Fail(ex.Message));
            }
        }
    }
}
