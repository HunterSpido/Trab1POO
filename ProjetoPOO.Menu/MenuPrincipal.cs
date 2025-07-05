using System;
using ProjetoPOO.Menu;
using ProjetoPOO.Services;

namespace ProjetoPOO.Menu // ou o namespace que você definiu
{
    public class MenuPrincipal
    {
        private readonly ClienteService _clienteService;
        private readonly FornecedorService _fornecedorService;

        private readonly TransportadoraService _transportadoraService;
        private readonly ProdutoService _produtoService;

        public MenuPrincipal(ClienteService clienteService, FornecedorService fornecedorService, TransportadoraService transportadoraService, ProdutoService produtoService)
        {
            _clienteService = clienteService ?? throw new ArgumentNullException(nameof(clienteService));
            _fornecedorService = fornecedorService ?? throw new ArgumentNullException(nameof(fornecedorService));
            _transportadoraService = transportadoraService ?? throw new ArgumentNullException(nameof(transportadoraService));
            _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));

        }

        public void TelaLogin()
        {
            while (true)
            {
                Console.WriteLine("\n=== TELA DE LOGIN ===");
                Console.WriteLine("1 - Fazer login");
                Console.WriteLine("2 - Cadastrar novo usuário");
                Console.WriteLine("3 - Sair");
                Console.Write("Escolha: ");
                if (!int.TryParse(Console.ReadLine(), out int opcao))
                {
                    Console.WriteLine("Opção inválida.");
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        FazerLogin();
                        break;
                    case 2:
                        _clienteService.Cadastrar();
                        break;
                    case 3:
                        Console.WriteLine("Saindo...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }
            }
        }

        private void FazerLogin()
        {
            Console.Write("Login: ");
            var nome = Console.ReadLine() ?? "";
            Console.Write("Senha: ");
            var senha = Console.ReadLine() ?? "";

            if (nome == "admin" && senha == "admin")
            {
                Console.WriteLine("Bem-vindo, admin!");
                new AdminMenu(_fornecedorService,_transportadoraService,_produtoService).MenuAdmin();
                return;
            }

            if (_clienteService.EhLoginValido(nome, senha))
            {
                Console.WriteLine("Login efetuado com sucesso!");
                var cliente = _clienteService.ObterCliente(nome, senha)!;
                new UsuarioNormalMenu(cliente, _fornecedorService, _transportadoraService,_produtoService).MenuUsuario(); 
            }
            else
            {
                Console.WriteLine("Login ou senha incorretos.");
            }
        }
    }
}
