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
    /// Логика взаимодействия для ServiceList.xaml
    /// </summary>
    public partial class ServiceList : Page
    {
        long MinPrice = 0;
        long MaxPrice = 0;
        public ServiceList()
        {
            InitializeComponent();
            LVPage.ItemsSource = context.Service.ToList();
        }

        private void TBName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void TBMin_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Int64.TryParse(TBMin.Text, out MinPrice);
            }
            catch(Exception ex)
            {
                throw new Exception("Ошибка", ex);
            }

            Filter();
        }

        private void TBMax_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Int64.TryParse(TBMax.Text, out MaxPrice);
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка", ex);
            }

            Filter();
        }

        public void Filter()
        {
            var list = context.Service.Where(i => i.ServiceName.Contains(TBName.Text)).ToList();

            LVPage.ItemsSource = list;

            if (TBMin.Text == "" && TBMax.Text != "")
            {
                list = context.Service.Where(i => i.ServiceName.Contains(TBName.Text))
                                      .Where(i => i.Price < MaxPrice).ToList();

                LVPage.ItemsSource = list;
            }
            else if (TBMin.Text != "" && TBMax.Text == "")
            {
                list = context.Service.Where(i => i.ServiceName.Contains(TBName.Text))
                                      .Where(i => i.Price > MinPrice).ToList();

                LVPage.ItemsSource = list;
            }
            else if (TBMin.Text != "" && TBMax.Text != "")
            {
                list = context.Service.Where(i => i.ServiceName.Contains(TBName.Text))
                                      .Where(i => i.Price > MinPrice)
                                      .Where(i => i.Price < MaxPrice).ToList();

                LVPage.ItemsSource = list;
            }

        }

        private void TBMin_KeyDown(object sender, KeyEventArgs e)
        {
            Check(e);
        }

        private void TBMax_KeyDown(object sender, KeyEventArgs e)
        {
            Check(e);
        }

        private void Check(KeyEventArgs e)
        {
            if ((e.Key >= Key.D0 && e.Key <= Key.D9) || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9))
            {
                return;
            }

            e.Handled = true;
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            WindowServiceAdd windowServiceAdd = new WindowServiceAdd();
            windowServiceAdd.ShowDialog();
            LVPage.ItemsSource = context.Service.ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (LVPage.SelectedItem is Service service)
            {
                idService = service.idService;
                WindowEditService windowEditService = new WindowEditService();
                windowEditService.ShowDialog();
                LVPage.ItemsSource = context.Service.ToList();
            }
            else
            {
                MessageBox.Show("ВЫберите услугу из списка!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите удалить запись?",
                                        "Удаление услуги", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var result2 = MessageBox.Show("Точно?", "Удаление услуги", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result2 == MessageBoxResult.Yes)
                {
                    if (LVPage.SelectedItem is Service service)
                    {
                        context.Service.Remove(context.Service.Where(i => i.idService == service.idService).FirstOrDefault());
                        context.SaveChanges();
                        MessageBox.Show("Запись успешно удалена!",
                                        "Удаление услуги", MessageBoxButton.OK, MessageBoxImage.Information);
                        LVPage.ItemsSource = context.Service.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Выберите услугу из списка!",
                                        "Удаление услуги", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
            }
        }
    }
}
