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
    /// Логика взаимодействия для Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1()
        {
            InitializeComponent();
            LVPage.ItemsSource = context.User.ToList();

            List<Role> roles = context.Role.ToList();
            roles.Insert(0, new Role() { nameRole = "Все роли" });
            CBRole.ItemsSource = roles;
            CBRole.DisplayMemberPath = "nameRole";
            CBRole.SelectedIndex = 0;
        }

        private void TBLname_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void FMname_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void TBLogin_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void CBRole_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        public void Filter()
        {
            var list = context.User.Where(i => i.lastName.Contains(TBLname.Text))
                                    .Where(i => i.firstName.Contains(FMname.Text))
                                    .Where(i => i.login.Contains(TBLogin.Text)).ToList();

            LVPage.ItemsSource = list;

            if (CBRole.SelectedIndex == 0)
            {
                LVPage.ItemsSource = list;
            }
            else
            {
                var Role = CBRole.SelectedItem as Role;
                list = list.Where(i => i.idRole == Role.idRole).ToList();
                LVPage.ItemsSource = list;
            }
        }

        private void BtnAddNew_Click(object sender, RoutedEventArgs e)
        {
            WindowClientAdd windowClientAdd = new WindowClientAdd();
            windowClientAdd.ShowDialog();
            LVPage.ItemsSource = context.User.ToList();
        }

        private void EditBtn_Click(object sender, RoutedEventArgs e)
        {
            if(LVPage.SelectedItem is User user)
            {
                idClient = user.idUser;
                WindowEditClient windowEditClient = new WindowEditClient();
                windowEditClient.ShowDialog();
                LVPage.ItemsSource = context.User.ToList();
            }
            else
            {
                MessageBox.Show("ВЫберите клиента из списка!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (LVPage.SelectedItem is User user)
            {
                var result = MessageBox.Show("Вы действительно хотите удалить запись?",
                                         "Удаление клиента", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    if(context.Booking.Where(i => i.idUser == user.idUser).FirstOrDefault() != null)
                    {
                        MessageBox.Show("У выбранного пользователя есть записи на услуги, удаление запрещено!",
                            "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        context.User.Remove(context.User.Where(i => i.idUser == user.idUser).FirstOrDefault());
                        context.SaveChanges();
                        MessageBox.Show("Запись успешно удалена!",
                                        "Удаление клиента", MessageBoxButton.OK, MessageBoxImage.Information);
                        LVPage.ItemsSource = context.User.ToList();
                    }
                    



                    
                }
            }
            else
            {
                MessageBox.Show("Выберите пользователя из списка!",
                                "Удаление клиента", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            LVPage.SelectedItem = null;
            CBRole.SelectedIndex = 0;
            TBLname.Clear();
            TBLogin.Clear();
            FMname.Clear();
        }

        private void LVPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
