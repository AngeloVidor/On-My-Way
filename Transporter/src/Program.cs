using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using src.Business.Services.Implementations.Manager;
using src.Business.Services.Implementations.Tokens;
using src.Business.Services.Interfaces.Manager;
using src.Business.Services.Interfaces.Tokens;
using src.Controllers.Middlewares;
using src.Infrastructure.Repositories.Implementations.Manager;
using src.Infrastructure.Repositories.Interfaces.Manager;
using Transporter.Business.Services.Implementations;
using Transporter.Business.Services.Interfaces;
using Transporter.Controllers.DTOs.Mappers;
using Transporter.Infrastructure.Data;
using Transporter.Infrastructure.Repositories.Implementations;
using Transporter.Infrastructure.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Enter JWT token in format: Bearer {your_token}",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer"
        }
    );

    c.AddSecurityRequirement(
        new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        }
    );
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddScoped<ITransporterRepository, TransporterRepository>();
builder.Services.AddScoped<ITransporterService, TransporterService>();
builder.Services.AddScoped<ITransporterManagerRepository, TransporterManagerRepository>();
builder.Services.AddScoped<ITransporterManagerService, TransporterManagerService>();
builder.Services.AddScoped<IBearerTokenManagement, BearerTokenManagement>();


builder.Services.AddAutoMapper(typeof(MappingProfile));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<JwtAuthorizationMiddleware>();


app.UseHttpsRedirection();


app.MapControllers();

app.Run();

