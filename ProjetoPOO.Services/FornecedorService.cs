using System;
using System.Collections.Generic;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Services
{
    public class FornecedorService
    {
        private readonly IRepository<Fornecedor> _repo;
        private readonly EnderecoService _enderecoService;

        // Injeção de dependências via construtor
        public FornecedorService(IRepository<Fornecedor> repositorio, EnderecoService enderecoService)
        {
            _repo = repositorio;
            _enderecoService = enderecoService;

            // Carrega do arquivo ao iniciar
            _repo.Carregar();
        }

        // 1) CADASTRAR
        public void Cadastrar()
        {
            Console.WriteLine("=== Cadastro de Fornecedor ===");
            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? "";

            Console.Write("Descrição: ");
            string desc = Console.ReadLine() ?? "";

            // Pede endereço usando o seu serviço de endereço
            Endereco end = _enderecoService.PedirEndereco();

            // Monta o objeto
            var f = new Fornecedor
            {
                Nome = nome,
                Descricao = desc,
                Endereco = end
            };

            // Adiciona e salva
            bool ok = _repo.Adicionar(f);
            if (ok)
            {
                _repo.Salvar();
                Console.WriteLine("Fornecedor cadastrado com sucesso!");
            }
            else
            {
                Console.WriteLine("Falha ao cadastrar fornecedor.");
            }
        }

        // 2) ALTERAR
        public void Alterar()
        {
            Console.WriteLine("=== Alteração de Fornecedor ===");
            Console.Write("ID do fornecedor: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            // Busca manualmente na lista
            Fornecedor? alvo = null;
            foreach (var x in _repo.Listar())
                if (x.IdFornecedor == id)
                {
                    alvo = x;
                    break;
                }

            if (alvo == null)
            {
                Console.WriteLine("Fornecedor não encontrado.");
                return;
            }

            // Pergunta campos novos (ENTER para manter)
            Console.Write($"Novo nome ({alvo.Nome}): ");
            string nome = Console.ReadLine() ?? "";
            if (nome != "") alvo.Nome = nome;

            Console.Write($"Nova descrição ({alvo.Descricao}): ");
            string desc = Console.ReadLine() ?? "";
            if (desc != "") alvo.Descricao = desc;

            // Atualiza endereço inteiro
            Console.WriteLine("Atualizar endereço:");
            alvo.Endereco = _enderecoService.PedirEndereco();

            // Chama repositório e salva
            //bool ok = _repo.Atualizar(alvo);
            //if (ok)
            //{
            //    _repo.Salvar();
            //    Console.WriteLine("Fornecedor alterado com sucesso!");
            //}
            //else
            //{
            //    Console.WriteLine("Falha ao alterar fornecedor.");
            //}
        }

        // 3) EXCLUIR
        public void Excluir()
        {
            //Console.WriteLine("=== Exclusão de Fornecedor ===");
            //Console.Write("ID do fornecedor: ");
            //if (!int.TryParse(Console.ReadLine(), out int id))
            //{
            //    Console.WriteLine("ID inválido.");
            //    return;
            //}

            // Remove pelo ID
        //    bool ok = _repo.Remover(id);
        //    if (ok)
        //    {
        //        _repo.Salvar();
        //        Console.WriteLine("Fornecedor excluído.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Falha ao excluir (ID não encontrado).");
        //    }
        }

        // 4) CONSULTAR (menu interno)
        public void Consultar()
        {
            Console.WriteLine("=== Consultar Fornecedores ===");
            Console.WriteLine("1) Por ID");
            Console.WriteLine("2) Por nome");
            Console.Write("Opção: ");
            string op = Console.ReadLine() ?? "";

            if (op == "1") ConsultarPorId();
            else if (op == "2") ConsultarPorNome();
            else Console.WriteLine("Opção inválida.");
        }

        private void ConsultarPorId()
        {
            Console.Write("Digite o ID: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            //Fornecedor? f = _repo.ConsultarPorId(id);
            //if (f == null)
            //{
            //    Console.WriteLine("Não encontrado.");
            //}
            //else
            //{
            //    Exibir(f);
            //}
        }

        private void ConsultarPorNome()
        {
            Console.Write("Digite parte do nome: ");
            string termo = (Console.ReadLine() ?? "").ToLower();

            var achados = new List<Fornecedor>();
            foreach (var f in _repo.Listar())
                if (f.Nome.ToLower().Contains(termo))
                    achados.Add(f);

            if (achados.Count == 0)
            {
                Console.WriteLine("Nenhum fornecedor encontrado.");
                return;
            }

            foreach (var f in achados)
            {
                Exibir(f);
                Console.WriteLine("--------------------------");
            }
        }

        // Mostra todos os campos de um Fornecedor
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

        // Lista todos os fornecedores (para menus externos)
        public List<Fornecedor> GetTodos() => _repo.Listar();
    }
}
