using VotoRestaurante.Models;

namespace VotoRestaurante.Services.Restaurantes;

public interface IRestauranteService
{
    Task<IEnumerable<Restaurante>> GetRestaurantes();

    Task<Restaurante> GetRestaurante(int id);

    Task<IEnumerable<Restaurante>> GetRestaurantesByParcitipa(bool parcitapa);

    Task CreateRestaurante(Restaurante restaurante);

    Task UpdateRestaurante(Restaurante restaurante);

    Task DeleteRestaurante(Restaurante restaurante);

    Task<IEnumerable<Restaurante>> UpdateReinicarRestaurantes();

    Task<IEnumerable<Restaurante>> UpdateBloqueaRestaurante(int id);

}
