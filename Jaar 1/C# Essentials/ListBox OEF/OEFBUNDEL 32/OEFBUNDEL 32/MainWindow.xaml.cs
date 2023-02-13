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

namespace OEFBUNDEL_32
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

        private void B_Vervang_Click(object sender, RoutedEventArgs e)
        {
            int index = LB_Simple.SelectedIndex;
            if (index!=-1)
            {
                ListBoxItem item = new ListBoxItem();
                item.Content = TB_Vervang.Text;
                LB_Simple.Items.RemoveAt(index);
                LB_Simple.Items.Insert(index,item);
            }
            foreach (ListBoxItem item in LB_Multiple.SelectedItems)
            {
                item.Content = TB_Vervang.Text;
            }
            TB_Vervang.Clear();
        }

        private void B_VoegToe_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem item = new ListBoxItem();
            ListBoxItem itemMultiple = new ListBoxItem();
            item.Content = TB_VoegToe.Text;
            itemMultiple.Content = TB_VoegToe.Text;
            LB_Simple.Items.Add(item);

            LB_Multiple.Items.Add(itemMultiple);

            TB_VoegToe.Clear();



        }

        private void B_Zoeken_Click(object sender, RoutedEventArgs e)
        {
            L_Zoeken.Content = "";
            string teZoeken = TB_Zoeken.Text;
            foreach (ListBoxItem item in LB_Simple.Items)
            {
                if (item.Content.ToString()==teZoeken)
                {
                    L_Zoeken.Content = $"Het item staat op plaats {LB_Simple.ItemContainerGenerator.IndexFromContainer(item)}";
                }
            }
            if (L_Zoeken.Content.ToString()=="")
            {
                L_Zoeken.Content = "Het item is niet gevonden";
            }
        }

        private void B_Verwijder_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Simple.SelectedIndex!=-1)
            {
                LB_Simple.Items.RemoveAt(LB_Simple.SelectedIndex);
            }
            if (LB_Multiple.SelectedIndex!=-1)
            {
                int count = LB_Multiple.SelectedItems.Count;
                for (int i = 0; i < count; i++)
                {
                    LB_Multiple.Items.RemoveAt(LB_Multiple.SelectedIndex);
                }

            }

            
        }

        private void B_ListBoxVerwijder_Click(object sender, RoutedEventArgs e)
        {
            LB_Simple.Items.Clear();
            LB_Multiple.Items.Clear();
        }

        private void B_Sorteren_Click(object sender, RoutedEventArgs e)
        {
            List<string> list = new List<string>();
            foreach (ListBoxItem item in LB_Simple.Items)
            {
                list.Add(item.Content.ToString());
            }
            list.Sort();
            LB_Simple.Items.Clear();
            foreach (string item in list)
            {
                ListBoxItem listItem = new ListBoxItem();
                listItem.Content = item;
                LB_Simple.Items.Add(listItem);
            }



            List<string> list2 = new List<string>();
            foreach (ListBoxItem item in LB_Multiple.Items)
            {
                list2.Add(item.Content.ToString());
            }
            list2.Sort();
            LB_Multiple.Items.Clear();
            foreach (string item in list2)
            {
                ListBoxItem listItem2 = new ListBoxItem();
                listItem2.Content = item;
                LB_Multiple.Items.Add(listItem2);
            }
        }
    }
}
