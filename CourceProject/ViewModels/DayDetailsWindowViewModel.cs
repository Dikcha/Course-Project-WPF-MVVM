using CourceProject.Annotations;
using CourceProject.Calendar;
using CourceProject.Database;
using CourceProject.Views;
using NAudio.Wave;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace CourceProject.ViewModels
{
    public class DayDetailsWindowViewModel : INotifyPropertyChanged
    {
        public bool Result;
        public WaveIn waveSource;
        public WaveFileWriter waveFile;
        private bool IsRecordError;
        System.Windows.Threading.DispatcherTimer _timer = new System.Windows.Threading.DispatcherTimer();

        public DayDetailsWindowViewModel()
        {
            Save = new DelegateCommand<object>(OnSave);
            Cancel = new DelegateCommand<object>(OnCancel);
            DeleteRecord = new DelegateCommand<object>(OnDeleteRecord);
            StartRecord = new DelegateCommand(OnStartRecord);
            StopRecord = new DelegateCommand(OnStopRecord);
            _timer.Interval = new TimeSpan(0, 0, 1);
            _timer.Tick += OnTimerTick;

        }

        private string _notes;
        public string Notes
        {
            get
            {
                return _notes;
            }
            set
            {
                _notes = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Record> _records;
        public ObservableCollection<Record> Records
        {
            get
            {
                return _records;
            }
            set
            {
                _records = value;
                OnPropertyChanged();
            }
        }


        private Day _day;
        public Day Day
        {
            get
            {
                return _day;
            }
            set
            {
                _day = value;
                Notes = value.Notes ?? "";
                Records = value.Records ?? new ObservableCollection<Record>();
            }
        }

        private bool _canRecord = true;
        public bool CanRecord
        {
            get
            {
                return _canRecord;
            }
            set
            {
                _canRecord = value;
                OnPropertyChanged();
            }
        }

        private int _recordTime;
        public int RecordTime
        {
            get
            {
                return _recordTime;
            }
            set
            {
                _recordTime = value;
                OnPropertyChanged();
            }
        }

        private string _recordName { get; set; }
        public string RecordName { get; set; }

        public ICommand Cancel { get; set; }
        public ICommand Save { get; set; }
        public ICommand DeleteRecord { get; set; }
        public ICommand StartRecord { get; set; }
        public ICommand StopRecord { get; set; }

        public static event EventHandler CloseEvent;

        public static event EventHandler<string> DeleteEvent;

        public static void RaiseCloseEvent()
        {
            var handler = CloseEvent;
            handler?.Invoke(typeof(DayDetailsWindowViewModel), EventArgs.Empty);
        }
        public static void RaiseDeleteEvent(string recordPath)
        {
            var handler = DeleteEvent;
            handler?.Invoke(typeof(DayDetailsWindowViewModel), recordPath);
        }

        private void OnCancel(object window)
        {
            Result = false;
            ((DayDetailsWindow)window).Close();
        }
        private void OnSave(object window)
        {
            Result = true;
            ((DayDetailsWindow)window).Close();
        }
        private void OnDeleteRecord(object sender)
        {
            RaiseDeleteEvent(((Record)sender).RecordPath);
            Records.Remove((Record)sender);
            File.Delete(((Record)sender).RecordPath);
        }
        public void OnStartRecord()
        {
            RecordTime = 0;
            _recordName = null;
            IsRecordError = false;

            try
            {
                waveSource = new WaveIn();
                int a = WaveIn.DeviceCount;
                WaveInCapabilities deviceInfo = WaveIn.GetCapabilities(0);

                if (a == 1 && deviceInfo.ProductName.Contains("Стерео микшер") || deviceInfo.ProductName.Contains("Stereo Mix"))
                    throw new Exception("Нет микро");
                waveSource.WaveFormat = new WaveFormat(44100, 1);

                waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
                waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

                string _recordDirectory = MainWindowViewModel.RecordStorage +
                                            MainWindowViewModel.CurrentUserName +
                                            $"\\{Day.Date.ToShortDateString()}";

                _recordName = RecordName;

                Directory.CreateDirectory(_recordDirectory);

                if (File.Exists($"{_recordDirectory}\\{RecordName}.wav"))
                    _recordName += $" {Guid.NewGuid()}";

                waveFile = new WaveFileWriter($"{_recordDirectory}\\{_recordName}.wav", waveSource.WaveFormat);

                waveSource.StartRecording();
                _timer.Start();
                CanRecord = false;
            }
            catch (Exception err)
            {
                IsRecordError = true;
                MessageBox.Show(err.Message);
            }
        }
        public void OnStopRecord()
        {
            if (!IsRecordError)
            {
                CanRecord = true;
                _timer.Stop();
                waveSource.StopRecording();

                string recordPath = MainWindowViewModel.RecordStorage +
                                    MainWindowViewModel.CurrentUserName +
                                    $"\\{Day.Date.ToShortDateString()}\\{_recordName}.wav";

                Records.Add(new Record()
                {
                    RecordDuration = waveFile?.TotalTime ?? new TimeSpan(0, 0, 5),
                    RecordPath = recordPath,
                    RecordTitle = _recordName
                });
            }
        }

        public void OnTimerTick(object sender, EventArgs e)
        {
            RecordTime++;
        }

        void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveFile != null)
            {
                waveFile.Write(e.Buffer, 0, e.BytesRecorded);
                waveFile.Flush();
            }
        }
        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}