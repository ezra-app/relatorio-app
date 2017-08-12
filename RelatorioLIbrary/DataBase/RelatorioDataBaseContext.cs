﻿
using Horas;
using Horas.DataBase.Repository;
using Horas.Model;
using Microsoft.Phone.Data.Linq;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Diagnostics;
using System.Windows;

namespace Horas.DataBase
{
    public class RelatorioDataBaseContext : DataContext
    {
        //private const string ConnectionString = @"isostore:/Relatorio2.sdf";
        public static string ConnectionString = "Data Source=isostore:/Tese2.sdf";
        private static int DB_VERSION = 4;

        public RelatorioDataBaseContext(string connectionString)
            : base(connectionString)
        {
            if (!this.DatabaseExists())
            {
                try
                {
                    this.CreateDatabase();
                    DatabaseSchemaUpdater dbUpdater = this.CreateDatabaseSchemaUpdater();
                    dbUpdater.DatabaseSchemaVersion = DB_VERSION;
                    dbUpdater.Execute();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.StackTrace);
                    Debug.WriteLine(e.Message);
                    MessageBox.Show("PROBLEMA AO CRIAR BANCO DE DADOS");
                }
            }
            else
            {
                try
                {
                    DatabaseSchemaUpdater dbUpdater = this.CreateDatabaseSchemaUpdater();

                    if (dbUpdater.DatabaseSchemaVersion < DB_VERSION)
                    {
                        if (dbUpdater.DatabaseSchemaVersion == 0)
                        {
                            dbUpdater.AddTable<Estudo>();
                            dbUpdater.DatabaseSchemaVersion = DB_VERSION;
                            dbUpdater.Execute();
                        }
                        if (dbUpdater.DatabaseSchemaVersion < 2)
                        {
                            dbUpdater.AddColumn<Relatorio>("Folhetos");
                            dbUpdater.DatabaseSchemaVersion = DB_VERSION;
                            dbUpdater.Execute();
                        }
                        if (dbUpdater.DatabaseSchemaVersion < 4)
                        {
                            /*dbUpdater.AddColumn<Relatorio>("Publicacoes");
                            dbUpdater.AddColumn<Relatorio>("Videos");
                            dbUpdater.DatabaseSchemaVersion = DB_VERSION;
                            dbUpdater.Execute();*/

                            RelatorioRepository relatorioRepository = new RelatorioRepository();
                            IList<Relatorio> relatorios = relatorioRepository.Lista();

                            foreach(Relatorio relat in relatorios)
                            {
                                relat.Publicacoes = relat.Livros + relat.Revistas + relat.Folhetos;
                                relatorioRepository.Update(relat);
                            }

                        }

                    }


                }
                catch (Exception e)
                {
                    //this.DeleteDatabase();
                    Debug.WriteLine(e.StackTrace);
                    Debug.WriteLine(e.Message);
                    MessageBox.Show("PROBLEMA AO ATUALIZAR BANCO DE DADOS");
                }
            }
        }

        public RelatorioDataBaseContext()
            : base(ConnectionString)
        {
            if (!this.DatabaseExists())
            {
                this.CreateDatabase();
            }
        }

        public Table<Relatorio> RelatorioTable
        {
            get
            {
                return this.GetTable<Relatorio>();
            }
        }


        public Table<Estudo> EstudoTable
        {
            get
            {
                return this.GetTable<Estudo>();
            }
        }

    }
}
