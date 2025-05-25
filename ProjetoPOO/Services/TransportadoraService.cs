using System;
using ProjetoPOO.Classes;

namespace ProjetoPOO.Services;

public static class TransportadoraService
{
    private static Transportadora[] vetorTransportadoras = new Transportadora[100];
    private static int qtdTransportadoras = 0;

    public static void MenuTransportadora()
    {
        Console.WriteLine("Selecione uma opção");
        Console.WriteLine("1 - Adicione uma transportadora");
        Console.WriteLine("2 - Altere uma transportadora");
        Console.WriteLine("3 - Exclua uma transportadora");
        Console.WriteLine("4 - Consulte uma transportadora");

        int v = int.Parse(Console.ReadLine()!);
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
                Console.WriteLine("Digite entre a opção 1-4");
                break;
        }
    }

    public static void Adicione()
    {
        if (qtdTransportadoras >= vetorTransportadoras.Length)
        {
            Console.WriteLine("Limite de transportadoras atingido.");
            return;
        }

        Console.Write("Digite o ID da transportadora: ");
        int id = int.Parse(Console.ReadLine()!);

        Console.Write("Digite o nome da transportadora: ");
        string nome = Console.ReadLine()!;

        Console.Write("Digite o preço por KM: ");
        double preco = double.Parse(Console.ReadLine()!);

        var nova = new Transportadora
        {
            IdTransporadora = id,
            Nome = nome,
            PrecoPorKm = preco
        };

        vetorTransportadoras[qtdTransportadoras] = nova;
        qtdTransportadoras++;

        Console.WriteLine("Transportadora adicionada com sucesso!");
    }

    public static void Alteracao()
    {
        Console.Write("Digite o ID da transportadora a alterar: ");
        int id = int.Parse(Console.ReadLine()!);

        for (int i = 0; i < qtdTransportadoras; i++)
        {
            if (vetorTransportadoras[i].IdTransporadora == id)
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

    public static void Exclusao()
    {
        Console.Write("Digite o ID da transportadora a excluir: ");
        int id = int.Parse(Console.ReadLine()!);

        for (int i = 0; i < qtdTransportadoras; i++)
        {
            if (vetorTransportadoras[i].IdTransporadora == id)
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

    public static void Consulta()
    {
        Console.Write("Digite o ID da transportadora que deseja consultar: ");
        int id = int.Parse(Console.ReadLine()!);

        for (int i = 0; i < qtdTransportadoras; i++)
        {
            if (vetorTransportadoras[i].IdTransporadora == id)
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
        Console.WriteLine($"ID: {t.IdTransporadora}");
        Console.WriteLine($"Nome: {t.Nome}");
        Console.WriteLine($"Preço por KM: {t.PrecoPorKm}");
    }
}
