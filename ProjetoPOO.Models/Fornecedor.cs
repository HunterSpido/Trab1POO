namespace ProjetoPOO.Models;

using System;


public class Fornecedor:IIdentificavel
{
    public int IdFornecedor { get; set; }
    public string? Nome { get; set; }
    public string? Descricao{get;set;}
    public string? Telefone{get;set;}    
    public string? Email{get;set;}
    public int Id => IdFornecedor;

    public Endereco? Endereco{ get; set; }







}