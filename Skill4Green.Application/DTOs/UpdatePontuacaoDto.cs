namespace Skill4Green.Application.DTOs;

/// <summary>Dados para atualizar uma pontuação existente.</summary>
public class UpdatePontuacaoDto
{
    public string Nome { get; set; } = string.Empty;
    public int EcoCoins { get; set; }
    public int NivelVerde { get; set; }
    public DateTime AtualizadoEm { get; set; }
}