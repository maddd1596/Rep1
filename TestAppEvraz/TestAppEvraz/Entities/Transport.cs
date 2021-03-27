using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAppEvraz.Entities
{
    public abstract class Transport : INotifyPropertyChanged
    {

        public Transport(int id)
        {
            Id = id;
        }
                
        public event PropertyChangedEventHandler PropertyChanged;
        public int Id {
            get { return id; }
            set { id = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("id")); }
        }
        private int id { get; set; }
        public string Name
        {
            get { return name; }
            set { name = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("name")); }
        }
        private string name {get; set;}
        public string TransportType {
            get { return transportType; }
            set { transportType = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("transportType")); }
        }
        private string transportType { get; set; }
        public double Speed
        {
            get { return speed; }
            set { speed = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("speed")); }
        }
        private double speed { get; set; }
        public int WheelPunctureProbabilityPercent { 
            get
            {
                return wheelPunctureProbabilityPercent;
            }

            set
            {
                if(value >= 0 && value <= 100)
                {
                    wheelPunctureProbabilityPercent = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("wheelPunctureProbabilityPercent"));
                }
            }
        }

        private int wheelPunctureProbabilityPercent { get; set; }

        public string AdditionalInfo {
            get { return additionalInfo; }
            set { additionalInfo = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("additionalInfo")); }
        }
        private string additionalInfo { get; set; }

        public double ActualSpeed { get; set; }
       
        public void StartRace(Config config, ref RaceResultModel raceResult)
        {
            //int checkIntervalMs = (int)(((double)config.CircleLength) / ActualSpeed);
            int checkIntervalMs = (int)(1000 / ActualSpeed);
            //double coveredDistance = 0;
            //RaceResultModel raceResult = new RaceResultModel();
            raceResult.TransportName = this.Name;
            Stopwatch raceTimer = new Stopwatch();
            raceTimer.Restart();
            bool wheelWillBePunctured = false;
            int wheelPunctureMoment = new Random().Next(0, config.CircleLength);
            if (new Random().Next(0, 100) <= WheelPunctureProbabilityPercent)
            {
                wheelWillBePunctured = true;
            }
            for (int i = 0; i < config.CircleLength; i++)
            {
                System.Threading.Thread.Sleep(checkIntervalMs);
                raceResult.CoveredDistance += 1;

                if (wheelWillBePunctured && i == wheelPunctureMoment)
                {
                    raceResult.State = "Прокол! Чиним...";
                    System.Threading.Thread.Sleep(config.WheelPunctureTimeConsumingMs);
                }
                    
                
                raceResult.State = "В пути";
            }
            raceTimer.Stop();
            raceResult.RaceTimeHours += ((double)raceTimer.ElapsedMilliseconds / 1000);
            //return raceResult;
        }

    }
}
