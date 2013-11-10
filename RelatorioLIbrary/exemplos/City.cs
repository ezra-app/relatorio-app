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
    public class City
    {
        private Nullable<int> countryID;
        private EntityRef<Country> countryRef = new EntityRef<Country>();

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

        [Column(Storage = "countryID", DbType = "Int")]
        public int? CountryID
        {
            get
            {
                return this.countryID;
            }
            set
            {
                this.countryID = value;
            }
        }

        [Association(Name = "FK_Country_Cities", Storage = "countryRef", ThisKey = "CountryID", OtherKey = "ID", IsForeignKey = true)]
        public Country Country
        {
            get
            {
                return this.countryRef.Entity;
            }
            set
            {
                Country previousValue = this.countryRef.Entity;
                if (((previousValue != value) || (this.countryRef.HasLoadedOrAssignedValue == false)))
                {
                    if ((previousValue != null))
                    {
                        this.countryRef.Entity = null;
                        previousValue.Cities.Remove(this);
                    }
                    this.countryRef.Entity = value;
                    if ((value != null))
                    {
                        value.Cities.Add(this);
                        this.countryID = value.ID;
                    }
                    else
                    {
                        this.countryID = default(Nullable<int>);
                    }
                }
            }
        }
    }
}
