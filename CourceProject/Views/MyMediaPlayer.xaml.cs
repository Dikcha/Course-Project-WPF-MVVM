using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using CourceProject.Annotations;
using CourceProject.Calendar;
using CourceProject.ViewModels;
using Prism.Commands;

namespace CourceProject.Views
{
    public partial class MyMediaPlayer
    {
        public MyMediaPlayer()
        {
            InitializeComponent();
            DataContext = this;

            _timer.Tick += TimerTick;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            _timer.Start();

            Play = new DelegateCommand(OnPlay);
            Pause = new DelegateCommand(OnPause);
            Stop = new DelegateCommand(OnStop);

            DurationSlider.AddHandler(MouseLeftButtonUpEvent,
                new MouseButtonEventHandler(durationSlider_MouseLeftButtonUp),
                true);

            DayDetailsWindowViewModel.CloseEvent += (sender, args) => { _player.Stop(); };
            DayDetailsWindowViewModel.DeleteEvent += (sender, args) =>
            {
                if (Record.RecordPath.Equals(args))
                {
                    _player.Close();
                    _timer.Stop();
                }
            };

            _player.MediaEnded += (sender, args) => { OnStop(); };

            RecordPlayStarted += (sender, s) =>
            {
                if (!Record.RecordPath.Equals(s))
                    _player.Pause();
            };
        }

        readonly System.Windows.Threading.DispatcherTimer _timer = new System.Windows.Threading.DispatcherTimer();

        private void TimerTick(object sender, EventArgs e)
        {
            DurationSlider.Value = _player.Position.TotalSeconds / Record.RecordDuration.TotalSeconds * 100;
        }

        public Record Record
        {
            get { return (Record)GetValue(RecordProperty); }
            set { SetValue(RecordProperty, value); }
        }

        public static readonly DependencyProperty RecordProperty =
            DependencyProperty.Register("Record", typeof(Record), typeof(MyMediaPlayer),
        new PropertyMetadata(new Record { RecordDuration = new TimeSpan(0, 0, 0), RecordPath = "C:/", RecordTitle = "Default" }));

        private bool _hasSource;

        public ICommand Play { get; set; }

        public ICommand Pause { get; set; }

        public ICommand Stop { get; set; }

        private void OnPlay()
        {
            try
            {
                if(!_hasSource)
                    _player.Source = new Uri(Record.RecordPath);
                _player.Play();
                RaiseRecordStartEvent(Record.RecordPath);
                _hasSource = true;
            }
            catch
            {
                MessageBox.Show("Произошла ошибка при открытии или проигрывании аудио");
            }
        }
        private void OnPause()
        {
            _player.Pause();
        }
        private void OnStop()
        {
            _player.Stop();
        }
        public void ClosePlayer()
        {
            _player.Close();
        }

        private void durationSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _player.Position = TimeSpan.FromSeconds(DurationSlider.Value/100 * Record.RecordDuration.TotalSeconds);
        }

        public static event EventHandler<string> RecordPlayStarted;

        public static void RaiseRecordStartEvent(string recordPath)
        {
            var handler = RecordPlayStarted;
            handler?.Invoke(typeof(DayDetailsWindowViewModel), recordPath);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
