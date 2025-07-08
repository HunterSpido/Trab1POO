using System;
using System.Collections.Generic;
using System.Linq;
using ProjetoPOO.Models;
using ProjetoPOO.Repository.Interfaces;
using ProjetoPOO.Services.Exceptions;

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
            if (pedido == null || pedido.Itens == null || pedido.Itens.Count == 0)
                throw new ExcecaoEntradaInvalida("Pedido inválido. Verifique os itens.");

            pedido.Data = DateTime.Now;
            pedido.Status = "Novo";
            return _repo.Adicionar(pedido);
        }

        public List<Pedido> ListarPedidos()
        {
            return _repo.Listar();
        }

        public Pedido ObterPorId(int id)
        {
            var pedido = _repo.ObterPorId(id);
            if (pedido == null)
                throw new ExcecaoEntidadeNaoEncontrada($"Pedido com ID {id} não encontrado.");
            return pedido;
        }

        public List<Pedido> ObterPorCliente(int clienteId)
        {
            return _repo.Listar()
                        .Where(p => p.Cliente != null && p.Cliente.Id == clienteId)
                        .ToList();
        }

        public List<Pedido> ObterPorData(DateTime inicio, DateTime fim)
        {
            if (fim < inicio)
                throw new ExcecaoEntradaInvalida("A data final não pode ser anterior à inicial.");

            return _repo.Listar()
                        .Where(p => p.Data.Date >= inicio.Date && p.Data.Date <= fim.Date)
                        .ToList();
        }

        public bool AlterarStatus(int pedidoId, string novoStatus, DateTime? dataEnvio = null, DateTime? dataCancelamento = null, DateTime? dataSaiuParaTransporte = null)
        {
            var pedido = _repo.ObterPorId(pedidoId);
            if (pedido == null)
                throw new ExcecaoEntidadeNaoEncontrada("Pedido não encontrado para alteração de status.");

            pedido.Status = novoStatus;
            if (dataEnvio != null) pedido.DataEnvio = dataEnvio;
            if (dataCancelamento != null) pedido.DataCancelamento = dataCancelamento;
            if (dataSaiuParaTransporte != null) pedido.DataSaiuParaTransporte = dataSaiuParaTransporte;

            return _repo.Alterar(pedido);
        }

        public void ExibirDetalhes(Pedido pedido, ProdutoService produtoService)
        {
            Console.WriteLine($"\nPedido N° {pedido.Id} | Data: {pedido.Data:dd/MM/yyyy HH:mm} | Status: {pedido.Status}");

            foreach (var item in pedido.Itens)
            {
                try
                {
                    var produto = produtoService.ObterPorId(item.ProdutoId);
                    Console.WriteLine($"Produto: {produto.Nome} | Qtd: {item.Quantidade} | Valor do Produto: {item.PrecoUnitario:C} | Total: {item.PrecoTotal:C}");
                    Console.WriteLine($"Descrição: {produto.Descricao}");
                }
                catch (ExcecaoEntidadeNaoEncontrada)
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
            if (pedido.Status == "Em transporte" && pedido.DataSaiuParaTransporte != null)
                Console.WriteLine($"Data que saiu para Transporte: {pedido.DataSaiuParaTransporte}");
        }

        public void ConsultarPedidosDoCliente(Cliente cliente, ProdutoService produtoService)
        {
            var pedidos = ObterPorCliente(cliente.Id);

            Console.WriteLine("Consultar pedidos por:");
            Console.WriteLine("1 - Número do pedido");
            Console.WriteLine("2 - Intervalo de datas");
            Console.Write("Opção: ");
            string op = Console.ReadLine() ?? throw new ExcecaoEntradaInvalida("Opção inválida.");

            if (op == "1")
            {
                Console.Write("Digite o número do pedido: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                    throw new ExcecaoEntradaInvalida("Número inválido.");

                var pedido = pedidos.FirstOrDefault(p => p.Id == id);
                if (pedido != null)
                    ExibirDetalhes(pedido, produtoService);
                else
                    throw new ExcecaoEntidadeNaoEncontrada("Pedido não encontrado.");
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
                    throw new ExcecaoEntidadeNaoEncontrada("Nenhum pedido encontrado nesse intervalo.");

                foreach (var pedido in filtrados)
                {
                    ExibirDetalhes(pedido, produtoService);
                    Console.WriteLine("--------------------------");
                }
            }
            else
            {
                throw new ExcecaoEntradaInvalida("Opção inválida.");
            }
        }

        public void ConsultarPedidosViaConsole(ProdutoService produtoService)
        {
            Console.WriteLine("\n== CONSULTA DE PEDIDOS ==");
            Console.WriteLine("1 - Consultar por número e realizar alteração de STATUS");
            Console.WriteLine("2 - Consultar por intervalo de datas");
            Console.Write("Opção: ");
            string op = Console.ReadLine() ?? throw new ExcecaoEntradaInvalida("Opção inválida.");

            if (op == "1")
            {
                Console.Write("Número do pedido: ");
                if (!int.TryParse(Console.ReadLine(), out int num))
                    throw new ExcecaoEntradaInvalida("Número inválido.");

                var pedido = ObterPorId(num);
                ExibirDetalhes(pedido, produtoService);
                AlterarStatusViaConsole(pedido);
            }
            else if (op == "2")
            {
                Console.Write("Data inicial (yyyy-mm-dd): ");
                DateTime d1 = DateTime.Parse(Console.ReadLine()!);

                Console.Write("Data final (yyyy-mm-dd): ");
                DateTime d2 = DateTime.Parse(Console.ReadLine()!);

                var pedidos = ObterPorData(d1, d2);
                if (!pedidos.Any())
                    throw new ExcecaoEntidadeNaoEncontrada("Nenhum pedido nesse intervalo.");

                foreach (var pedido in pedidos)
                {
                    ExibirDetalhes(pedido, produtoService);
                    Console.WriteLine("-------------------------");
                }
            }
            else
            {
                throw new ExcecaoEntradaInvalida("Opção inválida.");
            }
        }

        public void AlterarStatusViaConsole(Pedido pedido)
        {
            Console.WriteLine("\nDeseja alterar o status deste pedido?");
            Console.WriteLine("1 - Marcar como ENVIADO");
            Console.WriteLine("2 - Marcar como CANCELADO");
            Console.WriteLine("3 - Marcar como EM TRANSPORTE");
            Console.WriteLine("4 - Não alterar");
            Console.Write("Opção: ");
            string op = Console.ReadLine() ?? throw new ExcecaoEntradaInvalida("Opção inválida.");

            if (op == "1")
            {
                AlterarStatus(pedido.Id, "Enviado", dataEnvio: DateTime.Now);
                Console.WriteLine("Status alterado para ENVIADO!");
            }
            else if (op == "2")
            {
                AlterarStatus(pedido.Id, "Cancelado", dataCancelamento: DateTime.Now);
                Console.WriteLine("Status alterado para CANCELADO!");
            }
            else if (op == "3")
            {
                AlterarStatus(pedido.Id, "Em transporte", dataSaiuParaTransporte: DateTime.Now);
                Console.WriteLine("Status alterado para EM TRANSPORTE!");
            }
            else if (op == "4")
            {
                Console.WriteLine("Não alterado.");
            }
            else
            {
                throw new ExcecaoEntradaInvalida("Opção inválida.");
            }
        }
    }
}
