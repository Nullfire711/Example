using Microsoft.Win32;
using System.IO;
using System.Windows.Media.Imaging;
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
using System.Windows.Shapes;
using static OPExample.AppData.DataFrame;

namespace OPExample
{
    /// <summary>
    /// Логика взаимодействия для WindowClientAdd.xaml
    /// </summary>
    public partial class WindowClientAdd : Window
    {
        private string pathPhoto;
        public WindowClientAdd()
        {
            InitializeComponent();
            CBRole.ItemsSource = context.Role.Select(i => i.nameRole).ToList();
        }

        private void AddClientBtn_Click(object sender, RoutedEventArgs e)
        {
            //Имя и Отчество могут быть составными в случае с Китайцами, Корейцами, Испанцами и Французами,
            //поэтому нет смысла ограничивать ввод пробела в этих полях.
            //Пароль так же может иметь пробел
            //В итоге ограничение на наличие пробелов рационально оставить только в поле Login
            if (TBlogin.Text.Contains(" "))
            {
                MessageBox.Show("Поле Login не должно содержать пробелов!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                TBlogin.Clear();
                return;
            }

            if (string.IsNullOrWhiteSpace(TBFname.Text) ||
               string.IsNullOrWhiteSpace(TBLname.Text) ||
               string.IsNullOrWhiteSpace(TBlogin.Text) ||
               string.IsNullOrWhiteSpace(TBPassword.Text) ||
               CBRole.SelectedItem == null)
            {
                MessageBox.Show("Заполните обязательные для ввода поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if(TBPassword.Text.Length < 6)
            {
                MessageBox.Show("Длина пароля не должна быть менее 6 символов!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            


            if (pathPhoto != null)
            {
                context.User.Add(new User
                {
                    lastName = TBLname.Text,
                    firstName = TBFname.Text,
                    middleName = TBMname.Text,
                    login = TBlogin.Text,
                    password = TBPassword.Text,
                    idRole = context.Role.Where(i => i.nameRole == CBRole.SelectedItem.ToString()).Select(i => i.idRole).FirstOrDefault(),
                    image = File.ReadAllBytes(pathPhoto)
                });
            }
            else
            {
                context.User.Add(new User
                {
                    lastName = TBLname.Text,
                    firstName = TBFname.Text,
                    middleName = TBMname.Text,
                    login = TBlogin.Text,
                    password = TBPassword.Text,
                    idRole = context.Role.Where(i => i.nameRole == CBRole.SelectedItem.ToString()).Select(i => i.idRole).FirstOrDefault()
                });
            }
            
            context.SaveChanges();
            MessageBox.Show($"Клиент {TBLname.Text} {TBFname.Text} успешно добавлен!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void BtnAddImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == true)
            {
                photoUser.Source = new BitmapImage(new Uri(fileDialog.FileName));

                pathPhoto = fileDialog.FileName;
            }
        }
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TBLName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBLname.Text))
            {
                TBLname.BorderBrush = Brushes.Red;
            }
            else
            {
                TBLname.BorderBrush = Brushes.Green;
            }
        }

        private void TBFame_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBFname.Text))
            {
                TBFname.BorderBrush = Brushes.Red;
            }
            else
            {
                TBFname.BorderBrush = Brushes.Green;
            }
        }

        private void TBMname_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBMname.Text))
            {
                TBMname.BorderBrush = Brushes.Red;
            }
            else
            {
                TBMname.BorderBrush = Brushes.Green;
            }
        }

        private void TBlogin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBlogin.Text))
            {
                TBlogin.BorderBrush = Brushes.Red;
            }
            else
            {
                TBlogin.BorderBrush = Brushes.Green;
            }
        }

        private void TBPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBPassword.Text))
            {
                TBPassword.BorderBrush = Brushes.Red;
            }
            else
            {
                TBPassword.BorderBrush = Brushes.Green;
            }
        }

        private void TBLname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = (!Char.IsLetter(e.Text, 0));
        }

        private void TBFname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = (!Char.IsLetter(e.Text, 0));
        }

        private void TBMname_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = (!Char.IsLetter(e.Text, 0));
        }

        private void TBlogin_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            
        }
    }
}
