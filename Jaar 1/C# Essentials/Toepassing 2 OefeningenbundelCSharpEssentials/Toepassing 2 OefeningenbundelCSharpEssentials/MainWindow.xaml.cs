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

namespace Toepassing_2_OefeningenbundelCSharpEssentials
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
        double pi = Math.PI;
        private void Bereken_Click(object sender, RoutedEventArgs e)
        {
            Double Straal = Convert.ToDouble(InvulveldStraal.Text);
            Double Omtrek =Straal*2*pi;
            Double Oppervlakte = Straal * pi * pi;
            InvulveldOmtrek.Text = Convert.ToString(Omtrek)+ " cm";
            InvulveldOppervlakte.Text = Convert.ToString(Oppervlakte) + " vierkante cm";
        }

        private void Wissen_Click(object sender, RoutedEventArgs e)
        {
            InvulveldStraal.Text = " ";
            InvulveldOmtrek.Text = " ";
            InvulveldOppervlakte.Text = " ";
        }
    }
}
