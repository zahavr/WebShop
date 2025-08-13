using Catalog.API;
using Catalog.API.Configurations;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.AddExternalServices();
builder.Services.AddInternalServices();
builder.Services.AddPersistence(builder.Configuration, builder.Environment.IsEnvironment(Constants.Environments.Local));
builder.Services.AddHealthChecks();
builder.Services.AddExceptionHandler<CommonApiExceptionHandler>();

#endregion

WebApplication app = builder.Build();

#region HTTP request pipelines

app.MapCarter();
app.UseExceptionHandler(options => {});
app.UseHealthChecks("/health", new HealthCheckOptions()
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

#endregion

app.Run();