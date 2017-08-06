using Common;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WindowsFormChart.example.Model;

namespace WindowsFormChart.example.ViewModel
{
    class MainWindowVM : ViewModelBase
    {
        private SomethingModel model { get; set; }

        private List<Point> _data0 = new List<Point>();
        public List<Point> Data0
        {
            get
            {
                return _data0;
            }
            set
            {
                _data0 = value;
                RaisePropertyChanged();
            }
        }

        private List<Point> _data1 = new List<Point>();
        public List<Point> Data1
        {
            get
            {
                return _data1;
            }
            set
            {
                _data1 = value;
                RaisePropertyChanged();
            }
        }

        private List<Point> _data2 = new List<Point>();
        public List<Point> Data2
        {
            get
            {
                return _data2;
            }
            set
            {
                _data2 = value;
                RaisePropertyChanged();
            }
        }

        private List<Point> _data3 = new List<Point>();
        public List<Point> Data3
        {
            get
            {
                return _data3;
            }
            set
            {
                _data3 = value;
                RaisePropertyChanged();
            }
        }

        public ICommand Run { get; }
        public ICommand Stop { get; }

        private bool _isBusy = false;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            private set
            {
                _isBusy = value;
                RaisePropertyChanged();
            }
        }

        public MainWindowVM()
        {
            model = new SomethingModel();

            Run = new DelegateCommand(async () =>
            {
                IsBusy = true;

                Data0.Clear();
                var j0 = model.RunSin((d) =>
                {
                    Data0.AddRange(d);
                    RaisePropertyChanged(nameof(Data0));
                });
                Data1.Clear();
                var j1 = model.RunCos((d) =>
                {
                    Data1.AddRange(d);
                    RaisePropertyChanged(nameof(Data1));
                });
                Data2.Clear();
                var j2 = model.RunRect((d) =>
                {
                    Data2.AddRange(d);
                    RaisePropertyChanged(nameof(Data2));
                });
                Data3.Clear();
                var j3 = model.RunSinc((d) =>
                {
                    Data3.AddRange(d);
                    RaisePropertyChanged(nameof(Data3));
                });
                await Task.WhenAll(j0, j1, j2, j3);

                IsBusy = false;
            });

            Stop = new DelegateCommand(async () =>
            {
                await model.Cancel();
            });
        }
    }
}
