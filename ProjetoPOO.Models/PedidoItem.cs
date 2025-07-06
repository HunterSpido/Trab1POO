using System;

namespace ProjetoPOO.Models;

public class PedidoItem
{
   public int ProdutoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal PrecoTotal {  get; set; }

    // Não serializar este campo!
    [System.Text.Json.Serialization.JsonIgnore]
    public Produto? Produto { get; set; }
}
