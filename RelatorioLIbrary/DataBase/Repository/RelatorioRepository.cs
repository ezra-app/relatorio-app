﻿using Horas.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace Horas.DataBase.Repository
{
    public class RelatorioRepository
    {
        public IList<Relatorio> Lista()
        {
            IList<Relatorio> relatorioList = null;
            using (RelatorioDataBaseContext context = new RelatorioDataBaseContext(RelatorioDataBaseContext.ConnectionString))
            {
                try
                {
                    IQueryable<Relatorio> query = from c in context.RelatorioTable select c;
                    relatorioList = query.ToList();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                    Debug.WriteLine(e.Message);
                    MessageBox.Show("PROBLEMA NA LISTAGEM! ");
                }
            }

            return relatorioList;
        }

        public IList<Relatorio> ListaPorData(DateTime data)
        {
            IList<Relatorio> relatorioList = new List<Relatorio>();
            using (RelatorioDataBaseContext context = new RelatorioDataBaseContext(RelatorioDataBaseContext.ConnectionString))
            {
                try
                {
                    IQueryable<Relatorio> query =
                        from c in context.RelatorioTable
                        where c.Data.Month == data.Month && c.Data.Year == data.Year
                        orderby c.Data descending
                        select c;
                    relatorioList = query.ToList();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                    Debug.WriteLine(e.Message);
                    MessageBox.Show("PROBLEMA NA LISTAGEM POR DATA");
                }
            }

            return relatorioList;
        }

        public IList<Relatorio> Delete(long Id)
        {
            IList<Relatorio> relatorioList = null;
            using (RelatorioDataBaseContext context = new RelatorioDataBaseContext(RelatorioDataBaseContext.ConnectionString))
            {
                try
                {
                    IQueryable<Relatorio> query =
                        from c in context.RelatorioTable
                        where c.Id == Id
                        select c;
                    Relatorio relatorioToDelete = query.FirstOrDefault();
                    context.RelatorioTable.DeleteOnSubmit(relatorioToDelete);
                    context.SubmitChanges();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                    Debug.WriteLine(e.Message);
                    MessageBox.Show("PROBLEMA NA LISTAGEM POR DATA");
                }
            }

            return relatorioList;
        }

        public Relatorio GetRelatorioTotalMes()
        {
            IList<Relatorio> relatorios = ListaPorData(DateTime.Now);

            Relatorio relatorioTotal = null;
            long totalRevistas = 0;
            long totalRevisistas = 0;
            long totalBrochuras = 0;
            long totalLivros = 0;
            int totalHoras = 0;
            int totalMinutos = 0;

            foreach (Relatorio r in relatorios)
            {
                totalHoras += r.Horas;
                totalMinutos += r.Minutos;
                totalRevisistas += r.Revisitas;
                totalRevistas += r.Revistas;
                totalLivros += r.Livros;
                totalBrochuras += r.Brochuras;
            }

            relatorioTotal = new Relatorio(totalHoras, totalMinutos, totalRevistas,
                totalRevisistas, totalBrochuras, totalLivros, DateTime.Now);

            return relatorioTotal;
        }

        public Relatorio GetRelatorioTotalMes(DateTime date)
        {
            IList<Relatorio> relatorios = ListaPorData(date);

            Relatorio relatorioTotal = null;
            long totalRevistas = 0;
            long totalRevisistas = 0;
            long totalBrochuras = 0;
            long totalLivros = 0;
            int totalHoras = 0;
            int totalMinutos = 0;

            foreach (Relatorio r in relatorios)
            {
                totalHoras += r.Horas;
                totalMinutos += r.Minutos;
                totalRevisistas += r.Revisitas;
                totalRevistas += r.Revistas;
                totalLivros += r.Livros;
                totalBrochuras += r.Brochuras;
            }

            relatorioTotal = new Relatorio(totalHoras, totalMinutos, totalRevistas,
                totalRevisistas, totalBrochuras, totalLivros, DateTime.Now);

            return relatorioTotal;
        }

        public void Add(Relatorio relatorio)
        {
            using (RelatorioDataBaseContext context =
                new RelatorioDataBaseContext(RelatorioDataBaseContext.ConnectionString))
            {
                try
                {
                    context.RelatorioTable.InsertOnSubmit(relatorio);
                    context.SubmitChanges();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                    Debug.WriteLine(e.Message);
                    MessageBox.Show("PROBLEMA AO ADICIONAR! ");
                }

            }
        }

        public void Clear()
        {
            using (RelatorioDataBaseContext context =
                new RelatorioDataBaseContext(RelatorioDataBaseContext.ConnectionString))
            {
                try
                {
                    context.DeleteDatabase();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                    Debug.WriteLine(e.Message);
                    MessageBox.Show("PROBLEMA AO APAGAR! ");
                }

            }
        }

    }
}
