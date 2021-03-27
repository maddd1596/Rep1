using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAppEvraz.Entities;
using Newtonsoft.Json;
using System.IO;

namespace TestAppEvraz
{
    public class Config
    {
        public int CircleLength = 100;
        public List<Transport> TransportList = new List<Transport>();
        public int WheelPunctureTimeConsumingMs = 1000;
        public double Truck_CargoWeightKgSpeedConsuming = 0.05;
        public double Car_OneManSpeedConsuming = 1;
        public double Motorcycle_CarriageSpeedConsuming = 10;

        public static void SaveConfig(Config config)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            string json = JsonConvert.SerializeObject(config, settings);
            File.WriteAllText("Config.json", json);
        }

        public static Config LoadConfig()
        {
            try
            {
                if (!File.Exists("Config.json"))
                {
                    return null;
                }
                string json = File.ReadAllText("Config.json");
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                Config config = JsonConvert.DeserializeObject<Config>(json, settings);
                return config;
            }
            catch(Exception ex)
            {               
                return null;
            }            
        }
    }

    
}
