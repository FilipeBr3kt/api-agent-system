using api_sistema_agente.Application;
using api_sistema_agente.Domain;
using api_sistema_agente.Presentation;
using api_sistema_agente.Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);
{
    if (builder.Environment.IsDevelopment())
    {
        builder.Configuration.AddUserSecrets<Program>();
    }

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services
        .AddApplication()
        .AddPresentation()
        .AddInfrastructure(builder.Configuration)
        .AddControllers();
}

{
    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.MapControllers();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.Run();
}