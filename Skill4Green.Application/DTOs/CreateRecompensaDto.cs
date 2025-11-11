namespace Skill4Green.Application.DTOs;

/// <summary>Dados para criar uma nova recompensa.</summary>
public class CreateRecompensaDto
{
    public string Nome { get; set; } = string.Empty;
    public int CustoEcoCoins { get; set; }
    public string Tipo { get; set; } = string.Empty;
}