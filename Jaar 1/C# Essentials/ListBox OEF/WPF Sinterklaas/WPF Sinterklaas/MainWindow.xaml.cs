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

namespace WPF_Sinterklaas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            B_NaarFlink.Content = "<=====";

        }

        private void B_Registreer_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxItem geselecteerdItem = (ComboBoxItem)CB_Gedrag.SelectedItem;
            if (geselecteerdItem==null)
            {

            }
            else if (geselecteerdItem==CBI_Stout)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = TB_InvulVeld.Text;
                LB_Stout.Items.Add(item);
            }
            else if (CB_Gedrag.SelectedItem==CBI_Flink)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = TB_InvulVeld.Text;
                LB_Flink.Items.Add(item);
            }
            TB_InvulVeld.Text = "";
            TB_InvulVeld.Focus();
        }

        private void B_Verwijder_Click(object sender, RoutedEventArgs e)
        {
            LB_Flink.Items.Remove(LB_Flink.SelectedItem);
            LB_Stout.Items.Remove(LB_Stout.SelectedItem);
        }

        private void B_NaarStout_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Flink.SelectedItem!=null)
            {
                ListBoxItem temp = (ListBoxItem)LB_Flink.SelectedItem;
                LB_Flink.Items.Remove(LB_Flink.SelectedItem);
                LB_Stout.Items.Add(temp);
            }
        }

        private void B_NaarFlink_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Stout.SelectedItem != null)
            {
                ListBoxItem temp = (ListBoxItem)LB_Stout.SelectedItem;
                LB_Stout.Items.Remove(LB_Stout.SelectedItem);
                LB_Flink.Items.Add(temp);
                LB_Flink.Items.Add("test");
            }
        }
    }
}
