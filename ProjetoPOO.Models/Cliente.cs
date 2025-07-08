using ProjetoPOO.Models;

public class Cliente : IIdentificavel
{
    public int Id { get; set; }
    public string? Nome { get; set; }
    public string Senha { get; set; } = string.Empty;
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public Endereco Endereco { get; set; } = new Endereco();
    public List<Pedido> Pedidos { get; set; } = new();
    // …
}
