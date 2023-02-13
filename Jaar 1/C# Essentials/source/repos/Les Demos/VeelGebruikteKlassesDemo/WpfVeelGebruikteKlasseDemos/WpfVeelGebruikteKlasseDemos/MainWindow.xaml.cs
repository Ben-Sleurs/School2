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
using System.Windows.Threading;

namespace WpfVeelGebruikteKlasseDemos
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //1. Declareren
            DispatcherTimer dispatcher;
            //2. Initialiseren
            dispatcher = new DispatcherTimer();

            //3. Interval
            dispatcher.Interval = new TimeSpan(0,0,1);

            //4 Event toevoegen
            // += om methoden te koppelen aan tick
            // Note: geen ronde haakjes bij methode
           
            dispatcher.Tick += Timer_Tick;

            //5. Start dispatcher
            dispatcher.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TBL_Test.Text = DateTime.Now.ToLongTimeString();
        }

        private void RandomStuff()
        {
            Random randomGetallenMachine;
            randomGetallenMachine = new Random(2000);

            Random randomGetallenMachineTwee = new Random(2000);

            int getal = randomGetallenMachine.Next(0,10);
            int getal2 = randomGetallenMachineTwee.Next(0,10);
            int testBreakLine = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RandomStuff();
        }
    }
}
