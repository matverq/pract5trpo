using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.IO;
using System.Text.Json;

namespace pract5_trpo
{
    /// <summary>
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        public ObservableCollection<ExamWork> works { get; set; } = new();
        public ExamWork? selectedWork { get; set; }
        public ListPage()
        {
            InitializeComponent();
            load();
            DataContext = this;
        }
        private void load()
        {
            foreach (var file in Directory.GetFiles(".", "*.json"))
            {
                string json = File.ReadAllText(file);
                var work = JsonSerializer.Deserialize<ExamWork>(json);
                if (work != null) works.Add(work);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ExamWorkPage(works));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if(selectedWork==null)
            {
                MessageBox.Show("Запись не выбрана");
                return;
            }
            var result = MessageBox.Show("Вы точно хотите удалить запись?", " Подтверждение", MessageBoxButton.YesNo);
            if(result==MessageBoxResult.Yes)
            {
                File.Delete($"{selectedWork.Id}.json");
                works.Remove(selectedWork);
            }
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(selectedWork==null)
            {
                MessageBox.Show("Элемет списка не выбран");
                return;
            }
            NavigationService.Navigate(new ExamWorkPage(works, selectedWork));
        }
    }
}
