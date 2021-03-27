﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using TestAppEvraz.Entities;
using TestAppEvraz.Windows;

namespace TestAppEvraz
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ConfigTable.ItemsSource = TransportList;
        }

        public ObservableCollection<Transport> TransportList = new ObservableCollection<Transport>();
        private void AddRowBtn_Click(object sender, RoutedEventArgs e)
        {
            ChooseTransportTypeWindow window = new ChooseTransportTypeWindow();
            window.ShowDialog();            
            string chosenType = window.ChosenType;
            switch(chosenType)
            {
                case "Мотоцикл":
                    TransportWindow mtw = new TransportWindow(chosenType);
                    mtw.ShowDialog();
                    
                    TransportList.Add(new Motorcycle(TransportList.Count > 0 ? TransportList.Max(t => t.Id + 1) : 1)
                    {
                        Name =  mtw.NameTB.Text,
                        TransportType = chosenType,
                        Speed = uint.Parse(mtw.SpeedTB.Text),
                        WheelPunctureProbabilityPercent = int.Parse(mtw.WheelPunctureTB.Text),
                        HaveCarriage = mtw.AdditionalInfoChB.IsChecked.Value,
                        AdditionalInfo = mtw.AdditionalInfoChB.IsChecked.Value == true ? "С коляской" : "Без коляски"
                    });
                    break;
                case "Грузовик":
                    TransportWindow ttw = new TransportWindow(chosenType);
                    ttw.ShowDialog();

                    TransportList.Add(new Truck(TransportList.Count > 0 ? TransportList.Max(t => t.Id + 1) : 1)
                    {
                        Name = ttw.NameTB.Text,
                        TransportType = chosenType,
                        Speed = uint.Parse(ttw.SpeedTB.Text),
                        WheelPunctureProbabilityPercent = int.Parse(ttw.WheelPunctureTB.Text),
                        CargoWeight = int.Parse(ttw.AdditionalInfoTB.Text),
                        AdditionalInfo = "Вес груза: " + ttw.AdditionalInfoTB.Text + " кг"
                    });
                    break;
                case "Легковая машина":
                    TransportWindow ctw = new TransportWindow(chosenType);
                    ctw.ShowDialog();

                    TransportList.Add(new Car(TransportList.Count > 0 ? TransportList.Max(t => t.Id + 1) : 1)
                    {
                        Name = ctw.NameTB.Text,
                        TransportType = chosenType,
                        Speed = uint.Parse(ctw.SpeedTB.Text),
                        WheelPunctureProbabilityPercent = int.Parse(ctw.WheelPunctureTB.Text),
                        PeopleInsideCount = int.Parse(ctw.AdditionalInfoTB.Text),
                        AdditionalInfo = "Человек внутри: " + ctw.AdditionalInfoTB.Text
                    });
                    break;
                default: break;
            }
            //ConfigTable.ItemsSource = TransportList;
            
        }

        private void RemoveRowBtn_Click(object sender, RoutedEventArgs e)
        {
            if(ConfigTable.SelectedItem != null)
            {
                int id = ((Transport)ConfigTable.SelectedItem).Id;
                
                if(TransportList.Any(t => t.Id == id))
                {
                    TransportList.Remove(TransportList.First(t => t.Id == id));
                }
            }
        }

        private void SaveRowBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}