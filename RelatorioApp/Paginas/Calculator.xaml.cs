using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace RelatorioApp.Paginas
{
    public partial class Page1 : PhoneApplicationPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void b1_Click(object sender, RoutedEventArgs e)
        {
            textCalc.Text += "1";
        }

        private void b2_Click(object sender, RoutedEventArgs e)
        {
            textCalc.Text += "2";
        }

        private void bPoint_Click(object sender, RoutedEventArgs e)
        {
            textCalc.Text += ":";
        }

        private void b3_Click(object sender, RoutedEventArgs e)
        {
            textCalc.Text += "3";
        }

        private void b4_Click(object sender, RoutedEventArgs e)
        {
            textCalc.Text += "4";
        }

        private void b5_Click(object sender, RoutedEventArgs e)
        {
            textCalc.Text += "5";
        }

        private void b6_Click(object sender, RoutedEventArgs e)
        {
            textCalc.Text += "6";
        }

        private void b7_Click(object sender, RoutedEventArgs e)
        {
            textCalc.Text += "7";
        }

        private void b8_Click(object sender, RoutedEventArgs e)
        {
            textCalc.Text += "8";
        }

        private void b9_Click(object sender, RoutedEventArgs e)
        {
            textCalc.Text += "9";
        }

        private void b0_Click(object sender, RoutedEventArgs e)
        {
            textCalc.Text += "0";
        }
    }
}