using System.ComponentModel;
using ProjetoPOO;

namespace Program;

public static class Program
{
    public static void Main(string[] args)
    {
        System.Console.WriteLine("Bem vindo!");
        ProjetoPOO.Menu.Menu.telaLogin();   
    }

}

/*
Primeira tela é mostrando login ou cadastro
    se cadastro{
        mostrar nome, senha e tipo(admin ou cliente)
    }
    se login{
        Verificar se usuario existe 
            se existir mostrar telas (admin ou cliente)
            se nao existir falar que nao sxiste
    }   
*/