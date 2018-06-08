using CourceProject.Annotations;
using CourceProject.Calendar;
using CourceProject.Database;
using CourceProject.Repositories;
using CourceProject.Views;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using NAudio.Wave;

namespace CourceProject.ViewModels
{
    public class CalendarViewModel : INotifyPropertyChanged
    {
        System.Windows.Threading.DispatcherTimer _timer = new System.Windows.Threading.DispatcherTimer();
        Calendar.Calendar Calendar = new Calendar.Calendar();
        NotesRepository notes = new NotesRepository();
        int offset;

        public CalendarViewModel()
        {
            _timer.Tick += TimerTick;
            _timer.Interval = new TimeSpan(0, 0, 0, 1);
            _timer.Start();

            MonthesNames = new ObservableCollection<string>(new[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" });
            DayNames = new ObservableCollection<string> { "MONDAY", "TUESDAY", "WEDNESDAY", "THUESDAY", "FRIDAY", "SATURDAY", "SUNDAY" };
            for (int i = -20; i < 20; i++)
                YearsNames.Add(DateTime.Today.AddYears(i).Year.ToString());

            SelectedMonth = MonthesNames[DateTime.Today.Month - 1];
            SelectedYear = DateTime.Today.Year.ToString();
            SelectedDate = DateTime.Today.ToShortDateString();
          
            BuildCalendar(DateTime.Today);

            GoToDate = new DelegateCommand(() => { ParseDateTime(SelectedDate); }, () => !IsBusy);
            MonthUp = new DelegateCommand(MonthUpMethod, () => !IsBusy);
            MonthDown = new DelegateCommand(MonthDownMethod, () => !IsBusy);
            YearUp = new DelegateCommand(YearUpMethod, () => !IsBusy);
            YearDown = new DelegateCommand(YearDownMethod, () => !IsBusy);
            SetFocus = new DelegateCommand<object>(Border_PreviewMouseDown);
            OpenDayDetailsWindow = new DelegateCommand<object>(OpenDayDetailsWindowMethod);
        }

        public bool IsBusy { get; set; }

        public ICommand GoToDate { get; set; }
        public ICommand MonthUp { get; set; }
        public ICommand MonthDown { get; set; }
        public ICommand YearUp { get; set; }
        public ICommand YearDown { get; set; }
        public ICommand SetFocus { get; set; }
        public ICommand OpenDayDetailsWindow { get; set; }

        public  ObservableCollection<string> DayNames { get; set; }

        public static ObservableCollection<string> MonthesNames { get; set; }

        public ObservableCollection<string> YearsNames { get; set; } = new ObservableCollection<string>();

        public ObservableCollection<Day> Days { get; set; } = new ObservableCollection<Day>();


        private string _currentTime;
        public string CurrentTime
        {
            get { return _currentTime; }
            set
            {
                _currentTime = value;
                OnPropertyChanged();
            }
        }

        public  string _selectedMonth;
        public  string SelectedMonth
        {
            get { return _selectedMonth; }
            set
            {
                _selectedMonth = value;
                OnPropertyChanged();
            }
        }

        private string _selectedYear;
        public string SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                _selectedYear = value;
                OnPropertyChanged();
            }
        }

        private string _selectedDate;
        public string SelectedDate
        {
            get { return _selectedDate; }
            set
            {
                _selectedDate = value;
                OnPropertyChanged();
            }
        }

        private void MonthUpMethod()
        {
            if (MonthesNames.IndexOf(SelectedMonth) == 11)
                SelectedYear = (int.Parse(SelectedYear) + 1).ToString();
            SelectedMonth = MonthesNames[((MonthesNames.IndexOf(SelectedMonth) + 1) % 12)];
            RefreshCalendar();
        }

        private void MonthDownMethod()
        {
            if (MonthesNames.IndexOf(SelectedMonth) == 0)
                SelectedYear = (int.Parse(SelectedYear) - 1).ToString();
            SelectedMonth = MonthesNames[((MonthesNames.IndexOf(SelectedMonth) - 1) % 12)];
            RefreshCalendar();
        }

