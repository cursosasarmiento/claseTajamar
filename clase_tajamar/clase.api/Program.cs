using clase.api;
using clase.api.Contracts;
using clase.api.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
var mascotasConnectionString = builder.Configuration.GetConnectionString("mascotasConnectionString");
builder.Services.AddDbContext<MascotasDbContext>(options =>
{
    options.UseSqlServer(mascotasConnectionString);
});

builder.Services.AddScoped<IPersonaService, PersonaService>();
builder.Services.AddScoped<IMascotaService, MascotaService>();
builder.Services.AddScoped<IMascotaTipoService, MascotaTipoService>();

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
