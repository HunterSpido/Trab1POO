using System;

namespace ProjetoPOO.Classes;

public class Produto
{
    public int IdProduto { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public double Preco { get; set; }
    public int Estoque { get; set; }
    public Fornecedor Fornecedor { get; set; } // Relacionamento com Fornecedor
}
