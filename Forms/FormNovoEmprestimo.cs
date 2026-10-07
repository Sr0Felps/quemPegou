using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using quemPegou.Modelo;
using quemPegou.Negocio;

namespace quemPegou.Forms
{
    // Cadastro de um novo empréstimo.
    // A tela valida o FORMATO (ErrorProvider, máscara, TryParse);
    // as regras do sistema ficam em EmprestimoService.
    public partial class FormNovoEmprestimo : Form
    {
        private const string FormatoData = "dd/MM/yyyy";
        private EmprestimoService servico;

        public FormNovoEmprestimo(EmprestimoService servico)
        {
            InitializeComponent();
            this.servico = servico;

            // sugere a data de hoje e deixa o foco no primeiro campo
            mtbDataEmprestimo.Text = DateTime.Today.ToString(FormatoData, CultureInfo.InvariantCulture);
            this.ActiveControl = txtItem;
        }

        private bool ValidarObrigatorio(TextBox caixa, string mensagem)
        {
            if (caixa.Text.Trim().Length == 0)
            {
                epErros.SetError(caixa, mensagem);
                return false;
            }

            epErros.SetError(caixa, "");
            return true;
        }

        // Converte a data da caixa com máscara usando TryParseExact
        private bool LerData(MaskedTextBox caixa, bool obrigatoria, out DateTime? data)
        {
            data = null;
            epErros.SetError(caixa, "");

            string digitos = caixa.Text.Replace("/", "").Replace("_", "").Trim();
            if (digitos.Length == 0)
            {
                if (obrigatoria)
                {
                    epErros.SetError(caixa, "Informe a data.");
                    return false;
                }
                return true;
            }

            DateTime convertida;
            if (!DateTime.TryParseExact(caixa.Text, FormatoData, CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out convertida))
            {
                epErros.SetError(caixa, "Data inválida. Use o formato dd/mm/aaaa.");
                return false;
            }

            data = convertida;
            return true;
        }

        private bool ValidarContato()
        {
            if (!EmprestimoService.ContatoValido(txtContato.Text))
            {
                epErros.SetError(txtContato, "Informe um telefone com DDD ou um e-mail válido.");
                return false;
            }

            epErros.SetError(txtContato, "");
            return true;
        }

        private void txtItem_Validating(object sender, CancelEventArgs e)
        {
            ValidarObrigatorio(txtItem, "Informe o item emprestado.");
        }

        private void txtNomeAmigo_Validating(object sender, CancelEventArgs e)
        {
            ValidarObrigatorio(txtNomeAmigo, "Informe o nome do amigo.");
        }

        private void txtContato_Validating(object sender, CancelEventArgs e)
        {
            ValidarContato();
        }

        private void mtbDataEmprestimo_Validating(object sender, CancelEventArgs e)
        {
            DateTime? data;
            LerData(mtbDataEmprestimo, true, out data);
        }

        private void mtbDataPrevista_Validating(object sender, CancelEventArgs e)
        {
            DateTime? data;
            LerData(mtbDataPrevista, false, out data);
        }

        // ----- salvar -----

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            DateTime? dataEmprestimo;
            DateTime? dataPrevista;

            // roda todas para marcar todos os erros de uma vez
            bool itemOk = ValidarObrigatorio(txtItem, "Informe o item emprestado.");
            bool amigoOk = ValidarObrigatorio(txtNomeAmigo, "Informe o nome do amigo.");
            bool contatoOk = ValidarContato();
            bool emprestimoOk = LerData(mtbDataEmprestimo, true, out dataEmprestimo);
            bool previstaOk = LerData(mtbDataPrevista, false, out dataPrevista);

            // foco no primeiro campo com problema
            if (!itemOk) { txtItem.Focus(); return; }
            if (!amigoOk) { txtNomeAmigo.Focus(); return; }
            if (!contatoOk) { txtContato.Focus(); return; }
            if (!emprestimoOk) { mtbDataEmprestimo.Focus(); return; }
            if (!previstaOk) { mtbDataPrevista.Focus(); return; }

            Itens novo = new Itens();
            novo.setItem(txtItem.Text);
            novo.setNomeAmigo(txtNomeAmigo.Text);
            novo.setContato(txtContato.Text);
            novo.setDataEmprestimo(dataEmprestimo.Value);
            novo.setDataDevolucaoPrevista(dataPrevista);

            try
            {
                servico.Registrar(novo);
                this.DialogResult = DialogResult.OK;
            }
            catch (NegocioException ex)
            {
                MessageBox.Show(ex.Message, "Verifique os dados", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (ApplicationException ex)
            {
                MessageBox.Show(ex.Message, "Quem Pegou?", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormNovoEmprestimo_Load(object sender, EventArgs e)
        {

        }
    }
}
