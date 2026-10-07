using System;
using System.Collections.Generic;
using quemPegou.Dados;
using quemPegou.Modelo;

namespace quemPegou.Negocio
{
    // Regras do sistema. A tela chama esta classe; ela chama o banco.
    // Quando uma regra é quebrada, lança NegocioException com a mensagem.
    public class EmprestimoService
    {
        private ItensDB banco = new ItensDB();

        public List<Itens> Listar()
        {
            return banco.ObterTodos();
        }

        public void Registrar(Itens novo)
        {
            novo.setItem(novo.getItem().Trim());
            novo.setNomeAmigo(novo.getNomeAmigo().Trim());
            novo.setContato(novo.getContato().Trim());

            if (novo.getItem() == "")
                throw new NegocioException("Informe o item emprestado.");

            if (novo.getNomeAmigo() == "")
                throw new NegocioException("Informe o nome do amigo.");

            if (!ContatoValido(novo.getContato()))
                throw new NegocioException("Informe um contato válido: telefone com DDD ou e-mail.");

            if (novo.getDataEmprestimo().Date > DateTime.Today)
                throw new NegocioException("A data do empréstimo não pode estar no futuro.");

            if (novo.getDataDevolucaoPrevista().HasValue
                && novo.getDataDevolucaoPrevista().Value.Date < novo.getDataEmprestimo().Date)
                throw new NegocioException("A devolução combinada não pode ser antes do empréstimo.");

            banco.Inserir(novo);
        }

        // A data da devolução é sempre a de hoje
        public void RegistrarDevolucao(Itens item)
        {
            if (item.getDevolvido())
                throw new NegocioException("Este item já foi devolvido.");

            bool registrou = banco.RegistrarDevolucao(item.getId(), DateTime.Today);
            if (!registrou)
                throw new NegocioException("Este empréstimo já foi devolvido ou não existe mais. A lista foi atualizada.");
        }

        // Contato válido = e-mail simples ou telefone com 8 a 13 dígitos.
        // É público e estático para a tela usar a mesma regra no ErrorProvider.
        public static bool ContatoValido(string contato)
        {
            if (contato == null)
                return false;

            contato = contato.Trim();
            if (contato.Length == 0)
                return false;

            // e-mail: um "@" no meio, sem espaços e com "." depois do "@"
            if (contato.Contains("@"))
            {
                int arroba = contato.IndexOf('@');
                bool umSoArroba = arroba == contato.LastIndexOf('@');
                bool temPontoDepois = contato.IndexOf('.', arroba) > arroba + 1;
                bool semEspaco = !contato.Contains(" ");
                return umSoArroba && arroba > 0 && temPontoDepois
                       && !contato.EndsWith(".") && semEspaco;
            }

            // telefone: só dígitos e símbolos comuns, com 8 a 13 dígitos
            int digitos = 0;
            foreach (char c in contato)
            {
                if (char.IsDigit(c))
                    digitos++;
                else if ("+()- ".IndexOf(c) < 0)
                    return false;
            }
            return digitos >= 8 && digitos <= 13;
        }
    }
}
