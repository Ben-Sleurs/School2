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

namespace OEF_22_dobbelstenen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Reset();
        }

        private void B_Start_Click(object sender, RoutedEventArgs e)
        {
            DobbelTot6();
            B_Start.IsEnabled = false;
            B_Opnieuw.Focus();
        }

        private void B_Opnieuw_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }
        private void DobbelTot6() 
        {
            Random Dobbel = new Random();
            int uitkomst = 0;
            int count = 1;
            do
            {
                uitkomst = Dobbel.Next(1, 7);
                TB_Output.Text = TB_Output.Text + $"Worp {count} geeft {uitkomst}\n";
                count++;
            } while (uitkomst != 6);
        }

        private void B_Sluiten_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Reset()
        {
            TB_Output.Text = "";
            B_Start.IsEnabled = true;
            B_Start.Focus();
        }
    }
}
