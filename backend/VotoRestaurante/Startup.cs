using Microsoft.Extensions.Options;
using VotoRestaurante.Context;
using VotoRestaurante.Services.Restaurantes;
using VotoRestaurante.Services.Votos;

namespace VotoRestaurante;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
        
    }

    public IConfiguration Configuration { get; }


    public void ConfigureServices(IServiceCollection services)
    {
        

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddRouting(options => options.LowercaseUrls = true);

        services.AddDbContext<AppDbContext>();

        services.AddScoped<IVotoService, VotosServices>();
        services.AddScoped<IRestauranteService, RestaurantesServices>();
    }

    public void Configure(WebApplication app, IWebHostEnvironment env)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        

        app.UseCors(options =>
        {
            options.WithOrigins("http://localhost:3000");
            options.AllowAnyMethod();
            options.AllowAnyHeader();
        });

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthorization();

        app.MapControllers();

    }
}
