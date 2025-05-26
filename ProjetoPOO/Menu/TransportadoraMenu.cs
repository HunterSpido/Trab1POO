using System;
using ProjetoPOO.Classes;
using ProjetoPOO.Services;

namespace ProjetoPOO.Menu;

public class TransportadoraMenu
{
    public static void MenuTransportadora()
    {
        while (true) 
        {
            Console.WriteLine("Selecione uma opção");
            Console.WriteLine("1 - Adicione uma transportadora");
            Console.WriteLine("2 - Altere uma transportadora");
            Console.WriteLine("3 - Exclua uma transportadora");
            Console.WriteLine("4 - Consulte uma transportadora");
            Console.WriteLine("5 - Voltar");

            int v = int.Parse(Console.ReadLine()!);
            switch (v)
            {
                case 1:
                    TransportadoraService.Adicione();
                    break;
                case 2:
                    TransportadoraService.Alteracao();
                    break;
                case 3:
                    TransportadoraService.Exclusao();
                    break;
                case 4:
                    TransportadoraService.Consulta();
                    break;
                case 5:
                    return; // Sai do método e volta para o AdminMenu
                default:
                    Console.WriteLine("Digite entre a opção 1-5");
                    break;
            }
        }
    }
}
