using System;

namespace ProjetoPOO.Models;

public class Pedido
{
    public int Numero{get;set;}
    public DateTime DataHoraPedido{get;set;}
    public DateTime DataHoraEntrega{get;set;}
    public string? Situacao{get;set;}
    public double PrecoFrete{get;set;}

    



}
