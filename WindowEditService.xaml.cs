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
    /// Логика взаимодействия для WindowEditService.xaml
    /// </summary>
    public partial class WindowEditService : Window
    {
        private string pathPhoto;
        public WindowEditService()
        {
            InitializeComponent();
            var service = context.Service.Where(i => i.idService == idService).FirstOrDefault();

            ServiceName.Text = service.ServiceName;
            TBPrice.Text = service.Price.ToString();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
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

        private void TBPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (TBPrice.Text.Length > 7)
            {
                e.Handled = true;
            }
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                return;
            }

            e.Handled = true;
        }

        private void EditServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            long price;
            Int64.TryParse(TBPrice.Text, out price);
            var service = context.Service.Where(i => i.idService == idService).FirstOrDefault();
            if (pathPhoto != null)
            {
                service.ServiceName = ServiceName.Text;
                service.Price = price;
                service.image = File.ReadAllBytes(pathPhoto);
            }
            else
            {
                service.ServiceName = ServiceName.Text;
                service.Price = price;
            }

            context.SaveChanges();
            MessageBox.Show($"Данные успешно сохранены!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }
    }
}
