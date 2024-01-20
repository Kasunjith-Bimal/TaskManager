using MassTransit;
using TaskManager.Application.Command.TaskReleted.CreateTask;
using TaskManager.Application.Command.TaskReleted.DeleteTask;
using TaskManager.Application.Command.TaskReleted.UpdateTask;

namespace TaskManager.API.Configuration
{
    public static class MassTransitConfiguration
    {
        public static void AddMassTransitComponents(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediator(x =>
            {
                x.AddConsumer<CreateTask>();
                x.AddConsumer<UpdateTask>();
                x.AddConsumer<DeleteTask>();
            });
        }
    }
}
