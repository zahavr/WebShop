using Catalog.API;
using Catalog.API.Configurations;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.AddExternalServices();
builder.Services.AddInternalServices();
builder.Services.AddPersistence(builder.Configuration, builder.Environment.IsEnvironment(Constants.Environments.Local));
builder.Services.AddExceptionHandler<CommonApiExceptionHandler>();

#endregion

WebApplication app = builder.Build();

#region HTTP request pipelines

app.MapCarter();
app.UseExceptionHandler(options => {});

#endregion

app.Run();