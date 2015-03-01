using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Horas.Model;
using Horas.DataBase.Repository;
using RelatorioLibrary.Util;

namespace RelatorioApp.Paginas
{
    public partial class Editar : PhoneApplicationPage
    {
        private Relatorio relatorio;
        private RelatorioRepository relatorioRepository = new RelatorioRepository();
        public Editar()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string id;
            if (NavigationContext.QueryString.TryGetValue("relatorioId", out id))
            {
                relatorio = relatorioRepository.get(Convert.ToInt32(id));
                if (relatorio != null)
                {
                    relatorio.SetFormatedRelatorio();
                    Titulo.Text = relatorio.FormatedDia;
                    InputHora.Text = string.Format("{0:00}", relatorio.Horas); ;
                    InputMin.Text = string.Format("{0:00}", relatorio.Minutos);
                    InputRevistas.Text = Convert.ToString(relatorio.Revistas);
                    InputRevisitas.Text = Convert.ToString(relatorio.Revisitas);
                    InputLivros.Text = Convert.ToString(relatorio.Livros);
                    InputBrochuras.Text = Convert.ToString(relatorio.Brochuras);
                    InputFolhetos.Text = Convert.ToString(relatorio.Folhetos);

                }

            }
            

        }

        private void InputBrochuras_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = "";
        }

        private void InputBrochuras_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                ((TextBox)sender).Text = "0";
            }
        }

        private void InputHoras_LostFocus(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                ((TextBox)sender).Text = "00";
            }
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            relatorio.Horas = Convert.ToInt32(InputHora.Text);
            relatorio.Minutos = Convert.ToInt32(InputMin.Text);
            relatorio.Revistas = Convert.ToInt32(InputRevistas.Text);
            relatorio.Revisitas = Convert.ToInt32(InputRevisitas.Text);
            relatorio.Livros = Convert.ToInt32(InputLivros.Text);
            relatorio.Brochuras = Convert.ToInt32(InputBrochuras.Text);
            relatorio.Folhetos = Convert.ToInt32(InputFolhetos.Text);

            relatorioRepository.Update(relatorio);
            NavigationService.GoBack();
        }
    }
}