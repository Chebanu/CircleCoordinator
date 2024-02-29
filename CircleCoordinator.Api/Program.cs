using CircleCoordinator.Domain.Commands;
using CircleCoordinator.Domain.Databases;
using Microsoft.EntityFrameworkCore;

using CircleCoordinator.Domain;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
        .AddDbContext<AppDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
        .AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateCircleCoordinatorCommand>());

builder.Services.AddDomainServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

await app.RunAsync();