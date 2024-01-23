// Setup logger for bootstrap process
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using TaskManager.API.Configuration;
using TaskManager.API.Middleware;

Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();
try
{
    Log.Information("Application Start");
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var allowedHosts = builder.Configuration.GetValue(typeof(string), "AllowedHosts") as string;

    builder.Services.AddCors(options =>
    {
        options.AddDefaultPolicy(
            builder =>
            {
                if (allowedHosts == null || allowedHosts == "*")
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                    return;
                }
                string[] hosts;
                if (allowedHosts.Contains(';'))
                    hosts = allowedHosts.Split(';');
                else
                {
                    hosts = new string[1];
                    hosts[0] = allowedHosts;
                }
                builder.WithOrigins(hosts)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
            });
    });


    //manage all services 
    builder.Services.AddHttpContextAccessor();

    builder.Services.TaskManagerServices(builder.Configuration, builder.Environment);

    builder.Services.AddMassTransitComponents(builder.Configuration);
    
    builder.Services.AddControllers();

    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    });
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
    }


    app.UseForwardedHeaders();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    //Configure cors
    app.UseCors(x => x
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials());


    app.MapControllers();
    app.UseRouting();
    app.Run();

}
catch (Exception ex)
{

    Log.Fatal(ex, "Host terminated unexpectedly");
    
}
finally
{
    Log.CloseAndFlush();
}



