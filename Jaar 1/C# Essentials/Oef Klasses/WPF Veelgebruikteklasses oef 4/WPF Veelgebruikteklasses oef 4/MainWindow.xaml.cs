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

namespace WPF_Veelgebruikteklasses_oef_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int temp = 1;
        TimeSpan interval = new TimeSpan();
        DateTime start = DateTime.Now;
        int intervalseconden;
        //1. Declareren
        DispatcherTimer dispatcher;
        public MainWindow()
        {
            InitializeComponent();

            
            //2. Initialiseren
            dispatcher = new DispatcherTimer();

            //3. Interval
            dispatcher.Interval = new TimeSpan(0, 0, 1);

            //4 Event toevoegen
            // += om methoden te koppelen aan tick
            // Note: geen ronde haakjes bij methode

            dispatcher.Tick += PlusOne;
            dispatcher.Tick += PlusTwo;


            //5. Start dispatcher
            dispatcher.Start();
        }

        private void PlusOne(object sender, EventArgs e)
        {
          
            Label_Getal.Content = Convert.ToString(temp++);
        }

        private void Button_Verstreken_Click(object sender, RoutedEventArgs e)
        {
            //interval = DateTime.Now - start;
            //intervalseconden = interval.TotalSeconds;
            //start = DateTime.Now;
            //Label_Verstreken.Content = intervalseconden;
            Label_Test.Content = dispatcher.Interval.TotalSeconds;
        }

        private void PlusTwo(object sender, EventArgs e)
        {
            //Label_Test.Content = dispatcher.Interval.TotalSeconds;
            
        }
    }
}
