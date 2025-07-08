namespace ProjetoPOO.Menu;
using ProjetoPOO.Services;
using ProjetoPOO.Services.Exceptions;
using System;

public class MenuFornecedor
{
    private readonly FornecedorService _fornecedorService;

    public MenuFornecedor(FornecedorService fornecedorService)
    {
        _fornecedorService = fornecedorService ?? throw new ArgumentNullException(nameof(fornecedorService));
    }

    public void TelaFornecedor()
    {
        while (true)
        {
            try
            {
                Console.WriteLine("\n== MENU DO FORNECEDOR ==");
                Console.WriteLine("1 - Adicione um fornecedor");
                Console.WriteLine("2 - Altere um fornecedor");
                Console.WriteLine("3 - Exclua um fornecedor");
                Console.WriteLine("4 - Consultar fornecedor");
                Console.WriteLine("5 - Voltar");
                Console.Write("Opção: ");

                if (!int.TryParse(Console.ReadLine(), out int opcao))
                    throw new ExcecaoEntradaInvalida("Digite um número válido para a opção.");

                switch (opcao)
                {
                    case 1:
                        _fornecedorService.Cadastrar();
                        break;

                    case 2:
                        _fornecedorService.Alterar();
                        break;

                    case 3:
                        _fornecedorService.Excluir();
                        break;

                    case 4:
                        _fornecedorService.Consultar();
                        break;

                    case 5:
                        return;

                    default:
                        Console.WriteLine("Digite uma opção entre 1 e 5.");
                        break;
                }
            }
            catch (ExcecaoEntradaInvalida ex)
            {
                Console.WriteLine($"Erro de entrada: {ex.Message}");
            }
            catch (ExcecaoEntidadeNaoEncontrada ex)
            {
                Console.WriteLine($"Erro: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro inesperado: {ex.Message}");
            }
        }
    }
}
