using BackendAPI;
using BackendAPI.Data;
using Microsoft.EntityFrameworkCore;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppConfiguration>(builder.Configuration.GetSection("settings"));

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<UserContext>(options =>
{
    options.UseSqlServer("Server=" + Environment.GetEnvironmentVariable("DATABASECONNECTION") + ";Database=BarberDB;Trusted_Connection=True;TrustServerCertificate=True;");
});

// Enable CORS
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    Console.WriteLine(options);
    options.AddPolicy(name: myAllowSpecificOrigins,
        builder =>
        {
            builder.WithOrigins(Environment.GetEnvironmentVariable("FRONT_END_CONNECTION")!)
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
