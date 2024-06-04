using Microsoft.EntityFrameworkCore;
using VotoRestaurante.Context;
using VotoRestaurante.Models;

namespace VotoRestaurante.Services.Restaurantes;

public class RestaurantesServices : IRestauranteService
{
    private readonly AppDbContext _context;

    public RestaurantesServices(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Restaurante>> GetRestaurantes()
    {
        try
        {
            return await _context.Restaurantes.ToListAsync();
        }
        catch (Exception)
        {
            throw new NotImplementedException("Não foi encontrado nenhum restuarnte");
        }
    }



    public async Task<IEnumerable<Restaurante>> GetRestaurantesByParcitipa(bool participa)
    {
        try
        {
            IEnumerable<Restaurante> restaurantes;

            restaurantes = await _context.Restaurantes.Where(p => p.Participa == participa).ToListAsync();

            return restaurantes;
        }
        catch (Exception)
        {
            throw new NotImplementedException("Erro Desconhecido");
        }
    }


    public async Task<Restaurante> GetRestaurante(int id)
    {
        try
        {
            var restaurante = await _context.Restaurantes.FindAsync(id);

            return restaurante;
        }
        catch (Exception)
        {
            throw new NotImplementedException("ID invalido");
        }
    }


    public async Task CreateRestaurante(Restaurante restaurante)
    {
        try
        {
            _context.Restaurantes.Add(restaurante);
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new NotImplementedException("Erro para criar Restaurante");
        }
    }

    public async Task UpdateRestaurante(Restaurante restaurante)
    {
        try
        {
            _context.Entry(restaurante).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new NotImplementedException("Erro para alterar restaurante");
        }
    }

    public async Task DeleteRestaurante(Restaurante restaurante)
    {
        try
        {
            _context.Entry(restaurante).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw new NotImplementedException("Erro para excluir restaurante");
        }
    }


    public async Task<IEnumerable<Restaurante>> UpdateReinicarRestaurantes()
    {
        try
        {

            _context.Restaurantes.ToList().ForEach(a => a.Participa = true);

            await _context.SaveChangesAsync();
            return _context.Restaurantes;
        }
        catch (Exception)
        {
            throw new NotImplementedException("Erro para alterar restaurante");
        }
    }

    public async Task<IEnumerable<Restaurante>> UpdateBloqueaRestaurante(int id)
    {
        try
        {

            _context.Restaurantes.Where(e => e.Id == id).ToList().ForEach(a => a.Participa = false);

            await _context.SaveChangesAsync();
            return _context.Restaurantes;
        }
        catch (Exception)
        {
            throw new NotImplementedException("Erro para alterar restaurante");
        }
    }

}
