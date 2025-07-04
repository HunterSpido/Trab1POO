using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;
using System;
using System.Collections.Generic;

namespace ProjetoPOO.Services
{
    public class FornecedorService
    {
        private readonly IRepository<Fornecedor> _repositorio;
        private readonly EnderecoService _enderecoService;

        public FornecedorService(IRepository<Fornecedor> repositorio)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _enderecoService = new EnderecoService();

            try
            {
                _repositorio.Carregar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar fornecedores: {ex.Message}");
            }
        }

        public void Adicionar()
        {
            try
            {
                Console.Write("Digite o nome: ");
                string nome = Console.ReadLine() ?? "";
                Console.Write("Digite a descrição: ");
                string descricao = Console.ReadLine() ?? "";
                Console.Write("Digite o telefone: ");
                string telefone = Console.ReadLine() ?? "";
                Console.Write("Digite o email: ");
                string email = Console.ReadLine() ?? "";
                var endereco = _enderecoService.PedirEndereco();

                var fornecedor = new Fornecedor
                {
                    Nome = nome,
                    Descricao = descricao,
                    Telefone = telefone,
                    Email = email,
                    Endereco = endereco,
                };

                if (_repositorio.Adicionar(fornecedor))
                {
                    _repositorio.Salvar();
                    Console.WriteLine("Fornecedor adicionado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Erro ao adicionar fornecedor.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no cadastro: {ex.Message}");
            }
        }

        public void Alterar()
        {
            try
            {
                Console.Write("Digite o ID do fornecedor a alterar: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("ID inválido.");
                    return;
                }

                var fornecedor = _repositorio.Listar().Find(f => f.IdFornecedor == id);
                if (fornecedor == null)
                {
                    Console.WriteLine("Fornecedor não encontrado.");
                    return;
                }

                Console.Write("Digite o novo nome (ou ENTER para manter): ");
                string nome = Console.ReadLine() ?? "";
                if (!string.IsNullOrWhiteSpace(nome)) fornecedor.Nome = nome;

                Console.Write("Digite a nova descrição (ou ENTER para manter): ");
                string descricao = Console.ReadLine() ?? "";
                if (!string.IsNullOrWhiteSpace(descricao)) fornecedor.Descricao = descricao;

                Console.Write("Digite o novo telefone (ou ENTER para manter): ");
                string telefone = Console.ReadLine() ?? "";
                if (!string.IsNullOrWhiteSpace(telefone)) fornecedor.Telefone = telefone;

                Console.Write("Digite o novo email (ou ENTER para manter): ");
                string email = Console.ReadLine() ?? "";
                if (!string.IsNullOrWhiteSpace(email)) fornecedor.Email = email;

                Console.WriteLine("Informe o novo endereço:");
                fornecedor.Endereco = _enderecoService.PedirEndereco();

                if (_repositorio.Alterar(fornecedor))
                {
                    _repositorio.Salvar();
                    Console.WriteLine("Fornecedor alterado com sucesso!");
                }
                else
                {
                    Console.WriteLine("Erro ao alterar fornecedor.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na alteração: {ex.Message}");
            }
        }

        public void Excluir()
        {
            try
            {
                Console.Write("Digite o ID do fornecedor a excluir: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("ID inválido.");
                    return;
                }

                var fornecedor = _repositorio.Listar().Find(f => f.IdFornecedor == id);
                if (fornecedor == null)
                {
                    Console.WriteLine("Fornecedor não encontrado.");
                    return;
                }

                if (_repositorio.Remover(fornecedor))
                {
                    _repositorio.Salvar();
                    Console.WriteLine("Fornecedor excluído com sucesso!");
                }
                else
                {
                    Console.WriteLine("Erro ao excluir fornecedor.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na exclusão: {ex.Message}");
            }
        }

        public void Consultar()
        {
            Console.WriteLine("Consulta por:");
            Console.WriteLine("1 - ID");
            Console.WriteLine("2 - Nome (parcial)");
            Console.Write("Escolha: ");
            string opcao = Console.ReadLine() ?? "";

            switch (opcao)
            {
                case "1": ConsultarPorId(); break;
                case "2": ConsultarPorNome(); break;
                default: Console.WriteLine("Opção inválida."); break;
            }
        }

        public void ConsultarPorId()
        {
            Console.Write("Digite o ID do fornecedor: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            var fornecedor = _repositorio.Listar().Find(f => f.IdFornecedor == id);
            if (fornecedor != null)
                Exibir(fornecedor);
            else
                Console.WriteLine("Fornecedor não encontrado.");
        }

        public void ConsultarPorNome()
        {
            Console.Write("Digite parte do nome para busca: ");
            string termo = (Console.ReadLine() ?? "").ToLower();

            var resultados = _repositorio.Listar()
                .FindAll(f => f.Nome.ToLower().Contains(termo));

            if (resultados.Count == 0)
            {
                Console.WriteLine("Nenhum fornecedor encontrado.");
                return;
            }

            foreach (var f in resultados)
            {
                Exibir(f);
                Console.WriteLine("----------------------------");
            }
        }

        private void Exibir(Fornecedor f)
        {
            Console.WriteLine($"ID: {f.IdFornecedor}");
            Console.WriteLine($"Nome: {f.Nome}");
            Console.WriteLine($"Descrição: {f.Descricao}");
            Console.WriteLine($"Telefone: {f.Telefone}");
            Console.WriteLine($"Email: {f.Email}");
            Console.WriteLine("Endereço:");
            Console.WriteLine($"  Estado: {f.Endereco.Estado}");
            Console.WriteLine($"  Cidade: {f.Endereco.Cidade}");
            Console.WriteLine($"  Bairro: {f.Endereco.Bairro}");
            Console.WriteLine($"  Rua: {f.Endereco.Rua}");
            Console.WriteLine($"  Número: {f.Endereco.Numero}");
            Console.WriteLine($"  CEP: {f.Endereco.Cep}");
        }
        public List<Fornecedor> GetLista()
        {
            return _repositorio.Listar();
        }
        // Se você quiser acessar de fora, ex:
        public Fornecedor? GetFornecedor(int id) => _repositorio.Listar().Find(f => f.IdFornecedor == id);
        public int GetQuantidade() => _repositorio.Listar().Count;
    }
}
