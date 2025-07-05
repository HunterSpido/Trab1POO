using System;

namespace ProjetoPOO.Models;

public class PedidoItem
{
    public Produto Produto { get; set; } = null!;
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; } // Armazena o valor na data da compra
    public decimal PrecoTotal => PrecoUnitario * Quantidade;
}
