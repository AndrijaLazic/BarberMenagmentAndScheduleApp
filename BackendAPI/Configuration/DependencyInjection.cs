

using BackendAPI.Data;
using BackendAPI.Services.DataService;
using BackendAPI.Services.UserService;
using BackendAPI.Services.WorkerService;

namespace BackendAPI.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration) {
            // Add services to the container.
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IWorkerService, WorkerService>();


            // Socket
            services.AddSignalR(options =>
            {
                options.EnableDetailedErrors = true;
            });
            services.AddSingleton<SharedDB>();

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddDbContext<BarberDBContext>();

            return services;
        }
    }
}
