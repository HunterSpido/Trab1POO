using System;

namespace ProjetoPOO.Models;

public class Pedido
{
    public int Id { get; set; }
    public Cliente Cliente { get; set; } = null!;
    public DateTime Data { get; set; }
    public List<PedidoItem> Itens { get; set; } = new();
    public decimal ValorFrete { get; set; }
    public decimal ValorTotal { get; set; }
    public string Status { get; set; } = "Novo";
    public DateTime? DataEnvio { get; set; }
    public DateTime? DataCancelamento { get; set; }
    public DateTime? DataSaiuParaTransporte { get; set; }
    public Transportadora? Transportadora { get; set; }

}
