using DogsWebApi.Core.Domain.RepositoryContracts;
using DogsWebApi.Core.ServiceContracts;
using DogsWebApi.Core.Services;
using DogsWebApi.Infrastructure.ApplicationDbContext;
using DogsWebApi.Infrastructure.Repositories;
using DogsWebApi.UI.Middlewares;
using Microsoft.EntityFrameworkCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(opts => { });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDogsRepository, DogsRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
builder.Services.AddScoped<IDogService, DogService>();

builder.Services.AddRateLimiter(options =>
{
    options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(_ =>
        RateLimitPartition.GetFixedWindowLimiter("global", _ => new FixedWindowRateLimiterOptions
        {
            PermitLimit = 10,
            Window = TimeSpan.FromSeconds(1),
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst, 
            QueueLimit = 0
        })
    );
    options.OnRejected = (context, ct) =>
    {
        context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
        return new ValueTask();
    };
});

var app = builder.Build();

app.UseRateLimiter();
app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();

public partial class Program { }
