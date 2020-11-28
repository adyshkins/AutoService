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
using AutoService.EF;
using AutoService.Windows;

namespace AutoService
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class ClientListWindow : Window
    {

        List<string> sort = new List<string>() { "Все", "фамилии", "дате последнего посещения", "количеству посещений" };
        List<string> gender = new List<string>() { "Все", "Мужской", "Женский" };
        List<string> countClient = new List<string>() { "Все", "10", "50", "200" };

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

        private void Filter()
        {
            var filterClientList = Context.Client.ToList();

            if (DateOfBirthCheck.IsChecked == true)
            {
                listUser.ItemsSource = filterClientList
                .Where(i => i.Email.ToLower().Contains(emailSearchTxt.Text.ToLower()))
                .Where(i => i.Phone.ToLower().Contains(phoneSearchTxt.Text.ToLower()))
                .Where(i => i.FIO.ToLower().Contains(nameSearchTxt.Text.ToLower()))
               .Where(i => i.BirthDate.Month == DateTime.Now.Month);
            }
            else if(DateOfBirthCheck.IsChecked == false)
            {
                listUser.ItemsSource = filterClientList
                 .Where(i => i.Email.ToLower().Contains(emailSearchTxt.Text.ToLower()))
                 .Where(i => i.Phone.ToLower().Contains(phoneSearchTxt.Text.ToLower()))
                 .Where(i => i.FIO.ToLower().Contains(nameSearchTxt.Text.ToLower()));
            }
        }

        private void AddClientBtn_Click(object sender, RoutedEventArgs e)
        {
            AddEditWindow addEdit = new AddEditWindow();
            this.Hide();
            addEdit.ShowDialog();
            listUser.ItemsSource = Context.Client.ToList();
            this.Show();
        }

        private void EditClientBtn_Click(object sender, RoutedEventArgs e)
        {
            if (listUser.SelectedItem is Client client)
            {
                AddEditWindow addEdit = new AddEditWindow(client);
                this.Hide();
                addEdit.ShowDialog();
                this.Show();
            }

        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены?", "Удаление клиента", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                if (listUser.SelectedItem is Client client)
                {
                    Context.Client.Remove(client);
                    Context.SaveChanges();
                    MessageBox.Show("Клиент удален");
                    listUser.ItemsSource = Context.Client.ToList();
                }
            }
        }


        private void emailSearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void phoneSearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void nameSearchTxt_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void DateOfBirthCheck_Checked(object sender, RoutedEventArgs e)
        {
            Filter();
        }

        private void DateOfBirthCheck_Unchecked(object sender, RoutedEventArgs e)
        {
            Filter();
        }
    }
}
