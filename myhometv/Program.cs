using myhometv.Data;
using myhometv.Utils.Sports;
using myhometv.Utils.Weather;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var allowAllOrigins = "AllowAll";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowAllOrigins,
        policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
});

builder.Services.AddHostedService<WeatherFetchTask>();
builder.Services.AddHostedService<ScoreFetchTask>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(allowAllOrigins);

// Serve static files from wwwroot
app.UseStaticFiles();

// Serve static files from React build directory
var clientBuildPath = Path.Combine(app.Environment.ContentRootPath, "wwwroot");

if (Directory.Exists(clientBuildPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(clientBuildPath),
        RequestPath = ""
    });
}

app.UseRouting();

// Map API controllers
app.MapControllers();

// Handle non-existent API routes
app.Map("/api/{**catch-all}", () => Results.NotFound("API endpoint not found"));

// SPA fallback - serve index.html for all non-API routes
app.MapFallback(async context =>
{
    var indexPath = Path.Combine(clientBuildPath, "index.html");
    
    if (File.Exists(indexPath))
    {
        context.Response.ContentType = "text/html";
        await context.Response.SendFileAsync(indexPath);
    }
    else
    {
        context.Response.StatusCode = 404;
        await context.Response.WriteAsync("SPA not found. Make sure to build your React app.");
    }
});

app.Run();
