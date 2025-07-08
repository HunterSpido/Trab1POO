using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;
using ProjetoPOO.Services.Exceptions;

namespace ProjetoPOO.Services
{
    public class TransportadoraService
    {
        private readonly IRepositoryTransportadora _repo;

        public TransportadoraService(IRepositoryTransportadora repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _repo.Carregar();
        }

        public void Adicionar()
        {
            Console.Write("Digite o nome: ");
            string nome = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(nome))
                throw new ExcecaoEntradaInvalida("Nome da transportadora inválido.");

            Console.Write("Digite o preço por KM: ");
            if (!double.TryParse(Console.ReadLine(), out double preco) || preco < 0)
                throw new ExcecaoEntradaInvalida("Preço por KM inválido.");

            var nova = new Transportadora
            {
                Nome = nome,
                PrecoPorKm = preco
            };

            if (_repo.Adicionar(nova))
            {
                _repo.Salvar();
                Console.WriteLine("Transportadora adicionada com sucesso!");
            }
            else
            {
                throw new Exception("Erro ao adicionar transportadora.");
            }
        }

        public void Alterar()
        {
            Console.Write("Digite o ID da transportadora a alterar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
                throw new ExcecaoEntradaInvalida("ID inválido.");

            var t = _repo.Listar().FirstOrDefault(t => t.IdTransportadora == id);
            if (t == null)
                throw new ExcecaoEntidadeNaoEncontrada("Transportadora não encontrada.");

            Console.Write($"Novo nome ({t.Nome}): ");
            string nome = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(nome))
                t.Nome = nome;

            Console.Write($"Novo preço por KM ({t.PrecoPorKm}): ");
            string precoStr = Console.ReadLine()!;
            if (double.TryParse(precoStr, out double preco) && preco >= 0)
                t.PrecoPorKm = preco;

            if (_repo.Alterar(t))
            {
                _repo.Salvar();
                Console.WriteLine("Transportadora alterada com sucesso!");
            }
            else
            {
                throw new Exception("Erro ao alterar transportadora.");
            }
        }

        public void Excluir()
        {
            Console.Write("Digite o ID da transportadora a excluir: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
                throw new ExcecaoEntradaInvalida("ID inválido.");

            var t = _repo.Listar().FirstOrDefault(t => t.IdTransportadora == id);
            if (t == null)
                throw new ExcecaoEntidadeNaoEncontrada("Transportadora não encontrada.");

            if (_repo.Remover(t))
            {
                _repo.Salvar();
                Console.WriteLine("Transportadora excluída com sucesso!");
            }
            else
            {
                throw new Exception("Erro ao excluir transportadora.");
            }
        }

        public void ConsultarId()
        {
            Console.Write("Digite o ID da transportadora que deseja consultar: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
                throw new ExcecaoEntradaInvalida("ID inválido.");

            var t = _repo.Listar().FirstOrDefault(t => t.IdTransportadora == id);
            if (t == null)
                throw new ExcecaoEntidadeNaoEncontrada("Transportadora não encontrada.");

            Exibir(t);
        }

        public void ConsultarPorNome()
        {
            Console.Write("Digite parte do nome para buscar: ");
            string termo = (Console.ReadLine() ?? "").Trim().ToLower();
            if (string.IsNullOrWhiteSpace(termo))
                throw new ExcecaoEntradaInvalida("Termo de busca inválido.");

            var achadas = _repo.Listar()
                .Where(t => t.Nome.ToLower().Contains(termo))
                .ToList();

            if (!achadas.Any())
                throw new ExcecaoEntidadeNaoEncontrada("Nenhuma transportadora encontrada.");

            foreach (var t in achadas)
            {
                Exibir(t);
                Console.WriteLine("---------------------");
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
                case "1": ConsultarId(); break;
                case "2": ConsultarPorNome(); break;
                default: throw new ExcecaoEntradaInvalida("Opção inválida.");
            }
        }

        private void Exibir(Transportadora t)
        {
            Console.WriteLine("=== Transportadora encontrada ===");
            Console.WriteLine($"ID: {t.IdTransportadora}");
            Console.WriteLine($"Nome: {t.Nome}");
            Console.WriteLine($"Preço por KM: {t.PrecoPorKm}");
        }

        public List<Transportadora> GetTodos()
        {
            return _repo.Listar();
        }
    }
}
