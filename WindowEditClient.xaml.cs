using Microsoft.Win32;
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
using static OPExample.AppData.DataFrame;

namespace OPExample
{
    /// <summary>
    /// Логика взаимодействия для WindowEditClient.xaml
    /// </summary>
    public partial class WindowEditClient : Window
    {
        private string pathPhoto;
        public WindowEditClient()
        {
            InitializeComponent();
            CBRole.ItemsSource = context.Role.Select(i => i.nameRole).ToList();
            var user = context.User.Where(i => i.idUser == idClient).FirstOrDefault();
            CBRole.SelectedItem = context.Role.Where(i => i.idRole == user.idRole).Select(i => i.nameRole).FirstOrDefault();

            TBLname.Text = user.lastName;
            TBFname.Text = user.firstName;
            TBMname.Text = user.middleName;
            TBlogin.Text = user.login;
            TBPassword.Text = user.password;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void EditClientBtn_Click(object sender, RoutedEventArgs e)
        {
            var user = context.User.Where(i => i.idUser == idClient).FirstOrDefault();
            if(pathPhoto != null)
            {
                user.lastName = TBLname.Text;
                user.firstName = TBFname.Text;
                user.middleName = TBMname.Text;
                user.login = TBlogin.Text;
                user.password = TBPassword.Text;
                user.idRole = context.Role.Where(i => i.nameRole == CBRole.SelectedItem.ToString()).Select(i => i.idRole).FirstOrDefault();
                user.image = File.ReadAllBytes(pathPhoto);
            }
            else
            {
                user.lastName = TBLname.Text;
                user.firstName = TBFname.Text;
                user.middleName = TBMname.Text;
                user.login = TBlogin.Text;
                user.password = TBPassword.Text;
                user.idRole = context.Role.Where(i => i.nameRole == CBRole.SelectedItem.ToString()).Select(i => i.idRole).FirstOrDefault();
            }
                

            context.SaveChanges();
            MessageBox.Show("Данные успешно сохранены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
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
    }
}
