using Data.Factories;
using Data.Models;
using Data.Repositories;
using Data.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//Behöver detta för att min frontend är på en annan port, då går inte anropen igenom
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

//automatiskt konverterar strängar till enum
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IProjektRepository, ProjektRepository>();
builder.Services.AddScoped<IKundRepository, KundRepository>();
builder.Services.AddScoped<ITjanstRepository, TjanstRepository>();
builder.Services.AddScoped<IProjektService, ProjektService>();
builder.Services.AddScoped<IKundService, KundService>();
builder.Services.AddScoped<ITjanstService, TjanstService>();
builder.Services.AddScoped<ProjektFactory>();
builder.Services.AddScoped<KundFactory>();

var app = builder.Build();

app.UseCors("AllowAllOrigins");


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
