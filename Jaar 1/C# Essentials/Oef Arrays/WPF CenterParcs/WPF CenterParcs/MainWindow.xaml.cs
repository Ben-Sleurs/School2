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

namespace WPF_CenterParcs
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int[] aantalDagen = new int[]{ 1, 2, 5, 7, 8, 12, 14, 21 };
        private string[,] woningMetPrijs = new string[5, 2]{{"2 personen","80"},
                                                            {"4 personen","120"} ,
                                                            { "4 personen lux","140"} ,
                                                            { "6 personen","180"},
                                                            { "8 personen","200"}};
    public MainWindow()
        {
            InitializeComponent();
            Loaded += FillComboBoxes;
        }

        private void FillComboBoxes(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < aantalDagen.Length; i++)
            {
                ComboBoxItem cbi = new ComboBoxItem();
                cbi.Content = aantalDagen[i];
                CB_Dagen.Items.Add(cbi); 
            }
            for (int i = 0; i < woningMetPrijs.GetLength(0); i++)
            {
                ComboBoxItem cbi2 = new ComboBoxItem();
                cbi2.Content = woningMetPrijs[i, 0];
                CB_Woning.Items.Add(cbi2);
            }
        }

        private void CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_Dagen.SelectedIndex!=-1 && CB_Woning.SelectedIndex!=-1)
            {
                TB_Prijs.Text = $"{aantalDagen[CB_Dagen.SelectedIndex] * int.Parse(woningMetPrijs[CB_Woning.SelectedIndex, 1])}";
            }
        }
    }
}
