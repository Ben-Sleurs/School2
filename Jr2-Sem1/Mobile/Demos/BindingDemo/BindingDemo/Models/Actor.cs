using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BindingDemo.Models
{
    internal class Actor : INotifyPropertyChanged
    {
        private string name;
        public string Name { 
            get => name;
            set {
                name = value;
                FireTheEvent(nameof(Name));
            }

        }
        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                FireTheEvent(nameof(FirstName));
            }

        }
        public int BirthYear { get; set; }
        public string ProfilePictureUrl { get; set; }
        public void FireTheEvent(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
