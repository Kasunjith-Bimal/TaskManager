using MassTransit;
using TaskManager.API.Extensions;
using TaskManager.Application.Command.TaskReleted.CreateTask;
using TaskManager.Application.Command.TaskReleted.DeleteTask;
using TaskManager.Application.Command.TaskReleted.UpdateTask;
using TaskManager.Application.Queries.TaskReleted.GetAllTask;
using TaskManager.Application.Queries.TaskReleted.GetById;

namespace TaskManager.API.Configuration
{
    public static class MassTransitConfiguration
    {
        public static void AddMassTransitComponents(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediator(x =>
            {   // commands
                x.AddConsumer<CreateTask>();
                x.AddConsumer<UpdateTask>();
                x.AddConsumer<DeleteTask>();

                // queries
                x.AddConsumer<GetAllTask>();
                x.AddConsumer<GetById>();
                x.ConfigureMediator((context, cfg) => cfg.UseHttpContextScopeFilter(context));
            });

        }
    }
}
