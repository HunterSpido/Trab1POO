using System;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.ClienteRepository;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Services
{
    public class ClienteService
    {
        private readonly EnderecoService _enderecoService;
        private readonly RepositorioBaseCliente _repositorioCliente;

        public ClienteService(IRepositoryClientes repositoryClientes, EnderecoService enderecoService)
        {
            _enderecoService = enderecoService ?? throw new ArgumentNullException(nameof(enderecoService));
            _repositorioCliente = new RepositorioBaseCliente(repositoryClientes ?? throw new ArgumentNullException(nameof(repositoryClientes)));

            // Tenta carregar os dados existentes ao iniciar o serviço
            try
            {
                _repositorioCliente.Carregar();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar os dados: {ex.Message}");
            }
        }

        public void CadastrarUsuario()
        {
            try
            {
                Console.Write("Digite o nome: ");
                string? nome = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(nome))
                {
                    Console.WriteLine("Nome inválido.");
                    return;
                }

                Console.Write("Digite a senha: ");
                string? senha = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(senha))
                {
                    Console.WriteLine("Senha inválida.");
                    return;
                }

                Endereco endereco = _enderecoService.PedirEndereco();

                Console.Write("Digite o telefone: ");
                string telefone = Console.ReadLine() ?? "";

                Console.Write("Digite o email: ");
                string email = Console.ReadLine() ?? "";

                // Verifica se o cliente já existe
                if (_repositorioCliente.ConsultarPorNomeESenha(nome, senha) != null)
                {
                    Console.WriteLine("Erro: Cliente já cadastrado!");
                    return;
                }

                var novoCliente = new Cliente
                {
                    Nome = nome,
                    Senha = senha,
                    Email = email,
                    Telefone = telefone,
                    Endereco = endereco
                };

                if (_repositorioCliente.AdicionarCliente(novoCliente))
                {
                    try
                    {
                        _repositorioCliente.Salvar();
                        Console.WriteLine("Cliente cadastrado com sucesso!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao salvar os dados do cliente: {ex.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Erro ao cadastrar cliente!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no cadastro: {ex.Message}");
            }
        }

        public bool ValidarLogin(string nome, string senha)
        {
            if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(senha))
            {
                return false;
            }

            try
            {
                return _repositorioCliente.ValidarLogin(nome, senha);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro na validação do login: {ex.Message}");
                return false;
            }
        }

        public Cliente? ObterCliente(string nome, string senha)
        {
            try
            {
                return _repositorioCliente.ConsultarPorNomeESenha(nome, senha);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter cliente: {ex.Message}");
                return null;
            }
        }

        public void ListarTodosClientes()
        {
            try
            {
                var clientes = _repositorioCliente.Listar();
                if (clientes == null || clientes.Count == 0)
                {
                    Console.WriteLine("Nenhum cliente cadastrado.");
                    return;
                }

                Console.WriteLine("\nLista de Clientes Cadastrados:");
                foreach (var cliente in clientes)
                {
                    Console.WriteLine($"- Nome: {cliente.Nome}, Email: {cliente.Email}, Telefone: {cliente.Telefone}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar clientes: {ex.Message}");
            }
        }

        public void ListarPedidosClientes()
        {
            try
            {
                _repositorioCliente.ListarPedidos();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar pedidos dos clientes: {ex.Message}");
            }
        }
    }
}
