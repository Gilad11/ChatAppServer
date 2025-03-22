using ChatAppServer.Data;
using ChatAppServer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// ✅ Add SignalR services
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.MaximumReceiveMessageSize = 102400; // Increase message size if needed
});

// ✅ Configure CORS policy
const string CorsPolicy = "AllowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(CorsPolicy, policy => policy
        .WithOrigins("http://localhost:5173", "https://localhost:5173") // Adjust frontend URL
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

// ✅ Configure JSON serialization options
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.MaxDepth = 0;
});

// ✅ Configure Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Configure database context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ChatDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;";

builder.Services.AddDbContext<ChatAppDbContext>(options =>
    options.UseSqlServer(connectionString)
           .ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));

builder.Services.AddSingleton<ChatHub>(); // ✅ Register ChatHub as Singleton
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
    options.MaximumReceiveMessageSize = 102400;
});

var app = builder.Build();

// ✅ Enable Swagger in development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//seed
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider.GetRequiredService<ChatAppDbContext>();
    services.Database.EnsureDeleted();
    services.Database.EnsureCreated();
}

app.UseRouting();
app.UseCors(CorsPolicy);
app.UseAuthentication();
app.UseAuthorization();

// ✅ Map SignalR Hub and Controllers
app.MapHub<ChatHub>("/chatHub", options =>
{
    options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets |
                         Microsoft.AspNetCore.Http.Connections.HttpTransportType.LongPolling;
});



app.MapControllers();

app.Run();