using GoCycling;
using GoCycling.Controllers;
using GoCycling.Queries;
using Microsoft.AspNetCore.Authentication.Cookies;
using Neo4jClient;

var builder = WebApplication.CreateBuilder(args);
GoCyclingDbClient.adress = new Uri(builder.Configuration.GetConnectionString("neo4jUri"));
GoCyclingDbClient.password = builder.Configuration.GetConnectionString("neo4jPassword");
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

var client = new BoltGraphClient(new Uri("bolt://localhost:7687"), "neo4j", "12345678");
client.ConnectAsync();
builder.Services.AddSingleton<IGraphClient>(client);

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
