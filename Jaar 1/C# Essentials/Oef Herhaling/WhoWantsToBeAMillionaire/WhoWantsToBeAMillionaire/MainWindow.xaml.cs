﻿using System;
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

namespace WhoWantsToBeAMillionaire
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string[,] vragen = new string[,]
        {
            { "100", "In the UK, the abbreviation NHS stands for National what Service?", "Humanity", "Health", "Honour", "Household", "2" },
            { "200", "Which Disney character famously leaves a glass slipper behind at a royal ball?", "Pocahontas", "Sleeping Beauty", "Cinderella", "Elsa", "3" },
            { "300", "What name is given to the revolving belt machinery in an airport that delivers checked luggage from the plane to baggage reclaim?", "Hangar", "Terminal", "Concourse", "Carousel", "4" },
            { "500", "Which of these brands was chiefly associated with the manufacture of household locks?" , "Phillips", "Flymo", "Chubb", "Ronseal", "3" },
            { "1000", "The hammer and sickle is one of the most recognisable symbols of which political ideology?", "Republicanism", "Communism", "Conservatism", "Liberalism", "2" },
            { "2000", "Which toys have been marketed with the phrase 'robots in disguise'?", "Bratz Dolls", "Sylvanian Families", "Hatchimals", "Transformers" , "4" },
            { "4000", "What does the word loquacious mean?", "Angry", "Chatty", "Beautiful", "Shy", "2" },
            { "8000", "Obstetrics is a branch of medicine particularly concerned with what?", "Childbirth", "Broken bones", "Heart conditions", "Old age", "1" },
            { "16000", "In Doctor Who, what was the signature look of the fourth Doctor, as portrayed by Tom Baker?", "Bow-tie, braces and tweed jacket", "Wide-brimmed hat and extra long scarf", "Pinstripe suit and trainers", "Cape, velvet jacket and frilly shirt", "2" },
            { "32000", "Which of these religious observances lasts for the shortest period of time during the calendar year?", "Ramadan", "Diwali", "Lent", "Hanukkah", "2" },
            { "64000", "At the closest point, which island group is only 50 miles south-east of the coast of Florida?", "Bahamas", "US Virgin Islands", "Turks and Caicos Islands", "Bermuda", "1"},
            { "125000", "Construction of which of these famous landmarks was completed first?" , "Empire State Building", "Royal Albert Hall", "Eiffel Tower", "Big Ben Clock Tower", "4" },
            { "250000", "Which of these cetaceans is classified as a 'toothed whale'?", "Gray whale", "Minke whale", "Sperm whale", "Humpback whale", "3" },
            { "500000", "Who is the only British politician to have held all four 'Great Offices of State' at some point during their career?" , "David Lloyd George", "Harold Wilson", "James Callaghan", "John Major", "3" },
            { "1 million", "In 1718, which pirate died in battle off the coast of what is now North Carolina?" , "Calico Jack", "Blackbeard", "Bartholomew Roberts", "Captain Kidd", "2" }
        };

        private DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1);
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (CB_Tijd.SelectedIndex==1)
            {
                TBL_Tijd.Text = DateTime.Now.ToShortDateString();
            }
            else if (CB_Tijd.SelectedIndex==2)
            {
                TBL_Tijd.Text= $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}"
            }
            else
            {
                TBL_Tijd.Text = DateTime.Now.ToLongTimeString();
            }
        }

        private void B_FinalAnswer_Click(object sender, RoutedEventArgs e)
        {
            // TODO:
            // 1. Welke RadioButton is aangeduid
            // 2. Controleer of antwoord juist is
            //      a) Nieuwe vraag inladen en prijsgeld updaten
            //      b) Verliest het spel, troost de speler
            int indexAntwoord = getIndexAntwoord();
            if (isAntwoordJuist(indexAntwoord))
            {
                LaadVolgendeVraag();
            }
            else
            {
                VerliesSpel();
            }
        }

        private void VerliesSpel()
        {
            throw new NotImplementedException();
        }

        private void LaadVolgendeVraag()
        {
            throw new NotImplementedException();
        }

        private bool isAntwoordJuist(int indexAntwoord)
        {
            throw new NotImplementedException();
        }

        private int getIndexAntwoord()
        {
            throw new NotImplementedException();
        }


    }
}
