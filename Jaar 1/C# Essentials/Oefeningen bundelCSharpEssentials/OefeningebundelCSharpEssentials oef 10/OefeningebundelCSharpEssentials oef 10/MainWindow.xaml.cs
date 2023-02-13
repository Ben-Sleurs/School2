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

namespace OefeningebundelCSharpEssentials_oef_10
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
        private void ValidateInputGeheelGetal(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void ValidateInputKommaGetal(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void Button_bereken_Click(object sender, RoutedEventArgs e)
        {
            Tekst_resultaat.Opacity = 1;

            //check of het juist getallen zijn
            if (double.TryParse(TB_basisvlucht.Text, out double basisvlucht) &&
                int.TryParse(TB_vluchtklasse.Text, out int vluchtklasse) &&
                double.TryParse(TB_basisprijs.Text, out double basisprijs) &&
                int.TryParse(TB_aantaldagen.Text, out int aantaldagen) &&
                int.TryParse(TB_aantalpersonen.Text, out int aantalpersonen) &&
                double.TryParse(TB_kortingspercentage.Text, out double kortingspercentage))
            {
                //check en aanpassing basisprijs voor vluchtklasse
                switch (vluchtklasse)
                {
                    case 1:
                        basisvlucht = basisvlucht * 1.3;
                        break;
                    case 3:
                        basisvlucht = basisvlucht * 0.8;
                        break;
                    default:
                        break;
                }
                //berekening verblijfsprijs afhankelijk van aantal personen
                double verblijfsprijs;
                if (aantalpersonen<=2)
                {
                    verblijfsprijs = basisprijs * aantaldagen * aantalpersonen;
                }
                else if (aantalpersonen==3)
                {
                    verblijfsprijs = basisprijs * aantaldagen * 2.5;
                }
                else
                {
                    verblijfsprijs = (basisprijs * aantaldagen * 2.5) + (basisprijs * aantaldagen * (aantalpersonen - 3)*0.3);
                }

                //berekening rest
                double vluchtprijs = basisvlucht * aantalpersonen;
                double totalereisprijs = vluchtprijs + verblijfsprijs;
                double korting = totalereisprijs * (kortingspercentage / 100);
                double tebetalen = totalereisprijs - korting;

                //uitprinten resultaat
                Tekst_resultaat.Text = "REISKOST VOLGENS BESTELLING NAAR " + TB_bestemming.Text + Environment.NewLine +
                Environment.NewLine +
                "Totale vluchtprijs: € " + vluchtprijs + Environment.NewLine +
                "Totale verblijfsprijs: € " + verblijfsprijs + Environment.NewLine +
                "Totale reisprijs; € " + totalereisprijs + Environment.NewLine +
                "Korting: € " + korting + Environment.NewLine +
                 Environment.NewLine +
                "Te betalen: €" + tebetalen;


            }
            else
            {
                Tekst_resultaat.Text = "Invalide input";
            }
        }

        private void TB_vluchtklasse_MouseEnter(object sender, MouseEventArgs e)
        {
            Label_vluchtklasseinfo.Content = "1=Businessclass \n2=Standaard Lijnvlucht \n3=Charter";
            Label_vluchtklasseinfo.Opacity = 1;
        }

        private void TB_vluchtklasse_MouseLeave(object sender, MouseEventArgs e)
        {
            Label_vluchtklasseinfo.Content = "";
            Label_vluchtklasseinfo.Opacity = 0;
        }

        private void Button_wissen_Click(object sender, RoutedEventArgs e)
        {
            Tekst_resultaat.Opacity = 0;
            Tekst_resultaat.Text = "";
            TB_bestemming.Text = "";
            TB_basisvlucht.Text = "";
            TB_vluchtklasse.Text = "";
            TB_basisprijs.Text = "";
            TB_aantaldagen.Text = "";
            TB_aantalpersonen.Text = "";
            TB_kortingspercentage.Text = "";
        }

        private void Button_afsluiten_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TB_vluchtklasse_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^1-3]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
