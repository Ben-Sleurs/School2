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

namespace MyFirstWPFApp
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

        private void Login_Button_Click(object sender, RoutedEventArgs e)
        {
            tekstlabel.Content = "Welkom, U wordt ingelogd.";
        }

        private void Annuleren_Button_Click(object sender, RoutedEventArgs e)
        {
           tekstlabel.Content = "Even geduld.";
            double aantalDollar = Convert.ToDouble(InvulVeldTextBox.Text)*1.18;
            tekstlabel.Content = aantalDollar + " $";
        }

        private void Annuleren_MouseEnter(object sender, MouseEventArgs e)
        {
            tekstlabel.Content = "Bent u zeker?";
        }

        private void Wissen_Click(object sender, RoutedEventArgs e)
        {
            InvulVeldTextBox.Text = "";
        }
    }
}
