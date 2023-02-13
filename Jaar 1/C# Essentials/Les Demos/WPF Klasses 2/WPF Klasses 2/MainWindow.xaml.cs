using Microsoft.VisualBasic;
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

namespace WPF_Klasses_2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StringBuilder inhoudLijst;
        public MainWindow()
        {
            InitializeComponent();
            inhoudLijst = new StringBuilder();

            char karakter = 's';
            if (Char.IsLower(karakter))
            {
                MessageBox.Show("Het is een kleine letter");
            }

            if (Char.IsDigit(karakter))
            {
                MessageBox.Show("Het is een digit");
            }

            string nieuwTekst = "";
            if (String.IsNullOrEmpty(nieuwTekst))
            {
                MessageBox.Show("De tekst is leeg");
            }

        }

        private void Button_VoegToe_Click(object sender, RoutedEventArgs e)
        {
            string newItem = TB_Input.Text;
            inhoudLijst.AppendLine(newItem);

            //InputBox werkt niet omdat hier netcore 3.1 is gebruikt ipv 5.0
            string aantal = Interaction.InputBox("Geef de hoeveelheid van je product in", "Hoeveel?", "1", 500);
            inhoudLijst.AppendLine(aantal);

            //inhoudLijst.Replace('a', '4');
            //inhoudLijst.Replace('e', '3');
            //inhoudLijst.Remove(1, 1);

            TB_Output.Text = inhoudLijst.ToString();
        }

        private void Button_Wissen_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult resultaat = MessageBox.Show("Ben je zeker?", "Wissen", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (resultaat == MessageBoxResult.Yes)
            {
                TB_Output.Text = "";
                inhoudLijst.Clear();
            }
        }
    }
}
