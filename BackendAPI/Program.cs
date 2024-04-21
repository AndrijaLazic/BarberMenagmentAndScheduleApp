using BackendAPI;
using BackendAPI.Data;
using BackendAPI.Models.Socket;
using BackendAPI.Services.DataService;
using BackendAPI.Services.UserService;
using BackendAPI.Services.WorkerService;
using Microsoft.EntityFrameworkCore;

DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppConfiguration>(builder.Configuration.GetSection("settings"));

// Add services to the container.
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IWorkerService, WorkerService>();


// Socket
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = true;
});
builder.Services.AddSingleton<SharedDB>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BarberDBContext>();

// Enable CORS
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
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

app.MapHub<WorkerSocketHub>("/worker-chat");

app.Run();
