using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SensorDataAPI.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddControllers();


builder.Services.AddDbContext<SensorData.Data.SensorDBcontext>(options =>
    options.UseInMemoryDatabase("Cheiro de Problema"));

// Configuração do EmailSettings a partir do appsettings.json
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

// Configuração do EmailService
builder.Services.AddTransient<EmailService>();

builder.Services.AddSingleton<TelegramService>();

builder.Services.AddHostedService<TelegramPollingService>();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();

builder.Host.UseSerilog(); 


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


