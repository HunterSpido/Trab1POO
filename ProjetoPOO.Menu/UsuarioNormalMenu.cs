using System;
using ProjetoPOO.Models;
using ProjetoPOO.Services;

namespace ProjetoPOO.Menu;

public class UsuarioNormalMenu
{

    private readonly Cliente _clienteLogado ;
    private readonly FornecedorService _fornecedorService;
    private readonly TransportadoraService _transportadoraService;

    private readonly ProdutoService _produtoService;

    private readonly PedidoService _pedidoService;

    private List<PedidoItem> _carrinho = new List<PedidoItem>();


    public UsuarioNormalMenu(Cliente cliente, FornecedorService fornecedorService, TransportadoraService transportadoraService, ProdutoService produtoService, PedidoService pedidoService)
    {
        _clienteLogado = cliente ?? throw new ArgumentNullException(nameof(cliente));
        _transportadoraService = transportadoraService ?? throw new ArgumentNullException(nameof(transportadoraService));
        _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));
        _fornecedorService = fornecedorService ?? throw new ArgumentNullException(nameof(fornecedorService));
        _pedidoService = pedidoService ?? throw new ArgumentNullException(nameof(pedidoService));

    }

    private readonly CarrinhoService _carrinhoService = new CarrinhoService();

    private void AdicionarProdutoAoCarrinho()
    {
        // Exemplo básico:
        Console.Write("Digite o código do produto: ");
        int id = int.Parse(Console.ReadLine()!);
        var produto = _produtoService.ObterPorId(id);

        if (produto == null || produto.Estoque <= 0)
        {
            Console.WriteLine("Produto não encontrado ou sem estoque!");
            return;
        }

        Console.Write("Quantidade: ");
        int qtd = int.Parse(Console.ReadLine()!);
        if (qtd > produto.Estoque)
        {
            Console.WriteLine($"Só há {produto.Estoque} em estoque.");
            return;
        }

        _carrinhoService.AdicionarProduto(produto, qtd);
        Console.WriteLine("Produto adicionado ao carrinho!");
    }

    private void VisualizarCarrinho()
    {
        var itens = _carrinhoService.Itens;
        if (!itens.Any())
        {
            Console.WriteLine("Carrinho vazio!");
            return;
        }
        foreach (var item in itens)
            Console.WriteLine($"Produto: {item.Produto.Nome}, Qtd: {item.Quantidade}, Total: {item.PrecoTotal:C}");
        Console.WriteLine($"Total do carrinho: {_carrinhoService.CalcularTotal():C}");
    }

    private void FinalizarCarrinho()
    {
        var itens = _carrinhoService.Itens.ToList();
        if (!itens.Any())
        {
            Console.WriteLine("Carrinho vazio!");
            return;
        }

        // Selecionar transportadora (exemplo simples, ajuste conforme sua modelagem)
        Console.WriteLine("Determine a distancia");
        double distancia = double.Parse(Console.ReadLine()!);

        // Selecionar transportadora
        Console.WriteLine("Escolha uma transportadora:");
        var transportadoras = _transportadoraService.GetTodos();
        for (int i = 0; i < transportadoras.Count; i++)
            Console.WriteLine($"{i + 1} - {transportadoras[i].Nome} (Preço por km: {transportadoras[i].PrecoPorKm:C})");
        Console.Write("Opção: ");
        int idxT = int.Parse(Console.ReadLine()!);
        var transportadora = transportadoras[idxT - 1];

        // Calcula total do frete usando PrecoPorKm * distancia
        decimal valorFrete = (decimal)transportadora.PrecoPorKm * (decimal)distancia;
        decimal totalItens = itens.Sum(x => x.PrecoTotal);
        decimal valorTotal = totalItens + valorFrete;   

        Console.WriteLine($"Total dos itens: {totalItens:C}");
        Console.WriteLine($"Frete: {valorFrete:C}");
        Console.WriteLine($"Total do pedido: {valorTotal:C}");
        Console.Write("Confirmar pedido? (s/n): ");
        var confirma = Console.ReadLine() ?? "n";
        if (confirma.ToLower() != "s")
        {
            Console.WriteLine("Pedido cancelado.");
            return;
        }

        // Atualiza estoque
        foreach (var item in itens)
        {
            var produto = _produtoService.ObterPorId(item.Produto.Id);
            if (produto != null)
            {
                produto.Estoque -= item.Quantidade;
                _produtoService.Atualizar(produto);
            }
        }

        // Cria e salva o pedido
        var pedido = new Pedido
        {
            Cliente = _clienteLogado,
            Data = DateTime.Now,
            Itens = itens,
            ValorTotal = valorTotal,
            Transportadora = transportadora,
            ValorFrete = valorFrete
        };
        _pedidoService.RealizarPedido(pedido);

        _carrinhoService.Limpar();
        Console.WriteLine("Pedido realizado com sucesso!");
    }

    private void ConsultarMeusPedidos()
    {
        Console.WriteLine("Consultar pedidos por:");
        Console.WriteLine("1 - Número do pedido");
        Console.WriteLine("2 - Intervalo de datas");
        Console.Write("Opção: ");
        string op = Console.ReadLine() ?? "";

        List<Pedido> meusPedidos = _pedidoService.ObterPorCliente(_clienteLogado.Id);

        if (op == "1")
        {
            Console.Write("Digite o número do pedido: ");
            int num = int.Parse(Console.ReadLine()!);
            var pedido = meusPedidos.FirstOrDefault(p => p.Id == num);
            if (pedido != null)
                ExibirDetalhesPedido(pedido);
            else
                Console.WriteLine("Pedido não encontrado.");
        }
        else if (op == "2")
        {
            Console.Write("Data inicial (yyyy-mm-dd): ");
            DateTime d1 = DateTime.Parse(Console.ReadLine()!);
            Console.Write("Data final (yyyy-mm-dd): ");
            DateTime d2 = DateTime.Parse(Console.ReadLine()!);

            var pedidos = meusPedidos
                .Where(p => p.Data.Date >= d1.Date && p.Data.Date <= d2.Date)
                .ToList();

            if (!pedidos.Any())
            {
                Console.WriteLine("Nenhum pedido nesse intervalo.");
                return;
            }
            foreach (var pedido in pedidos)
            {
                ExibirDetalhesPedido(pedido);
                Console.WriteLine("--------------------------");
            }
        }
    }

    private void ExibirDetalhesPedido(Pedido pedido)
    {
        Console.WriteLine($"\nPedido Nº {pedido.Id} | Data: {pedido.Data:dd/MM/yyyy HH:mm}");
        foreach (var item in pedido.Itens)
        {
            var produto = _produtoService.ObterPorId(item.ProdutoId);
            if (produto != null)
            {
                Console.WriteLine($"Produto: {produto.Nome} | Qtd: {item.Quantidade} | Unit: {item.PrecoUnitario:C} | Total: {item.PrecoTotal:C}");
                Console.WriteLine($"Descrição: {produto.Descricao}");
            }
            else
            {
                Console.WriteLine($"Produto removido ou não encontrado (ID: {item.ProdutoId})");
            }
        }
        Console.WriteLine($"Frete: {pedido.ValorFrete:C}");
        Console.WriteLine($"Total do pedido: {pedido.ValorTotal:C}");

        if (pedido.Status == "Enviado" && pedido.DataEnvio != null)
            Console.WriteLine($"Data de envio: {pedido.DataEnvio:dd/MM/yyyy}");
        if (pedido.Status == "Cancelado" && pedido.DataCancelamento != null)
            Console.WriteLine($"Data de cancelamento: {pedido.DataCancelamento:dd/MM/yyyy}");
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

            opcao = int.Parse(Console.ReadLine()!);
            switch (opcao)
            {
                case 1:
                    _produtoService.Consultar();
                    break;
                case 2:
                    AdicionarProdutoAoCarrinho();
                    break;
                case 3:
                    VisualizarCarrinho();
                    break;
                case 4:
                    FinalizarCarrinho();
                    break;
                case 5:
                    ConsultarMeusPedidos();
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

        } while (opcao != 6);
    }
}
