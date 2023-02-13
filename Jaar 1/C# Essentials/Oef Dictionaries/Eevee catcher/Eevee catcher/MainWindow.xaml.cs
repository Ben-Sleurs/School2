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

namespace Eevee_catcher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer klokTimer;
        private DispatcherTimer randomImageTimer;
        private Random rng;
        private Dictionary<string, string> eeveeImages = new Dictionary<string, string> { 
            { "Eevee", "/images/Eevee.png" }, { "EeveeGigantamax", "/images/EeveeGigantamax.png" }, { "Espeon", "/images/Espeon.png" }, 
            { "Flareon", "/images/Flareon.png" }, { "Glaceon", "/images/Glaceon.png" }, { "Jolteon", "/images/Jolteon.png" }, 
            { "Leafeon", "/images/Leafeon.png" }, { "Sylveon", "/images/Sylveon.png" }, { "Umbreon", "/images/Umbreon.png" }, 
            { "Vaporeon", "/images/Vaporeon.png" } };
        public MainWindow()
        {
            InitializeComponent();
            StartKlok();
            rng = new Random();
        }

        private void StartKlok()
        {
            klokTimer = new DispatcherTimer();
            klokTimer.Interval = TimeSpan.FromMilliseconds(1);
            klokTimer.Tick += KlokTimer_Tick;
            klokTimer.Start();
        }

        private void KlokTimer_Tick(object sender, EventArgs e)
        {
            TBL_Time.Text = DateTime.Now.ToLongTimeString();
        }

        private void B_Start_Click(object sender, RoutedEventArgs e)
        {
            StartRandomImageTimer();
        }

        private void StartRandomImageTimer()
        {
            randomImageTimer = new DispatcherTimer();
            randomImageTimer.Interval = TimeSpan.FromSeconds(1);
            randomImageTimer.Tick += randomImageTimer_Tick;
            randomImageTimer.Start();
            
        }

        private void randomImageTimer_Tick(object sender, EventArgs e)
        {
            string randomPath = GetRandomPath();
            IMG_Random.Source = new BitmapImage(new Uri(randomPath, UriKind.Relative));
        }

        private string GetRandomPath()
        {
            List<string> keys = new List<string>(eeveeImages.Values);
            int randomIndex = rng.Next(keys.Count);
            return keys[randomIndex];
        }
    }
}
