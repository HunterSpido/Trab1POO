namespace ProjetoPOO.Menu;

using ProjetoPOO.Classes;
using ProjetoPOO.Services;
using System;
using System.Runtime.CompilerServices;

public static class FornecedorService
{
    private static Fornecedor[] vetorFornecedores = new Fornecedor[100];
    private static int qtdFornecedores = 0;

    private static int idFornecedores = 0;

    public static void Adicionar()
    {
        if (qtdFornecedores >= vetorFornecedores.Length)
        {
            Console.Write("Limite Atingido");
            return;
        }

        Console.Write("Digite o nome: ");
        string nome = Console.ReadLine();
        Console.Write("Digite a descricao: ");
        string descricao = Console.ReadLine();
        Console.Write("Digite o telefone: ");
        string telefone = Console.ReadLine();
        Console.Write("Digite o email: ");
        string email = Console.ReadLine();
        Endereco endereco = EnderecoService.PedirEndereco();

        var fornecedor = new Fornecedor
        {
            IdFornecedor = idFornecedores,
            Nome = nome,
            Descricao = descricao,
            Telefone = telefone,
            Email = email,
            Endereco = endereco,

        };

        vetorFornecedores[qtdFornecedores] = fornecedor;
        qtdFornecedores++;
        idFornecedores++;
        Console.Write("Fornecedor Criado com sucesso!");
    }

    public static void Alterar()
    {
        Console.Write("Digite o id que deseja alterar: ");
        int id = int.Parse(Console.ReadLine());
        int i;

        for (i = 0; i < qtdFornecedores; i++)
        {
            if (id == vetorFornecedores[i].IdFornecedor)
            {
                Console.Write("Digite o novo nome: ");
                vetorFornecedores[i].Nome = Console.ReadLine();
                Console.Write("Digite a nova descricao: ");
                vetorFornecedores[i].Descricao = Console.ReadLine();
                Console.Write("Digite o novo telefone: ");
                vetorFornecedores[i].Telefone = Console.ReadLine();
                Console.Write("Digite o novo email: ");
                vetorFornecedores[i].Email = Console.ReadLine();
                vetorFornecedores[i].Endereco = EnderecoService.PedirEndereco();

                Console.WriteLine("Fornecedora alterada com sucesso!");
                return;
            }
        }
        Console.WriteLine("Fornecedor não encontrado.");
    }

    public static void Excluir()
    {
        Console.Write("Digite o id que deseja Excluir: ");
        int id = int.Parse(Console.ReadLine());
        int i;

        for (i = 0; i < qtdFornecedores; i++)
        {
            if (id == vetorFornecedores[i].IdFornecedor)
            {

                // Deslocar os elementos
                for (int j = i; j < qtdFornecedores - 1; j++)
                {
                    vetorFornecedores[j] = vetorFornecedores[j + 1];
                }

                vetorFornecedores[qtdFornecedores - 1] = null!;
                qtdFornecedores--;

                Console.WriteLine("Forncedor excluido.");
                return;
            }
        }
        Console.WriteLine("Fornecedor não encontrado.");

    }

    public static void Consultar()
    {
        Console.Write("Digite o id que deseja consultar: ");
        int id = int.Parse(Console.ReadLine());
        int i;

        for (i = 0; i < qtdFornecedores; i++)
        {
            if (id == vetorFornecedores[i].IdFornecedor)
            {
                Exibir(vetorFornecedores[i]);
                return;
            }
        }

        Console.WriteLine("Fornecedor nao encontrado");

    }

    private static void Exibir(Fornecedor f)
    {
        Console.WriteLine("=== Fornecedor Encontrado ===");
        Console.WriteLine($"ID: {f.IdFornecedor}");
        Console.WriteLine($"Nome: {f.Nome}");
        Console.WriteLine($"Descricao: {f.Descricao}");
        Console.WriteLine($"Telefone: {f.Telefone}");
        Console.WriteLine($"Email: {f.Email}");
        Console.WriteLine($"Endereco:");
        Console.WriteLine($"Estado: {f.Endereco.Estado}");
        Console.WriteLine($"Cidade: {f.Endereco.Cidade}");
        Console.WriteLine($"Bairro: {f.Endereco.Bairro}");
        Console.WriteLine($"Rua: {f.Endereco.Rua}");
        Console.WriteLine($"Numero: {f.Endereco.Numero}");
        Console.WriteLine($"CEP: {f.Endereco.Cep}");
    }

    public static void Consultarf()
    {
        Console.Write("Digite o id que deseja consultar: ");
        int id = int.Parse(Console.ReadLine());
        int i;

        for (i = 0; i < qtdFornecedores; i++)
        {
            if (id == vetorFornecedores[i].IdFornecedor)
            {
                Exibir(vetorFornecedores[i]);
                return;
            }
        }

        Console.WriteLine("Fornecedor nao encontrado");

    }

    public static int GetQuantidade()
    {
        return qtdFornecedores;
    }

    public static Fornecedor GetFornecedor(int id)
    {
        for (int i = 0; i < qtdFornecedores; i++)
        {
            if (vetorFornecedores[i] != null && id == vetorFornecedores[i].IdFornecedor)
            {
                return vetorFornecedores[i];
            }
        }
        return null;
    }

    
    
}