var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices(builder.Configuration);
// Add services to the container.

var app = builder.Build();


// Configure the HTTP request pipeline.
app.UseCors("AllowOrigin");
app.Map("/", () => Results.Redirect("/api"));
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.Run();