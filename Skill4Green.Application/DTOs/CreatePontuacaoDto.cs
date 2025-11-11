namespace Skill4Green.Application.DTOs;

/// <summary>Dados para criar uma nova pontuação.</summary>
public class CreatePontuacaoDto
{
    public string Nome { get; set; } = string.Empty;
    public int EcoCoins { get; set; }
    public int NivelVerde { get; set; }
    public DateTime AtualizadoEm { get; set; }
}