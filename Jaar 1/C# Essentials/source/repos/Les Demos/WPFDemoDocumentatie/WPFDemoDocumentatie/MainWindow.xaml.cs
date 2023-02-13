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

namespace WPFDemoDocumentatie
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            bool isGoedkoper = HeeftKorting(321548, 5);
            teller++;
        }
        /// <summary>
        /// Bewaart het aantal klanten in de winkel.
        /// </summary>
        private int teller = 0;
        /// <summary>
        /// We verwerken de klant op basis van KlantID en het aantal jaren dat hij/zij/het klant is.
        /// </summary>
        /// <param name="id">KlantID</param>
        /// <param name="jarenKlant">Het aantal jaren dat de klant een klant is.</param>
        /// <returns>Goedkeuring voor korting</returns>
        private bool HeeftKorting(int id, int jarenKlant)
        {
            // Verwerk Klant
            return true;
        }
    }
}
