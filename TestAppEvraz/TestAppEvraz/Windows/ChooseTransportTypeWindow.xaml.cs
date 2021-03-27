using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace TestAppEvraz.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChooseTransportTypeWindow.xaml
    /// </summary>
    public partial class ChooseTransportTypeWindow : Window
    {
        public ChooseTransportTypeWindow()
        {
            InitializeComponent();
            TypesLView.Items.Add("Мотоцикл");
            TypesLView.Items.Add("Грузовик");
            TypesLView.Items.Add("Легковая машина");
        }

        public string ChosenType { get; set; }
        private void ChooseBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TypesLView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChosenType = TypesLView.SelectedItem.ToString();
            
        }
    }
}
