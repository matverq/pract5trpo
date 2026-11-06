using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
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


namespace pract5_trpo
{
    /// <summary>
    /// Логика взаимодействия для ExamWorkPage.xaml
    /// </summary>
    public partial class ExamWorkPage : Page
    {
        private ObservableCollection<ExamWork> _works;
        private ExamWork _work;
        private ExamWork? _origWork;

        public ExamTask? SelectedTask { get; set; }
        public ExamWorkPage(ObservableCollection<ExamWork> works, ExamWork? work = null)
        {
            _works = works;
            _origWork = work;

            if (work == null)
            {
                int nextId = 1;
                foreach (var w in works)
                {
                    if (w.Id >= nextId) nextId = w.Id + 1;
                }

                _work = new ExamWork { Id = nextId };
            }
            else
            {
                _work = new ExamWork
                {
                    Id = work.Id,
                    Student = work.Student,
                    Discipline = work.Discipline,
                    Date = work.Date
                };
                foreach (var t in work.Tasks)
                {
                    _work.Tasks.Add(new ExamTask
                    {
                        Title = t.Title,
                        MaxScore = t.MaxScore,
                        Score = t.Score
                    });
                }
            }
            InitializeComponent();
            DataContext = _work;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(newMax.Text, out double max) || !double.TryParse(newScore.Text, out double score))
            {
                MessageBox.Show("Введите числа для баллов.");
                return;
            }

            _work.Tasks.Add(new ExamTask
            {
                Title = newTitle.Text,
                MaxScore = max,
                Score = score
            });
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedTask == null)
            {
                MessageBox.Show("Задание не выбрано");
                return;
            }
            _work.Tasks.Remove(SelectedTask);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if(_origWork==null)
            {
                _works.Add(_work);
                _origWork = _work;
            } else
            {
                _origWork.Student = _work.Student;
                _origWork.Discipline = _work.Discipline;
                _origWork.Date = _work.Date;

                _origWork.Tasks.Clear();

                foreach(var t in _work.Tasks)
                {
                    _origWork.Tasks.Add(t);
                }
            }
            string json = JsonSerializer.Serialize(_origWork);
            File.WriteAllText($"{_origWork.Id}.json", json);
            NavigationService.GoBack();
        }
        
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
