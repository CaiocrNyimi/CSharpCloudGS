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
public class PontuacoesController : ControllerBase
{
    private readonly IPontuacaoService _service;
    private readonly PontuacaoHateoasBuilder _hateoas;

    public PontuacoesController(IPontuacaoService service)
    {
        _service = service;
        _hateoas = new PontuacaoHateoasBuilder();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ResourceWrapper<PontuacaoDto>>>> GetAll([FromQuery] int pagina = 1, [FromQuery] int tamanho = 10)
    {
        var result = await _service.ListarAsync(pagina, tamanho);
        var wrapped = result.Select(p => _hateoas.Build(p));
        return Ok(wrapped);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResourceWrapper<PontuacaoDto>>> GetById(int id)
    {
        var result = await _service.ObterPorIdAsync(id);
        return result == null ? NotFound() : Ok(_hateoas.Build(result));
    }

    [HttpPost]
    [SwaggerRequestExample(typeof(CreatePontuacaoDto), typeof(CreatePontuacaoDtoExample))]
    [ProducesResponseType(typeof(ResourceWrapper<PontuacaoDto>), StatusCodes.Status201Created)]
    [Consumes("application/json")]
    public async Task<ActionResult<ResourceWrapper<PontuacaoDto>>> Create([FromBody] CreatePontuacaoDto dto)
    {
        var created = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, _hateoas.Build(created));
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