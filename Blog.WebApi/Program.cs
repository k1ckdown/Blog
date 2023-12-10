using System.Text.Json.Serialization;
using Blog.Application;
using Blog.Infrastructure;
using Blog.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.OpenApi.Models;
using Blog.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationLayer();
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddPersistenceInfrastructure(builder.Configuration);

builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddTransient<JwtSessionMiddleware>();
builder.Services.AddControllers(opt =>
    {
        opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
    })
    .AddJsonOptions(config => config.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = JwtBearerDefaults.AuthenticationScheme
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<JwtSessionMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();

app.Run();