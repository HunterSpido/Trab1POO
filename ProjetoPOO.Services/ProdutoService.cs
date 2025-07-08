using System;
using System.Collections.Generic;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;
using ProjetoPOO.Services.Exceptions;

namespace ProjetoPOO.Services
{
    public class ProdutoService
    {
        private readonly IRepositoryProduto _repo;
        private readonly IRepositoryFornecedor _fornecedorRepo;

        public ProdutoService(IRepositoryProduto repo, IRepositoryFornecedor fornecedorRepo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _fornecedorRepo = fornecedorRepo ?? throw new ArgumentNullException(nameof(fornecedorRepo));
            _repo.Carregar();
        }

        public void Cadastrar()
        {
            Console.WriteLine("=== Cadastro de Produto ===");

            Console.Write("Nome: ");
            var nome = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(nome))
                throw new ExcecaoEntradaInvalida("Nome do produto não pode ser vazio.");

            Console.Write("Descrição: ");
            var desc = Console.ReadLine()?.Trim() ?? "";

            Console.Write("Preço: ");
            if (!double.TryParse(Console.ReadLine(), out var preco) || preco < 0)
                throw new ExcecaoEntradaInvalida("Preço inválido.");

            Console.Write("Estoque inicial: ");
            if (!int.TryParse(Console.ReadLine(), out var estoque) || estoque < 0)
                throw new ExcecaoEntradaInvalida("Estoque inválido.");

            Console.WriteLine("Fornecedores cadastrados:");
            var fornecedores = _fornecedorRepo.Listar();
            foreach (var f in fornecedores)
                Console.WriteLine($"ID {f.Id}: {f.Nome}");

            Console.Write("ID do fornecedor: ");
            if (!int.TryParse(Console.ReadLine(), out var fid))
                throw new ExcecaoEntradaInvalida("ID do fornecedor inválido.");

            var fornecedor = _fornecedorRepo.ObterPorId(fid);
            if (fornecedor == null)
                throw new ExcecaoEntidadeNaoEncontrada("Fornecedor não encontrado.");

            var p = new Produto
            {
                Nome = nome,
                Descricao = desc,
                Preco = preco,
                Estoque = estoque,
                Fornecedor = fornecedor
            };

            if (!_repo.Adicionar(p))
                throw new Exception("Falha ao cadastrar produto.");

            Console.WriteLine("Produto cadastrado com sucesso!");
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
            string op = Console.ReadLine() ?? throw new ExcecaoEntradaInvalida("Opção inválida.");

            switch (op)
            {
                case "1": ConsultarPorId(); break;
                case "2": ConsultarPorNome(); break;
                case "3": ConsultarPorFornecedor(); break;
                default: throw new ExcecaoEntradaInvalida("Opção inválida.");
            }
        }

        private void ConsultarPorId()
        {
            Console.Write("ID do produto: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
                throw new ExcecaoEntradaInvalida("ID inválido.");

            var p = _repo.Listar().Find(x => x.Id == id);
            if (p == null)
                throw new ExcecaoEntidadeNaoEncontrada("Produto não encontrado.");

            Console.WriteLine(p);
        }

        private void ConsultarPorNome()
        {
            Console.Write("Termo para busca: ");
            var termo = Console.ReadLine()?.Trim() ?? "";
            var resultados = _repo.ObterPorNomeOuDescricao(termo);

            if (resultados.Count == 0)
                throw new ExcecaoEntidadeNaoEncontrada("Nenhum produto encontrado.");

            resultados.ForEach(p => Console.WriteLine(p));
        }

        private void ConsultarPorFornecedor()
        {
            Console.Write("ID do fornecedor: ");
            if (!int.TryParse(Console.ReadLine(), out var fid))
                throw new ExcecaoEntradaInvalida("ID inválido.");

            var resultados = _repo.ObterPorFornecedor(fid);
            if (resultados.Count == 0)
                throw new ExcecaoEntidadeNaoEncontrada("Nenhum produto para esse fornecedor.");

            resultados.ForEach(p => Console.WriteLine(p));
        }

        public void Alterar()
        {
            Console.Write("ID do produto a alterar: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
                throw new ExcecaoEntradaInvalida("ID inválido.");

            var p = _repo.Listar().Find(x => x.Id == id);
            if (p == null)
                throw new ExcecaoEntidadeNaoEncontrada("Produto não encontrado.");

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

            if (!_repo.Alterar(p))
                throw new Exception("Falha ao atualizar produto.");

            Console.WriteLine("Produto atualizado!");
        }

        public void Remover()
        {
            Console.Write("ID do produto a remover: ");
            if (!int.TryParse(Console.ReadLine(), out var id))
                throw new ExcecaoEntradaInvalida("ID inválido.");

            var p = _repo.Listar().Find(x => x.Id == id);
            if (p == null)
                throw new ExcecaoEntidadeNaoEncontrada("Produto não encontrado.");

            if (!_repo.Remover(p))
                throw new Exception("Falha ao remover produto.");

            Console.WriteLine("Produto removido.");
        }

        public Produto ObterPorId(int id)
        {
            var produto = _repo.Listar().Find(x => x.Id == id);
            if (produto == null)
                throw new ExcecaoEntidadeNaoEncontrada($"Produto com ID {id} não encontrado.");
            return produto;
        }

        public bool Atualizar(Produto produto)
        {
            return _repo.Alterar(produto);
        }
    }
}
