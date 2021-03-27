using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TestAppEvraz.Windows
{
    /// <summary>
    /// Логика взаимодействия для TransportWindow.xaml
    /// </summary>
    public partial class TransportWindow : Window
    {
        public TransportWindow(string type, Config conf, bool isNew = true)
        {
            InitializeComponent();
            TransportType = type;
            AdditionalInfoChB.IsChecked = false;
            config = conf;
            IsNew = isNew;
        }
        bool IsNew;
        Config config;
        string TransportType;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Отображаем только нужные контролы
            switch(TransportType)
            {
                case "Мотоцикл": AdditionalInfoChB.Visibility = Visibility.Visible; 
                    AdditionalInfoTB.Visibility = Visibility.Collapsed; 
                    AdditionalInfoLabel.Content = "Есть коляска?"; 
                    break;
                case "Грузовик":
                    AdditionalInfoChB.Visibility = Visibility.Collapsed;
                    AdditionalInfoTB.Visibility = Visibility.Visible;
                    AdditionalInfoLabel.Content = "Вес груза, кг";
                    break;
                case "Легковая машина":
                    AdditionalInfoChB.Visibility = Visibility.Collapsed;
                    AdditionalInfoTB.Visibility = Visibility.Visible;
                    AdditionalInfoLabel.Content = "Кол-во человек внутри";
                    break;
                default: break;
            }
        }

        private void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            //Проверяем всё ли в порядке с данными
            uint checkInt = 0;
            if(!Regex.Match(NameTB.Text, @"(\w|\d)").Success)
            {
                MessageBox.Show("Некорректно указано имя");
                return;
            }
            else
            {
                if (config.TransportList.Any(t => t.Name == NameTB.Text) && IsNew)
                {
                    MessageBox.Show("Имя уже существует");
                    return;
                }
            }
            if(!uint.TryParse(SpeedTB.Text, out checkInt))
            {
                MessageBox.Show("Некорректно указана скорость");
                return;     
            }
            else
            {

                if (checkInt <= 0)
                {
                    MessageBox.Show("Скорость должна быть больше нуля");
                    return;
                }                
            }
            if (!uint.TryParse(WheelPunctureTB.Text, out checkInt))
            {
                MessageBox.Show("Некорректно указана вероятность прокола колеса");
                return;
            }
            
            if(TransportType == "Грузовик")
            {
                if (!uint.TryParse(AdditionalInfoTB.Text, out checkInt))
                {
                    MessageBox.Show("Некорректно указан вес груза");
                    return;
                }
                
            }
            else if (TransportType == "Легковая машина")
            {
                if (!uint.TryParse(AdditionalInfoTB.Text, out checkInt))
                {
                    MessageBox.Show("Некорректно указано кол-во человек внутри");
                    return;
                }                
            }
            

            //В этом блоке удостоверимся, что коляска, вес или люди в салоне не снизят скорось транспорта до нуля
            if (TransportType == "Грузовик")
            {
                var ch = double.Parse(SpeedTB.Text) - (config.Truck_CargoWeightKgSpeedConsuming * double.Parse(AdditionalInfoTB.Text));
                if (double.Parse(SpeedTB.Text) - (config.Truck_CargoWeightKgSpeedConsuming * double.Parse(AdditionalInfoTB.Text)) <= 0)
                {
                    MessageBox.Show($"За один кг веса от скорости отнимается {config.Truck_CargoWeightKgSpeedConsuming} км/ч." +
                     $"\n С нынешними показателями мы просто не поедем");
                    return;
                }                
            }
            else if (TransportType == "Легковая машина")
            {
                var ch = double.Parse(SpeedTB.Text) - (config.Car_OneManSpeedConsuming * double.Parse(AdditionalInfoTB.Text));
                if (double.Parse(SpeedTB.Text) - (config.Car_OneManSpeedConsuming * double.Parse(AdditionalInfoTB.Text)) <= 0)
                {
                    MessageBox.Show($"За одного человека в машине от скорости отнимается {config.Car_OneManSpeedConsuming} км/ч." +
                    $"\n С нынешними показателями мы просто не поедем");
                    return;
                }               
            }
            else if (TransportType == "Мотоцикл")
            {
                if (double.Parse(SpeedTB.Text) - config.Motorcycle_CarriageSpeedConsuming <= 0 && AdditionalInfoChB.IsChecked.Value)                     
                {
                    MessageBox.Show($"Коляска отнимает от скорости {config.Motorcycle_CarriageSpeedConsuming} км/ч." +
                     $"\n С нынешними показателями мы просто не поедем");
                    return;
                }
                
            }
            this.Close();
        }
    }
}
