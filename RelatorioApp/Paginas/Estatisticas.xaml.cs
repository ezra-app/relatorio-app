using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using RelatorioLibrary.Util;
using Horas.DataBase.Repository;
using Horas.Model;


namespace RelatorioApp.Paginas
{
    public partial class Page2 : PhoneApplicationPage
    {
        public Page2()
        {

            InitializeComponent();

            String alvoAnual = Utils.GetIsoSettingsAsString("config.alvoAnual");
            int mesInicial = 9;
            int anoRef = DateTime.Now.Year;
            if (DateTime.Now.Month >= mesInicial)
            {
                anoRef = DateTime.Now.Year + 1;
            }

            Titulo.Text += anoRef;
            if(alvoAnual != null && alvoAnual != ""){
                TimeSpan alvoAnualTs = new TimeSpan(Utils.ConvertToInt(alvoAnual), 0,0);

                RelatorioRepository relatorioRepository = new RelatorioRepository();
                Relatorio relatorio = relatorioRepository.GetRelatorioTotalAno();
                Relatorio relatorioMes = relatorioRepository.GetRelatorioTotalMes(DateTime.Now);

                int horasTotaisEmMin = (relatorio.Horas * 60) + relatorio.Minutos;
                int horasMesEmMinutos = (relatorioMes.Horas * 60) + relatorioMes.Minutos;
                int alvoAnualEmMin = Utils.ConvertToInt(alvoAnual) * 60;

                int mesesQueFaltam = 0;
                if (anoRef == DateTime.Now.Year)
                {
                    mesesQueFaltam = mesInicial - DateTime.Now.Month;
                }
                else
                {
                    mesesQueFaltam = mesInicial + (12 - DateTime.Now.Month);
                }

                /*
                if (DateTime.Now.Month != mesInicial - 1)
                {
                    mesesQueFaltam--;
                }
                 * */

                int faltamEmMinutos = alvoAnualEmMin - horasTotaisEmMin;
                if (faltamEmMinutos > 0)
                {
                    int porMes = (faltamEmMinutos + horasMesEmMinutos) / mesesQueFaltam;
                    FaltamLabel.Text += Utils.FormatTime(new TimeSpan(0, faltamEmMinutos, 0)) + " hrs";
                    PorMesLabel.Text += Utils.FormatTime(new TimeSpan(0, porMes , 0)) + " hrs";
                }
                else
                {
                    FaltamLabel.Text = "   Parabéns você já fechou o ano!";
                    PorMesLabel.Text = "  ";
                }

                TrabalhadasLabel.Text += relatorio.GetFormatedTime() + " hrs";
                RequisitoLabel.Text += alvoAnual + ":00 hrs";

                totaisRevistas.Text += relatorio.Revistas.ToString() + "  (" + relatorio.Revistas / (mesesQueFaltam == 12 ? 1 : 12 - mesesQueFaltam) + "/Mês)";
                totaisRevisitas.Text += relatorio.Revisitas.ToString() + "  (" + relatorio.Revisitas / (mesesQueFaltam == 12 ? 1 : 12 - mesesQueFaltam) + "/Mês)";
                totaisLivros.Text += relatorio.Livros.ToString() + "  (" + relatorio.Livros / (mesesQueFaltam == 12 ? 1 : 12 - mesesQueFaltam) + "/Mês)"; ;
                totaisBrochuras.Text += relatorio.Brochuras.ToString() + "  (" + relatorio.Brochuras / (mesesQueFaltam == 12 ? 1 : 12 - mesesQueFaltam) + "/Mês)";
                totaisFolhetos.Text += relatorio.Folhetos.ToString() + "  (" + relatorio.Folhetos / (mesesQueFaltam == 12 ? 1 : 12 - mesesQueFaltam) + "/Mês)";

                if (mesesQueFaltam == 12)
                {
                    horasPorMes.Text += relatorio.Horas + "/Mês";
                }
                else
                {
                    horasPorMes.Text += relatorio.Horas / (12 - mesesQueFaltam) + "/Mês";
                }
        
            }
            
        }
    }
}