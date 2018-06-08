using CourceProject.Calendar;
using CourceProject.ViewModels;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace CourceProject.Views
{
    /// <summary>
    /// Логика взаимодействия для DayDetailsWindow.xaml
    /// </summary>
    public partial class DayDetailsWindow : MetroWindow
    {
        public DayDetailsWindow(object day)
        {
            InitializeComponent();
            ((DayDetailsWindowViewModel)DataContext).Day = (Day)day;

            this.Loaded += DayDetailsWindow_Loaded;
        }

        void DayDetailsWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow(this);
            window.Closing += window_Closing;
        }

        private void window_Closing(object sender, CancelEventArgs e)
        {
            DayDetailsWindowViewModel.RaiseCloseEvent();
        }
    }
}
