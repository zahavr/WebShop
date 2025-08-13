using Catalog.API.Configurations;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Services

builder.Services.AddExternalServices(builder.Configuration);
builder.Services.AddInternalServices();
builder.Services.AddExceptionHandler<CommonApiExceptionHandler>();

#endregion

WebApplication app = builder.Build();

#region HTTP request pipelines

app.MapCarter();
app.UseExceptionHandler(options => {});

#endregion

app.Run();