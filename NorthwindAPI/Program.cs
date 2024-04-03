using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Models;
using NorthwindAPI.NorthwindAppDbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<NorthwindContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("northwind"))
);


builder.Services.AddControllers();
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x =>
x.AllowAnyMethod()
.WithOrigins("https://localhost:7231")
.AllowAnyHeader()
.SetIsOriginAllowed(origin => true)
.AllowCredentials());
app.UseAuthorization();

app.MapControllers();   

app.Run();
