using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Horas.DataBase.Repository;
using Horas.Model;
using System.Windows.Media;
using RelatorioLibrary.Util;

namespace Relatorio2._0.Paginas
{
    public partial class RelatorioDet : PhoneApplicationPage
    {
        private DateTime date = DateTime.Now;
        private IList<long> ids = new List<long>();
        RelatorioRepository relatorioRepository = new RelatorioRepository();
        public RelatorioDet()
        {
            InitializeComponent();
            //InitializeAllComponents();
        }


        private void InitializeAllComponents()
        {
            IList<Relatorio> items = relatorioRepository.ListaPorData(date);
            foreach (Relatorio rel in items)
            {
                rel.SetFormatedRelatorio();
            }
            DetalheListBox.ItemsSource = items;

            //DateTime now = DateTime.Now;
            String nomeMes = Utils.formatarNomeMes(date);
            nomeMes = nomeMes.Substring(0, 1).ToUpper() + nomeMes.Substring(1);
            MesTitulo.Text = nomeMes;
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string data;
            if(NavigationContext.QueryString.TryGetValue("data", out data))
            {
                date = Convert.ToDateTime(data);
            }
            InitializeAllComponents();
        }

        private void AppBarDetDelButton_Click(object sender, EventArgs e)
        {
            foreach (long id in ids)
            {
                relatorioRepository.Delete(id);
            }
            ids.Clear();
            InitializeAllComponents();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = ((CheckBox)sender);
            checkBox.Background = new SolidColorBrush(Colors.Red);
            string id = checkBox.CommandParameter.ToString();
            ids.Add(Convert.ToInt64(id));
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = ((CheckBox)sender);
            checkBox.Background = new SolidColorBrush(Colors.Blue);
            string idStr = checkBox.CommandParameter.ToString();
            ids.Remove(Convert.ToInt64(idStr));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            CustomMessageBox messageBox = new CustomMessageBox();
            messageBox.Message = "Tem certeza que quer apagar?";
            messageBox.LeftButtonContent = "SIM";
            messageBox.RightButtonContent = "NAO";

            messageBox.Dismissed += (s1, e1) =>
            {
                switch (e1.Result)
                {
                    case CustomMessageBoxResult.LeftButton:
                       Button button = ((Button)sender);
                        string idStr = button.CommandParameter.ToString();
                        relatorioRepository.Delete(Convert.ToInt64(idStr));
                        InitializeAllComponents();
                        break;
                    case CustomMessageBoxResult.RightButton:

                        break;
                    case CustomMessageBoxResult.None:
                        // Do something.
                        break;
                    default:
                        break;
                }
            };

            messageBox.Show();

            
        }
    }
}