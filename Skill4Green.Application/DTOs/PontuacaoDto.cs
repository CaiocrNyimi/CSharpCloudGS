namespace Skill4Green.Application.DTOs;

/// <summary>Dados retornados de uma pontuação.</summary>
public class PontuacaoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int EcoCoins { get; set; }
    public int NivelVerde { get; set; }
    public DateTime AtualizadoEm { get; set; }
}