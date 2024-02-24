using aplicacionExamen.Data;
using aplicacionExamen.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder; // Importante para usar UseCors

var builder = WebApplication.CreateBuilder(args);

//Configuraci�n de cadena de conexi�n a base de datos
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ElementoDbContext>(options =>
    options.UseSqlServer(connectionString));

// Agregar el servicio ElementoService a la inyecci�n de dependencias
builder.Services.AddScoped<ElementoService>();

// Add services to the container.
builder.Services.AddControllers();
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

// Configuraci�n de CORS
app.UseCors(options =>
{
    options.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
