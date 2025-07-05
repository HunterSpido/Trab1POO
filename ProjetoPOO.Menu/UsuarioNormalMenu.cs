using System;
using ProjetoPOO.Services;

namespace ProjetoPOO.Menu;

public  class UsuarioNormalMenu
{

    private readonly Cliente _clienteService;
    private readonly FornecedorService _fornecedorService;
    private readonly TransportadoraService _transportadoraService;

    private readonly ProdutoService _produtoService;

    public UsuarioNormalMenu(Cliente clienteService, FornecedorService fornecedorService, TransportadoraService transportadoraService, ProdutoService produtoService)
    {
        _clienteService = clienteService ?? throw new ArgumentNullException(nameof(clienteService));
        _transportadoraService = transportadoraService ?? throw new ArgumentNullException(nameof(transportadoraService));
        _produtoService = produtoService ?? throw new ArgumentNullException(nameof(produtoService));
        _fornecedorService = fornecedorService ?? throw new ArgumentNullException(nameof(fornecedorService));

    }
    public  void MenuUsuario()
    {
        int opcao;
        do
        {
            Console.WriteLine("\n== MENU DO USUÁRIO ==");
            Console.WriteLine("1 - Consultar Fornecedor");
            Console.WriteLine("2 - Consultar Transportadora");
            Console.WriteLine("3 - Consultar Produto");
            Console.WriteLine("4 - deve ser coisa de compra");
            Console.WriteLine("5 - Sair");
            Console.Write("Digite a opção: ");

            opcao = int.Parse(Console.ReadLine()!);
            switch (opcao)
            {
                case 1:
                    //fornecedorService.Consultar();
                    
                    break;
                case 2:
                    //transportadoraService.Consultar();
                    break;
                case 3:
                    //produtoService.Consultar();
                    break;
                case 4:
                    Console.WriteLine("Voltar Tela de Login...");
                    break;
                case 5:
                    Console.WriteLine("Voltar Tela de Login...");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

        } while (opcao != 4);
    }
}
