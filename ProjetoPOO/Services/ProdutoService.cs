using System;
using ProjetoPOO.Classes;
using ProjetoPOO.Menu;
namespace ProjetoPOO.Services;

public  class ProdutoService
{
    FornecedorService fornecedorService;
    public ProdutoService()
    {
        fornecedorService = new FornecedorService();
    }
    private  Produto[] vetorProdutos = new Produto[100];
    private  int qtdProdutos = 0;
    private  int idProduto = 0;

    public  void Adicionar()
    {
        if (qtdProdutos >= vetorProdutos.Length)
        {
            Console.WriteLine("Limite de produtos atingido!");
            return;
        }

        Console.Write("Nome do produto: ");
        string? nome = Console.ReadLine()!;

        Console.Write("Descrição: ");
        string? descricao = Console.ReadLine()!;

        Console.Write("Preço: ");
        double preco = double.Parse(Console.ReadLine()!);

        Console.Write("Quantidade em estoque: ");
        int estoque = int.Parse(Console.ReadLine()!);

        // Listar fornecedores disponíveis
        Console.WriteLine("\nFornecedores disponíveis:");
        for (int i = 0; i < fornecedorService.GetQuantidade(); i++)
        {
            var forn = fornecedorService.GetFornecedor(i);
            if (forn == null) continue;
            Console.WriteLine($"{forn.IdFornecedor} - {forn.Nome}");
        }

        Console.Write("ID do Fornecedor: ");
        int idFornecedor = int.Parse(Console.ReadLine()!);
        Fornecedor fornecedor = fornecedorService.GetFornecedor(idFornecedor);

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

    public  void Alterar()
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


    public  void Excluir()
    {
        Console.Write("Digite o ID do Produto a excluir: ");
        int id = int.Parse(Console.ReadLine()!);

        for (int i = 0; i < qtdProdutos; i++)
        {
            if (vetorProdutos[i].IdProduto == id)
            {
                // Deslocar os elementos
                for (int j = i; j < qtdProdutos - 1; j++)
                {
                    vetorProdutos[j] = vetorProdutos[j + 1];
                }

                vetorProdutos[qtdProdutos - 1] = null;
                qtdProdutos--;

                Console.WriteLine("Produto excluída com sucesso!");
                return;
            }
        }

        Console.WriteLine("Produto não encontrada.");
    }

    private  void Exibir(Produto p)
    {
        Console.WriteLine("\n=== DADOS DO PRODUTO ===");
        Console.WriteLine($"ID: {p.IdProduto}");
        Console.WriteLine($"Nome: {p.Nome}");
        Console.WriteLine($"Descrição: {p.Descricao}");
        Console.WriteLine($"Preço: R${p.Preco:F2}");
        Console.WriteLine($"Estoque: {p.Estoque} unidades");
        Console.WriteLine($"Fornecedor: {p.Fornecedor.Nome} (ID: {p.Fornecedor.IdFornecedor})");
    }
    public  void Consultar()
    {
        Console.WriteLine("\n--- Tipo de Consulta ---");
        Console.WriteLine("1 - Por ID");
        Console.WriteLine("2 - Por Nome (busca parcial)");
        Console.Write("Opção: ");

        string opcao = Console.ReadLine()!;

        switch (opcao)
        {
            case "1":
                ConsultarId();
                break;
            case "2":
                ConsultarPorNome();
                break;
            default:
                Console.WriteLine("Opção inválida.");
                break;
        }
    }

    public  void ConsultarPorNome()
    {
        Console.Write("Digite a parte do nome para buscar: ");
        string termo = Console.ReadLine()!.ToLower();

        bool encontrou = false;

        for (int i = 0; i < qtdProdutos; i++)
        {
            // Verifica se o produto na posição i é nulo
            if (vetorProdutos[i] == null)
            {
                continue; // Pula para o próximo produto
            }

            string nomeLower = vetorProdutos[i].Nome.ToLower();
            bool contemTodasLetras = true;

            if (!nomeLower.Contains(termo))
            {
                contemTodasLetras = false;
                continue;
            }

            if (contemTodasLetras)
            {
                Exibir(vetorProdutos[i]);
                Console.WriteLine("---------------------");
                encontrou = true;
            }
        }

        if (!encontrou)
        {
            Console.WriteLine($"Nenhum produto encontrado com: {termo}");
        }
    }
    public  void ConsultarId()
    {
        Console.Write("Digite o id que deseja consultar: ");
        int id = int.Parse(Console.ReadLine()!);
        int i;

        for (i = 0; i < qtdProdutos; i++)
        {
            if (id == vetorProdutos[i].IdProduto)
            {
                Exibir(vetorProdutos[i]);
                return;
            }
        }

        Console.WriteLine("Produto nao encontrado");

    }


}