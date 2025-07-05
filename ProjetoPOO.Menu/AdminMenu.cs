namespace ProjetoPOO.Menu;
using ProjetoPOO.Models;
using ProjetoPOO.Services;
using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

public class AdminMenu
{
    private readonly FornecedorService _fornecedorService;
    private readonly TransportadoraService _transportadoraService;

    public AdminMenu(FornecedorService fornecedorService, TransportadoraService transportadoraService)
    {
        _fornecedorService = fornecedorService ?? throw new ArgumentNullException(nameof(fornecedorService));
        _transportadoraService = transportadoraService ?? throw new ArgumentNullException(nameof(transportadoraService));

    }

    public void MenuAdmin()
    {
        int tes = 0;
        while (tes != 4) // Mantém o loop principal
        {
            System.Console.WriteLine("Selecione a opcão:");
            System.Console.WriteLine("1- Menu de Fornecedor:");
            System.Console.WriteLine("2- Menu de Produtos:");
            System.Console.WriteLine("3- Menu de Transportadora:");
            System.Console.WriteLine("4- Sair");

            tes = int.Parse(Console.ReadLine()!);

            switch (tes)
            {
                case 1:
                    Console.WriteLine("MENU FORNECEDOR");
                    MenuFornecedor menuFornecedor = new MenuFornecedor(_fornecedorService);
                    menuFornecedor.TelaFornecedor();
                    break;
                case 2:
                    Console.WriteLine("MENU PRODUTO");
                    ProdutoMenu produtoMenu = new ProdutoMenu();
                    produtoMenu.MenuProdutos();
                    break;
                case 3:
                    Console.WriteLine("MENU TRANSPORTADORA");
                    TransportadoraMenu transportadoraMenu = new TransportadoraMenu(_transportadoraService);
                    transportadoraMenu.MenuTransportadora();
                    break;
                case 4:
                    System.Console.WriteLine("Saindo...");
                    return; // Sai do MenuAdmin completamente
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }
}