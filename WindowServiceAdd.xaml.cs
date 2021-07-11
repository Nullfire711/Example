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
    /// Логика взаимодействия для WindowServiceAdd.xaml
    /// </summary>
    public partial class WindowServiceAdd : Window
    {
        private string pathPhoto;
        public WindowServiceAdd()
        {
            InitializeComponent();


            //Создание собственного контекстного меню
            //TBPrice.ContextMenu = new ContextMenu();

            TBPrice.CommandBindings.Add(new CommandBinding(ApplicationCommands.Paste, OnPasteCommand));

        }

        private void OnPasteCommand(object sender, ExecutedRoutedEventArgs e)
        {
            //Специально пустой метод
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

        private void AddClientBtn_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ServiceName.Text) ||
               string.IsNullOrWhiteSpace(TBPrice.Text))
            {
                MessageBox.Show("Заполните обязательные для ввода поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            long price;
            Int64.TryParse(TBPrice.Text, out price);
            if (pathPhoto != null)
            {

                context.Service.Add(new Service
                {
                    ServiceName = ServiceName.Text,
                    Price = price,
                    image = File.ReadAllBytes(pathPhoto)
                }); 
            }
            else
            {
                context.Service.Add(new Service
                {
                    ServiceName = ServiceName.Text,
                    Price = price
                });
            }

            context.SaveChanges();
            MessageBox.Show($"Услуга {ServiceName.Text} успешно добавлена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TBPrice_KeyDown(object sender, KeyEventArgs e)
        {
            if (TBPrice.Text.Length > 7)
            {
                e.Handled = true;
            }
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || 
                (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                 e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)
            {
                return;
            }

            e.Handled = true;
        }

        private void TBPrice_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TBPrice.Text))
            {
                TBPrice.BorderBrush = Brushes.Red;
            }
            else
            {
                TBPrice.BorderBrush = Brushes.Green;
            }
        }

        

        private void TBPrice_PreviewMouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        
    }
}
