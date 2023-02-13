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

namespace Oef_Klasses_2._1
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


        //OEF 1
        private int AantalDagen(DateTime date)
        {
            return 365 - date.DayOfYear;
        }
        //OEF 2
        private string BuildString(int product, string naam, double prijs)
        {
            StringBuilder totaal = new StringBuilder();
            totaal.Append(product.ToString());
            totaal.Append("\t");
            totaal.Append(naam);
            totaal.Append("\t");
            totaal.Append(prijs.ToString());
            totaal.Append("\t");
            totaal.Append((prijs * product).ToString());
            return totaal.ToString();
        }

    }
}
