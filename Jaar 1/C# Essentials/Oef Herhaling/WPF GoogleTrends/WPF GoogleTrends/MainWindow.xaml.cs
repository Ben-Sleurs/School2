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

namespace WPF_GoogleTrends
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[,] games;
        private string[,] news;
        private string[,] populaireTermen;
        private string[,] belgischePersonen;
        private Dictionary<string, string[,]> trendsDictionary;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Fill2DArrays();
            FillDictionary();
            FillComboBox();
            RB_Top3.IsChecked = true;
        }
        private void Fill2DArrays()
        {
            games = new string[,]
            {
                { "1", "Among Us" },
                { "2", "Battlefield 2042" },
                { "3", "Resident Evil Village" },
                { "4", "Valheim" },
                { "5", "Forza Horizon 5" },
                { "6", "Madden NFL 22" },
                { "7", "Outriders" },
                { "8", "Pokémon Unite" },
                { "9", "Biomutant" },
                { "10", "Friday Night Funkin" }
            };
            news = new string[,]
            {
                { "1", "Mega Millions" },
                { "2", "AMC Stock" },
                { "3", "Stimulus Check" },
                { "4", "Georgia Senate Race" },
                { "5", "GME" },
                { "6", "Dogecoin" },
                { "7", "Hurricane Ida" },
                { "8", "Kyle Rittenhouse Verdict" },
                { "9", "Afghanistan" },
                { "10", "Ethereum Price" }
            };
            populaireTermen = new string[,]
            {
                { "1", "NBA" },
                { "2", "DMX" },
                { "3", "Gabby Petito" },
                { "4", "Kyle Rittenhouse" },
                { "5", "Brian Laundrie" },
                { "6", "Mega Millions" },
                { "7", "AMC Stock" },
                { "8", "Stimulus Check" },
                { "9", "Georgia Senate Race" },
                { "10", "Squid Game" }
            };
            belgischePersonen = new string[,]
            {
                { "1", "Jurgen Conings" },
                { "2", "Jan Van Hecke" },
                { "3", "Eddy Demarez" },
                { "4", "Heidi De Pauw" },
                { "5", "Sihame El Kaouakibi" },
                { "6", "Gloria Monserez" },
                { "7", "Lara Switten" },
                { "8", "Kaat Bollen" },
                { "9", "Danny Verbiest" },
                { "10", "Barbara Mertens" }
            };
        }
        private void FillDictionary()
        {
            trendsDictionary = new Dictionary<string, string[,]>();
            trendsDictionary.Add("games",games);
            trendsDictionary.Add("news",news);
            trendsDictionary.Add("populaire termen",populaireTermen);
            trendsDictionary.Add("belgische personen",belgischePersonen);
        }

        private void FillComboBox()
        {
            foreach (string key in trendsDictionary.Keys)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = key;
                CB_Trends.Items.Add(item);
            }
        }

        private void CB_Trends_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CB_Trends.SelectedItem != null)
            {
                ComboBoxItem geselecteerdItem = (ComboBoxItem)CB_Trends.SelectedItem;
                bool isTrendAlAanwezig = false;
                foreach (ListBoxItem item in LB_Trends.Items)
                {
                    if (item.Content.ToString().Equals(geselecteerdItem.Content.ToString()))
                    {
                        isTrendAlAanwezig = true;
                    }
                }
                if (!isTrendAlAanwezig)
                {
                    ListBoxItem nieuweTrend = new ListBoxItem();
                    nieuweTrend.Content = geselecteerdItem.Content;
                    LB_Trends.Items.Add(nieuweTrend);
                }
                UpdateTrends();
            }
            CB_Trends.SelectedItem = null;
        }

        private void LB_Trends_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LB_Trends.Items.Remove(LB_Trends.SelectedItem);
            UpdateTrends();
        }
        private void UpdateTrends()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (ListBoxItem item in LB_Trends.Items)
            {
                string key = item.Content.ToString();
                string[,] trend = trendsDictionary[key];
                for (int i = 0; i < GetTop(); i++)
                {
                    stringBuilder.AppendLine($"{trend[i, 0]}) {trend[i, 1]}");
                }
                stringBuilder.AppendLine();
            }
            TB_Trends.Text = stringBuilder.ToString();
        }

        private int GetTop()
        {
            if (RB_Top3.IsChecked ==true)
            {
                return 3;
            }            
            else if (RB_Top5.IsChecked ==true)
            {
                return 5;
            }
            else
            {
                return 10;
            }
           
        }

        private void RB_Checked(object sender, RoutedEventArgs e)
        {
            UpdateTrends();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.C)
            {
                Clear();
            }
            if (e.Key == Key.Q)
            {
                CloseWithMessageBox();
            }
        }
        private void Clear()
        {
            MessageBoxResult result = MessageBox.Show("Ben je Zeker?", "Clearen", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                TB_Trends.Clear();
                LB_Trends.Items.Clear();
                RB_Top3.IsChecked = true;
            }
        }

        private void CloseWithMessageBox()
        {
            MessageBoxResult result = MessageBox.Show("Ben je Zeker?", "Sluiten", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (result == MessageBoxResult.Yes)
            {
                Close();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            CloseWithMessageBox();
        }
    }
}
