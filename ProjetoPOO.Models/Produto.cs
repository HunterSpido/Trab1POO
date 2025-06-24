using System;

namespace ProjetoPOO.Models;

public class Produto:IIdentificavel
{
    public int IdProduto { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public double Preco { get; set; }
    public int Estoque { get; set; }

    public int Id => IdProduto;
    public Fornecedor Fornecedor { get; set; } // Relacionamento com Fornecedor
}
