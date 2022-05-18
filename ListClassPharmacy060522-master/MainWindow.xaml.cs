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
using ListClass.Classes;
using System.IO;
using Microsoft.Win32;

namespace ListClass
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
      
        public MainWindow()
        {
            InitializeComponent();
            
           
        }
        /// <summary>
        /// вывод всех препаратов
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrint_Click(object sender, RoutedEventArgs e)
        {
            DtgListPreparate.ItemsSource = ConnectHelper.pharmacies.ToList();
            DtgListPreparate.SelectedIndex = -1;
            //препарат с максимальной ценой
            double prMax = ConnectHelper.pharmacies.Max(x => x.PricePreparate);
            TxbMaxPrice.Text = ConnectHelper.pharmacies.First(x => x.PricePreparate == prMax).NamePreparate.ToString();
            //препарат с минимальной ценой
            double prMin = ConnectHelper.pharmacies.Min(x => x.PricePreparate);
            TxbMinPrice.Text = ConnectHelper.pharmacies.First(x => x.PricePreparate == prMin).NamePreparate.ToString();
        }
        /// <summary>
        /// сортировка по алфавиту
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbUp_Checked(object sender, RoutedEventArgs e)
        {
            DtgListPreparate.ItemsSource = ConnectHelper.pharmacies.OrderBy(x=>x.NamePreparate).ToList();
        }
        /// <summary>
        /// сортировка в обратном порядке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RbDown_Checked(object sender, RoutedEventArgs e)
        {
            DtgListPreparate.ItemsSource = ConnectHelper.pharmacies.OrderByDescending(x => x.NamePreparate).ToList();
        }
        /// <summary>
        /// поиск по названию
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DtgListPreparate.ItemsSource = ConnectHelper.pharmacies.Where(x => 
                x.NamePreparate.ToLower().Contains(TxtSearch.Text.ToLower())).ToList();
        }

       /// <summary>
       /// фильтр по количеству
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void CmbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (CmbFiltr.SelectedIndex == 0)
            {
                DtgListPreparate.ItemsSource = ConnectHelper.pharmacies.Where(x =>
                    x.CountPreparate >=0 && x.CountPreparate <= 10).ToList();
                MessageBox.Show("Недостаточное количество препаратов на складе!",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
                if (CmbFiltr.SelectedIndex == 1)
            {
                DtgListPreparate.ItemsSource = ConnectHelper.pharmacies.Where(x =>
                    x.CountPreparate >= 11 && x.CountPreparate <= 50).ToList();
                MessageBox.Show("Необходимо пополнить запасы препаратов в ближайшее время",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                DtgListPreparate.ItemsSource = ConnectHelper.pharmacies.Where(x =>
                   x.CountPreparate >= 51).ToList();
                MessageBox.Show("Достаточное количество препаратов на складе!",
                    "Внимание", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            WindowAddPreparate windowAdd = new WindowAddPreparate();
            windowAdd.ShowDialog();
        }

       

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            DtgListPreparate.ItemsSource = null;
        }    
        /// <summary>
        /// удаление записи с подтверждением
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>    
        /// <summary>
        /// сохранить как
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>      
        /// <summary>
        /// открыть файл
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param> 
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if ((bool)openFileDialog.ShowDialog())
            {
                ConnectHelper.fileName = openFileDialog.FileName;
                ConnectHelper.ReadListFromFile(ConnectHelper.fileName);
            }
            else
                return;
            DtgListPreparate.ItemsSource = ConnectHelper.pharmacies.ToList();

        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
                if ((bool)saveFileDialog.ShowDialog())
            {
                string file = saveFileDialog.FileName;
                ConnectHelper.SaveListToFile(file);
            }
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            DtgListPreparate.ItemsSource = ConnectHelper.pharmacies.ToList();
            DtgListPreparate.SelectedIndex = -1;
            //препарат с максимальной ценой
            double prMax = ConnectHelper.pharmacies.Max(x => x.PricePreparate);
            TxbMaxPrice.Text = ConnectHelper.pharmacies.First(x => x.PricePreparate == prMax).NamePreparate.ToString();
            //препарат с минимальной ценой
            double prMin = ConnectHelper.pharmacies.Min(x => x.PricePreparate);
            TxbMinPrice.Text = ConnectHelper.pharmacies.First(x => x.PricePreparate == prMin).NamePreparate.ToString();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            WindowAddPreparate windowAdd = new WindowAddPreparate();
            windowAdd.ShowDialog();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            DtgListPreparate.ItemsSource = null;
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            WindowAddPreparate windowAdd = new WindowAddPreparate((sender as ContextMenu).DataContext as Pharmacy);
            windowAdd.ShowDialog();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            var resMessage = MessageBox.Show("Удалить запись?", "Подтверждение",
                MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (resMessage == MessageBoxResult.Yes)
            {
                int ind = DtgListPreparate.SelectedIndex;
                ConnectHelper.pharmacies.RemoveAt(ind);
                DtgListPreparate.ItemsSource = ConnectHelper.pharmacies.ToList();
                ConnectHelper.SaveListToFile(ConnectHelper.fileName);
            }
        }
    }
}
