using Tienda.MicroServicios.Autor.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Definir orígenes permitidos (pon aquí el dominio real de tu frontend)
var allowedOrigins = new[] { "https://vistalibrosautores-production.up.railway.app" };

builder.Services.AddControllers();

// Configurar CORS con política nombrada
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Access-Control-Allow-Origin", "*");
    context.Response.Headers.Append("Access-Control-Allow-Methods", "*");
    context.Response.Headers.Append("Access-Control-Allow-Headers", "*");

    if (context.Request.Method == "OPTIONS")
    {
        context.Response.StatusCode = 200;
        return;
    }

    await next();
});

builder.Services.AddOpenApi();
builder.Services.AddCustomServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Usar la política CORS
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
