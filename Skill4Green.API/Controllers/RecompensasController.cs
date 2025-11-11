using Microsoft.AspNetCore.Mvc;
using Skill4Green.Application.DTOs;
using Skill4Green.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Skill4Green.API.Examples;

namespace Skill4Green.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RecompensasController : ControllerBase
{
    private readonly IRecompensaService _service;

    public RecompensasController(IRecompensaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RecompensaDto>>> GetAll()
    {
        var result = await _service.ListarAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RecompensaDto>> GetById(int id)
    {
        var result = await _service.ObterPorIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [SwaggerRequestExample(typeof(CreateRecompensaDto), typeof(CreateRecompensaDtoExample))]
    [ProducesResponseType(typeof(RecompensaDto), StatusCodes.Status201Created)]
    [Consumes("application/json")]
    public async Task<ActionResult<RecompensaDto>> Create([FromBody] CreateRecompensaDto dto)
    {
        var created = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [SwaggerRequestExample(typeof(UpdateRecompensaDto), typeof(UpdateRecompensaDtoExample))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes("application/json")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRecompensaDto dto)
    {
        await _service.AtualizarAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeletarAsync(id);
        return NoContent();
    }

    [HttpPost("trocar")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Trocar([FromQuery] string nome, [FromQuery] int recompensaId)
    {
        try
        {
            var mensagem = await _service.TrocarAsync(nome, recompensaId);
            return Ok(mensagem);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}