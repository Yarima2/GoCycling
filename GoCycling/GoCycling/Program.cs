using GoCycling;
using GoCycling.Controllers;
using GoCycling.Queries;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
GoCycleDbContext.connString = builder.Configuration.GetConnectionString("DatabaseConnString");
StravaTokenHandler.clientId = builder.Configuration.GetConnectionString("StravaClientId");
StravaTokenHandler.clientSecret = builder.Configuration.GetConnectionString("StravaClientSecret");
StravaWebhookController.verifyToken = builder.Configuration.GetConnectionString("StravaWebhookVerifyToken");

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddRazorPages(options =>
{
	options.Conventions.AuthorizePage("/");
    options.Conventions.AuthorizePage("/Index");
    options.Conventions.AllowAnonymousToFolder("/Login");
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
		options.SlidingExpiration = true;
		options.AccessDeniedPath = "/Forbidden/";
		options.LoginPath = "/Login";
	});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);

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
app.MapRazorPages();


app.Run();
