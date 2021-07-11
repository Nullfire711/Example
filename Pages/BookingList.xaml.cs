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
    /// Логика взаимодействия для BookingList.xaml
    /// </summary>
    public partial class BookingList : Page
    {
        
        public BookingList()
        {
            InitializeComponent();
            LVBookingList.ItemsSource = context.Booking.ToList();
        }

        private void TB_ServiceName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void TB_login_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void DatePick_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void BTN_ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            TB_ServiceName.Clear();
            TB_login.Clear();
            DatePick.SelectedDate = null;
            LVBookingList.ItemsSource = context.Booking.ToList();
        }

        public void Filter()
        {
            var list = (from u in context.User
                    from s in context.Service
                    from b in context.Booking
                    where u.idUser == b.idUser
                    where s.idService == b.idService
                    where u.login.Contains(TB_login.Text) && s.ServiceName.Contains(TB_ServiceName.Text)
                    select b).ToList();


            if (DatePick.SelectedDate == null)
            {
                LVBookingList.ItemsSource = list;
            }
            else
            {
                list = list.Where(i => i.dateBooking.Equals(DatePick.SelectedDate)).ToList();
                LVBookingList.ItemsSource = list;
            }

        }

        
    }
}
