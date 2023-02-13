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

namespace WPFListBoxDemo
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

        private void VoegToeButton_Click(object sender, RoutedEventArgs e)
        {
            string inhoud = "";
            if (MijnComboBox.SelectedIndex!=-1)
            {
                ComboBoxItem geselecteerdComboBoxItem = (ComboBoxItem)MijnComboBox.SelectedItem;
                inhoud = " " + geselecteerdComboBoxItem.Content.ToString();
            }
            if (MijnListBox.Items.Contains(NieuwLidTextBox.Text))//???
            {
                
            }
            else
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = NieuwLidTextBox.Text + inhoud;
                MijnListBox.Items.Add(item);
            }
            
        }

        private void VerwijderButton_Click(object sender, RoutedEventArgs e)
        {
            MijnListBox.Items.Remove(MijnListBox.SelectedItem);
            MijnComboBox.SelectedItem = null;
            NieuwLidTextBox.Text = "";
            NieuwLidTextBox.Clear();
        }

        private void MijnListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MijnListBox.SelectedItem!=null || MijnListBox.SelectedIndex!=-1)
            {
                ListBoxItem geselecteerdItem = (ListBoxItem)MijnListBox.SelectedItem;
                NieuwLidTextBox.Text = geselecteerdItem.Content.ToString();
            }
            
        }

        private void MijnComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MijnListBox.SelectedItem != null || MijnListBox.SelectedIndex != -1)
            {
                ComboBoxItem geselecteerdItem = (ComboBoxItem)MijnComboBox.SelectedItem;
                NieuwLidTextBox.Text = geselecteerdItem.Content.ToString();
            }
        }
    }
}
