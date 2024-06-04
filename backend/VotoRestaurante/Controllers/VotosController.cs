using Microsoft.AspNetCore.Mvc;
using VotoRestaurante.Models;
using VotoRestaurante.Services.Restaurantes;
using VotoRestaurante.Services.Votos;

namespace VotoRestaurante.Controllers;
[Route("api/[controller]")]
[ApiController]
public class VotosController : ControllerBase
{
    private IVotoService _votoService;
    public VotosController(IVotoService votoService)
    {
        _votoService = votoService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IAsyncEnumerable<Voto>>> GetVotos()
    {
        try
        {
            var votos = await _votoService.GetVotos();

            return Ok(votos);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter restaurantes");
        }
    }

    [HttpGet]
    [Route("{id:int}/voto")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<Restaurante>> GetVoto(int id)
    {
        try
        {
            var voto = await _votoService.GetVoto(id);
            if (voto == null)
            {
                return NotFound("Não existe voto com esse ID");
            }

            return Ok(voto);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro desconhecido");
        }
    }

    [HttpGet("dia")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IAsyncEnumerable<Voto>>> GetTotalVotos()
    {
        try
        {
            var votos = await _votoService.GetTotalVotos();

            return Ok(votos);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter votos do dia");
        }
    }

    [HttpGet("campeao")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IAsyncEnumerable<VotoCampeao>>> GetVotoCampeao()
    {
        try
        {
            var votos = await _votoService.GetVotoCampeao();

            return Ok(votos);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter restaurantes");
        }
    }


    [HttpPost("incluir")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult> Create(Voto voto)
    {
        try
        {
            await _votoService.CreateVoto(voto);
            return Ok("OK");
        }
        catch (Exception)
        {
            return Ok("cadastrado");
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro desconhecido");
        }
    }
}
