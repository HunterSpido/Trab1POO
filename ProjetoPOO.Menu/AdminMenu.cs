namespace ProjetoPOO.Menu;

using ProjetoPOO.Models;
using ProjetoPOO.Services;
using ProjetoPOO.Services.Exceptions;
using System;

public class AdminMenu
{
    private readonly FornecedorService _fornecedorService;
    private readonly TransportadoraService _transportadoraService;
    private readonly ProdutoService _produtoService;
    private readonly PedidoService _pedidoService;

    public AdminMenu(FornecedorService fornecedorService, TransportadoraService transportadoraService, ProdutoService produtoService, PedidoService pedidoService)
    {
        _fornecedorService = fornecedorService ?? throw new ArgumentNullException(nameof(fornecedorService));
        _transportadoraService = transportadoraService ?? throw new ArgumentNullException(nameof(transportadoraService));
        _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));
        _pedidoService = pedidoService ?? throw new ArgumentNullException(nameof(pedidoService));
    }

    public void MenuAdmin()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("\n=== MENU ADMINISTRADOR ===");
                Console.WriteLine("1 - Menu de Fornecedores");
                Console.WriteLine("2 - Menu de Produtos");
                Console.WriteLine("3 - Menu de Transportadoras");
                Console.WriteLine("4 - Menu de Pedidos");
                Console.WriteLine("5 - Sair");
                Console.Write("Opção: ");

                if (!int.TryParse(Console.ReadLine(), out int opcao))
                    throw new ExcecaoEntradaInvalida("Digite um número válido entre 1 e 5.");

                switch (opcao)
                {
                    case 1:
                        new MenuFornecedor(_fornecedorService).TelaFornecedor();
                        break;
                    case 2:
                        new ProdutoMenu(_produtoService).MenuProdutos();
                        break;
                    case 3:
                        new TransportadoraMenu(_transportadoraService).MenuTransportadora();
                        break;
                    case 4:
                        new MenuPedidos(_pedidoService, _produtoService).MenuPedidosAdm();
                        break;
                    case 5:
                        Console.WriteLine("Saindo do menu administrador...");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Escolha entre 1 e 5.");
                        break;
                }
            }
            catch (ExcecaoEntradaInvalida ex)
            {
                Console.WriteLine($"Erro de entrada: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }
        }
    }
}
