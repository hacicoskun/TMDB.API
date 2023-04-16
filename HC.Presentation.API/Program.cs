using HC.Api.Identity;
using HC.Api.Identity.Extensions;
using HC.Api.Identity.Identity;
using HC.Presentation.API.Application.Mapping;
using HC.Shared.Infrastructure;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddUserSecrets<Program>(true);


builder.Services.AddEntityFrameworkNpgsql().AddDbContext<PostgreDbContext>(opt =>
opt.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionStringPSQL")));
builder.Services.AddEntityFrameworkNpgsql().AddDbContext<IdentityDbContext>(opt =>
opt.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionStringIdentity")));


builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
ServiceExtensions.ServiceExtensions.ServiceCollectionExtension(builder.Services, typeof(MappingProfile));
ServiceCollectionIdentityExtensions.AddApiDbContext(builder.Services, builder.Configuration);
//builder.Services.AddIdentity<ApiIdentityUser, ApiIdentityUserRole>().AddEntityFrameworkStores<IdentityDbContext>().AddDefaultTokenProviders();
builder.Services.AddDefaultIdentity<ApiIdentityUser>().AddEntityFrameworkStores<IdentityDbContext>();

 
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMassTransit(x => {
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(builder.Configuration["RabbitMQUrl"], "/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });
    });
});

builder.Services.AddMassTransitHostedService();

builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "TMD API",
        Description = "Swagger Dökümantasyon",
        Version = "v1",
    });
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Format: Bearer <your_api_key>"
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id ="Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            }, new string[]{}
        }
    });
});
 builder.Services.AddHttpClient();
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
    opt.SaveToken = true;
    opt.RequireHttpsMetadata = false;
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
 
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
