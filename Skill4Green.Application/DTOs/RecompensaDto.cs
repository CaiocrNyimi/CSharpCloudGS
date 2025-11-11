namespace Skill4Green.Application.DTOs;

/// <summary>Dados retornados de uma recompensa.</summary>
public class RecompensaDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int CustoEcoCoins { get; set; }
    public string Tipo { get; set; } = string.Empty;
}