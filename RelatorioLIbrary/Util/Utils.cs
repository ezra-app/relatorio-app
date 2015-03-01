using Horas.Model;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace RelatorioLibrary.Util
{
    public class Utils
    {
        public static int GetMinutesFromTimeString(string value)
        {
            try
            {
                return Convert.ToInt32(value.Substring(value.IndexOf(":") + 1, 2));
            }
            catch (Exception)
            {
                return 0;
            }
           
        }

        public static int GetHourFromTimeString(string value)
        {
            try
            {
                return Convert.ToInt32(value.Substring(0, value.IndexOf(":")));
            }
            catch (Exception)
            {
                return 0;
            }
            
        }

        public static string FormatTime(TimeSpan ts)
        {
            return string.Format("{0:00}:", (ts.Days * 24 + ts.Hours)) + string.Format("{0:00}", (ts.Minutes));
        }

        public static String GetTotalHorasAsString(IList<Relatorio> relatorios)
        {
            TimeSpan totalHoras = new TimeSpan();
            foreach (Relatorio r in relatorios)
            {
                totalHoras = totalHoras.Add(new TimeSpan(r.Horas, r.Minutos, 0));
            }
            return Utils.FormatTime(totalHoras);
        }

        public static TimeSpan GetTimeSpanFromTimePicker(TimePicker timePicker)
        {
            int hours = GetHourFromTimeString(timePicker.ValueString);
            int minutes = GetMinutesFromTimeString(timePicker.ValueString);
            TimeSpan timeSpan = new TimeSpan(hours, minutes, 0);
            return timeSpan;
        }

        public static long ConvertToLong(String value)
        {
            if (value != null)
            {
                return Convert.ToInt64(value.Equals("") ? "0" : value);
            }
            return 0;
        }

        public static int ConvertToInt(String value)
        {
            if (value != null)
            {
                return Convert.ToInt32(value.Equals("") ? "0" : value);
            }
            return 0;
        }

        public static String formatarNomeMes(DateTime now)
        {
            String nomeMes = String.Format("{0:MMMM}", now) + " " + now.Year.ToString();
            return nomeMes.Substring(0, 1).ToUpper() + nomeMes.Substring(1);
        }

        public static String formatarNomeSemana(DateTime now)
        {
            String nomeSemana = String.Format("{0:ddd}", now) + ", dia " + String.Format("{0:dd}", now);
            return nomeSemana.Substring(0, 1).ToUpper() + nomeSemana.Substring(1);
        }

        public static void AddToISOSettings(String key, String value)
        {
            IsolatedStorageSettings iso = IsolatedStorageSettings.ApplicationSettings;
            if (iso.Contains(key))
            {
                iso[key] = value;
            }
            else //Cria novas chaves
            {
                iso.Add(key, value);
            }
            iso.Save();
        }

        public static void AddToISOSettings(String key, object value)
        {
            IsolatedStorageSettings iso = IsolatedStorageSettings.ApplicationSettings;
            if (iso.Contains(key))
            {
                iso[key] = value;
            }
            else //Cria novas chaves
            {
                iso.Add(key, value);
            }
            iso.Save();
        }

        public static String GetIsoSettingsAsString(String key)
        {
            IsolatedStorageSettings iso = IsolatedStorageSettings.ApplicationSettings;
            String value;
            if (iso.TryGetValue<string>(key, out value))
            {
                return value;
            }
            return "";
        }

        public static object GetIsoSettings(String key)
        {
            return IsolatedStorageSettings.ApplicationSettings.Contains(key) ? IsolatedStorageSettings.ApplicationSettings[key] : null;
        }

        public static void teste(String key)
        {
           
        }

        public void InitializeTextBoxFocus(TextBox textbox, String gotFocus, String lostFocus)
        {
            if (FocusManager.GetFocusedElement() != textbox)
            {
                textbox.Text = lostFocus;
            }
            else
            {
                textbox.Text = gotFocus;
            }

            textbox.KeyDown += new KeyEventHandler(numercicTextBox_KeyDown);

        }

        void numercicTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(e.Key.ToString(), "[0-9]"))
                e.Handled = false;
            else e.Handled = true;

        }

    }
}
