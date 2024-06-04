using System.Threading.Tasks;
using VotoRestaurante.Models;

namespace VotoRestaurante.Services.Votos;

public interface IVotoService
{
    Task<IEnumerable<Voto>> GetVotos();

    Task<Voto> GetVoto(int id);

    Task<IEnumerable<Voto>> GetTotalVotos();

    Task<IEnumerable<VotoCampeao>> GetVotoCampeao();

    Task<IEnumerable<Voto>> GetVotosByDia(DateTime dataRegistro);

    Task CreateVoto(Voto voto);

    Task UpdateVoto(Voto voto);

    Task DeleteVoto(Voto voto);
}
