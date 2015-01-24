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
            int mesRef = 9;
            int anoRef = DateTime.Now.Year;
            if (DateTime.Now.Month < mesRef)
            {
                anoRef = DateTime.Now.Year - 1;
            }

            Titulo.Text += anoRef;
            if(alvoAnual != null && alvoAnual != ""){
                TimeSpan alvoAnualTs = new TimeSpan(Utils.ConvertToInt(alvoAnual), 0,0);

                RelatorioRepository relatorioRepository = new RelatorioRepository();
                Relatorio relatorio = relatorioRepository.GetRelatorioTotalAno();

                int horasTotaisEmMin = (relatorio.Horas * 60) + relatorio.Minutos;
                int alvoAnualEmMin = Utils.ConvertToInt(alvoAnual) * 60;

                int mesesQueFaltam = 0;
                if (anoRef == DateTime.Now.Year)
                {
                    mesesQueFaltam = mesRef + (12 - DateTime.Now.Month);
                }
                else
                {
                    mesesQueFaltam = mesRef - DateTime.Now.Month;
                }

                int faltamEmMinutos = alvoAnualEmMin - horasTotaisEmMin;
                if (faltamEmMinutos > 0)
                {
                    int porMes = faltamEmMinutos / mesesQueFaltam;
                    FaltamLabel.Text += Utils.FormatTime(new TimeSpan(0, faltamEmMinutos, 0)) + " hrs";
                    PorMesLabel.Text += Utils.FormatTime(new TimeSpan(0, porMes, 0)) + " hrs";
                }
                else
                {
                    FaltamLabel.Text = "   Parabéns você já fechou o ano!";
                }

                TrabalhadasLabel.Text += relatorio.GetFormatedTime() + " hrs";
                RequisitoLabel.Text += alvoAnual + ":00 hrs";

            }
            
        }
    }
}