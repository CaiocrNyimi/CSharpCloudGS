namespace Skill4Green.Application.DTOs;

/// <summary>Dados para atualizar uma recompensa existente.</summary>
public class UpdateRecompensaDto
{
    public string Nome { get; set; } = string.Empty;
    public int CustoEcoCoins { get; set; }
    public string Tipo { get; set; } = string.Empty;
}