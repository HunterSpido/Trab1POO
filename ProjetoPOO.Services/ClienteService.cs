using System;
using System.Collections.Generic;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Services
{
    public class ClienteService
    {
        private readonly IClienteRepository _repo;
        private readonly EnderecoService _enderecoService;

        public ClienteService(IClienteRepository repo, EnderecoService enderecoService)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _enderecoService = enderecoService ?? throw new ArgumentNullException(nameof(enderecoService));

            // Carrega dados ao iniciar
            _repo.Carregar();
        }

        public void Cadastrar()
        {
            Console.WriteLine("=== Cadastro de Cliente ===");
            Console.Write("Nome: ");
            var nome = Console.ReadLine()?.Trim() ?? "";
            if (nome == "")
            {
                Console.WriteLine("Nome inválido.");
                return;
            }

            Console.Write("Senha: ");
            var senha = Console.ReadLine()?.Trim() ?? "";
            if (senha == "")
            {
                Console.WriteLine("Senha inválida.");
                return;
            }

            // Evita duplicado
            if (_repo.ObterPorNomeESenha(nome, senha) != null)
            {
                Console.WriteLine("Já existe um cliente com esse nome/senha.");
                return;
            }

            Console.Write("Email: ");
            var email = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Telefone: ");
            var telefone = Console.ReadLine()?.Trim() ?? "";

            // Pede endereço
            var endereco = _enderecoService.PedirEndereco();

            var cliente = new Cliente
            {
                Nome = nome,
                Senha = senha,
                Email = email,
                Telefone = telefone,
                Endereco = endereco
            };

            if (_repo.Adicionar(cliente))
                Console.WriteLine("Cliente cadastrado com sucesso!");
            else
                Console.WriteLine("Falha ao cadastrar cliente.");
        }

        public Cliente? ValidarLogin(string nome, string senha)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(senha))
                return null;
            return _repo.ObterPorNomeESenha(nome, senha);
        }

        public void ListarTodos()
        {
            Console.WriteLine("=== Lista de Clientes ===");
            var todos = _repo.Listar();
            if (todos.Count == 0)
            {
                Console.WriteLine("Nenhum cliente cadastrado.");
                return;
            }
            foreach (var c in todos)
                Console.WriteLine($"ID {c.Id}: {c.Nome} — {c.Email} / {c.Telefone}");
        } 

        public void Atualizar()
        {
            Console.Write("ID do cliente a alterar: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var c = _repo.ObterPorId(id);
            if (c == null)
            {
                Console.WriteLine("Cliente não encontrado.");
                return;
            }

            Console.Write($"Novo nome ({c.Nome}): ");
            var nome = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(nome))
                c.Nome = nome;

            Console.Write($"Nova senha (****): ");
            var senha = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(senha))
                c.Senha = senha;

            Console.Write($"Novo email ({c.Email}): ");
            var email = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(email))
                c.Email = email;

            Console.Write($"Novo telefone ({c.Telefone}): ");
            var tel = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(tel))
                c.Telefone = tel;

            Console.WriteLine("Atualizar endereço:");
            c.Endereco = _enderecoService.PedirEndereco();

            if (_repo.Alterar(c))
                Console.WriteLine("Cliente atualizado!");
            else
                Console.WriteLine("Falha ao atualizar cliente.");
        }
        public bool EhLoginValido(string nome, string senha)
        {
            return _repo.ValidarLogin(nome, senha);
        }
        public Cliente? ObterCliente(string nome, string senha)
        {
            return _repo.ObterPorNomeESenha(nome, senha);
        }
        public void Remover()
        {
            Console.Write("ID do cliente a remover: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var c = _repo.ObterPorId(id);
            if (c == null)
            {
                Console.WriteLine("Cliente não encontrado.");
                return;
            }

            if (_repo.Remover(c))
                Console.WriteLine("Cliente removido.");
            else
                Console.WriteLine("Falha ao remover cliente.");
        }
    }
}
