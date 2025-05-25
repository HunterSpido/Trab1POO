using System;
using System.Collections;
using System.IO.Pipes;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace ProjetoPOO.Classes;

public class Transportadora
{
    public int IdTransporadora{ get; set; }
    public string? Nome { get; set; }
    public double PrecoPorKm { get; set; }

}
 