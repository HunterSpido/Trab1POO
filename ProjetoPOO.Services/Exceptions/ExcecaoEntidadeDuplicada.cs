using System;

namespace ProjetoPOO.Services.Exceptions
{

    public class ExcecaoEntidadeDuplicada : Exception
    {
        public ExcecaoEntidadeDuplicada(string entidade, object chave)
            : base($"{entidade} duplicada (chave: {chave}).")
        {
        }
    }
}
