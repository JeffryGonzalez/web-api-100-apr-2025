var builder = WebApplication.CreateBuilder(args);
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));
builder.Services.AddCors(options =>
{
    options.AddPolicy("remote", pol =>
    {
        pol.WithOrigins("http://localhost:5173");
        pol.AllowAnyMethod();
        pol.AllowAnyHeader();
    });
});
var app = builder.Build();
app.UseCors();
app.MapReverseProxy();
app.Run();