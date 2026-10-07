using System;

namespace quemPegou.Negocio
{
    // Dado que quebra uma regra do sistema (diferente de erro do banco)
    public class NegocioException : Exception
    {
        public NegocioException(string mensagem) : base(mensagem) { }
    }
}
