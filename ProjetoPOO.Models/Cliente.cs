namespace ProjetoPOO.Models;

using System;


public class Cliente:IIdentificavel
{
    public string? Nome{get;set;}
    public int IdCliente { get;set;} 
    public string Senha{ get; set; }
    public string? Telefone { get; set; }    
    public string? Email{get;set;}
    public Endereco Endereco{ get; set; }

    public int Id =>IdCliente ;

    public void fazerPedido()
    {

    }

    

}