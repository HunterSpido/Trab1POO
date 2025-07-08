using System;
using ProjetoPOO.Models;
using ProjetoPOO.Services;
using ProjetoPOO.Services.Exceptions;

namespace ProjetoPOO.Menu
{
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
                Console.WriteLine("\n--- Menu Transportadora ---");
                Console.WriteLine("1 - Adicionar transportadora");
                Console.WriteLine("2 - Alterar transportadora");
                Console.WriteLine("3 - Excluir transportadora");
                Console.WriteLine("4 - Consultar transportadora");
                Console.WriteLine("5 - Voltar");

                Console.Write("Opção: ");
                string entrada = Console.ReadLine() ?? "";

                if (!int.TryParse(entrada, out int opcao))
                {
                    Console.WriteLine("Entrada inválida. Digite um número de 1 a 5.");
                    continue;
                }

                try
                {
                    switch (opcao)
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
                            return;
                        default:
                            Console.WriteLine("Digite um número entre 1 e 5.");
                            break;
                    }
                }
                catch (ExcecaoEntradaInvalida ex)
                {
                    Console.WriteLine($"[Entrada inválida] {ex.Message}");
                }
                catch (ExcecaoEntidadeNaoEncontrada ex)
                {
                    Console.WriteLine($"[Entidade não encontrada] {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[Erro inesperado] {ex.Message}");
                }
            }
        }
    }
}
