using System.Text.Json.Serialization;
using Backend.Api.Statistics;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(pol =>
//    {
//        pol.AllowAnyOrigin();
//        pol.AllowAnyMethod();
//        pol.AllowAnyHeader();
//    });
//});
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.Configure<JsonOptions>(opts =>
{
    opts.SerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapStatisticsEndpoints();


app.Run();
