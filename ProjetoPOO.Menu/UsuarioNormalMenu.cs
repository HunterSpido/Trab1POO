using System;
using System.Collections.Generic;
using ProjetoPOO.Models;
using ProjetoPOO.Services;
using ProjetoPOO.Services.Exceptions;

namespace ProjetoPOO.Menu
{
    public class UsuarioNormalMenu
    {
        private readonly Cliente _clienteLogado;
        private readonly FornecedorService _fornecedorService;
        private readonly TransportadoraService _transportadoraService;
        private readonly ProdutoService _produtoService;
        private readonly PedidoService _pedidoService;
        private readonly CarrinhoService _carrinhoService;

        public UsuarioNormalMenu(
            Cliente cliente,
            FornecedorService fornecedorService,
            TransportadoraService transportadoraService,
            ProdutoService produtoService,
            PedidoService pedidoService)
        {
            _clienteLogado = cliente ?? throw new ArgumentNullException(nameof(cliente));
            _fornecedorService = fornecedorService ?? throw new ArgumentNullException(nameof(fornecedorService));
            _transportadoraService = transportadoraService ?? throw new ArgumentNullException(nameof(transportadoraService));
            _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));
            _pedidoService = pedidoService ?? throw new ArgumentNullException(nameof(pedidoService));
            _carrinhoService = new CarrinhoService(); // cada usuário tem seu carrinho
        }

        public void MenuUsuario()
        {
            int opcao;
            do
            {
                Console.WriteLine("\n== MENU DO USUÁRIO ==");
                Console.WriteLine("1 - Consultar Produtos");
                Console.WriteLine("2 - Adicionar Produto ao Carrinho");
                Console.WriteLine("3 - Visualizar Carrinho");
                Console.WriteLine("4 - Finalizar Pedido");
                Console.WriteLine("5 - Consultar Meus Pedidos");
                Console.WriteLine("6 - Sair");
                Console.Write("Digite a opção: ");

                string entrada = Console.ReadLine() ?? "";
                if (!int.TryParse(entrada, out opcao))
                {
                    Console.WriteLine("Opção inválida.");
                    continue;
                }

                try
                {
                    switch (opcao)
                    {
                        case 1:
                            _produtoService.Consultar();
                            break;
                        case 2:
                            _carrinhoService.AdicionarProdutoViaConsole(_produtoService);
                            break;
                        case 3:
                            _carrinhoService.Visualizar();
                            break;
                        case 4:
                            _carrinhoService.FinalizarPedidoViaConsole(
                                _clienteLogado,
                                _produtoService,
                                _transportadoraService,
                                _pedidoService
                            );
                            break;
                        case 5:
                            _pedidoService.ConsultarPedidosDoCliente(_clienteLogado, _produtoService);
                            break;
                        case 6:
                            Console.WriteLine("Saindo...");
                            break;
                        default:
                            Console.WriteLine("Digite uma opção entre 1 e 6.");
                            break;
                    }
                }
                catch (ExcecaoEntradaInvalida ex)
                {
                    Console.WriteLine($"[Erro de entrada] {ex.Message}");
                }
                catch (ExcecaoEntidadeNaoEncontrada ex)
                {
                    Console.WriteLine($"[Entidade não encontrada] {ex.Message}");
                }
                catch (ExcecaoEstoqueInsuficiente ex)
                {
                    Console.WriteLine($"[Estoque insuficiente] {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Erro inesperado] {ex.Message}");
                }

            } while (opcao != 6);
        }
    }
}
