// ProjetoPOO/Services/ProdutoService.cs
using System;
using System.Collections.Generic;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Services
{
    public class ProdutoService
    {
        private readonly IRepositoryProduto _repo;
        private readonly IRepositoryFornecedor _fornecedorRepo;

        public ProdutoService(
            IRepositoryProduto repo,
            IRepositoryFornecedor fornecedorRepo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _fornecedorRepo = fornecedorRepo
                ?? throw new ArgumentNullException(nameof(fornecedorRepo));
            _repo.Carregar();
        }

        public void Cadastrar()
        {
            Console.WriteLine("=== Cadastro de Produto ===");
            Console.Write("Nome: ");
            var nome = Console.ReadLine()?.Trim() ?? "";
            if (nome == "") { Console.WriteLine("Nome inválido."); return; }

            Console.Write("Descrição: ");
            var desc = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Preço: ");
            if (!double.TryParse(Console.ReadLine(), out var preco))
            {
                Console.WriteLine("Preço inválido."); return;
            }

            Console.Write("Estoque inicial: ");
            if (!int.TryParse(Console.ReadLine(), out var estoque))
            {
                Console.WriteLine("Estoque inválido."); return;
            }

            // Escolher fornecedor
            Console.WriteLine("Fornecedores cadastrados:");
            var fornecedores = _fornecedorRepo.Listar();
            foreach (var f in fornecedores)
                Console.WriteLine($"ID {f.Id}: {f.Nome}");
            Console.Write("ID do fornecedor: ");
            if (!int.TryParse(Console.ReadLine(), out var fid))
            {
                Console.WriteLine("ID inválido."); return;
            }
            var fornecedor = _fornecedorRepo.ObterPorId(fid);
            if (fornecedor == null)
            {
                Console.WriteLine("Fornecedor não encontrado."); return;
            }

            var p = new Produto
            {
                Nome = nome,
                Descricao = desc,
                Preco = preco,
                Estoque = estoque,
                Fornecedor = fornecedor
            };

            if (_repo.Adicionar(p))
                Console.WriteLine("Produto cadastrado com sucesso!");
            else
                Console.WriteLine("Falha ao cadastrar produto.");
        }

        public void ListarTodos()
        {
            Console.WriteLine("=== Lista de Produtos ===");
            var todos = _repo.Listar();
            if (todos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado.");
                return;
            }
            foreach (var p in todos)
                Console.WriteLine(p);
        }

        public void Consultar()
        {
            Console.WriteLine("1 - Por ID");
            Console.WriteLine("2 - Por nome/descrição");
            Console.WriteLine("3 - Por fornecedor");
            Console.Write("Opção: ");
            switch (Console.ReadLine())
            {
                case "1": ConsultarPorId(); break;
                case "2": ConsultarPorNome(); break;
                case "3": ConsultarPorFornecedor(); break;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }

        private void ConsultarPorId()
        {
            Console.Write("ID do produto: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID inválido."); return;
            }
            var p = _repo.Listar().Find(x => x.Id == id);
            Console.WriteLine(p != null ? p.ToString() : "Não encontrado.");
        }

        private void ConsultarPorNome()
        {
            Console.Write("Termo para busca: ");
            var termo = Console.ReadLine()?.Trim() ?? "";
            var resultados = _repo.ObterPorNomeOuDescricao(termo);
            if (resultados.Count == 0)
            {
                Console.WriteLine("Nenhum produto encontrado."); return;
            }
            resultados.ForEach(p => Console.WriteLine(p));
        }

        private void ConsultarPorFornecedor()
        {
            Console.Write("ID do fornecedor: ");
            if (!int.TryParse(Console.ReadLine(), out var fid))
            {
                Console.WriteLine("ID inválido."); return;
            }
            var resultados = _repo.ObterPorFornecedor(fid);
            if (resultados.Count == 0)
            {
                Console.WriteLine("Nenhum produto para esse fornecedor."); return;
            }
            resultados.ForEach(p => Console.WriteLine(p));
        }

        public void Alterar()
        {
            Console.Write("ID do produto a alterar: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID inválido."); return;
            }
            var p = _repo.Listar().Find(x => x.Id == id);
            if (p == null)
            {
                Console.WriteLine("Produto não encontrado."); return;
            }

            Console.Write($"Novo nome ({p.Nome}): ");
            var nome = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(nome)) p.Nome = nome;

            Console.Write($"Nova descrição ({p.Descricao}): ");
            var desc = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(desc)) p.Descricao = desc;

            Console.Write($"Novo preço ({p.Preco}): ");
            if (double.TryParse(Console.ReadLine(), out var preco)) p.Preco = preco;

            Console.Write($"Novo estoque ({p.Estoque}): ");
            if (int.TryParse(Console.ReadLine(), out var est)) p.Estoque = est;

            if (_repo.Alterar(p))
                Console.WriteLine("Produto atualizado!");
            else
                Console.WriteLine("Falha ao atualizar produto.");
        }

        public void Remover()
        {
            Console.Write("ID do produto a remover: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
            {
                Console.WriteLine("ID inválido."); return;
            }
            var p = _repo.Listar().Find(x => x.Id == id);
            if (p == null)
            {
                Console.WriteLine("Produto não encontrado."); return;
            }

            if (_repo.Remover(p))
                Console.WriteLine("Produto removido.");
            else
                Console.WriteLine("Falha ao remover produto.");
        }
    }
}