        private async void OpenDayDetailsWindowMethod(object day)
        {
            if (day == null || !((Day)day).IsTargetMonth)
                return;

            DayDetailsWindow window = new DayDetailsWindow(day);

            if (window.ShowDialog() == false)
            {
                var result = ((DayDetailsWindowViewModel)window.DataContext);
                if (result.Result)
                {

                    Days[offset - 1 + result.Day.Date.Day].Records = result.Records;

                    await notes.SaveUserNote(new UserNotes
                    {
                        Note = result.Notes,
                        UserLogin = MainWindowViewModel.CurrentUserName,
                        NoteDate = new DateTime(result.Day.Date.Year, result.Day.Date.Month, result.Day.Date.Day)
                    });

                    Days[offset - 1 + result.Day.Date.Day].Notes = result.Notes;
                }
            }
        }

        private void Border_PreviewMouseDown(object sender)
        {
            Keyboard.Focus(sender as Border);
        }

        private void YearUpMethod()
        {
            SelectedYear = (int.Parse(SelectedYear) + 1).ToString();
            RefreshCalendar();
        }

        private void YearDownMethod()
        {
            SelectedYear = (int.Parse(SelectedYear) - 1).ToString();
            RefreshCalendar();
        }

        public async void BuildCalendar(DateTime date)
        {
           
            if (MainWindowViewModel.CurrentUserName == null)
                return;
            DateTime endDate = Calendar.BuildCalendar(date, Days);
            IEnumerable<UserNotes> userNotes = await notes.GetNotesByLoginAndDate(MainWindowViewModel.CurrentUserName, endDate.AddDays(-42), endDate);
            DateTime beginningDate = new DateTime(date.Year, date.Month, 1);
            offset = Convert.ToInt32(beginningDate.DayOfWeek.ToString("D"));
            offset = offset == 0 ? 6 : --offset;

            foreach (var note in userNotes)
            {
                int notTargetMonthIndex = note.NoteDate.Month < date.Month ? note.NoteDate.AddDays(offset).Day :
                    note.NoteDate.Day + DateTime.DaysInMonth(date.Year, date.Month) + offset;

                int index = IsTargetMonth(date, note.NoteDate)
                    ? note.NoteDate.Day + offset
                    : notTargetMonthIndex;

                Days[--index].Notes = note.Note;
            }

            foreach (var day in Days)
            {
                string recordsPath = $"{MainWindowViewModel.RecordStorage}" +
                                     $"{MainWindowViewModel.CurrentUserName}" +
                                     $"\\{day.Date.ToShortDateString()}";

                if (Directory.Exists(recordsPath))
                {
                    foreach (var wavFile in Directory.GetFiles(recordsPath))
                    {
                        using (WaveFileReader wf = new WaveFileReader(wavFile))
                        {
                            day.Records.Add(
                                new Record
                                {
                                    RecordPath = wavFile,
                                    RecordTitle = wavFile.Split('\\').Last().Split('.').First(),
                                    RecordDuration = wf.TotalTime
                                }
                            );
                        }
                    }
                }
            }
            OnPropertyChanged(nameof(Days));
           
        }

        private bool IsTargetMonth(DateTime targetDate, DateTime otherDate)
        {
            return targetDate.Month.Equals(otherDate.Month);
        }

        private void ParseDateTime(string date)
        {
            try
            {
                DateTime newDateTime = DateTime.Parse(date);
                SelectedMonth = MonthesNames[newDateTime.Month - 1];
                SelectedYear = newDateTime.Year.ToString();
                RefreshCalendar();
            }
            catch { }
        }

        private void RefreshCalendar()
        {
            if (SelectedYear == null || SelectedMonth == null) return;

            DateTime targetDate = new DateTime(Convert.ToInt32(SelectedYear), MonthesNames.IndexOf(SelectedMonth) + 1, 1);

            BuildCalendar(targetDate);
        }

        private void TimerTick(object sender, EventArgs e)
        {
            int hour = DateTime.Now.Hour;
            string minutes = DateTime.Now.Minute < 10 ? '0' + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();
            CurrentTime = $"{hour}:{minutes}";
        }

        public event PropertyChangedEventHandler PropertyChanged;

       
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
