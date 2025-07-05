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
    // adiciona construct e 


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
                    ConsultarPedidos();
                    break;
                case "2":
                    return;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    private void ConsultarPedidos()
    {
        Console.WriteLine("\n== CONSULTA DE PEDIDOS ==");
        Console.WriteLine("1 - Consultar por número");
        Console.WriteLine("2 - Consultar por intervalo de datas");
        Console.Write("Opção: ");
        string op = Console.ReadLine() ?? "";

        List<Pedido> pedidos;

        if (op == "1")
        {
            Console.Write("Número do pedido: ");
            int num = int.Parse(Console.ReadLine()!);
            var pedido = _pedidoService.ObterPorId(num);
            if (pedido != null)
            {
                ExibirMestreDetalhe(pedido);
                AlterarStatusPedido(pedido);
            }
            else
                Console.WriteLine("Pedido não encontrado.");
        }
        else if (op == "2")
        {
            Console.Write("Data inicial (yyyy-mm-dd): ");
            DateTime d1 = DateTime.Parse(Console.ReadLine()!);
            Console.Write("Data final (yyyy-mm-dd): ");
            DateTime d2 = DateTime.Parse(Console.ReadLine()!);
            pedidos = _pedidoService.ObterPorData(d1, d2);

            if (!pedidos.Any())
            {
                Console.WriteLine("Nenhum pedido nesse intervalo.");
                return;
            }
            foreach (var pedido in pedidos)
            {
                ExibirMestreDetalhe(pedido);
                AlterarStatusPedido(pedido);
                Console.WriteLine("-------------------------");
            }
        }
    }

    private void ExibirMestreDetalhe(Pedido pedido)
    {
        Console.WriteLine($"\nPedido Nº: {pedido.Id}");
        Console.WriteLine($"Cliente: {pedido.Cliente.Nome}");
        Console.WriteLine($"Data: {pedido.Data:dd/MM/yyyy HH:mm}");
        Console.WriteLine($"Status: {pedido.Status}");
        Console.WriteLine($"Valor Total: {pedido.ValorTotal:C}");
        if (pedido.Transportadora != null)
            Console.WriteLine($"Transportadora: {pedido.Transportadora.Nome} | Frete: {pedido.ValorFrete:C}");

        Console.WriteLine("Itens do Pedido:");
        foreach (var item in pedido.Itens)
        {
            // Busca o produto atualizado pelo ID
            var produto = _produtoService.ObterPorId(item.ProdutoId);
            if (produto != null)
            {
                Console.WriteLine($"\tProduto: {produto.Nome}");
                Console.WriteLine($"\tDescrição: {produto.Descricao}");
                Console.WriteLine($"\tQtd: {item.Quantidade} | Unitário: {item.PrecoUnitario:C} | Total: {item.PrecoTotal:C}");
                Console.WriteLine("\t----------------------");
            }
            else
            {
                Console.WriteLine($"\tProduto removido ou não encontrado (ID: {item.ProdutoId})");
            }
        }
        if (pedido.Status == "Enviado" && pedido.DataEnvio != null)
            Console.WriteLine($"Data de envio: {pedido.DataEnvio:dd/MM/yyyy}");
        if (pedido.Status == "Cancelado" && pedido.DataCancelamento != null)
            Console.WriteLine($"Data de cancelamento: {pedido.DataCancelamento:dd/MM/yyyy}");
    }
    
    private void AlterarStatusPedido(Pedido pedido)
    {
        Console.WriteLine("\nDeseja alterar o status deste pedido?");
        Console.WriteLine("1 - Marcar como ENVIADO");
        Console.WriteLine("2 - Marcar como CANCELADO");
        Console.WriteLine("3 - Não alterar");
        Console.Write("Opção: ");
        string op = Console.ReadLine() ?? "";

        if (op == "1")
        {
            bool ok = _pedidoService.AlterarStatus(pedido.Id, "Enviado", dataEnvio: DateTime.Now);
            if (ok)
                Console.WriteLine("Status alterado para ENVIADO!");
            else
                Console.WriteLine("Falha ao alterar status.");
        }
        else if (op == "2")
        {
            bool ok = _pedidoService.AlterarStatus(pedido.Id, "Cancelado", dataCancelamento: DateTime.Now);
            if (ok)
                Console.WriteLine("Status alterado para CANCELADO!");
            else
                Console.WriteLine("Falha ao alterar status.");
        }
        else
        {
            Console.WriteLine("Não alterado.");
        }
    }

}