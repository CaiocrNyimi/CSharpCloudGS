using Microsoft.AspNetCore.Mvc;
using Skill4Green.Application.DTOs;
using Skill4Green.Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Skill4Green.API.Examples;
using Skill4Green.API.Hateoas;

namespace Skill4Green.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RecompensasController : ControllerBase
{
    private readonly IRecompensaService _service;
    private readonly RecompensaHateoasBuilder _hateoas;

    public RecompensasController(IRecompensaService service)
    {
        _service = service;
        _hateoas = new RecompensaHateoasBuilder();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResourceWrapper<RecompensaDto>>>> GetAll()
    {
        var result = await _service.ListarAsync();
        var wrapped = result.Select(r => _hateoas.Build(r));
        return Ok(wrapped);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResourceWrapper<RecompensaDto>>> GetById(int id)
    {
        var result = await _service.ObterPorIdAsync(id);
        return result == null ? NotFound() : Ok(_hateoas.Build(result));
    }

    [HttpPost]
    [SwaggerRequestExample(typeof(CreateRecompensaDto), typeof(CreateRecompensaDtoExample))]
    [ProducesResponseType(typeof(ResourceWrapper<RecompensaDto>), StatusCodes.Status201Created)]
    [Consumes("application/json")]
    public async Task<ActionResult<ResourceWrapper<RecompensaDto>>> Create([FromBody] CreateRecompensaDto dto)
    {
        var created = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, _hateoas.Build(created));
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
        var mensagem = await _service.TrocarAsync(nome, recompensaId);
        return Ok(mensagem);
    }
}