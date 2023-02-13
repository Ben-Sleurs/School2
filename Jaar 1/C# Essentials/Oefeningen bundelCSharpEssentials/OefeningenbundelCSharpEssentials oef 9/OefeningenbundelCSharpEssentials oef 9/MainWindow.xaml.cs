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

namespace OefeningenbundelCSharpEssentials_oef_9
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

        private void Button_testnumeriek_Click(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(TB_Jaar.Text,out int jaar))
            {
                Label_isnumeriek.Content = "is numeriek";
            }
            else
            {
                Label_isnumeriek.Content = "is niet numeriek";
            }
        }

        private void Button_bereken_Click(object sender, RoutedEventArgs e)
        {
            Label_isnumeriek.Content = "";
            int Jaar;
            bool IsJaar = int.TryParse(TB_Jaar.Text, out Jaar);
            if (IsJaar)
            {
                if (Jaar % 400 == 0)
                {
                    Label_isschrikkeljaar.Content = "is schrikkeljaar";
                    TB_studiepunten.Text = "68";
                    TB_inschrijvingsgeld.Text = "1064.2";
                }
                else if (Jaar%100==0)
                {
                    Label_isschrikkeljaar.Content = "is  geen schrikkeljaar";
                    TB_studiepunten.Text = "60";
                    TB_inschrijvingsgeld.Text = "939";
                }
                else if (Jaar%4==0)
                {
                    Label_isschrikkeljaar.Content = "is schrikkeljaar";
                    TB_studiepunten.Text = "68";
                    TB_inschrijvingsgeld.Text = "1064.2";
                }
                else
                {
                    Label_isschrikkeljaar.Content = "is  geen schrikkeljaar";
                    TB_studiepunten.Text = "60";
                    TB_inschrijvingsgeld.Text = "939";
                }
               
            }
            else
            {
                Label_isschrikkeljaar.Content = "geen valide jaar";
            }

        }

        private void Button_afsluiten_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TB_Jaar_TextChanged(object sender, TextChangedEventArgs e)
        {
            Label_isnumeriek.Content = "";
        }
    }
}
