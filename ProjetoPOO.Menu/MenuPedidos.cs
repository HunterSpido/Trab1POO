namespace ProjetoPOO.Menu;

using ProjetoPOO.Models;
using ProjetoPOO.Services;
using System;

public class MenuPedidos
{
    private readonly PedidoService _pedidoService;
    private readonly ProdutoService _produtoService;

    public MenuPedidos(PedidoService pedidoService, ProdutoService produtoService)
    {
        _pedidoService = pedidoService ?? throw new ArgumentNullException(nameof(pedidoService));
        _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));
    }

    public void MenuPedidosAdm()
    {
        while (true)
        {
            Console.WriteLine("\n== MENU PEDIDOS ADMIN ==");
            Console.WriteLine("1 - Consultar pedidos");
            Console.WriteLine("2 - Voltar");
            Console.Write("Opção: ");
            string op = Console.ReadLine() ?? "";

            switch (op)
            {
                case "1":
                    _pedidoService.ConsultarPedidosViaConsole(_produtoService);
                    break;
                case "2":
                    return;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}
