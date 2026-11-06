using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace pract5_trpo
{
    public class ExamWork
    {

        public ExamWork()
        {
            Tasks.CollectionChanged += (s, e) =>
            {
                if (e.OldItems != null)
                {
                    foreach (ExamTask t in e.OldItems)
                    {
                        t.PropertyChanged -= Task_propertyChanged;
                    }
                }
                if (e.NewItems != null)
                {
                    foreach (ExamTask t in e.NewItems)
                    {
                        t.PropertyChanged += Task_propertyChanged;
                    }
                }
                OnPropertyChanged(nameof(TotalScore));
            };
        }

        public int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }
        public string _student = "Студент";

        public string Student
        {
            get => _student;
            set
            {
                if (_student != value)
                {
                    _student = value;
                    OnPropertyChanged();
                }
            }
        }

        public string _discipline = "Дисциплина";

        public string Discipline
        {
            get => _discipline;
            set
            {
                if (_discipline != value)
                {
                    _discipline = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<ExamTask> Tasks { get; set; } = new();

        public double TotalScore => Tasks.Sum(l => l.Score);

        public void Task_propertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ExamTask.Score)) OnPropertyChanged(nameof(TotalScore));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}