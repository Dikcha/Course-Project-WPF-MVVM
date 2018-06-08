using CourceProject.Annotations;
using CourceProject.Calendar;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CourceProject.Database;
using CourceProject.Repositories;
using CourceProject.Views;
using NAudio.Wave;
using Prism.Commands;

namespace CourceProject.ViewModels
{
    public class SearchViewModel : INotifyPropertyChanged
    {
        NotesRepository notes = new NotesRepository();
         
        public SearchViewModel()
        {
            Days = new ObservableCollection<Day>();
            Search = new DelegateCommand(OnSearch);
            OpenDayDetailsWindow = new DelegateCommand<object>(OpenDayDetailsWindowMethod);
        }

        private bool _isActive;
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                OnPropertyChanged();
            }
        }


        public ICommand Search { get; set; }

        public ICommand OpenDayDetailsWindow { get; set; }

        public string SearchText { get; set; }

        private ObservableCollection<Day> _days;
        public ObservableCollection<Day> Days
        {
            get
            {
                return _days;
            }
            set
            {
                _days = value;
                OnPropertyChanged();
            }
        }


        private async void OnSearch()
        {
            IsActive = true;
            Days.Clear();

            foreach (var note in await notes.SearchNotesByText(MainWindowViewModel.CurrentUserName, SearchText))
            {
                Day day = new Day{ Notes = note.Note, Date = note.NoteDate };

                string recordsPath = $"{MainWindowViewModel.RecordStorage}" +
                                     $"{MainWindowViewModel.CurrentUserName}" +
                                     $"\\{note.NoteDate.ToShortDateString()}";

                day.Records = new ObservableCollection<Record>();

                if (Directory.Exists(recordsPath))
                {
                    foreach (var wavFile in Directory.GetFiles(recordsPath))
                    {
                        WaveFileReader wf = new WaveFileReader(wavFile);
                        
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

                Days.Add(day);
            }

            IsActive = false;
        }

        private async void OpenDayDetailsWindowMethod(object day)
        {
            if (day == null)
                return;

            DayDetailsWindow window = new DayDetailsWindow(day);

            if (window.ShowDialog() == false)
            {
                var result = ((DayDetailsWindowViewModel)window.DataContext);
                if (result.Result)
                {
                    Days.Where(d => d.Date.Equals(((Day)day).Date)).First().Records = result.Records;
                    
                    await notes.SaveUserNote(new UserNotes
                    {
                        Note = result.Notes,
                        UserLogin = MainWindowViewModel.CurrentUserName,
                        NoteDate = new DateTime(result.Day.Date.Year, result.Day.Date.Month, result.Day.Date.Day)
                    });
                }
            }

            OnSearch();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
