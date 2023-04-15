using Hangfire;
using Hangfire.PostgreSql;
using HC.Shared.Infrastructure;
using HC.TmdbBackgroundJob.Mapping;
using HC.TmdbBackgroundJob.Schedules;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
ServiceCollectionExtensions.AddAutoMapper(builder.Services, typeof(MappingProfile));

builder.Services.AddEntityFrameworkNpgsql().AddDbContext<PostgreDbContext>(opt =>
opt.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionStringPSQL")));
builder.Services.AddHangfire(opt => opt.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("ConnectionStringHangfire")));
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
} 
app.UseHttpsRedirection();
app.UseHangfireDashboard("/hangfire");
RecurringJobs.Start();

app.UseAuthorization();

app.MapControllers();

app.Run();
