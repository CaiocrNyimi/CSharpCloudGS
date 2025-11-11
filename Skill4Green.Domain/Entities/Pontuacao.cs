namespace Skill4Green.Domain.Entities;

public class Pontuacao
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int EcoCoins { get; set; }
    public int NivelVerde { get; set; }
    public DateTime AtualizadoEm { get; set; }
}