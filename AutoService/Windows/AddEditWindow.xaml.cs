using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using static AutoService.EF.AppData;

namespace AutoService.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        private bool _isEdit;
        private string pathPhoto = null;

        public AddEditWindow()
        {
            InitializeComponent();

            _isEdit = false;
        }

        public AddEditWindow(Client client)
        {
            InitializeComponent();

            _isEdit = true;

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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            genderCmb.ItemsSource = Context.Gender.ToList();
            genderCmb.DisplayMemberPath = "Name";
        }


        private void closeBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (_isEdit == false)
            {
                if (pathPhoto == null)
                {
                    MessageBox.Show("нет фото");
                    return;
                }
                try
                {
                    Context.Client.Add(new Client()
                    {
                        FirstName = firstNameTxt.Text,
                        LastName = lastNameTxt.Text,
                        MiddleName = middleNameTxt.Text,
                        Email = emailTxt.Text,
                        Phone = phoneTxt.Text,
                        BirthDate = birthDatePck.SelectedDate.Value,
                        GenderId = Context.Gender.Where(i => i.Name == genderCmb.Text).Select(i => i.Id).FirstOrDefault(),
                        IdTag = 1,
                        Photo = File.ReadAllBytes(pathPhoto),
                        RegDate = DateTime.Now
                    });


                    Context.SaveChanges();

                    MessageBox.Show("Пользователь добавлен");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void choosePhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                photoUser.Source = new BitmapImage(new Uri(fileDialog.FileName));
                pathPhoto = fileDialog.FileName;
            }
        }


    }
}
