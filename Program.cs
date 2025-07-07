using Microsoft.EntityFrameworkCore;
using Tienda.MicroServicios.Autor.Api.Extensions;
using MediatR;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// 👉 CORS policy
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:5173",
                "https://vista-libros-autores-xge2-efrenacevedos-projects.vercel.app"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Resto de servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseHttpsRedirection();

// 👉 Aquí activas la política CORS
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

// Mapear controladores
app.MapControllers();

app.Run();
