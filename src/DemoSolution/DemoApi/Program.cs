var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Up until this line (everything before this) is configuring the backend stuff in our API.
var app = builder.Build();
// Everything after this line is configuring "Middleware" - specificially how should HTTP Requests be mapped to our code.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/tacos", () => "Lunch Time is over - back to work dogs!");

app.Run();


