using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;
using ProjetoPOO.Services.Exceptions;

namespace ProjetoPOO.Services
{
    public class FornecedorService
    {
        private readonly IRepositoryFornecedor _repo;
        private readonly EnderecoService _enderecoService;

        public FornecedorService(IRepositoryFornecedor repositorio, EnderecoService enderecoService)
        {
            _repo = repositorio;
            _enderecoService = enderecoService;
            _repo.Carregar();
        }

        public void Cadastrar()
        {
            Console.WriteLine("=== Cadastro de Fornecedor ===");
            Console.Write("Nome: ");
            string nome = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrWhiteSpace(nome))
                throw new ExcecaoEntradaInvalida("Nome não pode ser vazio.");

            Console.Write("Descrição: ");
            string desc = Console.ReadLine() ?? "";

            Endereco end = _enderecoService.PedirEndereco();

            var f = new Fornecedor
            {
                Nome = nome,
                Descricao = desc,
                Endereco = end
            };

            if (!_repo.Adicionar(f))
                throw new Exception("Falha ao cadastrar fornecedor.");

            _repo.Salvar();
            Console.WriteLine("Fornecedor cadastrado com sucesso!");
        }

        public void Alterar()
        {
            Console.WriteLine("=== Alteração de Fornecedor ===");
            Console.Write("ID do fornecedor: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
                throw new ExcecaoEntradaInvalida("ID inválido.");

            var alvo = _repo.Listar().FirstOrDefault(f => f.IdFornecedor == id);
            if (alvo == null)
                throw new ExcecaoEntidadeNaoEncontrada("Fornecedor não encontrado.");

            Console.Write($"Novo nome ({alvo.Nome}): ");
            string nome = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(nome)) alvo.Nome = nome;

            Console.Write($"Nova descrição ({alvo.Descricao}): ");
            string desc = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(desc)) alvo.Descricao = desc;

            Console.WriteLine("Atualizar endereço:");
            alvo.Endereco = _enderecoService.PedirEndereco();

            if (!_repo.Alterar(alvo))
                throw new Exception("Falha ao alterar fornecedor.");

            _repo.Salvar();
            Console.WriteLine("Fornecedor alterado com sucesso!");
        }

        public void Excluir()
        {
            Console.WriteLine("=== Exclusão de Fornecedor ===");
            Console.Write("ID do fornecedor: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
                throw new ExcecaoEntradaInvalida("ID inválido.");

            var f = _repo.Listar().FirstOrDefault(f => f.IdFornecedor == id);
            if (f == null)
                throw new ExcecaoEntidadeNaoEncontrada("Fornecedor não encontrado.");

            if (!_repo.Remover(f))
                throw new Exception("Falha ao excluir fornecedor.");

            _repo.Salvar();
            Console.WriteLine("Fornecedor excluído com sucesso!");
        }

        public void Consultar()
        {
            Console.WriteLine("=== Consultar Fornecedores ===");
            Console.WriteLine("1) Por ID");
            Console.WriteLine("2) Por nome");
            Console.Write("Opção: ");
            string op = Console.ReadLine() ?? throw new ExcecaoEntradaInvalida("Opção inválida.");

            if (op == "1") ConsultarPorId();
            else if (op == "2") ConsultarPorNome();
            else throw new ExcecaoEntradaInvalida("Opção inválida.");
        }

        private void ConsultarPorId()
        {
            Console.Write("Digite o ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
                throw new ExcecaoEntradaInvalida("ID inválido.");

            var f = _repo.Listar().FirstOrDefault(f => f.IdFornecedor == id);
            if (f == null)
                throw new ExcecaoEntidadeNaoEncontrada("Fornecedor não encontrado.");

            Exibir(f);
        }

        private void ConsultarPorNome()
        {
            Console.Write("Digite parte do nome: ");
            string termo = (Console.ReadLine() ?? "").ToLower();

            var achados = _repo.Listar()
                .Where(f => f.Nome.ToLower().Contains(termo))
                .ToList();

            if (achados.Count == 0)
                throw new ExcecaoEntidadeNaoEncontrada("Nenhum fornecedor encontrado com esse nome.");

            foreach (var f in achados)
            {
                Exibir(f);
                Console.WriteLine("--------------------------");
            }
        }

        private void Exibir(Fornecedor f)
        {
            Console.WriteLine($"ID:        {f.IdFornecedor}");
            Console.WriteLine($"Nome:      {f.Nome}");
            Console.WriteLine($"Descrição: {f.Descricao}");
            Console.WriteLine("Endereço:");
            Console.WriteLine($"  Rua:    {f.Endereco.Rua}, {f.Endereco.Numero}");
            Console.WriteLine($"  Bairro: {f.Endereco.Bairro}");
            Console.WriteLine($"  Cidade: {f.Endereco.Cidade} - {f.Endereco.Estado}");
            Console.WriteLine($"  CEP:    {f.Endereco.Cep}");
        }

        public List<Fornecedor> GetTodos() => _repo.Listar();
    }
}
