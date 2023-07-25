using CorePush.Apple;
using CorePush.Google;
using Microsoft.EntityFrameworkCore;
using PerfectSelf.WebAPI;
using PerfectSelf.WebAPI.Common;
using PerfectSelf.WebAPI.Context;
using PerfectSelf.WebAPI.Models;
using System.Configuration;
//using System.Configuration;

String addressDB = Directory.GetCurrentDirectory() + "\\AddressDB\\";
if (Directory.Exists(addressDB))
{
    try
    {
        Task.Run(() => Global.ReadAllAddress(addressDB));
    }
    catch (Exception e)
    {
        Console.WriteLine("Failed initialize for project to language map.");
        return;
    }
}

//Log Thread
Task.Run(() => Global.LogMessageThread());

String logFolder = Directory.GetCurrentDirectory() + "\\Log";
if (!Directory.Exists(logFolder)) Directory.CreateDirectory(logFolder);

var configBuilder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
IConfiguration _configuration = configBuilder.Build();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                //options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
                //options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
            });

builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddHttpClient<FcmSender>();
builder.Services.AddHttpClient<ApnSender>();

// Configure strongly typed settings objects
var appSettingsSection = _configuration.GetSection("FcmNotification");
builder.Services.Configure<FcmNotificationSetting>(appSettingsSection);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<PerfectSelfContext>(options => options.UseSqlServer(_configuration.GetConnectionString("PerfectSelfConnect")));

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

using (var scope = app.Services.CreateScope())
{   
    PerfectSelfContext ctx = scope.ServiceProvider.GetRequiredService<PerfectSelfContext>();
    ctx.Seed();// Here PerfectSelfContext has been initialized
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
