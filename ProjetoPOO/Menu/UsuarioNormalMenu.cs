using System;
using ProjetoPOO.Services;

namespace ProjetoPOO.Menu;

public static class UsuarioNormalMenu
{
    public static void MenuUsuario()
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
            
            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                Console.WriteLine("Opção inválida.");
                continue;
            }

            switch (opcao)
            {
                case 1:
                    //FornecedorService.Consulta();
                    break;
                case 2:
                    TransportadoraService.Consulta();
                    break;
                case 3:
                    //ProdutoService.Consulta();
                    break;
                case 4:
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

        } while (opcao != 4);
    }
}
