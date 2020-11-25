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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static AutoService.EF.AppData;

namespace AutoService
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class ClientListWindow : Window
    {

        List<string> sort = new List<string>() {"Все", "фамилии", "дате последнего посещения", "количеству посещений"};

        List<string> gender = new List<string>() {"Все","Мужской", "Женский"};

        List<string> countClient = new List<string>() {"Все","10", "50", "200"};




        /*
        "фамилии"
        "дате последнего посещения"
        "количеству посещений"
         
         
         */
        public ClientListWindow()
        {
            InitializeComponent();

            filterCmb.ItemsSource = sort;
            filterCmb.SelectedIndex = 0;

            genderCmb.ItemsSource = gender;
            genderCmb.SelectedIndex = 0;

            Cmb.ItemsSource = countClient;
            Cmb.SelectedIndex = 0;

            listUser.ItemsSource = Context.Client.ToList();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
