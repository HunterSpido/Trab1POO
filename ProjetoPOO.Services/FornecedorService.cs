namespace ProjetoPOO.Menu;

using ProjetoPOO.Models;
using ProjetoPOO.Services;
using System;
using System.Runtime.CompilerServices;

public  class FornecedorService
{
    EnderecoService enderecoService;
    public FornecedorService()
    {
        enderecoService = new EnderecoService();
    }
    private  Fornecedor[] vetorFornecedores = new Fornecedor[100];
    private  int qtdFornecedores = 0;

    private  int idFornecedores = 0;

    public  void Adicionar()
    {
        if (qtdFornecedores >= vetorFornecedores.Length)
        {
            Console.Write("Limite Atingido");
            return;
        }

        Console.Write("Digite o nome: ");
        string nome = Console.ReadLine()!;
        Console.Write("Digite a descricao: ");
        string descricao = Console.ReadLine()!;
        Console.Write("Digite o telefone: ");
        string telefone = Console.ReadLine()!;
        Console.Write("Digite o email: ");
        string email = Console.ReadLine()!;
        Endereco endereco = enderecoService.PedirEndereco();

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
        Console.WriteLine("Fornecedor Criado com sucesso!");
    }

    public  void Alterar()
    {
        Console.Write("Digite o id que deseja alterar: ");
        int id = int.Parse(Console.ReadLine()!);
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
                vetorFornecedores[i].Endereco = enderecoService.PedirEndereco();

                Console.WriteLine("Fornecedora alterada com sucesso!");
                return;
            }
        }
        Console.WriteLine("Fornecedor não encontrado.");
    }


    public  void Excluir()
    {
        Console.Write("Digite o ID do Fornecedor a excluir: ");
        int id = int.Parse(Console.ReadLine()!);

        for (int i = 0; i < qtdFornecedores; i++)
        {
            if (vetorFornecedores[i].IdFornecedor == id)
            {
                // Deslocar os elementos
                for (int j = i; j < qtdFornecedores - 1; j++)
                {
                    vetorFornecedores[j] = vetorFornecedores[j + 1];
                }

                vetorFornecedores[qtdFornecedores - 1] = null!;
                qtdFornecedores--;

                Console.WriteLine("Fornecedor excluído com sucesso!");
                return;
            }
        }

        Console.WriteLine("Fornecedor não encontrada.");
    }
    public  void ConsultarId()
    {
        Console.Write("Digite o id que deseja consultar: ");
        int id = int.Parse(Console.ReadLine()!);
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

    private  void Exibir(Fornecedor f)
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
    public  int GetQuantidade()
    {
        return qtdFornecedores;
    }

    public  Fornecedor GetFornecedor(int id)
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

        for (int i = 0; i < qtdFornecedores; i++)
        {
            if (vetorFornecedores[i] == null)
            {
                continue; // Pula para o próximo produto se for nulo
            }
            string nomeLower = vetorFornecedores[i].Nome.ToLower();
            bool contemTodasLetras = true;


            if (!nomeLower.Contains(termo))
            {
                contemTodasLetras = false;
                continue;
            }


            if (contemTodasLetras)
            {
                Exibir(vetorFornecedores[i]);
                Console.WriteLine("---------------------");
                encontrou = true;
            }
        }

        if (!encontrou)
        {
            Console.WriteLine($"Nenhum fornecedor encontrado com: {termo}");
        }
    }



}