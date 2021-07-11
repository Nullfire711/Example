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
using static OPExample.AppData.DataFrame;

namespace OPExample.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageBooking.xaml
    /// </summary>
    public partial class PageBooking : Page
    {
        public PageBooking()
        {
            InitializeComponent();

            LVClientBook.ItemsSource = context.User.ToList();

            LVServiceBooking.ItemsSource = context.Service.ToList();

        }

        private void TBNameService_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterServices();
        }

        private void FilterServices()
        {
            var list = context.Service.Where(i => i.ServiceName.Contains(TBNameService.Text)).ToList();

            LVServiceBooking.ItemsSource = list;
        }

        private void TBLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterUsers();
        }

        public void FilterUsers()
        {
            var list = context.User.Where(i => i.login.Contains(TBLogin.Text)).ToList();

            LVClientBook.ItemsSource = list;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if(DateBooking.SelectedDate == null)
            {
                MessageBox.Show("Выберите дату для записи на услугу!",
                                                "Запись на услугу", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (LVClientBook.SelectedItem is User user)
            {
                if(LVServiceBooking.SelectedItem is Service service)
                {
                    context.Booking.Add(new Booking
                    {
                        idUser = user.idUser,
                        idService = service.idService,
                        comment = TBComment.Text,
                        dateBooking = DateBooking.SelectedDate
                    });
                    context.SaveChanges();
                    MessageBox.Show("Запись на услугу успешно добавлена!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show("Выберите услугу из списка!",
                        "Запись на услугу", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента из списка!",
                    "Запись на услугу", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            LVClientBook.SelectedItem = null;
            LVServiceBooking.SelectedItem = null;
            DateBooking.SelectedDate = null;
            TBComment.Clear();
        }
    }
}
