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

namespace Toepassing_3_Oefeningenbundel
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

        private void Button_Optelling_Click(object sender, RoutedEventArgs e)
        {
            double Getal1 = Convert.ToDouble(Invulveld_Getal_1.Text);
            double Getal2 = Convert.ToDouble(Invulveld_Getal_2.Text);
            double resultaat = Getal1 + Getal2;
            Invulveld_Resultaat.Text = Convert.ToString(resultaat);
        }

        private void Button_Vermenigvuldiging_Click(object sender, RoutedEventArgs e)
        {
            double Getal1 = Convert.ToDouble(Invulveld_Getal_1.Text);
            double Getal2 = Convert.ToDouble(Invulveld_Getal_2.Text);
            double resultaat = Getal1 * Getal2;
            Invulveld_Resultaat.Text = Convert.ToString(resultaat);
        }

        private void Button_Aftelling_Click(object sender, RoutedEventArgs e)
        {
            double Getal1 = Convert.ToDouble(Invulveld_Getal_1.Text);
            double Getal2 = Convert.ToDouble(Invulveld_Getal_2.Text);
            double resultaat = Getal1 - Getal2;
            Invulveld_Resultaat.Text = Convert.ToString(resultaat);
        }

        private void Button_Deling_Click(object sender, RoutedEventArgs e)
        {
            double Getal1 = Convert.ToDouble(Invulveld_Getal_1.Text);
            double Getal2 = Convert.ToDouble(Invulveld_Getal_2.Text);
            double resultaat = Getal1 / Getal2; 
            Invulveld_Resultaat.Text = Convert.ToString(resultaat);
        }

        private void Button_Wissen_Click(object sender, RoutedEventArgs e)
        {
            Invulveld_Getal_1.Text = "";
            Invulveld_Getal_2.Text = "";
            Invulveld_Resultaat.Text = "";
        }

        private void Button_Optelling_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter)
            {
                double Getal1 = Convert.ToDouble(Invulveld_Getal_1.Text);
                double Getal2 = Convert.ToDouble(Invulveld_Getal_2.Text);
                double resultaat = Getal1 + Getal2;
                Invulveld_Resultaat.Text = Convert.ToString(resultaat);
            }
        }
        private void LeesGetallen(string eersteGetal, string tweedeGetal)
        {
            double getal1 = double.Parse(eersteGetal);
            double getal2 = double.Parse(tweedeGetal);
        }
    }
}
