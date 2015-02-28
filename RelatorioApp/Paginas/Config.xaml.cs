using System;
using Microsoft.Phone.Controls;
using RelatorioLibrary.Util;
using System.Windows.Input;
using System.Collections.Generic;

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
            loadDiasSemanaTrabalho();

        }

        private void SalcarConfigButton_Click(object sender, EventArgs e)
        {

        }

        void numercicTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "[0-9]"))
                e.Handled = false;
            else e.Handled = true;

        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
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
            saveDiasSemanaTrabalho();
        }

        private void saveDiasSemanaTrabalho()
        {
            List<DayOfWeek> diasSemanaTrabalho = new List<DayOfWeek>();
            if (SegCheckBox.IsChecked == true)
                diasSemanaTrabalho.Add(DayOfWeek.Monday);
            if (TerCheckBox.IsChecked == true)
                diasSemanaTrabalho.Add(DayOfWeek.Tuesday);
            if (QuaCheckBox.IsChecked == true)
                diasSemanaTrabalho.Add(DayOfWeek.Wednesday);
            if (QuiCheckBox.IsChecked == true)
                diasSemanaTrabalho.Add(DayOfWeek.Thursday);
            if (SexCheckBox.IsChecked == true)
                diasSemanaTrabalho.Add(DayOfWeek.Friday);
            if (SabCheckBox.IsChecked == true)
                diasSemanaTrabalho.Add(DayOfWeek.Saturday);
            if (DomCheckBox.IsChecked == true)
                diasSemanaTrabalho.Add(DayOfWeek.Sunday);

            Utils.AddToISOSettings("config.diasSemanaTrab", diasSemanaTrabalho);
        }

        private void loadDiasSemanaTrabalho()
        {
            List<DayOfWeek> diasSemanaTrabalho = (List<DayOfWeek>) Utils.GetIsoSettings("config.diasSemanaTrab");
            if (diasSemanaTrabalho != null && (diasSemanaTrabalho.Count > 0))
            {
                if (!diasSemanaTrabalho.Contains(DayOfWeek.Monday))
                    SegCheckBox.IsChecked = false;
                if (!diasSemanaTrabalho.Contains(DayOfWeek.Tuesday))
                    TerCheckBox.IsChecked = false;
                if (!diasSemanaTrabalho.Contains(DayOfWeek.Wednesday))
                    QuaCheckBox.IsChecked = false;
                if (!diasSemanaTrabalho.Contains(DayOfWeek.Thursday))
                    QuiCheckBox.IsChecked = false;
                if (!diasSemanaTrabalho.Contains(DayOfWeek.Friday))
                    SexCheckBox.IsChecked = false;
                if (!diasSemanaTrabalho.Contains(DayOfWeek.Saturday))
                    SabCheckBox.IsChecked = false;
                if (!diasSemanaTrabalho.Contains(DayOfWeek.Sunday))
                    DomCheckBox.IsChecked = false;
            }
           
        }

        private void SegCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void TerCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void QuaCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void QuiCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void SexCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void SabCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void DomCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
        {

        }
       
    }
}