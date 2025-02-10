using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SensorDataAPI.Services;

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

// Configuração do EmailService


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


