using CustomerApi.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System;

var builder = WebApplication.CreateBuilder(args);

// Logging
Log.Logger = new LoggerConfiguration()
    .Enrich.FromLogContext()
    .WriteTo.Console(new Serilog.Formatting.Json.JsonFormatter())
    .CreateLogger();

builder.Host.UseSerilog();

// Add services
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlServer(connectionString));
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthorization();
app.MapControllers();
app.Run();
public partial class Program { }
