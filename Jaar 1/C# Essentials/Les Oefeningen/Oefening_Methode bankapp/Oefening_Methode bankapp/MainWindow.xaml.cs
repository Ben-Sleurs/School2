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

namespace Oefening_Methode_bankapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Wissen_Click(object sender, RoutedEventArgs e)
        {
            TB_Naam.Text = "";
            TB_Kapitaal.Text = "";
            TB_Rente.Text = "";
            TB_VolgendJaar.Text = "";
            TB_Kapitaal.Background = Brushes.White;
        }

        private void Button_Afsluiten_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private bool IsGroterDanNul(double getal)
        {
            if (getal>=0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private string BerekenRente(double kapitaal, double rentepercent, out double rente)
        {
            rente = kapitaal * rentepercent / 100;
            return $"{rente} euro";
        }

        private void Button_Bereken_Click(object sender, RoutedEventArgs e)
        {
            double kapitaal = Double.Parse(TB_Kapitaal.Text);
            double rentepercent = double.Parse(TB_Rente.Text);
            if (IsGroterDanNul(kapitaal))
            {
                Color color = (Color)ColorConverter.ConvertFromString("#FF32CD32");
                TB_Kapitaal.Background = new SolidColorBrush(color);


            }

            string rentetekst = BerekenRente(kapitaal, rentepercent, out double rente);
            TB_VolgendJaar.Text = $"Dag {TB_Naam.Text}\n" +
                $"Je zal volgend jaar {rentetekst} rente ontvangen";
        }
    }
}
