using Horas.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;

namespace Horas.DataBase.Repository
{
    public class EstudoRepository
    {
        public IList<Estudo> Lista()
        {
            IList<Estudo> result = null;
            using (RelatorioDataBaseContext context = new RelatorioDataBaseContext(RelatorioDataBaseContext.ConnectionString))
            {
                try
                {
                    IQueryable<Estudo> query = from c in context.EstudoTable select c;
                    result = query.ToList();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                    Debug.WriteLine(e.Message);
                    MessageBox.Show("PROBLEMA NA LISTAGEM! ");
                }
            }

            return result;
        }

        public Estudo GetByDate(DateTime data)
        {
            IList<Estudo> resultList = new List<Estudo>();
            using (RelatorioDataBaseContext context = new RelatorioDataBaseContext(RelatorioDataBaseContext.ConnectionString))
            {
                try
                {
                    IQueryable<Estudo> query =
                        from c in context.EstudoTable
                        where c.Data.Month == data.Month && c.Data.Year == data.Year
                        orderby c.Data descending
                        select c;
                    resultList = query.ToList();

                    if (resultList != null && resultList.Count() > 0)
                    {
                        return resultList.ElementAt(0);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                    Debug.WriteLine(e.Message);
                    MessageBox.Show("PROBLEMA NA LISTAGEM POR DATA");
                }
            }

            return null;
        }

        public int CountPorData(DateTime data)
        {
            int count = 0;
            using (RelatorioDataBaseContext context = new RelatorioDataBaseContext(RelatorioDataBaseContext.ConnectionString))
            {
                try
                {
                    count =
                        (from c in context.EstudoTable
                        where c.Data.Month == data.Month && c.Data.Year == data.Year
                        select c).Count();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                    Debug.WriteLine(e.Message);
                    MessageBox.Show("PROBLEMA NA LISTAGEM POR DATA");
                }
            }

            return count;
        }


        public void Add(Estudo estudo)
        {
            using (RelatorioDataBaseContext context =
                new RelatorioDataBaseContext(RelatorioDataBaseContext.ConnectionString))
            {
                try
                {
                    context.EstudoTable.InsertOnSubmit(estudo);
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

        public IList<Estudo> Delete(long Id)
        {
            IList<Estudo> relatorioList = null;
            using (RelatorioDataBaseContext context = new RelatorioDataBaseContext(RelatorioDataBaseContext.ConnectionString))
            {
                try
                {
                    IQueryable<Estudo> query =
                        from c in context.EstudoTable
                        where c.Id == Id
                        select c;
                    Estudo toDelete = query.FirstOrDefault();
                    context.EstudoTable.DeleteOnSubmit(toDelete);
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

        public void Update(Estudo estudo)
        {
            Estudo newEstudo = new Estudo(estudo.NomeEstudante, estudo.Data, estudo.Qtd);
            this.Delete(estudo.Id);
            this.Add(newEstudo);
        }

    }
}
