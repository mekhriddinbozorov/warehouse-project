using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Polling;
using warehouse_project.Data;
using warehouse_project.Dtos.CategoryDto;
using warehouse_project.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull);
builder.Services.AddScoped<IAppDbContext, AppDbContext>();

// builder.Services.AddDbContext<IAppDbContext, AppDbContext>(options =>
//     options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

var connectionString = builder.Configuration.GetConnectionString("Mysql");
builder.Services.AddDbContext<IAppDbContext, AppDbContext>(options =>
{
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddHostedService<BotBackgroundService>();
builder.Services.AddTransient<IUpdateHandler, UpdateHandler>();
builder.Services.AddSingleton<ITelegramBotClient, TelegramBotClient>(provider =>
    new TelegramBotClient(builder.Configuration.GetValue("BotApiKey", string.Empty)));

var app = builder.Build();

app.MapControllers();

app.Run();
