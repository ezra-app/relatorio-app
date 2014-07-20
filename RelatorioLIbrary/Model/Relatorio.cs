using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using RelatorioLibrary.Util;


namespace Horas.Model
{
    [Table(Name = "relatorio")]
    public class Relatorio
    {
        public Relatorio(int Horas, int Minutos, long Revistas,
            long Revisitas, long Brochuras, long Livros, DateTime Data, long Folhetos)
        {
            this.Horas = Horas;
            this.Minutos = Minutos;
            this.Revisitas = Revisitas;
            this.Revistas = Revistas;
            this.Brochuras = Brochuras;
            this.Livros = Livros;
            this.Data = Data;
            this.Folhetos = Folhetos;
            SetFormatedRelatorio();
        }

        public Relatorio() 
        {
            SetFormatedRelatorio();
        }

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", AutoSync = AutoSync.OnInsert, CanBeNull = false)]
        public int Id { get; set; }

        [Column(Name = "horas")]
        public int Horas { get; set; }

        [Column(Name = "minutos")]
        public int Minutos { get; set; }

        [Column(Name = "revisitas")]
        public long Revistas { get; set; }

        [Column(Name = "revistas")]
        public long Revisitas { get; set; }

        [Column(Name = "brocuras")]
        public long Brochuras { get; set; }

        [Column(Name = "livros")]
        public long Livros { get; set; }

        [Column(Name = "data_relatorio", CanBeNull = true)]
        public DateTime Data { get; set; }

        [Column(Name = "folhetos", DbType = "bigint DEFAULT 0", CanBeNull = true)]
        public long Folhetos { get; set; }

        public String FormatedRelatorio { get; set; }
        public String FormatedHoras { get; set; }
        public String FormatedDia { get; set; }

        
        //public String FormatedTime { get { return this.FormatedTime; } set { this.FormatedTime = GetFormatedTime(); } }

        public String GetFormatedTime()
        {
            TimeSpan ts = new TimeSpan(this.Horas, this.Minutos, 0);
            //this.FormatedTime = Utils.FormatTime(ts);
            return Utils.FormatTime(ts);
        }

        public void SetFormatedRelatorio()
        {
            TimeSpan ts = new TimeSpan(this.Horas, this.Minutos, 0);
            this.FormatedRelatorio = Utils.FormatTime(ts) + " hrs, " + this.Revistas + " revistas av., " + this.Revisitas + " revisitas, " +
                this.Livros + " livros, " + this.Brochuras + " brochuras.";

            this.FormatedHoras = Utils.FormatTime(ts);

            this.FormatedDia = Utils.formatarNomeSemana(this.Data);
        }
    }
}
