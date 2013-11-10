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
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace WPLocalDatabase
{
    [Table]
    public class Country
    {
        private EntitySet<City> citiesRef;

        public Country()
        {
            this.citiesRef = new EntitySet<City>(this.OnCityAdded, this.OnCityRemoved);
        }

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID
        {
            get;
            set;
        }

        [Column(CanBeNull = false)]
        public string Name
        {
            get;
            set;
        }

        [Association(Name = "FK_Country_Cities", Storage = "citiesRef", ThisKey = "ID", OtherKey = "CountryID")]
        public EntitySet<City> Cities
        {
            get
            {
                return this.citiesRef;
            }
        }

        private void OnCityAdded(City city)
        {
            city.Country = this;
        }

        private void OnCityRemoved(City city)
        {
            city.Country = null;
        }
    }
}
