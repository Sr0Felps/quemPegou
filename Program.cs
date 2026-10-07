using System;
using System.Threading;
using System.Windows.Forms;
using quemPegou.Forms;

namespace quemPegou
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Último recurso: se acontecer um erro que ninguém previu, mostra uma
            // mensagem simples em vez da janela padrão do .NET (que exibe a pilha de erro)
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += TratarErroInesperado;

            Application.Run(new FormPrincipal());
        }

        private static void TratarErroInesperado(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show("Ocorreu um erro inesperado. Tente novamente; se continuar, feche e abra o programa.",
                "Quem Pegou?", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
