using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using RelatorioLibrary.Util;
using Horas.Model;
using Microsoft.Phone.Shell;
using System.IO.IsolatedStorage;
using Horas.DataBase.Repository;

namespace RelatorioApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        private DateTime dateControlFlick = DateTime.Now;
        //private DateTime horaInicialTrabalho = new DateTime(1500, 1, 1);
        private RelatorioRepository relatorioRepository = new RelatorioRepository();
        private EstudoRepository estudoRepository = new EstudoRepository();

        public const String HORA_INI_TRABALHO_KEY = "trabalho.init";
        // Constructor
        public MainPage()
        {
            try
            {
                InitializeComponent();
                InitializeAllComponents();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.StackTrace);
            }
        }

        private void InitializeAllComponents()
        {

            //DateTime now = DateTime.Now;
            MesTitulo.Text = Utils.formatarNomeMes(dateControlFlick);
            if (dateControlFlick.Month == DateTime.Now.Month 
                && dateControlFlick.Year == DateTime.Now.Year)
            {
                DiaTitulo.Text = "Dia: " + dateControlFlick.Day.ToString();
            }
            
            InitializeTextBoxFocus(InputMin, "", "00");
            InitializeTextBoxFocus(InputHora, "", "00");
            InitializeTextBoxFocus(InputRevisitas, "", "0");
            InitializeTextBoxFocus(InputRevistas, "", "0");
            InitializeTextBoxFocus(InputLivros, "", "0");
            InitializeTextBoxFocus(InputBrochuras, "", "0");
            InitializeTextBoxFocus(InputFolhetos, "", "0");

            Relatorio relatorio = relatorioRepository.GetRelatorioTotalMes(dateControlFlick);
            SomaLivros.Text = Convert.ToString(relatorio.Livros);
            SomaRevisitas.Text = Convert.ToString(relatorio.Revisitas);
            SomaRevistas.Text = Convert.ToString(relatorio.Revistas);
            SomaBrochuras.Text = Convert.ToString(relatorio.Brochuras);
            SomaFolhetos.Text = Convert.ToString(relatorio.Folhetos);
            SomaHoras.Text = relatorio.GetFormatedTime();

            Estudo estudosMes = estudoRepository.GetByDate(dateControlFlick);
            if (estudosMes != null)
            {
                SomaEstudos.Text = Convert.ToString(estudosMes.Qtd);
            }
            else
            {
                SomaEstudos.Text = "0";
            }
           

            IsolatedStorageSettings iso = IsolatedStorageSettings.ApplicationSettings;
            String alvo;
            if (iso.TryGetValue<string>("config.alvo", out alvo))
            {
                if (alvo.Equals("") || alvo.Contains(","))
                {
                    alvo = "00";
                }

                MetaTextBlock.Text = alvo + ":00" + " h";
            }

            CaulculeMeta(relatorio);

            if (Utils.GetIsoSettingsAsString(HORA_INI_TRABALHO_KEY) == null)
            {
                ContadorTrabalho.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.ApplicationBar = ((ApplicationBar)this.Resources["IniciadoContAppBar"]);
                ContadorTrabalho.Text = "Trabalho Iniciado as: " + Utils.GetIsoSettingsAsString(HORA_INI_TRABALHO_KEY);
                ContadorTrabalho.Visibility = Visibility.Visible;
            }

        }

        void numercicTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "[0-9]"))
                e.Handled = false;
            else e.Handled = true;

        }

        public void InitializeTextBoxFocus(TextBox textbox, String gotFocus, String lostFocus)
        {
            if (FocusManager.GetFocusedElement() != textbox)
            {
                textbox.Text = lostFocus;
            }
            else
            {
                textbox.Text = gotFocus;
            }

            textbox.KeyDown += new KeyEventHandler(numercicTextBox_KeyDown);

        }

        private void CaulculeMeta(Relatorio relatorio)
        {
            // MetaTextBlock.Text = "70:00" + "hrs";

            TimeSpan metaTs = new TimeSpan(Utils.GetHourFromTimeString(MetaTextBlock.Text),
            Utils.GetMinutesFromTimeString(MetaTextBlock.Text), 0);
            TimeSpan totalMesTs = new TimeSpan(relatorio.Horas, relatorio.Minutos, 0);
            TimeSpan faltamTs = metaTs.Subtract(totalMesTs);

            int faltamEmMinutos = ((faltamTs.Days * 24 * 60) + (faltamTs.Hours * 60) + faltamTs.Minutes);
            if (faltamEmMinutos > 0)
            {
                FaltamTextBlock.Text = Utils.FormatTime(faltamTs) + " h";
            }
            else
            {
                FaltamTextBlock.Text = "-";
            }

            int totalDiasDoMes = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            int porDiaEmMinutos = faltamEmMinutos / ((totalDiasDoMes + 1) - DateTime.Now.Day);
            TimeSpan porDiaTs = new TimeSpan(0, porDiaEmMinutos, 0);
            if (porDiaEmMinutos > 0)
            {
                PorDiaTextBlock.Text = Utils.FormatTime(porDiaTs) + " h";
            }
            else
            {
                PorDiaTextBlock.Text = "-";
            }
        }


        private void MouseEnterTextBoxEvent(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).Text = "";
        }

        private void MouseLeaveTextBoxEvent(object sender, RoutedEventArgs e)
        {
            if (((TextBox)sender).Text == "")
            {
                ((TextBox)sender).Text = "00";
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

        private void AppBarrSalvar_Click(object sender, EventArgs e)
        {
            int horas = Utils.ConvertToInt(InputHora.Text);
            int min = Utils.ConvertToInt(InputMin.Text);
            long revista = Utils.ConvertToLong(InputRevistas.Text);
            long revisita = Utils.ConvertToLong(InputRevisitas.Text);
            long livro = Utils.ConvertToLong(InputLivros.Text);
            long brochura = Utils.ConvertToLong(InputBrochuras.Text);
            int estudosQtd = Utils.ConvertToInt(SomaEstudos.Text);
            long folhetos = Utils.ConvertToLong(InputFolhetos.Text);

            Estudo estudo = estudoRepository.GetByDate(dateControlFlick);
            if (estudo == null)
            {
                estudo = new Estudo(null, dateControlFlick, estudosQtd);
                estudoRepository.Add(estudo);
            }
            else
            {
                if(estudo.Qtd != estudosQtd){
                    estudo.Qtd = estudosQtd;
                    estudoRepository.Update(estudo);
                }
                
            }

            //DateTime data = Convert.ToDateTime(datePicker.ValueString);

            Relatorio relatorio = new Relatorio(horas, min, revista, revisita, brochura, livro, dateControlFlick, folhetos);
            relatorioRepository.Add(relatorio);
            InitializeAllComponents();
            updateTitle();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StartTransitionLeftFadeIn();
            //NavigationService.Navigate(new Uri("/Paginas/MainPage.xaml", UriKind.Relative));
        }

        private void StartTransitionLeftFadeIn()
        {
            //RotateTransition rotatetransition = new RotateTransition();
            // rotatetransition.Mode = RotateTransitionMode.In90Clockwise;

            SlideTransition slideTransition = new SlideTransition();
            slideTransition.Mode = SlideTransitionMode.SlideLeftFadeIn;

            PhoneApplicationPage phoneApplicationPage =
            (PhoneApplicationPage)(((PhoneApplicationFrame)Application.Current.RootVisual)).Content;

            ITransition transition = slideTransition.GetTransition(phoneApplicationPage);
            transition.Completed += delegate
            {
                transition.Stop();
            };
            transition.Begin();
            myStoryboard.Begin();
        }

        private void StartTransitionRigthFadeIn()
        {
            //RotateTransition rotatetransition = new RotateTransition();
            //rotatetransition.Mode = RotateTransitionMode.In90Clockwise;

            SlideTransition slideTransition = new SlideTransition();
            slideTransition.Mode = SlideTransitionMode.SlideRightFadeIn;

            PhoneApplicationPage phoneApplicationPage =
                (PhoneApplicationPage)(((PhoneApplicationFrame)Application.Current.RootVisual)).Content;

            ITransition transition = slideTransition.GetTransition(phoneApplicationPage);
            transition.Completed += delegate
            {
                transition.Stop();
            };
            transition.Begin();
            myStoryboard.Begin();
        }

        private void GestureListener_Flick(object sender, Microsoft.Phone.Controls.FlickGestureEventArgs e)
        {

            if (e.HorizontalVelocity < 0)
            {
                // Load the next
                if (dateControlFlick.Month != DateTime.Now.Month || dateControlFlick.Year != DateTime.Now.Year)
                {
                    rigthMoveStoryboard.Begin();
                    //fadeInOut.Begin();
                    //upStoryboard.Begin();
                    dateControlFlick = dateControlFlick.AddMonths(1);
                    InitializeAllComponents();
                    PrepareComponentesParaMesesPassados();
                    //StartTransitionRigthFadeIn();
                }

            }

            // User flicked towards right
            if (e.HorizontalVelocity > 0)
            {
                // Load the previous
                leftMoveStoryboard.Begin();
                //fadeInOut.Begin();
                //downStoryboard.Begin();
                dateControlFlick = dateControlFlick.AddMonths(-1);
                InitializeAllComponents();
                PrepareComponentesParaMesesPassados();
                //StartTransitionLeftFadeIn();
            }

        }

        private void PrepareComponentesParaMesesPassados()
        {
            if (dateControlFlick.Month != DateTime.Now.Month || dateControlFlick.Year != DateTime.Now.Year)
            {
                MesTitulo.Text = Utils.formatarNomeMes(dateControlFlick);
                DiaTitulo.Text = "";
            }
        }

        private void updateTitle()
        {
            // get application tile
            ShellTile tile = ShellTile.ActiveTiles.First();
            if (null != tile && dateControlFlick.Month == DateTime.Now.Month
                && dateControlFlick.Year == DateTime.Now.Year)
            {
                // create a new data for tile
                StandardTileData data = new StandardTileData();
                // tile foreground data
                data.BackgroundImage = new Uri("/Imagens/title_medio3.png", UriKind.Relative);
                // to make tile flip add data to background also
                data.BackTitle = "  Relatório";
                data.BackContent = "  Hrs.: " + SomaHoras.Text + "\n" + "  R.Avul.: " + SomaRevistas.Text +
                    "\n  Rev.: " + SomaRevisitas.Text + "\n  Liv.: " + SomaLivros.Text + "\n  Bro.: " + SomaBrochuras.Text +
                    "\n  Est.: " + SomaEstudos.Text;
                data.BackBackgroundImage = new Uri("/Imagens/back_backgroung.png", UriKind.Relative);
                // update tile
                tile.Update(data);

                /* Adicionando Live Title Secundaria
                ShellTile tile2 = ShellTile.ActiveTiles.FirstOrDefault(t => t.NavigationUri.ToString().Contains("Config.xaml"));

                if (tile2 == null) { 
                    StandardTileData tileData = new StandardTileData() 
                    { 
                        Title = "Artigos WP"
                    };
                    tileData.BackgroundImage = new Uri("/Imagens/title_medio3.png", UriKind.Relative);
                    ShellTile.Create(new Uri("/Paginas/Config.xaml", UriKind.Relative), tileData);
                }

                */
            }
        }

        private void AppBarApagar_Click(object sender, EventArgs e)
        {
            CustomMessageBox messageBox = new CustomMessageBox();
            messageBox.Message = "Tem certeza que quer apagar tudo?";
            messageBox.LeftButtonContent = "SIM";
            messageBox.RightButtonContent = "NAO";

            messageBox.Dismissed += (s1, e1) =>
            {
                switch (e1.Result)
                {
                    case CustomMessageBoxResult.LeftButton:
                        relatorioRepository.Clear();
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


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Paginas/RelatorioDet.xaml?data=" + dateControlFlick.ToString(), UriKind.Relative));
        }

        private void AppBarConfigButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Paginas/Config.xaml", UriKind.Relative));
        }

        private void LayoutRoot_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeAllComponents();
        }

        private void Iniciar_Click(object sender, EventArgs e)
        {
            if (Utils.GetIsoSettingsAsString(HORA_INI_TRABALHO_KEY) == null)
            {
                this.ApplicationBar = ((ApplicationBar)this.Resources["IniciadoContAppBar"]);
                DateTime now = DateTime.Now;
                Utils.AddToISOSettings(HORA_INI_TRABALHO_KEY, now.ToShortTimeString());
                ContadorTrabalho.Text = "Trabalho Iniciado as: " + now.ToShortTimeString();
                ContadorTrabalho.Visibility = Visibility.Visible;
            }
            else
            {
                this.ApplicationBar = ((ApplicationBar)this.Resources["DefaultAppBar"]);

                DateTime inicialDate = Convert.ToDateTime(Utils.GetIsoSettingsAsString(HORA_INI_TRABALHO_KEY));
                DateTime finalDate = DateTime.Now;
                TimeSpan finalTs = new TimeSpan(finalDate.Hour, finalDate.Minute, 0);
                TimeSpan inicialTs = new TimeSpan(inicialDate.Hour, inicialDate.Minute, 0);
                TimeSpan totalTrabalhadas = finalTs.Subtract(inicialTs);

                CustomMessageBox messageBox = new CustomMessageBox();
                messageBox.Message = "Deseja adicionar " + Utils.FormatTime(totalTrabalhadas) + " ao seu relatório?";
                messageBox.LeftButtonContent = "SIM";
                messageBox.RightButtonContent = "NAO";

                messageBox.Dismissed += (s1, e1) =>
                {
                    switch (e1.Result)
                    {
                        case CustomMessageBoxResult.LeftButton:
                            int horas = totalTrabalhadas.Hours;
                            int min = totalTrabalhadas.Minutes;

                            Relatorio relatorio = new Relatorio(horas, min, 0, 0, 0, 0, finalDate, 0);
                            relatorioRepository.Add(relatorio);
                            Utils.AddToISOSettings(HORA_INI_TRABALHO_KEY, null);
                            InitializeAllComponents();
                            updateTitle();
                            break;
                        case CustomMessageBoxResult.RightButton:
                            Utils.AddToISOSettings(HORA_INI_TRABALHO_KEY, null);
                            ContadorTrabalho.Visibility = Visibility.Collapsed;
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

        private void sobreAppBar_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Paginas/Sobre.xaml", UriKind.Relative));
        }

        private void SubEstudoBt_Click(object sender, RoutedEventArgs e)
        {
            int count = Utils.ConvertToInt(SomaEstudos.Text);
            if (count > 0)
            {
                SomaEstudos.Text = Convert.ToString(--count);
            }

        }

        private void AddEstudoBt_Click(object sender, RoutedEventArgs e)
        {
            int count = Utils.ConvertToInt(SomaEstudos.Text);

            SomaEstudos.Text = Convert.ToString(++count);

        }

        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Paginas/Calculator.xaml", UriKind.Relative));
        }




    }
}