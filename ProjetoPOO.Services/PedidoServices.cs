using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;

namespace ProjetoPOO.Services
{
    public class PedidoService
    {
        private readonly IRepositoryPedido _repo;

        public PedidoService(IRepositoryPedido repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
            _repo.Carregar();
        }

        public bool RealizarPedido(Pedido pedido)
        {
            pedido.Data = DateTime.Now;
            pedido.Status = "Novo";
            return _repo.Adicionar(pedido);
        }

        public List<Pedido> ListarPedidos()
        {
            return _repo.Listar();
        }

        public Pedido? ObterPorId(int id)
        {
            return _repo.ObterPorId(id);
        }

        public List<Pedido> ObterPorCliente(int clienteId)
        {
            return _repo.Listar()
                        .Where(p => p.Cliente != null && p.Cliente.Id == clienteId)
                        .ToList();
        }

        public List<Pedido> ObterPorData(DateTime inicio, DateTime fim)
        {
            return _repo.Listar()
                        .Where(p => p.Data.Date >= inicio.Date && p.Data.Date <= fim.Date)
                        .ToList();
        }

        public bool AlterarStatus(int pedidoId, string novoStatus, DateTime? dataEnvio = null, DateTime? dataCancelamento = null)
        {
            var pedido = _repo.ObterPorId(pedidoId);
            if (pedido == null) return false;

            pedido.Status = novoStatus;
            if (dataEnvio != null) pedido.DataEnvio = dataEnvio;
            if (dataCancelamento != null) pedido.DataCancelamento = dataCancelamento;

            return _repo.Alterar(pedido);
        }

        public void ExibirDetalhes(Pedido pedido, ProdutoService produtoService)
        {
            Console.WriteLine($"\nPedido Nº {pedido.Id} | Data: {pedido.Data:dd/MM/yyyy HH:mm}");

            foreach (var item in pedido.Itens)
            {
                var produto = produtoService.ObterPorId(item.ProdutoId);
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

        public void ConsultarPedidosDoCliente(Cliente cliente, ProdutoService produtoService)
        {
            var pedidos = ObterPorCliente(cliente.Id);

            Console.WriteLine("Consultar pedidos por:");
            Console.WriteLine("1 - Número do pedido");
            Console.WriteLine("2 - Intervalo de datas");
            Console.Write("Opção: ");
            string op = Console.ReadLine() ?? "";

            if (op == "1")
            {
                Console.Write("Digite o número do pedido: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("Número inválido.");
                    return;
                }

                var pedido = pedidos.FirstOrDefault(p => p.Id == id);
                if (pedido != null)
                    ExibirDetalhes(pedido, produtoService);
                else
                    Console.WriteLine("Pedido não encontrado.");
            }
            else if (op == "2")
            {
                Console.Write("Data inicial (yyyy-mm-dd): ");
                DateTime d1 = DateTime.Parse(Console.ReadLine()!);
                Console.Write("Data final (yyyy-mm-dd): ");
                DateTime d2 = DateTime.Parse(Console.ReadLine()!);

                var filtrados = pedidos
                    .Where(p => p.Data.Date >= d1.Date && p.Data.Date <= d2.Date)
                    .ToList();

                if (!filtrados.Any())
                {
                    Console.WriteLine("Nenhum pedido nesse intervalo.");
                    return;
                }

                foreach (var pedido in filtrados)
                {
                    ExibirDetalhes(pedido, produtoService);
                    Console.WriteLine("--------------------------");
                }
            }
        }
        public void ConsultarPedidosViaConsole(ProdutoService produtoService)
        {
            Console.WriteLine("\n== CONSULTA DE PEDIDOS ==");
            Console.WriteLine("1 - Consultar por número");
            Console.WriteLine("2 - Consultar por intervalo de datas");
            Console.Write("Opção: ");
            string op = Console.ReadLine() ?? "";

            if (op == "1")
            {
                Console.Write("Número do pedido: ");
                if (!int.TryParse(Console.ReadLine(), out int num))
                {
                    Console.WriteLine("Número inválido.");
                    return;
                }

                var pedido = ObterPorId(num);
                if (pedido != null)
                {
                    ExibirDetalhes(pedido, produtoService);
                    AlterarStatusViaConsole(pedido);
                }
                else
                {
                    Console.WriteLine("Pedido não encontrado.");
                }
            }
            else if (op == "2")
            {
                Console.Write("Data inicial (yyyy-mm-dd): ");
                DateTime d1 = DateTime.Parse(Console.ReadLine()!);
                Console.Write("Data final (yyyy-mm-dd): ");
                DateTime d2 = DateTime.Parse(Console.ReadLine()!);

                var pedidos = ObterPorData(d1, d2);
                if (!pedidos.Any())
                {
                    Console.WriteLine("Nenhum pedido nesse intervalo.");
                    return;
                }

                foreach (var pedido in pedidos)
                {
                    ExibirDetalhes(pedido, produtoService);
                    AlterarStatusViaConsole(pedido);
                    Console.WriteLine("-------------------------");
                }
            }
        }

        public void AlterarStatusViaConsole(Pedido pedido)
        {
            Console.WriteLine("\nDeseja alterar o status deste pedido?");
            Console.WriteLine("1 - Marcar como ENVIADO");
            Console.WriteLine("2 - Marcar como CANCELADO");
            Console.WriteLine("3 - Não alterar");
            Console.Write("Opção: ");
            string op = Console.ReadLine() ?? "";

            if (op == "1")
            {
                bool ok = AlterarStatus(pedido.Id, "Enviado", dataEnvio: DateTime.Now);
                Console.WriteLine(ok ? "Status alterado para ENVIADO!" : "Falha ao alterar status.");
            }
            else if (op == "2")
            {
                bool ok = AlterarStatus(pedido.Id, "Cancelado", dataCancelamento: DateTime.Now);
                Console.WriteLine(ok ? "Status alterado para CANCELADO!" : "Falha ao alterar status.");
            }
            else
            {
                Console.WriteLine("Não alterado.");
            }
        }


    }
}
