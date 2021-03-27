using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppEvraz.Entities
{
    public class RaceResultModel : INotifyPropertyChanged
    {
        public Transport Transport { get; set; }
        public double RaceTimeHours
        {
            get { return Math.Round(raceTimeHours,2); }
            set { raceTimeHours = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("raceTimeHours")); }
        }
        private double raceTimeHours { get; set; }

        public int CoveredDistance
        {
            get { return coveredDistance; }
            set { coveredDistance = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("coveredDistance")); }
        }
        private int coveredDistance { get; set; }
        public string TransportName
        {
            get { return transportName; }
            set { transportName = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("transportName")); }
        }
        private string transportName { get; set; }

        public string State
        {
            get { return state; }
            set { state = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("state")); }
        }
        private string state { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

       
    }
}
