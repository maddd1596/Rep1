using System;
using System.Collections.Generic;
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
        public TransportWindow(string type)
        {
            InitializeComponent();
            TransportType = type;
            AdditionalInfoChB.IsChecked = false;
        }

        string TransportType;
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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
            bool Ok = false;
            uint checkInt = 0;
            if(Regex.Match(NameTB.Text, @"(\w|\d)").Success)
            {
                Ok = true;
            }
            else
            {
                Ok = false;
                MessageBox.Show("Некорректно указано имя");
            }
            if(uint.TryParse(SpeedTB.Text, out checkInt))
            {
                if (checkInt > 0)
                {
                    Ok = true;
                }
                else
                {
                    MessageBox.Show("Скорость должна быть больше нуля");
                }

            }
            else
            {
                Ok = false;
                MessageBox.Show("Некорректно указана скорость");
            }
            if (uint.TryParse(WheelPunctureTB.Text, out checkInt))
            {
                Ok = true;
            }
            else
            {
                Ok = false;
                MessageBox.Show("Некорректно указана вероятность прокола колеса");
            }
            if(TransportType == "Грузовик")
            {
                if (uint.TryParse(AdditionalInfoTB.Text, out checkInt))
                {
                    Ok = true;
                }
                else
                {
                    Ok = false;
                    MessageBox.Show("Некорректно указан вес груза");
                }
            }
            else if (TransportType == "Легковая машина")
            {
                if (uint.TryParse(AdditionalInfoTB.Text, out checkInt))
                {
                    Ok = true;
                }
                else
                {
                    Ok = false;
                    MessageBox.Show("Некорректно указано кол-во человек внутри");
                }
            }
            
            if (Ok)
            {
                this.Close();
            }
        }
    }
}
