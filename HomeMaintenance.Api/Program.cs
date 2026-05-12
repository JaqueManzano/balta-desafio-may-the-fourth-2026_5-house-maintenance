using HomeMaintenance.Ai;
using HomeMaintenance.Infra;
using Microsoft.OpenApi;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddControllers();
builder.Services.AddAgents();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Home Maintenance API",
        Version = "v1",
        Description = "API de organização de tarefas da casa."
    });

    var xml = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xml);
    if (File.Exists(xmlPath))
        options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Home Maintenance API v1");
        options.RoutePrefix = "swagger";
    });
}

app.MapControllers();

app.Run();
