using Microsoft.EntityFrameworkCore;
using Npgsql;
using AutoMapper;
using OtusDZ_2.Data;
using System.Data.Common;
using WebApi;
using OtusDZ_2.Repository;
using WebApi.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
/*
builder.Services.AddDbContext<DataContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("db"));
    x.UseSnakeCaseNamingConvention();
});
*/

builder.Services.AddAutoMapper(typeof(AppMapping));
builder.Services.AddDbContext<DataContext>(m => m.UseNpgsql("Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=123456;"));
builder.Services.AddTransient(typeof(IRepository<>),typeof(EFRepository<>));
builder.Services.AddScoped(typeof(DbContext), typeof(DataContext));
//builder.Services.AddScoped(typeof(DbConnection),(_)=> new NpgsqlConnection(builder.Configuration.GetConnectionString("db")));


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
