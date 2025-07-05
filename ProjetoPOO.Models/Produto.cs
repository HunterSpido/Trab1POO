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
        => $"ID {Id}: {Nome} — R$ {Preco:N2} — Estoque: {Estoque} — Forn: {Fornecedor.Nome}";
}

