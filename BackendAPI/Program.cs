using BackendAPI;
using BackendAPI.Configuration;
using BLL.Services;
using BLL.Services.DataService;
using BLL.Services.Socket;
using DAL.Data;
using Domain;



DotNetEnv.Env.Load();
var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<AppConfiguration>(builder.Configuration.GetSection("settings"));

builder.Services.AddServices(builder.Configuration);

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
