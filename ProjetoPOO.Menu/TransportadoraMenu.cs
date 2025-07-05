using System;
using ProjetoPOO.Models;
using ProjetoPOO.Services;

namespace ProjetoPOO.Menu;

public class TransportadoraMenu
{
    private readonly TransportadoraService _transportadoraService;

    public TransportadoraMenu(TransportadoraService transportadoraService)
    {
        _transportadoraService = transportadoraService ?? throw new ArgumentNullException(nameof(transportadoraService));

    }
    public void MenuTransportadora()
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
                    _transportadoraService.Adicionar();
                    break;
                case 2:
                    _transportadoraService.Alterar();
                    break;
                case 3:
                    _transportadoraService.Excluir();
                    break;
                case 4:
                    _transportadoraService.Consultar();
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
