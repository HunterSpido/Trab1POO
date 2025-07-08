using System;

namespace ProjetoPOO.Services.Exceptions
{

    public class ExcecaoEntidadeNaoEncontrada : Exception
    {
        public ExcecaoEntidadeNaoEncontrada(string entidade, object chave)
            : base($"{entidade} não encontrada (chave: {chave}).")
        {
        }
    }
}
