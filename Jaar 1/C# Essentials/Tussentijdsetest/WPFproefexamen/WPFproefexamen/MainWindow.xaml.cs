using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPFproefexamen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Declaratie variabelen
        string output;
        double totaalprijs;
        DateTime temp;
        DispatcherTimer dispatcher;
        public MainWindow()
        {
            InitializeComponent();

            //Kiest 50/50 achtergrondkleur op start
            RandomBackground();
           
            dispatcher = new DispatcherTimer();

            //elke seconde time updaten
            dispatcher.Interval = TimeSpan.FromSeconds(1);

           
            //functie die tijd update elke seconde
            dispatcher.Tick += SetTime;

            //start als mainwindow geladen is
            Loaded += StartTimer;
        }

        private void SetTime(object sender, EventArgs e)
        {
            temp = DateTime.Now;
            TBL_Time.Text = temp.ToLongTimeString();
        }

        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            
            output = output + $"{TB_Hoeveelheid.Text} x {TB_Naam.Text}\n";
            TB_Output.Text = output;
            totaalprijs = totaalprijs + (int.Parse(TB_Hoeveelheid.Text) * double.Parse(TB_Prijs.Text));
        }
        private string KaderResultaat(double prijs)
        {
            string lengtecheck = Convert.ToString(prijs);
            int lengte = lengtecheck.Length;
            string eindresultaat = "***";
            for (int i = 0; i < lengte; i++)
            {
                eindresultaat = eindresultaat + "*";
            }
            eindresultaat = eindresultaat + $"**\n" + $"* €{prijs} *\n" + "***";
            for (int i = 0; i < lengte; i++)
            {
                eindresultaat = eindresultaat + "*";
            }
            eindresultaat = eindresultaat + "**";
            return eindresultaat;
        }

        private void Button_Checkout_Click(object sender, RoutedEventArgs e)
        {
            
            TB_Output.Text = TB_Output.Text + $"\n" + KaderResultaat(totaalprijs) + $"\n\n" + "PXL-Shop";
        }

        private void Button_Bestelling_Click(object sender, RoutedEventArgs e)
        {
            TB_Naam.Text = "";
            TB_Prijs.Text = "";
            TB_Hoeveelheid.Text = "";
            TB_Output.Text = "";
            output = "";
            totaalprijs = 0;
            
        }
        private void RandomBackground()
        {
            Random coinToss = new Random();
            int coinSide = coinToss.Next(1,3);
            if (coinSide==1)
            {
                Grid_Main.Background = Brushes.LimeGreen;
            }
            else
            {
                Grid_Main.Background = Brushes.LightGreen;
            }
        }
        private void StartTimer(object sender, EventArgs e)
        {
            dispatcher.Start();
        }
    }
}
