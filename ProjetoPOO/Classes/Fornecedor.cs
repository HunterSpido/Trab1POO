namespace ProjetoPOO.Classes;

using System;
using System.Reflection.Metadata.Ecma335;

public class Fornecedor 
{
    public string? Nome{get;set;}
    public string? Descricao{get;set;}
    public string? Telefone{get;set;}    
    public string? Email{get;set;}

    public Fornecedor(string nome, string descricao, string telefone, string email)
    {
        this.Nome = nome;
        this.Descricao = descricao;
        this.Telefone = telefone;
        this.Email = email;
    }

    public void cadastroTransportadora()
    {

    }



}