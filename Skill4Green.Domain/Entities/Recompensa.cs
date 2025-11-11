namespace Skill4Green.Domain.Entities;

public class Recompensa
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int CustoEcoCoins { get; set; }
    public string Tipo { get; set; } = string.Empty;
}