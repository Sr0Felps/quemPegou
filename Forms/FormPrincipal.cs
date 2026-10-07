using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using quemPegou.Modelo;
using quemPegou.Negocio;

namespace quemPegou.Forms
{
    // Tela inicial: lista as coisas emprestadas.
    // Só código de tela: regras e banco ficam em EmprestimoService.
    public partial class FormPrincipal : Form
    {
        private EmprestimoService servico;

        public FormPrincipal()
        {
            InitializeComponent();
            this.ActiveControl = dgvItens; // foco inicial na lista
        }

        private void FormPrincipal_Load(object sender, EventArgs e)
        {
            ConfigurarColunas();

            try
            {
                servico = new EmprestimoService();
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Quem Pegou?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            CarregarLista();
        }

        private void ConfigurarColunas()
        {
            dgvItens.Columns.Clear();
            dgvItens.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvItens.Columns.Add("colItem", "Item Emprestado");
            dgvItens.Columns.Add("colAmigo", "Amigo");
            dgvItens.Columns.Add("colContato", "Contato");
            dgvItens.Columns.Add("colEmprestimo", "Empréstimo");
            dgvItens.Columns.Add("colPrevista", "Devolução Combinada");
            dgvItens.Columns.Add("colReal", "Devolvido Em");
            dgvItens.Columns.Add("colSituacao", "Situação");
        }

        private void CarregarLista()
        {
            try
            {
                List<Itens> lista = servico.Listar();
                dgvItens.Rows.Clear();

                int atrasados = 0;
                foreach (Itens item in lista)
                {
                    int linha = dgvItens.Rows.Add(
                        item.getItem(),
                        item.getNomeAmigo(),
                        item.getContato(),
                        FormatarData(item.getDataEmprestimo()),
                        FormatarData(item.getDataDevolucaoPrevista()),
                        FormatarData(item.getDataDevolucaoReal()),
                        item.getSituacao());

                    // guarda o objeto na linha para usar ao devolver
                    dgvItens.Rows[linha].Tag = item;

                    // Atrasados em vermelho, devolvidos em cinza
                    if (item.estaAtrasado())
                    {
                        atrasados++;
                        dgvItens.Rows[linha].DefaultCellStyle.BackColor = Color.MistyRose;
                        dgvItens.Rows[linha].DefaultCellStyle.ForeColor = Color.DarkRed;
                        dgvItens.Rows[linha].DefaultCellStyle.SelectionBackColor = Color.Firebrick;
                        dgvItens.Rows[linha].DefaultCellStyle.SelectionForeColor = Color.White;
                    }
                    else if (item.getDevolvido())
                    {
                        dgvItens.Rows[linha].DefaultCellStyle.BackColor = Color.Gainsboro;
                        dgvItens.Rows[linha].DefaultCellStyle.ForeColor = Color.Gray;
                    }
                }

                tslResumo.Text = lista.Count + " item(ns) na lista, " + atrasados + " atrasado(s)";
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Quem Pegou?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            AtualizarBotaoDevolver();
        }

        private string FormatarData(DateTime? data)
        {
            if (!data.HasValue)
                return "";

            return data.Value.ToString("dd/MM/yyyy");
        }

        private Itens ItemSelecionado()
        {
            if (dgvItens.CurrentRow == null)
                return null;

            return dgvItens.CurrentRow.Tag as Itens;
        }

        // Só dá para devolver um item selecionado que ainda está emprestado
        private void AtualizarBotaoDevolver()
        {
            Itens item = ItemSelecionado();
            btnDevolver.Enabled = item != null && !item.getDevolvido();
        }

        private void dgvItens_SelectionChanged(object sender, EventArgs e)
        {
            AtualizarBotaoDevolver();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            using (FormNovoEmprestimo tela = new FormNovoEmprestimo(servico))
            {
                if (tela.ShowDialog(this) == DialogResult.OK)
                    CarregarLista();
            }
        }

        private void btnDevolver_Click(object sender, EventArgs e)
        {
            Itens item = ItemSelecionado();
            if (item == null)
                return;

            DialogResult resposta = MessageBox.Show(
                "Confirmar a devolução de \"" + item.getItem() + "\" por " + item.getNomeAmigo() + " hoje?",
                "Confirmar Devolução", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resposta != DialogResult.Yes)
                return;

            try
            {
                servico.RegistrarDevolucao(item);
            }
            catch (NegocioException ex)
            {
                MessageBox.Show(ex.Message, "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Quem Pegou?", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            CarregarLista();
        }
    }
}
