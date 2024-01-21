using Microsoft.EntityFrameworkCore;
using TaskManager.Domain.Intefaces.ITaskRepository;
using TaskManager.Domain.Intefaces;
using TaskManager.Infrastructure.Persistence.EFCore;
using TaskManager.Infrastructure.Persistence.Repository.TaskRepository;
using TaskManager.Domain.Services;

namespace TaskManager.API.Configuration
{
    public static class TaskManagerServiceConfiguration
    {
        public static void TaskManagerServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddDbContext<TaskManagerDbContext>(
                options =>
                {
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                }
              );

            //application Serrvice
            services.AddScoped<ITaskService, TaskService>();

            //application repository
            services.AddScoped<ITaskReadRepository, TaskReadRepository>();
            services.AddScoped<ITaskWriteRepository, TaskWriteRepository>();

         


        }
    }
}
