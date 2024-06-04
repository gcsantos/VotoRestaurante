using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VotoRestaurante.Models;
using VotoRestaurante.Services.Restaurantes;

namespace VotoRestaurante.Controllers;
[Route("api/[controller]")]
[ApiController]
public class RestaurantesController : ControllerBase
{
    private IRestauranteService _restauranteService;
    public RestaurantesController(IRestauranteService restauranteService)
    {
        _restauranteService = restauranteService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IAsyncEnumerable<Restaurante>>> GetRestaurantes()
    {
        try
        {
            var restaurantes = await _restauranteService.GetRestaurantes();

            return Ok(restaurantes);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter restaurantes");
        }
    }

    [HttpGet("disponivel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IAsyncEnumerable<Restaurante>>> GetRestaurantesByParcitipa([FromQuery] bool participa)
    {
        try
        {
            var restaurantes = await _restauranteService.GetRestaurantesByParcitipa(participa);
            if (restaurantes == null) { 
                return NotFound("Não existe nenhum resturante para votação");
            }

            return Ok(restaurantes);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro desconhecido");
        }
    }

    [HttpGet]
    [Route("{id:int}/restaurante")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Restaurante>> GetRestaurante(int id)
    {
        try
        {
            var restaurantes = await _restauranteService.GetRestaurante(id);
            if (restaurantes == null)
            {
                return NotFound("Não existe restaurante com esse ID");
            }

            return Ok(restaurantes);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro desconhecido");
        }
    }

    [HttpPost("incluir")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create(Restaurante restaurante)
    {
        try
        {
            await _restauranteService.CreateRestaurante(restaurante);
            return CreatedAtRoute(nameof(GetRestaurante), new { id = restaurante.Id}, restaurante);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro desconhecido");
        }
    }

    [HttpPut]
    [Route("{id:int}/alterar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Edit(int id, [FromBody] Restaurante restaurante)
    {
        try
        {
            if (restaurante.Id == id)
            {
                await _restauranteService.UpdateRestaurante(restaurante);
                return Ok("Restaurante alterado com sucesso");
            }
            
            return BadRequest("Dados inconsistentes");
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro desconhecido");
        }
    }

    [HttpDelete]
    [Route("{id:int}/excluir")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var restaurante =  await _restauranteService.GetRestaurante(id);

            if (restaurante != null )
            {
                await _restauranteService.DeleteRestaurante(restaurante);
                return Ok("Restaurante excluido com sucesso");
            }

            return NotFound("Restaurante não encontrado");
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro desconhecido");
        }
    }

    [HttpPut("reiniciarRestaurantes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IAsyncEnumerable<Restaurante>>> Edit()
    {
        try
        {
            await _restauranteService.UpdateReinicarRestaurantes();
            return Ok("Restaurante alterado com sucesso");

        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro desconhecido");
        }
    }

    [HttpPut]
    [Route("{id:int}/bloquearRestaurante")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Edit(int id)
    {
        try
        {
            await _restauranteService.UpdateBloqueaRestaurante(id);
            return Ok("Restaurante bloqueado com sucesso");
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro desconhecido");
        }
    }
}
