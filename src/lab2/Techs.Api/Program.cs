using FluentValidation;
using Marten;
using Techs.Api.Techs;
using Techs.Api.Techs.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorizationBuilder().AddPolicy("SoftwareCenter", pol =>
{
    pol.RequireRole("SoftwareCenter");
});
var connectionString = builder.Configuration.GetConnectionString("techs") ??
                       throw new Exception("No Connection String");

builder.Services.AddScoped<IValidator<TechCreateModel>, TechCreateModelValidator>();

builder.Services.AddMarten(options =>
{
    options.Connection(connectionString);
}).UseLightweightSessions();

builder.Services.AddScoped<ITechRepository, PostGresMartenTechRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();
app.UseAuthentication();
app.UseAuthorization();
public partial class Program;
