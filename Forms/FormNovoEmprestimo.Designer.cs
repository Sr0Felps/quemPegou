namespace quemPegou.Forms
{
    partial class FormNovoEmprestimo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lblItem = new System.Windows.Forms.Label();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.lblAmigo = new System.Windows.Forms.Label();
            this.txtNomeAmigo = new System.Windows.Forms.TextBox();
            this.lblContato = new System.Windows.Forms.Label();
            this.txtContato = new System.Windows.Forms.TextBox();
            this.lblDataEmprestimo = new System.Windows.Forms.Label();
            this.mtbDataEmprestimo = new System.Windows.Forms.MaskedTextBox();
            this.lblDataPrevista = new System.Windows.Forms.Label();
            this.mtbDataPrevista = new System.Windows.Forms.MaskedTextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.epErros = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.epErros)).BeginInit();
            this.SuspendLayout();
            // 
            // lblItem
            // 
            this.lblItem.Location = new System.Drawing.Point(20, 28);
            this.lblItem.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(253, 20);
            this.lblItem.TabIndex = 0;
            this.lblItem.Text = "&Item emprestado:";
            // 
            // txtItem
            // 
            this.txtItem.Location = new System.Drawing.Point(280, 25);
            this.txtItem.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtItem.MaxLength = 100;
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(252, 22);
            this.txtItem.TabIndex = 1;
            this.txtItem.Validating += new System.ComponentModel.CancelEventHandler(this.txtItem_Validating);
            // 
            // lblAmigo
            // 
            this.lblAmigo.Location = new System.Drawing.Point(20, 78);
            this.lblAmigo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAmigo.Name = "lblAmigo";
            this.lblAmigo.Size = new System.Drawing.Size(253, 20);
            this.lblAmigo.TabIndex = 2;
            this.lblAmigo.Text = "Nome do &amigo:";
            // 
            // txtNomeAmigo
            // 
            this.txtNomeAmigo.Location = new System.Drawing.Point(280, 74);
            this.txtNomeAmigo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNomeAmigo.MaxLength = 100;
            this.txtNomeAmigo.Name = "txtNomeAmigo";
            this.txtNomeAmigo.Size = new System.Drawing.Size(252, 22);
            this.txtNomeAmigo.TabIndex = 3;
            this.txtNomeAmigo.Validating += new System.ComponentModel.CancelEventHandler(this.txtNomeAmigo_Validating);
            // 
            // lblContato
            // 
            this.lblContato.Location = new System.Drawing.Point(20, 127);
            this.lblContato.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblContato.Name = "lblContato";
            this.lblContato.Size = new System.Drawing.Size(253, 20);
            this.lblContato.TabIndex = 4;
            this.lblContato.Text = "&Contato (telefone ou e-mail):";
            // 
            // txtContato
            // 
            this.txtContato.Location = new System.Drawing.Point(280, 123);
            this.txtContato.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtContato.MaxLength = 100;
            this.txtContato.Name = "txtContato";
            this.txtContato.Size = new System.Drawing.Size(252, 22);
            this.txtContato.TabIndex = 5;
            this.txtContato.Validating += new System.ComponentModel.CancelEventHandler(this.txtContato_Validating);
            // 
            // lblDataEmprestimo
            // 
            this.lblDataEmprestimo.Location = new System.Drawing.Point(20, 176);
            this.lblDataEmprestimo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDataEmprestimo.Name = "lblDataEmprestimo";
            this.lblDataEmprestimo.Size = new System.Drawing.Size(253, 20);
            this.lblDataEmprestimo.TabIndex = 6;
            this.lblDataEmprestimo.Text = "Data do &empréstimo:";
            // 
            // mtbDataEmprestimo
            // 
            this.mtbDataEmprestimo.Location = new System.Drawing.Point(280, 172);
            this.mtbDataEmprestimo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtbDataEmprestimo.Mask = "00/00/0000";
            this.mtbDataEmprestimo.Name = "mtbDataEmprestimo";
            this.mtbDataEmprestimo.Size = new System.Drawing.Size(119, 22);
            this.mtbDataEmprestimo.TabIndex = 7;
            this.mtbDataEmprestimo.Validating += new System.ComponentModel.CancelEventHandler(this.mtbDataEmprestimo_Validating);
            // 
            // lblDataPrevista
            // 
            this.lblDataPrevista.Location = new System.Drawing.Point(20, 225);
            this.lblDataPrevista.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDataPrevista.Name = "lblDataPrevista";
            this.lblDataPrevista.Size = new System.Drawing.Size(253, 20);
            this.lblDataPrevista.TabIndex = 8;
            this.lblDataPrevista.Text = "Devolução com&binada (opcional):";
            // 
            // mtbDataPrevista
            // 
            this.mtbDataPrevista.Location = new System.Drawing.Point(280, 222);
            this.mtbDataPrevista.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.mtbDataPrevista.Mask = "00/00/0000";
            this.mtbDataPrevista.Name = "mtbDataPrevista";
            this.mtbDataPrevista.Size = new System.Drawing.Size(119, 22);
            this.mtbDataPrevista.TabIndex = 9;
            this.mtbDataPrevista.Validating += new System.ComponentModel.CancelEventHandler(this.mtbDataPrevista_Validating);
            // 
            // btnSalvar
            // 
            this.btnSalvar.Location = new System.Drawing.Point(280, 277);
            this.btnSalvar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(120, 34);
            this.btnSalvar.TabIndex = 10;
            this.btnSalvar.Text = "&Salvar";
            this.btnSalvar.UseVisualStyleBackColor = true;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.CausesValidation = false;
            this.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancelar.Location = new System.Drawing.Point(413, 277);
            this.btnCancelar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120, 34);
            this.btnCancelar.TabIndex = 11;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = true;
            // 
            // epErros
            // 
            this.epErros.ContainerControl = this;
            // 
            // FormNovoEmprestimo
            // 
            this.AcceptButton = this.btnSalvar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancelar;
            this.ClientSize = new System.Drawing.Size(565, 335);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.mtbDataPrevista);
            this.Controls.Add(this.lblDataPrevista);
            this.Controls.Add(this.mtbDataEmprestimo);
            this.Controls.Add(this.lblDataEmprestimo);
            this.Controls.Add(this.txtContato);
            this.Controls.Add(this.lblContato);
            this.Controls.Add(this.txtNomeAmigo);
            this.Controls.Add(this.lblAmigo);
            this.Controls.Add(this.txtItem);
            this.Controls.Add(this.lblItem);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormNovoEmprestimo";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Novo Empréstimo";
            this.Load += new System.EventHandler(this.FormNovoEmprestimo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.epErros)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblItem;
        private System.Windows.Forms.TextBox txtItem;
        private System.Windows.Forms.Label lblAmigo;
        private System.Windows.Forms.TextBox txtNomeAmigo;
        private System.Windows.Forms.Label lblContato;
        private System.Windows.Forms.TextBox txtContato;
        private System.Windows.Forms.Label lblDataEmprestimo;
        private System.Windows.Forms.MaskedTextBox mtbDataEmprestimo;
        private System.Windows.Forms.Label lblDataPrevista;
        private System.Windows.Forms.MaskedTextBox mtbDataPrevista;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.ErrorProvider epErros;
    }
}
