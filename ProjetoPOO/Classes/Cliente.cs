namespace ProjetoPOO.Classes;

using System;
using System.Reflection.Metadata.Ecma335;

public class Cliente
{
    public string? Nome{get;set;}
    public string? Telefone{get;set;}    
    public string? Email{get;set;}

    public Cliente(string nome, string telefone, string email)
    {
        this.Nome = nome;
        this.Email = email;
        this.Telefone = telefone;
    }


    public void fazerPedido()
    {

    }

    

}