using System;
using System.Data.Linq.Mapping;

namespace Horas.Model
{
    [Table(Name = "estudo")]
    public class Estudo
    {
        public Estudo(String NomeEstudante, DateTime Data, int Qtd)
        {
            this.NomeEstudante = NomeEstudante;
            this.Data = Data;
            this.Qtd = Qtd;
        }

        public Estudo(){}

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", AutoSync = AutoSync.OnInsert, CanBeNull = true)]
        public int Id { get; set; }

        [Column(CanBeNull = true)]
        public String NomeEstudante { get; set; }

        [Column(CanBeNull = false)]
        public DateTime Data { get; set; }

        [Column(CanBeNull = true)]
        public int Qtd { get; set; }
    }
}
