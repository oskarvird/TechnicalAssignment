using ConsidTechnicalBackend.Database;
using ConsidTechnicalBackend.Helpers;
using ConsidTechnicalBackend.Repositories;
using ConsidTechnicalBackend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //builder.Services.AddAutoMapper(typeof(<name-of-profile>));

builder.Services.AddDbContext<ConsidContext>(
            options => options.UseSqlServer("name=ConnectionStrings:DefaultConnection"));

builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ILibraryItemService, LibraryItemService>();
builder.Services.AddScoped<IEmployeesService, EmployeesService>();
builder.Services.AddScoped<ICalculations, Calculations>();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ILibraryItemRepository, LibraryItemRepository>();
builder.Services.AddScoped<IEmployeesRepository, EmployeesRepository>();

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
