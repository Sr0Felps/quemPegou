using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using quemPegou.Modelo;

namespace quemPegou.Dados
{
    // Acesso ao banco. Todo SQL do sistema fica aqui.
    // Erros do MySQL viram ApplicationException com mensagem amigável.
    public class ItensDB
    {
        private string conexao;

        public ItensDB()
        {
            // A connection string fica no App.config
            ConnectionStringSettings config = ConfigurationManager.ConnectionStrings["myDataBaseConnection"];
            if (config == null)
                throw new ApplicationException("Configuração do banco não encontrada no App.config.");

            this.conexao = config.ConnectionString;
        }

        public void Inserir(Itens item)
        {
            string sql = "INSERT INTO itens (`item`, `nome`, `contato`, `emprestimo`, `devolucao prevista`, `devolvido`) "
                       + "VALUES (@item, @nome, @contato, @emprestimo, @prevista, 0)";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@item", item.getItem());
                    cmd.Parameters.AddWithValue("@nome", item.getNomeAmigo());
                    cmd.Parameters.AddWithValue("@contato", item.getContato());
                    cmd.Parameters.AddWithValue("@emprestimo", item.getDataEmprestimo().Date);

                    // a data combinada é opcional: sem data, grava NULL
                    if (item.getDataDevolucaoPrevista().HasValue)
                        cmd.Parameters.AddWithValue("@prevista", item.getDataDevolucaoPrevista().Value.Date);
                    else
                        cmd.Parameters.AddWithValue("@prevista", DBNull.Value);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch (MySqlException ex)
            {
                throw Traduzir(ex, "salvar o empréstimo");
            }
        }

        // Retorna todos os empréstimos: pendentes primeiro, devolvidos no fim
        public List<Itens> ObterTodos()
        {
            List<Itens> lista = new List<Itens>();

            string sql = "SELECT `id`, `item`, `nome`, `contato`, `emprestimo`, "
                       + "`devolucao prevista`, `devolucao real`, `devolvido` FROM itens "
                       + "ORDER BY `devolvido`, (`devolucao prevista` IS NULL), `devolucao prevista`, `emprestimo`";

            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    conn.Open();
                    using (MySqlDataReader leitor = cmd.ExecuteReader())
                    {
                        while (leitor.Read())
                        {
                            Itens item = new Itens();
                            item.setId(Convert.ToInt32(leitor["id"]));
                            item.setItem(leitor["item"].ToString());
                            item.setNomeAmigo(leitor["nome"].ToString());
                            item.setContato(leitor["contato"].ToString());
                            item.setDataEmprestimo(Convert.ToDateTime(leitor["emprestimo"]));
                            item.setDevolvido(Convert.ToBoolean(leitor["devolvido"]));

                            if (leitor["devolucao prevista"] != DBNull.Value)
                                item.setDataDevolucaoPrevista(Convert.ToDateTime(leitor["devolucao prevista"]));

                            if (leitor["devolucao real"] != DBNull.Value)
                                item.setDataDevolucaoReal(Convert.ToDateTime(leitor["devolucao real"]));

                            lista.Add(item);
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw Traduzir(ex, "carregar a lista");
            }

            return lista;
        }

        // Marca como devolvido e salva a data.
        // Usa transação: confere a situação e atualiza na mesma operação.
        // Retorna false se o item já estava devolvido ou não existe.
        public bool RegistrarDevolucao(int id, DateTime data)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(conexao))
                {
                    conn.Open();

                    using (MySqlTransaction transacao = conn.BeginTransaction())
                    {
                        try
                        {
                            object situacao;
                            using (MySqlCommand consulta = new MySqlCommand(
                                "SELECT `devolvido` FROM itens WHERE `id` = @id", conn, transacao))
                            {
                                consulta.Parameters.AddWithValue("@id", id);
                                situacao = consulta.ExecuteScalar();
                            }

                            if (situacao == null || Convert.ToBoolean(situacao))
                            {
                                transacao.Rollback();
                                return false;
                            }

                            using (MySqlCommand atualiza = new MySqlCommand(
                                "UPDATE itens SET `devolvido` = 1, `devolucao real` = @data WHERE `id` = @id",
                                conn, transacao))
                            {
                                atualiza.Parameters.AddWithValue("@id", id);
                                atualiza.Parameters.AddWithValue("@data", data.Date);
                                atualiza.ExecuteNonQuery();
                            }

                            transacao.Commit();
                            return true;
                        }
                        catch (MySqlException)
                        {
                            // se algo falhou no meio, desfaz tudo
                            transacao.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                throw Traduzir(ex, "registrar a devolução");
            }
        }

        // Troca o erro técnico do MySQL por uma mensagem que o usuário entende.
        // O erro original fica guardado em InnerException.
        private ApplicationException Traduzir(MySqlException ex, string acao)
        {
            string mensagem;

            switch (ex.Number)
            {
                case 0:
                case 1042:
                    mensagem = "Não foi possível conectar ao MySQL. Verifique se o servidor está ligado.";
                    break;
                case 1045:
                    mensagem = "Usuário ou senha do banco inválidos. Confira o App.config.";
                    break;
                case 1049:
                    mensagem = "O banco 'emprestimos' não existe. Execute o script da pasta banco.";
                    break;
                case 1146:
                    mensagem = "A tabela 'itens' não existe. Execute o script da pasta banco.";
                    break;
                default:
                    mensagem = "Erro no banco de dados ao " + acao + ".";
                    break;
            }

            return new ApplicationException(mensagem, ex);
        }
    }
}
