using System;

namespace ApiCatalogoJogos.Exceptions
{
    public class JogoNaoCadastradoException : Exception
    {
        public JogoNaoCadastradoException()
            : base ("Esse jogo não está cadastrado!")
        {}
    }
}