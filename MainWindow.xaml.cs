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

namespace OPExample
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            FRMain.Navigate(new Uri("Pages/ClientApplication.xaml", UriKind.Relative));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FRMain.Navigate(new Uri("Pages/ClientList.xaml", UriKind.Relative));
        }


        private void ServiceListBtn_Click(object sender, RoutedEventArgs e)
        {
            FRMain.Navigate(new Uri("Pages/ServiceList.xaml", UriKind.Relative));
        }

        private void ClientListBtn_Click(object sender, RoutedEventArgs e)
        {
            FRMain.Navigate(new Uri("Pages/ClientList.xaml", UriKind.Relative));
        }

        private void BookAServiceBtn_Click(object sender, RoutedEventArgs e)
        {
            FRMain.Navigate(new Uri("Pages/PageBooking.xaml", UriKind.Relative));
        }

        private void BookingListBtn_Click(object sender, RoutedEventArgs e)
        {
            FRMain.Navigate(new Uri("Pages/BookingList.xaml", UriKind.Relative));
        }

        private void LogInBtn_Click(object sender, RoutedEventArgs e)
        {
            
            Authorization authorization = new Authorization();
            authorization.ShowDialog();
            if(authorization.User.user != null)
            {
                LogInBtn.Visibility = Visibility.Hidden;
                RegistrationBtn.Visibility = Visibility.Hidden;
                LogOutBtn.Visibility = Visibility.Visible;
            }
        }
    }
}
