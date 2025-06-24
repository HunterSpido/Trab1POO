using System;
using ProjetoPOO.Services;

namespace ProjetoPOO.Menu;

public  class UsuarioNormalMenu
{
    FornecedorService fornecedorService;
    TransportadoraService transportadoraService;
    ProdutoService produtoService;
    public UsuarioNormalMenu()
    {
        fornecedorService = new FornecedorService();
        transportadoraService=new TransportadoraService();
        produtoService=new ProdutoService();
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
            Console.WriteLine("4 - Sair");
            Console.Write("Digite a opção: ");

            opcao = int.Parse(Console.ReadLine()!);
            switch (opcao)
            {
                case 1:
                    fornecedorService.Consultar();
                    
                    break;
                case 2:
                    transportadoraService.Consultar();
                    break;
                case 3:
                    produtoService.Consultar();
                    break;
                case 4:
                    Console.WriteLine("Voltar Tela de Login...");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

        } while (opcao != 4);
    }
}
