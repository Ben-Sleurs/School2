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

namespace DemoBesturingselementen
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

        private void Afsluiten_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Bestand_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hallo");
        }

        private void Fill_Click(object sender, RoutedEventArgs e)
        {
            BackgroundImage.Stretch = Stretch.Fill;
        }

        private void None_Click(object sender, RoutedEventArgs e)
        {
            BackgroundImage.Stretch = Stretch.None;
        }

        private void Uniform_Click(object sender, RoutedEventArgs e)
        {
            BackgroundImage.Stretch = Stretch.Uniform;
        }

        private void UniformFill_Click(object sender, RoutedEventArgs e)
        {
            BackgroundImage.Stretch = Stretch.UniformToFill;
        }

        //private void HiddenButton_Click(object sender, RoutedEventArgs e)
        //{
        //    TBL_Output.Visibility = Visibility.Hidden;
        //    Grid_Main.Background = Brushes.Azure;
        //}

        //private void VisibleButton_Click(object sender, RoutedEventArgs e)
        //{
        //    TBL_Output.Visibility = Visibility.Visible;
        //    Grid_Main.Background = Brushes.Green;
        //}

        //private void CollapsedButton_Click(object sender, RoutedEventArgs e)
        //{
        //    TBL_Output.Visibility = Visibility.Collapsed;
        //    Grid_Main.Background = Brushes.Yellow;
        //}

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    //bool a = CB_Isman.IsChecked; //true,false of null (bool geeft error)
        //    if (CB_Isman.IsChecked==true)
        //    {
        //        Label_Isman.Content = "Is Man";
        //    }
        //    else
        //    {
        //        Label_Isman.Content = "Is !Man";
        //    }
        //}

        //private void CB_Isman_Checked(object sender, RoutedEventArgs e)
        //{
        //    Label_Isman.Content = "Je hebt de checkbox aangevinkt";
        //}
    }
}
