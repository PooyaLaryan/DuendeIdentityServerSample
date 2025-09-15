using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAuthentication()
.AddJwtBearer(options =>
{
    options.Authority = "https://localhost:7094"; // IdentityServer
    options.TokenValidationParameters.ValidateAudience = false;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/identity", (ClaimsPrincipal user) => user.Claims.Select(c => new { c.Type, c.Value }))
.RequireAuthorization();

app.Run();
