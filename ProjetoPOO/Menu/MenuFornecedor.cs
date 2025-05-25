namespace ProjetoPOO.Menu;
using ProjetoPOO.Services;
using System;

public static class MenuFornecedor
{
    public static void TelaFornecedor()
    {
        while (true)
        {
            Console.WriteLine(" == MENU DO FORNECEDOR == ");
            Console.WriteLine("Selecione uma opcao: ");
            Console.WriteLine("1 - Adicione um fornecedor");
            Console.WriteLine("2 - Altere um fornecedor");
            Console.WriteLine("3 - Exclua um fornecedor");
            Console.WriteLine("4 - Consulte um fornecedor");
            Console.WriteLine("5 - Voltar");

            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {

                case 1:
                    FornecedorService.Adicionar();
                    break;

                case 2:
                    FornecedorService.Alterar();
                    break;

                case 3:
                    FornecedorService.Excluir();
                    break;

                case 4:
                    FornecedorService.Consultar();
                    break;

                case 5:
                    // Volta para o meun anterior
                    break;
                

            }            
        }


    }
}