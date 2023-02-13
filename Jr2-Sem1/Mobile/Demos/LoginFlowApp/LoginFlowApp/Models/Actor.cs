using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginFlowApp.Models
{
    public class Actor : INotifyPropertyChanged
    {
        private string name;
        private string firstName;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                FireTheEvent(nameof(Name));
            }
        }
        public string FirstName { 
            get => firstName; 
            set
            {
                firstName = value;
                FireTheEvent(nameof(FirstName));
            }
        }
        public int BirthYear { get; set; }

        public string ProfilePictureUrl { get; set; }

        public void FireTheEvent(string propname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
