using System;
using ProjetoPOO.Classes;
using ProjetoPOO.Menu;
namespace ProjetoPOO.Services;

public static class ProdutoService
{
    private static Produto[] vetorProdutos = new Produto[100];
    private static int qtdProdutos = 0;
    private static int idProduto = 0;

    public static void Adicionar()
    {
        if (qtdProdutos >= vetorProdutos.Length)
        {
            Console.WriteLine("Limite de produtos atingido!");
            return;
        }

        Console.Write("Nome do produto: ");
        string ?nome = Console.ReadLine()!;

        Console.Write("Descrição: ");
        string? descricao = Console.ReadLine()!;

        Console.Write("Preço: ");
        double preco = double.Parse(Console.ReadLine()!);

        Console.Write("Quantidade em estoque: ");
        int estoque = int.Parse(Console.ReadLine()!);

        // Listar fornecedores disponíveis
        Console.WriteLine("\nFornecedores disponíveis:");
        for (int i = 0; i < FornecedorService.GetQuantidade(); i++)
        {
            var forn = FornecedorService.GetFornecedor(i);
            Console.WriteLine($"{forn.IdFornecedor} - {forn.Nome}");
        }

        Console.Write("ID do Fornecedor: ");
        int idFornecedor = int.Parse(Console.ReadLine()!);
        Fornecedor fornecedor = FornecedorService.GetFornecedor(idFornecedor);

        if (fornecedor == null)
        {
            Console.WriteLine("Fornecedor não encontrado!");
            return;
        }

        var produto = new Produto
        {
            IdProduto = idProduto++,
            Nome = nome,
            Descricao = descricao,
            Preco = preco,
            Estoque = estoque,
            Fornecedor = fornecedor
        };

        vetorProdutos[qtdProdutos++] = produto;
        Console.WriteLine("Produto cadastrado com sucesso!");
    }

    public static void Alterar()
    {
        Console.Write("ID do produto a alterar: ");
        int id = int.Parse(Console.ReadLine()!);

        for (int i = 0; i < qtdProdutos; i++)
        {
            if (vetorProdutos[i].IdProduto == id)
            {
                Console.Write("Novo nome: ");
                vetorProdutos[i].Nome = Console.ReadLine()!;

                Console.Write("Nova descrição: ");
                vetorProdutos[i].Descricao = Console.ReadLine()!;

                Console.Write("Novo preço: ");
                vetorProdutos[i].Preco = double.Parse(Console.ReadLine()!);

                Console.Write("Novo estoque: ");
                vetorProdutos[i].Estoque = int.Parse(Console.ReadLine()!);

                Console.WriteLine("Produto alterado!");
                return;
            }
        }
        Console.WriteLine("Produto não encontrado.");
    }

    public static void Excluir()
    {
        Console.Write("ID do produto a excluir: ");
        int id = int.Parse(Console.ReadLine()!);

        for (int i = 0; i < qtdProdutos; i++)
        {
            if (vetorProdutos[i].IdProduto == id)
            {
                for (int j = i; j < qtdProdutos - 1; j++)
                {
                    vetorProdutos[j] = vetorProdutos[j + 1];
                }
                qtdProdutos--;
                Console.WriteLine("Produto excluído!");
                return;
            }
        }
        Console.WriteLine("Produto não encontrado.");
    }

    public static void Consultar()
    {
        Console.Write("ID do produto: ");
        int id = int.Parse(Console.ReadLine()!);

        for (int i = 0; i < qtdProdutos; i++)
        {
            if (vetorProdutos[i].IdProduto == id)
            {
                Exibir(vetorProdutos[i]);
                return;
            }
        }
        Console.WriteLine("Produto não encontrado.");
    }

    private static void Exibir(Produto p)
    {
        Console.WriteLine("\n=== DADOS DO PRODUTO ===");
        Console.WriteLine($"ID: {p.IdProduto}");
        Console.WriteLine($"Nome: {p.Nome}");
        Console.WriteLine($"Descrição: {p.Descricao}");
        Console.WriteLine($"Preço: R${p.Preco:F2}");
        Console.WriteLine($"Estoque: {p.Estoque} unidades");
        Console.WriteLine($"Fornecedor: {p.Fornecedor.Nome} (ID: {p.Fornecedor.IdFornecedor})");
    }


}