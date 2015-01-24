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
            if(alvoAnual != null && alvoAnual != ""){
                TimeSpan alvoAnualTs = new TimeSpan(Utils.ConvertToInt(alvoAnual), 0,0);

                RelatorioRepository relatorioRepository = new RelatorioRepository();
                Relatorio relatorio = relatorioRepository.GetRelatorioTotalAno();

                int horasTotaisEmMin = (relatorio.Horas * 60) + relatorio.Minutos;
                int alvoAnualEmMin = Utils.ConvertToInt(alvoAnual) * 60;

                int faltamEmMinutos = alvoAnualEmMin - horasTotaisEmMin;

                if (faltamEmMinutos > 0)
                {
                    FaltamLabel.Text += Utils.FormatTime(new TimeSpan(0, faltamEmMinutos, 0)) + " hrs";
                }
                else
                {
                    FaltamLabel.Text = "   Parabéns você já fechou o ano!";
                }

                TrabalhadasLabel.Text += relatorio.GetFormatedTime();
                RequisitoLabel.Text += alvoAnual + ":00 hrs";

            }
            
        }
    }
}