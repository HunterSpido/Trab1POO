using System;


namespace ProjetoPOO.Models;

public class Transportadora:IIdentificavel
{
    public int IdTransportadora{ get; set; }
    public string? Nome { get; set; }
    public double PrecoPorKm { get; set; }
    public decimal ValorFrete { get; set; }

    int IIdentificavel.Id { get => IdTransportadora; set => throw new NotImplementedException(); }
}
 