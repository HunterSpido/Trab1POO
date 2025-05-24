using System;

namespace ProjetoPOO.Classes;

public class Produto
{
    public string? Nome { get; set; }
    public double Valor { get; set; }
    public int Quantidade { get; set; }
    public Fornecedor PFornecedor { get; set; }

    public Produto(string nome, double valor, int quantidade, Fornecedor pfornecedor)
    {
        this.Nome = nome;
        this.Valor = valor;
        this.Quantidade = quantidade;
        this.PFornecedor = pfornecedor;
    }

}
