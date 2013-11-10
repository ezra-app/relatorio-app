using System.Data.Linq.Mapping;

namespace Horas.Model
{
    public class EntidadeBase
    {
         [Column(Name = "id", IsPrimaryKey = true, IsDbGenerated = true,
                CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        private long id { get; set; }
    }
}
