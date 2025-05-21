using System;
using System.Collections;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace ProjetoPOO.Classes;

public class Transportadora
{
    public string? Nome { get; set; }
    public double PrecoPorKm { get; set; }

    public void MenuTransportadora()
    {
        int v=-1;
        Console.WriteLine("Selecione uma opção");
        Console.WriteLine("1- Adicone uma transportadora");
        Console.WriteLine("2- Altere uma transportadora");
        Console.WriteLine("3- Exclua uma transportadora");
        Console.WriteLine("4- Consulte uma transportadora");

        switch (v)
        {
            case 1:
                this.Alteracao();
                break;
            case 2:
            this.
        }
           
    }

    public void Alteracao()
    {

    }
    

    public void Alteracao()
    {

    }

    public void Exclusao()
    {

    }
    public static void Consulta()
    {

    }
    


}
 