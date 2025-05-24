using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoPOO.Classes;

namespace ProjetoPOO.Services;

public static class TransportadoraService
{
    // Lista estática
    private static List<Transportadora> ListTransportadora = new List<Transportadora>();

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

        ListTransportadora.Add(nova);
        Console.WriteLine("Transportadora adicionada com sucesso!");
    }

    public static void Alteracao()
    {
        Console.Write("Digite o ID da transportadora a alterar: ");
        int id = int.Parse(Console.ReadLine()!);

        Transportadora transportadoraEncontrada = null;

        foreach (Transportadora t in ListTransportadora)
        {
            if (t.IdTransporadora == id)
            {
                transportadoraEncontrada = t;
                break;
            }
        }

        if (transportadoraEncontrada != null)
        {
            Console.Write("Novo nome: ");
            transportadoraEncontrada.Nome = Console.ReadLine()!;

            Console.Write("Novo preço por KM: ");
            transportadoraEncontrada.PrecoPorKm = double.Parse(Console.ReadLine()!);

            Console.WriteLine("Alterado com sucesso!");
        }
        else
        {
            Console.WriteLine("Transportadora não encontrada.");
        }
    }


    public static void Exclusao()
    {
        Console.Write("Digite o ID da transportadora a excluir: ");
        int id = int.Parse(Console.ReadLine()!);

        Transportadora transportadoraEncontrada = null;

        foreach (Transportadora t in ListTransportadora)
        {
            if (t.IdTransporadora == id)
            {
                transportadoraEncontrada = t;
                break;
            }
        }

        if (transportadoraEncontrada != null)
        {
            ListTransportadora.Remove(transportadoraEncontrada);
            Console.WriteLine("Transportadora excluída com sucesso!");
        }
        else
        {
            Console.WriteLine("Transportadora não encontrada.");
        }
    }

    public static void Consulta()
    {
        Console.Write("Digite o ID da tranportadora que deseja consultar: ");
        int id = int.Parse(Console.ReadLine()!);

        Transportadora transportadoraEncontrada = null;

        foreach (Transportadora t in ListTransportadora)
        {
            if (t.IdTransporadora == id)
            {
                transportadoraEncontrada = t;
                break;
            }
        }
        if (transportadoraEncontrada != null)
        {
            Console.WriteLine("=== Transportadora encontrada ===");
            Console.WriteLine($"ID: {transportadoraEncontrada.IdTransporadora}");
            Console.WriteLine($"Nome: {transportadoraEncontrada.Nome}");
            Console.WriteLine($"Preço por KM: {transportadoraEncontrada.PrecoPorKm}");
        }
        else
        {
            Console.WriteLine("Transportadora não encontrada.");
        }
    }
}
