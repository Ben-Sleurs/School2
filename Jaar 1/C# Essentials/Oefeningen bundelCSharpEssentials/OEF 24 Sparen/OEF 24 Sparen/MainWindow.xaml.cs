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

namespace OEF_24_Sparen
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

        private void B_Bereken_Click(object sender, RoutedEventArgs e)
        {
            double start = double.Parse(TB_WeekgeldInput.Text);
            double weekBedrag = 1;
            double verhoging = double.Parse(TB_VerhogingInput.Text);
            double spaarBedrag = double.Parse(TB_SpaarbedragInput.Text);
            int weken = 0;
            while (start < spaarBedrag)
            {
                start = start + weekBedrag;
                weekBedrag = weekBedrag + verhoging;
                weken++;
            }
            TB_Output.Text = $"Je moet {weken} weken sparen voor een totaalspaarbedrag van {start} Euro" + "\n\n" + $"Extra weekgeld op dat moment is {weekBedrag} Euro";
        }

        private void B_Wissen_Click(object sender, RoutedEventArgs e)
        {
            TB_SpaarbedragInput.Text = "";
            TB_VerhogingInput.Text = "";
            TB_WeekgeldInput.Text = "";
            TB_Output.Text = "";

        }

        private void B_Afsluiten_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
