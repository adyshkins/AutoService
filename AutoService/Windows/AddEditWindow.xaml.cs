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
using AutoService.EF;
using static AutoService.EF.AppData;

namespace AutoService.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        public AddEditWindow()
        {
            InitializeComponent();
        }

        public AddEditWindow(Client client)
        {
            InitializeComponent();
            genderCmb.ItemsSource = Context.Gender.ToList();
            genderCmb.DisplayMemberPath = "Name";
            
            lastNameTxt.Text = client.LastName;

            if (client.GenderId == "м")
            {
                genderCmb.SelectedIndex = 1;
            }
            else
            {
                genderCmb.SelectedIndex = 0;
            }

            birthDatePck.SelectedDate = client.BirthDate;


        }

        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
