using Microsoft.EntityFrameworkCore;
using Order.core.Context;
using Order.core.Interfaces;
using Order.core.Services;
using Order.Core.Interfaces;
using Order.Core.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("Normal", builder =>
    {
        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IStatistical, Statistical>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Normal");

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
