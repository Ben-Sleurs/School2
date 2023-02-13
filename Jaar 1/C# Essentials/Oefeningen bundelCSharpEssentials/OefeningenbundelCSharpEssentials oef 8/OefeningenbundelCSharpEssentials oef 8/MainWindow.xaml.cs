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

namespace OefeningenbundelCSharpEssentials_oef_8
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

        private void Button_Bereken_Click(object sender, RoutedEventArgs e)
        {
            string Personeelslid =TB_Persooneelslid.Text;
            string uurloontekst = TB_Uurloon.Text; ;
            string aantalurentekst = TB_AantalUren.Text;

            bool IsUurloonValid = double.TryParse(uurloontekst, out double uurloon);
            bool IsAantalUrenValid = double.TryParse(aantalurentekst, out double aantaluren);

            if (IsUurloonValid&&IsAantalUrenValid)
            {
                double belasting;
                double bruto = uurloon * aantaluren;
                if (bruto>50000)
                {
                    belasting = 14000 + ((bruto - 50000) * 0.5);
                }
                else if (bruto>25000)
                {
                    belasting = 4000 + ((bruto - 25000) * 0.4);
                }
                else if (bruto > 15000)
                {
                    belasting = 1000 + ((bruto - 15000) * 0.3);
                }
                else if (bruto>10000)
                {
                    belasting = (bruto - 10000) * 0.2;
                }
                else
                {
                    belasting = 0;
                }
                double netto = bruto - belasting;


                TB_Resultaat.Text = $"LOONFICHE VAN {TB_Persooneelslid.Text}\n\n"+ 
                    $"Aantal gewerkte uren \t: {TB_AantalUren.Text}\n"+
                    $"Uurloon            \t\t: €{uurloon}\n"+
                    $"Brutojaarwedde       \t: €{bruto}\n"+
                    $"Belasting          \t\t: €{belasting}\n"+
                    $"Nettojaarwedde       \t: €{netto} ";
            }
            else
            {
                TB_Resultaat.Text = "Invalide input";
            }
        }

        private void Button_Wissen_Click(object sender, RoutedEventArgs e)
        {
            TB_AantalUren.Text = "";
            TB_Persooneelslid.Text = "";
            TB_Uurloon.Text = "";
            TB_Resultaat.Text = "";
        }

        private void Button_Afsluiten_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
