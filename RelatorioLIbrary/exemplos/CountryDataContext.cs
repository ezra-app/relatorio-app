using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Data.Linq;

namespace WPLocalDatabase
{
    public class CountryDataContext : DataContext
    {
        public CountryDataContext(string connectionString)
            : base(connectionString)
        {
        }

        public Table<Country> Countries
        {
            get
            {
                return this.GetTable<Country>();
            }
        }

        public Table<City> Cities
        {
            get
            {
                return this.GetTable<City>();
            }
        }
    }
}
