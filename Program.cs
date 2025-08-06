
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Tienda.MicroServicios.Autor.Api.Application;
using Tienda.MicroServicios.Autor.Api.Extensions;
using Tienda.MicroServicios.Autor.Api.Percistence; // Asegúrate que este sea el namespace correcto para ContextoLibreria

var builder = WebApplication.CreateBuilder(args);

// Configuración del JWT (si lo usas)
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];

// Variables de conexión
var configuration = builder.Configuration;
var writeConn = configuration.GetConnectionString("MySqlWrite");
var readConn1 = configuration.GetConnectionString("MySqlRead1");
var readConn2 = configuration.GetConnectionString("MySqlRead2");
var serverVersion = new MySqlServerVersion(new Version(8,0,33));

// -------------------------------------------------
// DbContext para escritura (master)
builder.Services.AddDbContext<WriteDBContext>(opt =>
    opt.UseMySql(writeConn, ServerVersion.AutoDetect(writeConn)));

// DbContextFactory para lectura (usamos una factory base con una de las conn strings
// pero al construir el ReadDBContext scoped reemplazamos la conexión con la elegida)
builder.Services.AddDbContextFactory<ReadDBContext>(opt =>
    opt.UseMySql(readConn1, ServerVersion.AutoDetect(readConn1))); // valor "placeholder"

// Servicio para seleccionar replica (aleatorio). Si quieres round-robin, lo cambiamos.
builder.Services.AddSingleton<IReadConnectionProvider>(sp =>
    new ReadConnectionProvider(new[] { readConn1, readConn2 }));

// Registrar ReadDBContext como scoped usando la factory y la connection elegida al vuelo
builder.Services.AddScoped<ReadDBContext>(sp =>
{
    var provider = sp.GetRequiredService<IReadConnectionProvider>();
    var conn = provider.GetRandom(); // selecciona la réplica

    var optionsBuilder = new DbContextOptionsBuilder<ReadDBContext>();
    var logger = sp.GetRequiredService<ILogger<ReadDBContext>>();
    optionsBuilder.UseMySql(conn, serverVersion);

    return new ReadDBContext(optionsBuilder.Options);
});

// -------------------------------------------------
// Registrar MediatR
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Registrar AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

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

// Registrar servicios personalizados (tu extensión)
builder.Services.AddCustomServices(builder.Configuration);

// Controllers, swagger, etc.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

// Mostrar errores detallados en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

// app.UseAuthentication(); // Descomenta si configuras JWT
app.UseAuthorization();

app.MapControllers();

app.Run();
