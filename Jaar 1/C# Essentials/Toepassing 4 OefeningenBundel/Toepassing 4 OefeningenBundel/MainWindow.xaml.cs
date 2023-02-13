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

namespace Toepassing_4_OefeningenBundel
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


        private void Button_Controle_nummer_Click(object sender, RoutedEventArgs e)
        {
            int restdeling = 97-(Convert.ToInt32(Invulveld_Ondernemingsnummer.Text) % 97);
            Invulveld_Controlenummer.Text = restdeling.ToString();
        }
    }
}
