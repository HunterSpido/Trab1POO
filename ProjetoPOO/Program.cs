using System;
using ProjetoPOO.Repository.ClienteRepository;
using ProjetoPOO.Repository.Interfaces;
using ProjetoPOO.Services;
using ProjetoPOO.Menu;
using ProjetoPOO.Repository.FornecedorRepository;
using ProjetoPOO.Repository.TransportadoraRepository;  // onde seu Menu está

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


            if (escolha == "V")
            {
                clierepo = new ClienteRepositoryVetor();
                fornerrepo = new FornecedorRepositoryVetor();
                transportadora = new TransportadoraRepositoryVetor();
            }
            else
            {
                clierepo = new ClienteRepositoryList();
                fornerrepo = new FornecedorRepositoryList();
                transportadora = new TransportadoraRepositoryList();
            }

            var enderecoService = new EnderecoService();
            var clienteService = new ClienteService(clierepo, enderecoService);
            var fornecedoreService = new FornecedorService(fornerrepo, enderecoService);
            var transportadoraService = new TransportadoraService(transportadora);

            // Agora o compilador sabe que Menu é a classe em UI.Menus
            var menu = new MenuPrincipal(clienteService, fornecedoreService, transportadoraService);
            menu.TelaLogin();
        }
    }
}
