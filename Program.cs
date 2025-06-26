using Tienda.MicroServicios.Autor.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Definir or�genes permitidos (pon aqu� el dominio real de tu frontend)
var allowedOrigins = new[] { "https://vistalibrosautores-production.up.railway.app" };

builder.Services.AddControllers();

// Configurar CORS con pol�tica nombrada
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins(allowedOrigins)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

builder.Services.AddOpenApi();
builder.Services.AddCustomServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Usar la pol�tica CORS
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
