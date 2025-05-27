using System;
using ProjetoPOO.Classes;

namespace ProjetoPOO.Services;

public static class TransportadoraService
{
    private static Transportadora[] vetorTransportadoras = new Transportadora[100];
    private static int qtdTransportadoras = 0;
    private static int Id = 0;
    public static void Adicionar()
    {
        if (qtdTransportadoras >= vetorTransportadoras.Length)
        {
            Console.WriteLine("Limite de transportadoras atingido.");
            return;
        }
        Console.Write("Digite o nome: ");
        string nome = Console.ReadLine()!;

        Console.Write("Digite o preço por KM: ");
        double preco = double.Parse(Console.ReadLine()!);

        var nova = new Transportadora
        {
            IdTransportadora = Id,
            Nome = nome,
            PrecoPorKm = preco
        };

        vetorTransportadoras[qtdTransportadoras] = nova;
        qtdTransportadoras++;
        Id++;

        Console.WriteLine("Transportadora adicionada com sucesso!");
    }

    public static void Alterar()
    {
        Console.Write("Digite o ID da transportadora a alterar: ");
        int id = int.Parse(Console.ReadLine()!);

        for (int i = 0; i < qtdTransportadoras; i++)
        {
            if (vetorTransportadoras[i].IdTransportadora == id)
            {
                Console.Write("Novo nome: ");
                vetorTransportadoras[i].Nome = Console.ReadLine()!;

                Console.Write("Novo preço por KM: ");
                vetorTransportadoras[i].PrecoPorKm = double.Parse(Console.ReadLine()!);

                Console.WriteLine("Transportadora alterada com sucesso!");
                return;
            }
        }

        Console.WriteLine("Transportadora não encontrada.");
    }

    public static void Excluir()
    {
        Console.Write("Digite o ID da transportadora a excluir: ");
        int id = int.Parse(Console.ReadLine()!);

        for (int i = 0; i < qtdTransportadoras; i++)
        {
            if (vetorTransportadoras[i].IdTransportadora == id)
            {
                // Deslocar os elementos
                for (int j = i; j < qtdTransportadoras - 1; j++)
                {
                    vetorTransportadoras[j] = vetorTransportadoras[j + 1];
                }

                vetorTransportadoras[qtdTransportadoras - 1] = null!;
                qtdTransportadoras--;

                Console.WriteLine("Transportadora excluída com sucesso!");
                return;
            }
        }

        Console.WriteLine("Transportadora não encontrada.");
    }

    public static void ConsultarId()
    {
        Console.Write("Digite o ID da transportadora que deseja consultar: ");
        int Id = int.Parse(Console.ReadLine()!);


        for (int i = 0; i < qtdTransportadoras; i++)
        {
            if (vetorTransportadoras[i].IdTransportadora == Id)
            {
                Exibir(vetorTransportadoras[i]);
                return;
            }
        }

        Console.WriteLine("Transportadora não encontrada.");
    }
    private static void Exibir(Transportadora t)
    {
        Console.WriteLine("=== Transportadora encontrada ===");
        Console.WriteLine($"ID: {t.IdTransportadora}");
        Console.WriteLine($"Nome: {t.Nome}");
        Console.WriteLine($"Preço por KM: {t.PrecoPorKm}");
    }

    public static void ConsultarPorNome()
    {
        Console.Write("Digite parte do nome para buscar: ");
        string termo = Console.ReadLine()!.ToLower();

        bool encontrou = false;

        for (int i = 0; i < qtdTransportadoras; i++)
        {
            if (vetorTransportadoras == null)
            {
                continue;
            }
            string nomeLower = vetorTransportadoras[i].Nome.ToLower();
            bool contemTodasLetras = true;


            if (!nomeLower.Contains(termo))
            {
                contemTodasLetras = false;
                continue;
            }


            if (contemTodasLetras)
            {
                Exibir(vetorTransportadoras[i]);
                Console.WriteLine("---------------------");
                encontrou = true;
            }
        }

        if (!encontrou)
        {
            Console.WriteLine($"Nenhuma transportadora encontrado com: {termo}");
        }
    }

    public static void Consultar()
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
}
