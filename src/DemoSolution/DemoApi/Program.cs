var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers(); // this adds some services associated with activating controllers for requests.

// Up until this line (everything before this) is configuring the backend stuff in our API.
var app = builder.Build();
// Everything after this line is configuring "Middleware" - specificially how should HTTP Requests be mapped to our code.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) // ASPNETCORE_ENVIRONMENT = "Development"
{
    app.MapOpenApi();
}

app.MapGet("/tacos", () => "Lunch Time is over - back to work dogs!");
// You need to tell the API to create it's phone directory. Look at all the Controller classes, read those attributes,
// and get ready to rock.

app.MapControllers(); // "Reflection" 

Console.WriteLine("Fixin' to run your API");
// hey, if anyone does a GET to who-is-on-call, then (later) create that controller, call that method.
app.Run(); // this is a blocking call, it is an infinite loop that just listens for HTTP requests.
Console.WriteLine("API is done now.");


