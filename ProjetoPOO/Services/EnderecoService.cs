using System;
using ProjetoPOO.Classes;

namespace ProjetoPOO.Services;

public class EnderecoService
{
    public static Endereco PedirEndereco()
    {
        Endereco endereco = new Endereco();
        Console.WriteLine("Informe seu Endereço:");
        Console.Write("Estado: ");
        endereco.Estado = Console.ReadLine();

        Console.Write("Cidade: ");
        endereco.Cidade = Console.ReadLine();

        Console.Write("Rua: ");
        endereco.Rua = Console.ReadLine();

        Console.Write("Bairro: ");
        endereco.Bairro = Console.ReadLine();

        Console.Write("Número: ");
        endereco.Numero = Console.ReadLine();

        Console.Write("Complemento (casa/apartamento): ");
        endereco.Complemento = Console.ReadLine();

        Console.Write("CEP: ");
        endereco.Cep = Console.ReadLine();

        return endereco;
    }


}

