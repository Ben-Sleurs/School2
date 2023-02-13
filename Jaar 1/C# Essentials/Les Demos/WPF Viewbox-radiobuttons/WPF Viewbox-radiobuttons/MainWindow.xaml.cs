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

namespace WPF_Viewbox_radiobuttons
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

        private void B_Verzend_Click(object sender, RoutedEventArgs e)
        {
            string antwoord = "";
            if (RB_Feest_Ja.IsChecked==true)
            {
                antwoord += "nice";
            }
            else if (RB_Feest_Nee.IsChecked==true)
            {
                antwoord += "aight np";
            }
            else if (RB_Feest_Misschien.IsChecked==true)
            {
                antwoord += "let me know";
            }
            if (RB_Pizza_Ja.IsChecked==true)
            {
                antwoord += " fuck jouw";
            }
            else if (RB_Pizza_Nee.IsChecked==true)
            {
                antwoord += " Held";
            }

            MessageBox.Show(antwoord);
        }
    }
}
