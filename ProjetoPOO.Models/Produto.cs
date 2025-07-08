using System;

namespace ProjetoPOO.Models;

public class Produto : IIdentificavel
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public double Preco { get; set; }
    public int Estoque { get; set; }
    public Fornecedor Fornecedor { get; set; } = new Fornecedor();

    public override string ToString()
    {
        string disponivel;
        if (Estoque > 0)
        {
            disponivel = $"Em estoque: {Estoque}";
        }
        else
        {
            disponivel = "Indisponível";
        }

        return $"ID {Id}: {Nome} – R$ {Preco:N2} – {disponivel} – Forn: {Fornecedor.Nome}";
    }
}

