using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace pract5_trpo
{
    public class ExamTask : INotifyPropertyChanged
    {
        public string _title="Задание";
        public string Title
        {
            get => _title;
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged();
                }
            }
        }
        public double _maxscore=5;
        public double MaxScore
        {
            get => _maxscore;
            set
            {
                if (_maxscore != value)
                {
                    _maxscore = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TaskPercent));
                }
            }
        }
        public double _score=0;
        public double Score
        {
            get => _score;
            set
            {
                if (_score != value)
                {
                    _score = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TaskPercent));
                }
            }
        }

        public double TaskPercent => MaxScore == 0 ? 0 : Score / MaxScore * 100;

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}