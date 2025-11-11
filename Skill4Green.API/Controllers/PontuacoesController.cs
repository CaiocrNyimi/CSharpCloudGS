using Microsoft.AspNetCore.Mvc;
using Skill4Green.Application.DTOs;
using Skill4Green.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Skill4Green.API.Examples;

namespace Skill4Green.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PontuacoesController : ControllerBase
{
    private readonly IPontuacaoService _service;

    public PontuacoesController(IPontuacaoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PontuacaoDto>>> GetAll([FromQuery] int pagina = 1, [FromQuery] int tamanho = 10)
    {
        var result = await _service.ListarAsync(pagina, tamanho);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PontuacaoDto>> GetById(int id)
    {
        var result = await _service.ObterPorIdAsync(id);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [SwaggerRequestExample(typeof(CreatePontuacaoDto), typeof(CreatePontuacaoDtoExample))]
    [ProducesResponseType(typeof(PontuacaoDto), StatusCodes.Status201Created)]
    [Consumes("application/json")]
    public async Task<ActionResult<PontuacaoDto>> Create([FromBody] CreatePontuacaoDto dto)
    {
        var created = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id}")]
    [SwaggerRequestExample(typeof(UpdatePontuacaoDto), typeof(UpdatePontuacaoDtoExample))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [Consumes("application/json")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePontuacaoDto dto)
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
}