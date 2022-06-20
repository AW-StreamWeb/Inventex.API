using Google.Protobuf.WellKnownTypes;
using Microsoft.EntityFrameworkCore;
using Inventex.API.Management.Domain.Repositories;
using Inventex.API.Management.Domain.Services;
using Inventex.API.Management.Persistence.Repositories;
using Inventex.API.Management.Services;
using Inventex.API.Security.Authorization.Handlers.Implementations;
using Inventex.API.Security.Authorization.Handlers.Interfaces;
using Inventex.API.Security.Authorization.Middleware;
using Inventex.API.Security.Authorization.Settings;
using Inventex.API.Security.Domain.Repositories;
using Inventex.API.Security.Domain.Services;
using Inventex.API.Security.Persistence.Repositories;
using Inventex.API.Security.Services;
using Inventex.API.Shared.Domain.Repositories;
using Inventex.API.Shared.Persistence.Contexts;
using Inventex.API.Shared.Persistence.Repositories;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Add CORS Service
builder.Services.AddCors();

// AppSettings Configuration
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddSwaggerGen(options =>
    {
        //Add API Documentation Information
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "STREAMWEB Inventex API",
            Description = "STREAMWEB Inventex RESTful API",
            TermsOfService = new Uri("https://streamweb-inventex.com/tos"),
            Contact = new OpenApiContact
            {
                Name = "STREAMWEB.studio",
                Url = new Uri("https://streamweb.studio")
            },
            License = new OpenApiLicense
            {
                Name = "STREAMWEB Inventex Resources License",
                Url = new Uri("https://streamweb-inventex.com/license")
            }
        });
        options.EnableAnnotations();
    }
    
    );

// Add DataBase Connection
var connectionString=builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    options=>options.UseMySQL(connectionString)
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableSensitiveDataLogging()
    .EnableDetailedErrors());

//Add lowercase routes
builder.Services.AddRouting(options=>options.LowercaseUrls=true);

//Dependency Injection Configuration
builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IMachineRepository, MachineRepository>();
builder.Services.AddScoped<IMachineService, MachineService>();

builder.Services.AddScoped<IFinanceRepository, FinanceRepository>();
builder.Services.AddScoped<IFinanceService, FinanceService>();

builder.Services.AddScoped<IInventoryRepository,InventoryRepository>();
builder.Services.AddScoped<IInventoryService, InventoryService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// AutoMapper Configuration
builder.Services.AddAutoMapper(
    typeof(Inventex.API.Management.Mapping.ModelToResourceProfile), 
    typeof(Inventex.API.Management.Mapping.ResourceToModelProfile),
    typeof(Inventex.API.Security.Mapping.ModelToResourceProfile),
    typeof(Inventex.API.Security.Mapping.ResourceToModelProfile));

var app = builder.Build();

//Validation for ensuring Database Objects are created
using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<AppDbContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Configure CORS
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

// Middleware Services Configuration

// Configure Error Handler Middleware
app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure JSON Web Token Handling Middleware
app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();