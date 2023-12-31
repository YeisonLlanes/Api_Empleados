using Microsoft.EntityFrameworkCore;
using primera_Api.Data;
using primera_Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DbEmpresaContext>(Options =>
{
    Options.UseSqlServer(builder.Configuration.GetConnectionString("ConexionBD"));
});


builder.Services.AddControllers();

builder.Services.AddScoped<IDepartamento, DepartamentoService>();
//builder.Services.AddControllersWithViews();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
