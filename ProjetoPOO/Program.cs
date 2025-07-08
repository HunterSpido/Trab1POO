using System;
using ProjetoPOO.Repository.ClienteRepository;
using ProjetoPOO.Repository.Interfaces;
using ProjetoPOO.Services;
using ProjetoPOO.Menu;
using ProjetoPOO.Repository.FornecedorRepository;
using ProjetoPOO.Repository.TransportadoraRepository;
using ProjetoPOO.Repository.ProdutoRepository;
using ProjetoPOO.Repository.PedidoRepository;  // onde seu Menu está

namespace ProjetoPOO
{
    class Program
    {
        static void Main()
        {
            Console.Write("Usar Vetor (V) ou Lista (L)? ");
            var escolha = (Console.ReadLine() ?? "").Trim().ToUpper();
            IClienteRepository clierepo;
            IRepositoryFornecedor fornerrepo;
            IRepositoryTransportadora transportadora;
            IRepositoryProduto produto;
            IRepositoryPedido pedido;


            if (escolha == "V")
            {
                clierepo = new ClienteRepositoryVetor();
                fornerrepo = new FornecedorRepositoryVetor();
                transportadora = new TransportadoraRepositoryVetor();
                produto = new ProdutoRepositoryVetor();
                pedido = new PedidoRepositoryVetor();
            }
            else
            {
                clierepo = new ClienteRepositoryList();
                fornerrepo = new FornecedorRepositoryList();
                transportadora = new TransportadoraRepositoryList();
                produto = new ProdutoRepositoryList();
                pedido = new PedidoRepositoryList();
            }

            var enderecoService = new EnderecoService();
            var clienteService = new ClienteService(clierepo, enderecoService);
            var fornecedoreService = new FornecedorService(fornerrepo, enderecoService);
            var transportadoraService = new TransportadoraService(transportadora);
            var produtoService = new ProdutoService(produto, fornerrepo);
            var PedidoService = new PedidoService(pedido);


            var menu = new MenuPrincipal(clienteService, fornecedoreService, transportadoraService, produtoService, PedidoService);
            menu.TelaLogin();
        }
    }
}
