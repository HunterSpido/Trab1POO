namespace ProjetoPOO.Menu;
using ProjetoPOO.Services;
using System;

public  class MenuFornecedor
{
    private readonly FornecedorService _fornecedorService;

    public MenuFornecedor(FornecedorService fornecedorService)
    {
        _fornecedorService = fornecedorService ?? throw new ArgumentNullException(nameof(fornecedorService));

    }
    // adiciona construct e 
    public void TelaFornecedor()
    {
        while (true)
        {
            Console.WriteLine(" == MENU DO FORNECEDOR == ");
            Console.WriteLine("Selecione uma opcao: ");
            Console.WriteLine("1 - Adicione um fornecedor");
            Console.WriteLine("2 - Altere um fornecedor");
            Console.WriteLine("3 - Exclua um fornecedor");
            Console.WriteLine("4 - Consultar fornecedor");
            Console.WriteLine("5 - Voltar");
            //FornecedorService fornecedorService = new FornecedorService();

            int opcao = int.Parse(Console.ReadLine()!);

            switch (opcao)
            {

                case 1:
                    Console.WriteLine("escolheu 1");
                    _fornecedorService.Cadastrar();
                    break;

                case 2:
                    Console.WriteLine("escolheu 1");
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
            }
        }


    }
}