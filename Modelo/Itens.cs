using System;

namespace quemPegou.Modelo
{
    // Um item emprestado a um amigo
    public class Itens
    {
        // Atributos
        private int id;
        private string item;
        private string nomeAmigo;
        private string contato;
        private DateTime dataEmprestimo;
        private DateTime? dataDevolucaoPrevista;
        private DateTime? dataDevolucaoReal;
        private bool devolvido;

        // Construtor vazio
        public Itens() { }

        // Construtor completo
        public Itens(int id, string item, string nomeAmigo, string contato, DateTime dataEmprestimo,
                     DateTime? dataDevolucaoPrevista, DateTime? dataDevolucaoReal, bool devolvido)
        {
            this.id = id;
            this.item = item;
            this.nomeAmigo = nomeAmigo;
            this.contato = contato;
            this.dataEmprestimo = dataEmprestimo;
            this.dataDevolucaoPrevista = dataDevolucaoPrevista;
            this.dataDevolucaoReal = dataDevolucaoReal;
            this.devolvido = devolvido;
        }

        // Getters
        public int getId() { return id; }
        public string getItem() { return item; }
        public string getNomeAmigo() { return nomeAmigo; }
        public string getContato() { return contato; }
        public DateTime getDataEmprestimo() { return dataEmprestimo; }
        public DateTime? getDataDevolucaoPrevista() { return dataDevolucaoPrevista; }
        public DateTime? getDataDevolucaoReal() { return dataDevolucaoReal; }
        public bool getDevolvido() { return devolvido; }

        // Setters
        public void setId(int id) { this.id = id; }
        public void setItem(string item) { this.item = item; }
        public void setNomeAmigo(string nomeAmigo) { this.nomeAmigo = nomeAmigo; }
        public void setContato(string contato) { this.contato = contato; }
        public void setDataEmprestimo(DateTime data) { this.dataEmprestimo = data; }
        public void setDataDevolucaoPrevista(DateTime? data) { this.dataDevolucaoPrevista = data; }
        public void setDataDevolucaoReal(DateTime? data) { this.dataDevolucaoReal = data; }
        public void setDevolvido(bool devolvido) { this.devolvido = devolvido; }

        // Atrasado = não foi devolvido e a data combinada já passou
        public bool estaAtrasado()
        {
            return !devolvido
                && dataDevolucaoPrevista.HasValue
                && dataDevolucaoPrevista.Value.Date < DateTime.Today;
        }

        // Texto mostrado na coluna "Situação"
        public string getSituacao()
        {
            if (devolvido) return "Devolvido";
            if (estaAtrasado()) return "Atrasado";
            return "Emprestado";
        }
    }
}
