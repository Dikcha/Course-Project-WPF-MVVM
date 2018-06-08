using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CourceProject.Annotations;
using CourceProject.Database;
using CourceProject.Repositories;
using CourceProject.Views;
using Prism.Commands;

namespace CourceProject.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public static string CurrentUserName { get; private set; }
        public static string RecordStorage { get; private set; } = @"D:\Records\";
        public User CurrentUser { get; set; }

        private NotesRepository notes;

        public MainWindowViewModel()
        {
            CalendarView = new CalendarView();
            SearchView = new SearchView();
            UserLoginView = new UserLoginView();
            notes = new NotesRepository();

            SetSearchView = new DelegateCommand(SetSearchViewMethod);
            SetCalendarView = new DelegateCommand(SetCalendarViewMethod);
            LogIn = new DelegateCommand<object>(OnLogInMethod);
            LogOut = new DelegateCommand(OnLogOutMethod);
            WindowTitle = "Your Notes";

            SetLogInViewMethod();
        }

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        private Visibility _menuVisibility;
        public Visibility MenuVisibility
        {
            get
            {
                return _menuVisibility;
            }
            private set
            {
                _menuVisibility = value;
                OnPropertyChanged();
            }
        }

        private string _error;
        public string Error
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
                OnPropertyChanged();
            }
        }

        private string _login;
        public string Login
        {
            get
            {
                return _login;
            }
            set
            {
                _login = value;
                Error = null;
                OnPropertyChanged();
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        private string _windowTitle;
        public string WindowTitle
        {
            get
            {
                return _windowTitle;
            }
            set
            {
                _windowTitle = value;
                OnPropertyChanged();
            }
        }


        public ICommand SetSearchView { get; set; }
        public ICommand SetCalendarView { get; set; }
        public ICommand LogOut { get; set; }
        public ICommand LogIn { get; set; }


        public CalendarView CalendarView { get; set; }
        public SearchView SearchView { get; set; }
        public UserLoginView UserLoginView { get; set; }


        public void SetSearchViewMethod()
        {
            CurrentView = new SearchView();
            
        }
        public void SetCalendarViewMethod()
        {
            CurrentView = new CalendarView();
        }
        public void SetLogInViewMethod()
        {
            MenuVisibility = Visibility.Collapsed;
            CurrentView = UserLoginView;
            UserLoginView.PasswordBox.Clear();
        }
        private async void OnLogInMethod(object passwordBox)
        {
            try
            {
                Error = "";
                IsBusy = true;
                CurrentUserName = null;
                if ((CurrentUser = await notes.CheckUserCredentials(Login, ((PasswordBox)passwordBox).Password)) != null)
                {
                    CurrentUserName = CurrentUser.Login;
                    MenuVisibility = Visibility.Visible;
                    WindowTitle = $"Your Notes -- {CurrentUser.FirstName} {CurrentUser.LastName}";
                    SetCalendarViewMethod();
                    ((CalendarViewModel)CalendarView.DataContext).BuildCalendar(DateTime.Today);
                }
                else
                    Error = "Invalid Login or Password";

                IsBusy = false;
            }
            catch (Exception ex)
            {
                Error = "Inexpected Error";
                IsBusy = false;
            }
        }
        private void OnLogOutMethod()
        {
            WindowTitle = "Your Notes";
           
            SetLogInViewMethod();
            
        }


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
