using Microsoft.EntityFrameworkCore;
using PerfectSelf.WebAPI;
using PerfectSelf.WebAPI.Common;
using PerfectSelf.WebAPI.Context;
//using System.Configuration;

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
