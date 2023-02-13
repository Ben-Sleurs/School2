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

namespace SelectieDemo
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

        private void Is_even_Click(object sender, RoutedEventArgs e)
        {
            string inputTekst = InputTB.Text;
            int getal = Convert.ToInt32(inputTekst);
            // hoe weten we of iets even is
            int rest = getal % 2;

            string output = "Output: ";
            if (rest==0)
            {
                output = output + "Is even ";
                if (getal>9000)
                {
                    output = output + "& >9000";
                }
            }
            else
            {
                output = output + "is oneven ";
                if (getal > 9000)
                {
                    output = output + "& >9000";
                }
            }
            InputTB.Text = output;
        }

        private void Is_Lekker_Click(object sender, RoutedEventArgs e)
        {
            string fruit = InputTB.Text;
            string output = "";
            switch (fruit)
            {
                case "Appel":
                    output = "Appels zijn lekker";
                    break;
                case "Banaan":
                    output = "Bananen zijn lekker";
                        break;
                case "Citroen":
                    output = "Citroenen zijn niet lekker";
                    break;
                default:
                    output = "Welk fruit?";
                    break;
            }
            InputTB.Text = output;
        }

        private void Conversion_Click(object sender, RoutedEventArgs e)
        {
            string inputTekst = InputTB.Text;
            short getal;

            //TryParse om crash te vermijden
            bool isHetConverterenGelukt = short.TryParse(inputTekst, out getal);

            if (isHetConverterenGelukt == true)
            {
                //parse methode
                getal = short.Parse(inputTekst);
                //convert methode
                getal = Convert.ToInt16(inputTekst);

            }
            //TryParse
            //bool isHetConverterenGelukt;
            //isHetConverterenGelukt = short.TryParse(inputTekst, out getal);

            InputTB.Text = Convert.ToString(getal * 2);
        }
    }
}
