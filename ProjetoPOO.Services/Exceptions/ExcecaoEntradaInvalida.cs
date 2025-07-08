using System;

namespace ProjetoPOO.Services.Exceptions
{

    public class ExcecaoEntradaInvalida : Exception
    {
        public ExcecaoEntradaInvalida(string campo)
            : base($"Valor inválido para '{campo}'.")
        {
        }
    }
}
