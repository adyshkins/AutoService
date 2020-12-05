using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
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
using static AutoService.HelpClass.StringGen;

namespace AutoService.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        private bool _isEdit;
        private string pathPhoto = null;
        Client clientEdit;
        private bool _isChoosePhoto = false;
        int countClick = 0;



        public AddEditWindow()
        {
            InitializeComponent();

            _isEdit = false;
        }

        public AddEditWindow(Client client)
        {
            InitializeComponent();

            this.Width = 800;
            _isEdit = true;
            clientEdit = client;

            lastNameTxt.Text = client.LastName;
            firstNameTxt.Text = client.FirstName;
            middleNameTxt.Text = client.MiddleName;
            phoneTxt.Text = client.Phone;
            emailTxt.Text = client.Email;

            gridTag.ItemsSource = Context.Client.Where(i => i.Id == client.Id).ToList();

            using (MemoryStream stream = new MemoryStream(client.Photo))
            {
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                bitmapImage.StreamSource = stream;
                bitmapImage.EndInit();
                photoUser.Source = bitmapImage;
            }




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
            if (string.IsNullOrWhiteSpace(lastNameTxt.Text))
            {
                MessageBox.Show("Поле фамилия не может быть пустым");
                return;
            }

            if (string.IsNullOrWhiteSpace(firstNameTxt.Text))
            {
                MessageBox.Show("Поле имя  не может быть пустым");
                return;
            }

            if (genderCmb.SelectedIndex == -1)
            {
                MessageBox.Show("Укажите пол");
                return;
            }

            if (string.IsNullOrWhiteSpace(phoneTxt.Text))
            {
                MessageBox.Show("Поле телефон не может быть пустым");
                return;
            }

            if (!birthDatePck.SelectedDate.HasValue)
            {
                MessageBox.Show("Укажите дату рождения");
                return;
            }

            if (string.IsNullOrWhiteSpace(emailTxt.Text))
            {
                MessageBox.Show("Поле электронная почта не может быть пустым");
                return;
            }

            if (pathPhoto == null)
            {
                MessageBox.Show("нет фото");
                return;
            }

            /*
             sdfsdf @  sdfsdf.ru
             sdfsdf . ru
             */

            string[] mail1 = emailTxt.Text.Split('@');

            if (mail1.Length == 2 && mail1[0].Length > 0 && mail1[1].Length > 0)
            {
                string[] mail2 = mail1[1].Split('.');
                if (mail2.Length != 2 || mail2[0].Length == 0 || mail2[1].Length == 0)
                {
                    MessageBox.Show("Неверный формат электронной почты");
                    return;  
                }
            }
            else
            {
                MessageBox.Show("Неверный формат электронной почты.");
                return;
            }


            if (_isEdit == false)
            {
                // Добавление нового клиента
                
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

            else
            {
                // Изменение клиента
                clientEdit.FirstName = firstNameTxt.Text;
                clientEdit.LastName = lastNameTxt.Text;
                clientEdit.MiddleName = middleNameTxt.Text;
                clientEdit.Email = emailTxt.Text;
                clientEdit.Phone = phoneTxt.Text;
                clientEdit.BirthDate = birthDatePck.SelectedDate.Value;
                clientEdit.GenderId = Context.Gender.Where(i => i.Name == genderCmb.Text).Select(i => i.Id).FirstOrDefault();

                if (_isChoosePhoto == true)
                {
                    clientEdit.Photo = File.ReadAllBytes(pathPhoto);
                }

                Context.SaveChanges();
                MessageBox.Show("Данные сохранены успешно");
                this.Close();
            }
        }

        private void choosePhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            // диалоговое окно для выбора изобрадения пользователя
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                photoUser.Source = new BitmapImage(new Uri(fileDialog.FileName));
                pathPhoto = fileDialog.FileName;
                _isChoosePhoto = true;
            }
        }

        private void tagBtn_Click(object sender, RoutedEventArgs e)
        {
            // изменение размера окна, для работы с тегами
            countClick++;

            if (countClick % 2 == 1)
            {
                this.Width = 800;
            }
            else
            {
                this.Width = 400;
            }
        }

        private void updateTagBtn_Click(object sender, RoutedEventArgs e)
        {
            // Должно быть обновление Списка тегов клиента
        }

        private void lastNameTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = StringGenName().IndexOf(e.Text) < 0; // запрет ввода всех символов кроме StringGen()
        }

        private void emailTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = StringGenEmail().IndexOf(e.Text) < 0; // запрет ввода всех символов кроме StringGen()
        }

        private void emailTxt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space) // запрет ввода пробела
            {
                e.Handled = true;
            }
        }

        private void phoneTxt_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = StringGenPhone().IndexOf(e.Text) < 0; // запрет ввода всех символов кроме StringGen()
        }
    }
}
