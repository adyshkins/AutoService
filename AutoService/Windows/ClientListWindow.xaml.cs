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

        List<Client> filterClientList = Context.Client.ToList();


        List<Client> pageList = new List<Client>();

        public ClientListWindow()
        {
            InitializeComponent();

            filterCmb.ItemsSource = sort;
            filterCmb.SelectedIndex = 0;

            genderCmb.ItemsSource = gender;
            genderCmb.SelectedIndex = 0;

            CountClientCmb.ItemsSource = countClient;
            CountClientCmb.SelectedIndex = 0;

           
            listUser.ItemsSource = filterClientList;
        }

        private void Filter()
        {
           

            if (DateOfBirthCheck.IsChecked == true)
            {
                listUser.ItemsSource = pageList
                .Where(i => i.Email.ToLower().Contains(emailSearchTxt.Text.ToLower()))
                .Where(i => i.Phone.ToLower().Contains(phoneSearchTxt.Text.ToLower()))
                .Where(i => i.FIO.ToLower().Contains(nameSearchTxt.Text.ToLower()))
                .Where(i => i.BirthDate.Month == DateTime.Now.Month);
                //.OrderBy(i => i.LastName);
            }
            else if(DateOfBirthCheck.IsChecked == false)
            {
                listUser.ItemsSource = pageList
                 .Where(i => i.Email.ToLower().Contains(emailSearchTxt.Text.ToLower()))
                 .Where(i => i.Phone.ToLower().Contains(phoneSearchTxt.Text.ToLower()))
                 .Where(i => i.FIO.ToLower().Contains(nameSearchTxt.Text.ToLower()));
                 //.OrderBy(i => i.LastName);
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
                listUser.ItemsSource = Context.Client.ToList();
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

        private void genderCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void CountClientCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string cmbValue = CountClientCmb.SelectedItem.ToString();
            listUser.ItemsSource = filterClientList;
            int countClient;
            pageList.Clear();
            

            switch (cmbValue)
            {
                case "Все":
                    countClient = Context.Client.ToList().Count;
                    break;

                case "10":
                    if (Context.Client.ToList().Count >= 10)
                    { 
                        countClient = 10;
                    }
                    else
                    {
                        countClient = Context.Client.ToList().Count;
                    }
                    break;

                case "50":
                    if (Context.Client.ToList().Count >= 50)
                    {
                        countClient = 50;
                    }
                    else
                    {
                        countClient = Context.Client.ToList().Count;
                    }
                    break;

                case "200":
                    if (Context.Client.ToList().Count >= 200)
                    {
                        countClient = 200;
                    }
                    else
                    {
                        countClient = Context.Client.ToList().Count;
                    }
                    break;

                default:
                    countClient = Context.Client.ToList().Count;
                    break;
            }

            

            for (int i = 0; i < countClient; i++)
            {
                pageList.Add(filterClientList[i]);
            }

            listUser.ItemsSource = pageList;
        }
    }
}
