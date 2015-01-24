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
            AlvoConfig.Text = Utils.GetIsoSettingsAsString("config.alvo");
            EmailConfigTextBox.Text = Utils.GetIsoSettingsAsString("config.email");
            AlvoAnualConfig.Text = Utils.GetIsoSettingsAsString("config.alvoAnual");
            if (AlvoConfig.Text.Equals("") || AlvoConfig.Text == null)
            {
                AlvoConfig.Text = "00";
            }
            if (AlvoAnualConfig.Text.Equals("") || AlvoAnualConfig.Text == null)
            {
                AlvoAnualConfig.Text = "00";
            }
            AlvoConfig.KeyDown += new KeyEventHandler(numercicTextBox_KeyDown);
            AlvoAnualConfig.KeyDown += new KeyEventHandler(numercicTextBox_KeyDown);


        }

        private void SalcarConfigButton_Click(object sender, EventArgs e)
        {
            if (AlvoConfig.Text.Equals(""))
            {
                AlvoConfig.Text = "00";
            }
            if (AlvoAnualConfig.Text.Equals("") || AlvoAnualConfig.Text == null)
            {
                AlvoAnualConfig.Text = "00";
            }
            Utils.AddToISOSettings("config.alvo", AlvoConfig.Text);
            Utils.AddToISOSettings("config.alvoAnual", AlvoAnualConfig.Text);
            Utils.AddToISOSettings("config.email", EmailConfigTextBox.Text);
        }

        void numercicTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "[0-9]"))
                e.Handled = false;
            else e.Handled = true;

        }


       
    }
}