using System;
using System.Data.Linq.Mapping;

namespace Horas.Model
{
    [Table(Name = "estudante")] 
    public class Estudante : EntidadeBase
    {
        //[Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", AutoSync = AutoSync.OnInsert, CanBeNull = true)]
        //public int Id { get; set; }

        [Column(Name = "nome", CanBeNull = false)]
        String Nome { get; set; }

        [Column(Name = "publicacao", CanBeNull = false)]
        String Publicacao { get; set; }

    }
}
