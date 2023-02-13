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

namespace WpfFoutafhandeling
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string input = TB_Input.Text;
            double inputGetal=0;
            try
            {
                inputGetal = double.Parse(input);
                if (inputGetal<0)
                {
                    throw new Exception("getal moet positief zijn!");
                }
                inputGetal = inputGetal * 10;
            }
            catch (FormatException exception)
            {
                MessageBox.Show($"Er is iets fout gelopen: {exception.Message}","Oef",MessageBoxButton.OK,MessageBoxImage.Error);
                
            }
            catch(Exception exception)
            {
                MessageBox.Show($"De input was niet correct: {exception.Message}","Lees het nog eens",MessageBoxButton.OK,MessageBoxImage.Warning);
                inputGetal = 1;
            }
            finally
            {
                TBL_Output.Text = inputGetal.ToString();
            }
            
        }
    }
}
