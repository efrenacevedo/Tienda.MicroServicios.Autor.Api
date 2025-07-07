using Tienda.MicroServicios.Autor.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);


var allowedOrigins = new[] {
    "https://vista-libros-autores-xge2-efrenacevedos-projects.vercel.app"
};

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
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

// ✅ Aplica la política de CORS antes de cualquier middleware
app.UseCors("AllowFrontend");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();