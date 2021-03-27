﻿using System;
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
        //длина круга
        public int CircleLength = 100;
        //сохраненный транспорт
        public List<Transport> TransportList = new List<Transport>();
        //сколько миллисекунд реального времени займет замена колеса
        public int WheelPunctureTimeConsumingMs = 1000;
        //Понижение скорости за один кг веса в грузовике
        public double Truck_CargoWeightKgSpeedConsuming = 0.05;
        //Понижение скорости за одного человека в легковушке
        public double Car_OneManSpeedConsuming = 1;
        //Понижение скорости за коляску у мотоцикла
        public double Motorcycle_CarriageSpeedConsuming = 10;

        //Сохранение конфига
        public static void SaveConfig(Config config)
        {
           
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            string json = JsonConvert.SerializeObject(config, settings);
            File.WriteAllText("Config.json", json);
        }
        //Загрузка конфига
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
