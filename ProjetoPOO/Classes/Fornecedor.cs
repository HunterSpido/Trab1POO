namespace ProjetoPOO.Classes;

using System;
using System.Reflection.Metadata.Ecma335;

public class Fornecedor 
{
    public int IdFornecedor { get; set; }
    public string? Nome { get; set; }
    public string? Descricao{get;set;}
    public string? Telefone{get;set;}    
    public string? Email{get;set;}

    public Endereco Endereco{ get; set; }



    public void cadastroTransportadora()
    {

    }



}