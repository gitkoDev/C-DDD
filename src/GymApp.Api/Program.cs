using GymApp.Application;
using GymApp.Infrastructure;
using GymApp.Infrastructure.Middleware;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddProblemDetails();
    builder.Services.AddHttpContextAccessor();
    
    builder.Services
        .AddApplication()
        .AddInfrastructure();

}

var app = builder.Build();
{
    app.UseExceptionHandler();
    app.UseMiddleware<EventualConsistencyMiddleware>();
    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();

    app.Run();
}