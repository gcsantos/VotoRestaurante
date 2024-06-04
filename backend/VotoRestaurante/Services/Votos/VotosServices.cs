using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using VotoRestaurante.Context;
using VotoRestaurante.Models;

namespace VotoRestaurante.Services.Votos;

public class VotosServices : IVotoService
{
    private readonly AppDbContext _context;

    public VotosServices(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Voto>> GetVotos()
    {
        try
        {
            return await _context.Votos.ToListAsync();
        }
        catch (Exception)
        {
            throw new NotImplementedException("Não foi encontrado nenhum voto");
        }
    }



    public async Task<IEnumerable<Voto>> GetVotosByDia(DateTime dataRegistro)
    {
        try
        {
            IEnumerable<Voto> votos;

            votos = await _context.Votos.Where(v => v.DataRegistro == dataRegistro).ToListAsync();

            return votos;
        }
        catch (Exception)
        {
            throw new NotImplementedException("Erro Desconhecido");
        }
    }


    public async Task<Voto> GetVoto(int id)
    {
        try
        {
            var voto = await _context.Votos.FindAsync(id);

            return voto;
        }
        catch (Exception)
        {
            throw new NotImplementedException("ID invalido");
        }
    }

   
public async Task<IEnumerable<Voto>>GetTotalVotos()
{
 try
 {
            IEnumerable<Voto> votos;
            votos = await _context.Database.SqlQuery<Voto>($"SELECT Id, Nome, Cpf, RestauranteId, DataRegistro  FROM Votos WHERE date(dataRegistro) = date(datetime('now','localtime'))").ToListAsync();


           return votos;
 }
 catch (Exception)
 {
     throw new NotImplementedException("Erro Desconhecido");
 }
}


public async Task<IEnumerable<VotoCampeao>> GetVotoCampeao()
{
    try
    {
            IEnumerable<VotoCampeao> voto;

        voto = await _context.Database.SqlQuery<VotoCampeao>($"SELECT  RestauranteId Id, REST.Nome, COUNT(RestauranteId) Total  FROM Votos VOT INNER join Restaurantes REST ON REST.Id = VOT.RestauranteId WHERE date(dataRegistro) = date(datetime('now','localtime'))  group by RestauranteId, REST.Nome  ORDER by COUNT(RestauranteId) desc  LIMIT 1").ToListAsync();

            return voto;
    }
    catch (Exception)
    {
        throw new NotImplementedException("ID invalido");
    }
}



    public async Task CreateVoto(Voto voto)
    {
        try
        {
            var cpf = voto.Cpf;
            var dados = _context.Database.SqlQuery<VotoLista>($"SELECT Id  FROM Votos WHERE date(dataRegistro) = date(datetime('now','localtime')) and Cpf = {cpf}");

            var resultado = await dados.CountAsync();

            if (resultado > 0)
            {
                throw new ArgumentException("CPF ja cadastrado");
            }


            _context.Votos.Add(voto);
            await _context.SaveChangesAsync();
        }
        catch
        {
            throw new NotImplementedException("Erro para incluir voto");
        }
    }

    public async Task UpdateVoto(Voto voto)
    {
        try
        {
            _context.Entry(voto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new NotImplementedException("Erro para alterar voto");
        }
    }

    public async Task DeleteVoto(Voto voto)
    {
        try
        {
            _context.Entry(voto).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new NotImplementedException("Erro para excluir voto");
        }
    }

}
