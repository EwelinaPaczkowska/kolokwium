using kolokwium.Repositories;
using webApiC.Middlewares;

namespace kolokwium;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        
        builder.Services.AddScoped<IAppoinmentsRepository, AppoinmentsRepository>();
        //builder.Services.AddScoped<IDeliveriesService, DeliveriesService>();

        var app = builder.Build();
        
        app.UseGlobalExceptionHandling();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        
        app.MapControllers();

        app.Run();
    }
}