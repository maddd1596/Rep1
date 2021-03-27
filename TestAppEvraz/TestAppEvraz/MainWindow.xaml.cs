using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            config = Config.LoadConfig();
            if(config == null)
            {
                config = new Config();
            }
            TransportList.Clear();
            foreach(Transport item in config.TransportList)
            {
                TransportList.Add(item);
            }
            CircleLengthTB.Text = config.CircleLength.ToString();
        }

        Config config = new Config();
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
                    TransportWindow mtw = new TransportWindow(chosenType, config);
                    mtw.ShowDialog();
                    
                    TransportList.Add(new Motorcycle(TransportList.Count > 0 ? TransportList.Max(t => t.Id + 1) : 1)
                    {
                        Name =  mtw.NameTB.Text,
                        TransportType = chosenType,
                        Speed = uint.Parse(mtw.SpeedTB.Text),
                        WheelPunctureProbabilityPercent = int.Parse(mtw.WheelPunctureTB.Text),
                        HaveCarriage = mtw.AdditionalInfoChB.IsChecked.Value,
                        AdditionalInfo = mtw.AdditionalInfoChB.IsChecked.Value == true ? "С коляской" : "Без коляски",
                        ActualSpeed = mtw.AdditionalInfoChB.IsChecked.Value ? double.Parse(mtw.SpeedTB.Text) - config.Motorcycle_CarriageSpeedConsuming : double.Parse(mtw.SpeedTB.Text)
                    });
                    config.TransportList.Clear();
                    config.TransportList.AddRange(TransportList);
                    break;
                case "Грузовик":
                    TransportWindow ttw = new TransportWindow(chosenType, config);
                    ttw.ShowDialog();

                    TransportList.Add(new Truck(TransportList.Count > 0 ? TransportList.Max(t => t.Id + 1) : 1)
                    {
                        Name = ttw.NameTB.Text,
                        TransportType = chosenType,
                        Speed = uint.Parse(ttw.SpeedTB.Text),
                        WheelPunctureProbabilityPercent = int.Parse(ttw.WheelPunctureTB.Text),
                        CargoWeight = int.Parse(ttw.AdditionalInfoTB.Text),
                        AdditionalInfo = "Вес груза: " + ttw.AdditionalInfoTB.Text + " кг",
                        ActualSpeed = double.Parse(ttw.SpeedTB.Text) - uint.Parse(ttw.AdditionalInfoTB.Text) * config.Truck_CargoWeightKgSpeedConsuming
                    });
                    config.TransportList.Clear();
                    config.TransportList.AddRange(TransportList);
                    break;
                case "Легковая машина":
                    TransportWindow ctw = new TransportWindow(chosenType, config);
                    ctw.ShowDialog();

                    TransportList.Add(new Car(TransportList.Count > 0 ? TransportList.Max(t => t.Id + 1) : 1)
                    {
                        Name = ctw.NameTB.Text,
                        TransportType = chosenType,
                        Speed = uint.Parse(ctw.SpeedTB.Text),
                        WheelPunctureProbabilityPercent = int.Parse(ctw.WheelPunctureTB.Text),
                        PeopleInsideCount = int.Parse(ctw.AdditionalInfoTB.Text),
                        AdditionalInfo = "Человек внутри: " + ctw.AdditionalInfoTB.Text,
                        ActualSpeed = double.Parse(ctw.SpeedTB.Text) - uint.Parse(ctw.AdditionalInfoTB.Text) * config.Car_OneManSpeedConsuming
                    });
                    config.TransportList.Clear();
                    config.TransportList.AddRange(TransportList);
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
                    config.TransportList.Clear();
                    config.TransportList.AddRange(TransportList);
                }
            }
        }

        private void SaveRowsBtn_Click(object sender, RoutedEventArgs e)
        {
            if(CircleLengthValid())
            {
                config.CircleLength = int.Parse(CircleLengthTB.Text);
            }
            Config.SaveConfig(config);
        }

        private void StartCircleBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CircleLengthValid())
            {
                config.CircleLength = int.Parse(CircleLengthTB.Text);

                Task.Run(() =>
                {
                    ObservableCollection<RaceResultModel> raceResults = new ObservableCollection<RaceResultModel>();
                    Dispatcher?.Invoke(() =>
                    {
                        AddRowBtn.IsEnabled = 
                        RemoveRowBtn.IsEnabled = 
                        SaveRowsBtn.IsEnabled = 
                        StartCircleBtn.IsEnabled = 
                        false;
                        ResultTable.ItemsSource = null;
                        ResultTable.Items.Clear();
                        ResultTable.ItemsSource = raceResults;
                        foreach(Transport item in TransportList)
                        {
                            raceResults.Add(new RaceResultModel() { Transport = item, TransportName = item.Name });
                        }
                    });

                    
                    
                    Parallel.ForEach(raceResults, raceResult =>
                    {
                        raceResult.Transport.StartRace(config, ref raceResult);                        
                    });

                    Dispatcher?.Invoke(() =>
                    {

                        ResultTable.ItemsSource = raceResults;
                        AddRowBtn.IsEnabled =
                        RemoveRowBtn.IsEnabled =
                        SaveRowsBtn.IsEnabled =
                        StartCircleBtn.IsEnabled =
                        true;
                    });
                });
            }
        }

        private bool CircleLengthValid()
        {
            if(Regex.Match(CircleLengthTB.Text, @"\D").Success)
            {
                MessageBox.Show("Некорректно указана длинна круга");
                return false;
            }
            return true;
        }

    }
}
