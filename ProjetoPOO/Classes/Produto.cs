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

    public void MenuProdutos()
    {
        int v = -1;
        Console.WriteLine("Selecione uma opção");
        Console.WriteLine("1- Adicone um produto");
        Console.WriteLine("2- Altere um produto");
        Console.WriteLine("3- Exclua um produto");
        Console.WriteLine("4- Consulte um produto");

        switch (v)
        {
            case 1:
                Adicione();
                break;
            case 2:
                Alteracao();
                break;
            case 3:
                Exclusao();
                break;
            case 4:
                Consulta();
                break;
            default:
                System.Console.WriteLine("Digite entre a opção 1-4");
                break;
        }
    }

    private void Consulta()
    {
        
    }

    private void Exclusao()
    {
       
    }

    private void Alteracao()
    {
        
    }

    public void Adicione()
    {

    }

}
