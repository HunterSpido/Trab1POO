using System;
using ProjetoPOO.Models;

namespace ProjetoPOO.Services;

public class EnderecoService
{
    public  Endereco PedirEndereco()
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

