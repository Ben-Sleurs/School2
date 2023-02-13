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

namespace WPF_Dictionaries
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //array : DataType[]
            int[] reeksVanGetallen = new int[5];

            //List : List<DataType>
            List<int> lijstVanGetallen = new List<int>();

            //Dictionary: Dictionary<KeyType, ValueType>
            Dictionary<string, int> restaurantScores = new Dictionary<string, int>();

            Dictionary<int, string> dict2 = new Dictionary<int, string>() { { 1, "twee" }, { 2, "drie" }, { 9001, "Over 9000" } };



            Dictionary<string, double> scores;
            scores = new Dictionary<string, double>()
            {
            { "Martinell", 7.5},
            { "PizzaHut", 5.8},
            { "Venus", 7.3}
            };
            StringBuilder stringBuilder = new StringBuilder();

            double scoreVenus = scores["Venus"];
            stringBuilder.AppendLine(scoreVenus.ToString());

            scores["PizzaHut"] = 10;
            scores["The Century"] = 8.8;

            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            dictionary[0] = "hallo";
            dictionary[100] = "test";

            stringBuilder.AppendLine(dictionary.Count.ToString());
            stringBuilder.AppendLine(scores.Count.ToString());

            stringBuilder.AppendLine("inhoud van mijn dictionary scores");

            scores.Add("Bavet", 7);
            scores.Add("Jip's place", 9);
            foreach (string key in scores.Keys)
            {
                stringBuilder.AppendLine($"{key}: {scores[key].ToString()}");
            }

            foreach (double value in scores.Values)
            {
                stringBuilder.AppendLine($"Er is een waarde {value}");
            }

            //Geeft foutmelding
            //scores.Add("Jip's place", 10);

            stringBuilder.AppendLine($"Er zijn {scores.Count} aantal paren");
            scores.Clear();
            stringBuilder.AppendLine($"Er zijn {scores.Count} aantal paren");


            TB_Output.Text = stringBuilder.ToString();


        }
    }
}
