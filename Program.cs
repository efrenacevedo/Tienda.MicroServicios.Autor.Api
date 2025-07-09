using Microsoft.EntityFrameworkCore;
using MediatR;
using System.Reflection;
using Tienda.MicroServicios.Autor.Api.Percistence; // Asegúrate que este sea el namespace correcto para ContextoLibreria

var builder = WebApplication.CreateBuilder(args);

// Registrar DbContext con cadena de conexión desde configuración
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ContextoAutor>(options =>
    options.UseNpgsql(connectionString)); // Cambia UseSqlServer si usas otra BD

// Registrar MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Nombre de la política CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Resto de servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

// Middleware para mostrar errores detallados en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Activar política CORS ANTES de autorización


app.UseAuthorization();

app.MapControllers();

app.Run();
