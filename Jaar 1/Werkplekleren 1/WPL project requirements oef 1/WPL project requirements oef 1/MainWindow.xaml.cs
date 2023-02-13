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
using System.Text.RegularExpressions;

namespace WPL_project_requirements_oef_1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int i = 1;
        int j = 1;
        int x = 1;
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private void ValidateInputGeheelGetal(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^1-3]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_Registreer_Click(object sender, RoutedEventArgs e)
        {
            if (TB_Lokaal.Text=="1"||TB_Lokaal.Text=="2"||TB_Lokaal.Text=="3")
            {
                TB_Resultaat.Text = $"Welkom {TB_Naam.Text} in Ruimte {TB_Lokaal.Text}";
                TB_Resultaat.Visibility = Visibility.Visible;
                Button_Bevestig.Visibility = Visibility.Visible;
            }

        }

        private void Button_Bevestig_Click(object sender, RoutedEventArgs e)
        {

            if (TB_Lokaal.Text=="1"&&i<=10)
            {
                TB_Lokaal1.Text = $"{TB_Lokaal1.Text}" + $"{i}. {TB_Naam.Text}\n";
                i++;
                Button_Bevestig.Visibility = Visibility.Hidden;
            }
            else if (i > 10)
            {
                TB_Resultaat.Text = "Het Maximale toegestane aanwezigen voor ruimte 1 is bereikt.";
                Button_Bevestig.Visibility = Visibility.Hidden;
            }

            if (TB_Lokaal.Text=="2"&&j<=10)
            {
                TB_Lokaal2.Text = $"{TB_Lokaal2.Text}" + $"{j}. {TB_Naam.Text}\n";
                j++;
                Button_Bevestig.Visibility = Visibility.Hidden;
            }
            else if (j > 10)
            {
                TB_Resultaat.Text = "Het Maximale toegestane aanwezigen voor ruimte 2 is bereikt.";
                Button_Bevestig.Visibility = Visibility.Hidden;
            }

            if (TB_Lokaal.Text=="3"&&x<=10)
            {
                TB_Lokaal1.Text = $"{TB_Lokaal3.Text}" + $"{x}. {TB_Naam.Text}\n";
                x++;
                Button_Bevestig.Visibility = Visibility.Hidden;
            }
            else if (x > 10)
            {
                TB_Resultaat.Text = "Het Maximale toegestane aanwezigen voor ruimte 3 is bereikt.";
                Button_Bevestig.Visibility = Visibility.Hidden;
            }

        }

        private void Button_Afsluiten_Click(object sender, RoutedEventArgs e)
        {
            TB_Resultaat.Text = "Bent u zeker dat u de applicatie wilt afsluiten?";
            Button_Ja.Visibility = Visibility.Visible;
            Button_Nee.Visibility = Visibility.Visible;
            
        }

        private void Button_Ja_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
