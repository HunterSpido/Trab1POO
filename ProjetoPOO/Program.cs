using System;
using ProjetoPOO.Repository.ClienteRepository;
using ProjetoPOO.Repository.Interfaces;
using ProjetoPOO.Services;
using ProjetoPOO.Menu;  // onde seu Menu está

namespace ProjetoPOO
{
    class Program
    {
        static void Main()
        {
            Console.Write("Usar Vetor (V) ou Lista (L)? ");
            var escolha = (Console.ReadLine() ?? "").Trim().ToUpper();

            IClienteRepository repo = escolha == "V"
                ? new ClienteRepositoryVetor()
                : new ClienteRepositoryList();

            var enderecoService = new EnderecoService();
            var clienteService = new ClienteService(repo, enderecoService);

            // Agora o compilador sabe que Menu é a classe em UI.Menus
            var menu = new MenuPrincipal(clienteService);
            menu.TelaLogin();
        }
    }
}
