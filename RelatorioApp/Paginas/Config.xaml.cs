using System;
using Microsoft.Phone.Controls;
using RelatorioLibrary.Util;
using System.Windows.Input;

namespace Relatorio2._0.Paginas
{
    public partial class PivotPage1 : PhoneApplicationPage
    {
        public PivotPage1()
        {
            InitializeComponent();
            AlvoConfig.KeyDown += new KeyEventHandler(numercicTextBox_KeyDown);
        }

        private void SalcarConfigButton_Click(object sender, EventArgs e)
        {
            Utils.AddToISOSettings("config.alvo", AlvoConfig.Text);
        }

        void numercicTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "[0-9]"))
                e.Handled = false;
            else e.Handled = true;

        }

       
    }
}