using System;
using ProjetoPOO.Repository.ClienteRepository;
using ProjetoPOO.Repository.Interfaces;
using ProjetoPOO.Services;
using ProjetoPOO.Menu;
using ProjetoPOO.Repository.FornecedorRepository;  // onde seu Menu está

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


            if (escolha == "V")
            {
                clierepo = new ClienteRepositoryVetor();
                fornerrepo = new FornecedorRepositoryVetor();
            }
            else
            {
                clierepo = new ClienteRepositoryList();
                fornerrepo = new FornecedorRepositoryList();
            }

            var enderecoService = new EnderecoService();
            var clienteService = new ClienteService(clierepo, enderecoService);
            var fornecedoreService = new FornecedorService(fornerrepo, enderecoService);

            // Agora o compilador sabe que Menu é a classe em UI.Menus
            var menu = new MenuPrincipal(clienteService, fornecedoreService);
            menu.TelaLogin();
        }
    }
}
