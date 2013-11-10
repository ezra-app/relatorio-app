using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace RelatorioLibrary.Model
{
    [Table(Name = "config")]
    class Config
    {
        [Column(Name = "horas")]
        String alvo { get; set; }
    }
}
