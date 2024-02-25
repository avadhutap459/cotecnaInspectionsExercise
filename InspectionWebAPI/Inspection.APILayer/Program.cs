global using Inspection.ServiceLayer.Interface;
global using Inspection.ServiceLayer.Model;
using Inspection.APILayer.Global._Exception;
using Inspection.ServiceLayer.ServiceExtension;

string Path = Directory.GetCurrentDirectory();
var config = new ConfigurationBuilder().SetBasePath(Path).AddJsonFile("appsettings.json").Build();

string env = config.GetSection("Env").Value;

IConfigurationRoot configuration = new ConfigurationBuilder()
    .SetBasePath(Path)
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
     .AddJsonFile($"appsettings.{env}.json", optional: true)
    .Build();


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDIServices(builder.Configuration);
builder.Services.AddControllersWithViews().
        AddJsonOptions(options =>
        {
          options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
          options.JsonSerializerOptions.PropertyNamingPolicy = null;
        });
var app = builder.Build();

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseHttpsRedirection();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionHandlerMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
