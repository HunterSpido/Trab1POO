using System;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Services;

public  class TransportadoraService
{
    private readonly IRepositoryTransportadora _repo;
    
    public TransportadoraService(IRepositoryTransportadora repo)
        {
            _repo = repo;

            // Carrega do arquivo ao iniciar
            _repo.Carregar();
        }
    public void Adicionar()
    {
        Console.Write("Digite o nome: ");
        string nome = Console.ReadLine()!;

        Console.Write("Digite o preço por KM: ");
        double preco = double.Parse(Console.ReadLine()!);

        // Se quiser, pode pedir endereço também
        // Endereco end = _enderecoService.PedirEndereco();

        var nova = new Transportadora
        {
            Nome = nome,
            PrecoPorKm = preco
            // Endereco = end
        };

        bool ok = _repo.Adicionar(nova);
        if (ok)
        {
            _repo.Salvar();
            Console.WriteLine("Transportadora adicionada com sucesso!");
        }
        else
        {
            Console.WriteLine("Falha ao adicionar transportadora.");
        }
    }

    public  void Alterar()
    {
        Console.Write("Digite o ID da transportadora a alterar: ");
        int id = int.Parse(Console.ReadLine()!);

        var t = _repo.Listar().FirstOrDefault(t => t.IdTransportadora == id);
        if (t == null)
        {
            Console.WriteLine("Transportadora não encontrada.");
            return;
        }

        Console.Write($"Novo nome ({t.Nome}): ");
        string nome = Console.ReadLine()!;
        if (!string.IsNullOrWhiteSpace(nome))
            t.Nome = nome;

        Console.Write($"Novo preço por KM ({t.PrecoPorKm}): ");
        string precoStr = Console.ReadLine()!;
        if (double.TryParse(precoStr, out double preco))
            t.PrecoPorKm = preco;

        bool ok = _repo.Alterar(t);
        if (ok)
        {
            _repo.Salvar();
            Console.WriteLine("Transportadora alterada com sucesso!");
        }
        else
        {
            Console.WriteLine("Falha ao alterar transportadora.");
        }
    }

    public  void Excluir()
    {
        Console.Write("Digite o ID da transportadora a excluir: ");
        int id = int.Parse(Console.ReadLine()!);

        var t = _repo.Listar().FirstOrDefault(t => t.IdTransportadora == id);
        if (t == null)
        {
            Console.WriteLine("Transportadora não encontrada.");
            return;
        }

        bool ok = _repo.Remover(t);
        if (ok)
        {
            _repo.Salvar();
            Console.WriteLine("Transportadora excluída com sucesso!");
        }
        else
        {
            Console.WriteLine("Falha ao excluir transportadora.");
        }
    }

    public void ConsultarId()
    {
        Console.Write("Digite o ID da transportadora que deseja consultar: ");
        int id = int.Parse(Console.ReadLine()!);

        var t = _repo.Listar().FirstOrDefault(t => t.IdTransportadora == id);
        if (t == null)
        {
            Console.WriteLine("Transportadora não encontrada.");
            return;
        }
        Exibir(t);
    }

    public void ConsultarPorNome()
    {
        Console.Write("Digite parte do nome para buscar: ");
        string termo = (Console.ReadLine() ?? "").ToLower();

        var lista = _repo.Listar();
        bool encontrou = false;

        foreach (var t in lista)
        {
            if (t.Nome.ToLower().Contains(termo))
            {
                Exibir(t);
                Console.WriteLine("---------------------");
                encontrou = true;
            }
        }

        if (!encontrou)
        {
            Console.WriteLine($"Nenhuma transportadora encontrada com: {termo}");
        }
    }

    public void Consultar()
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

    private void Exibir(Transportadora t)
    {
        Console.WriteLine("=== Transportadora encontrada ===");
        Console.WriteLine($"ID: {t.IdTransportadora}");
        Console.WriteLine($"Nome: {t.Nome}");
        Console.WriteLine($"Preço por KM: {t.PrecoPorKm}");
    }

}
