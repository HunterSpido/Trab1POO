namespace ProjetoPOO.Menu;
using ProjetoPOO.Services;
using System;

public  class MenuFornecedor
{
    public  void TelaFornecedor()
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
            FornecedorService fornecedorService = new FornecedorService();

            int opcao = int.Parse(Console.ReadLine()!);

            switch (opcao)
            {

                case 1:
                    fornecedorService.Adicionar();
                    break;

                case 2:
                    fornecedorService.Alterar();
                    break;

                case 3:
                    fornecedorService.Excluir();
                    break;

                case 4:
                    fornecedorService.Consultar();
                    break;

                case 5:
                    return;
            }
        }


    }
}