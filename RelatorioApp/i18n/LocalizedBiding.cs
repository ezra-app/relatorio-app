using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace RelatorioApp.i18n
{
    public partial class LocalizedBinding : Binding
    { 
    public LocalizedBinding() { Source = Application.Current.Resources["LocalizedStrings"]; }
    private string _lc;
    public string LC
    {
        get { return _lc; }
        set
        {
            _lc = value;
            Path = new PropertyPath(string.Format(@"AppResources.{0}", value));
        }
    }
}
}
