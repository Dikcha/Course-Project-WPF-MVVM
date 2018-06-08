using CourceProject.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace CourceProject.Calendar
{
    public class Calendar : Control
    {
        
        public Calendar()
        {
            //CurrentDate = DateTime.Today;
        }
        
        public static readonly DependencyProperty DaysProperty = DependencyProperty.Register("Days",
            typeof(ObservableCollection<Day>), typeof(Calendar), new PropertyMetadata());
        public ObservableCollection<Day> Days
        {
            get { return (ObservableCollection<Day>)GetValue(DaysProperty); }
            set { SetValue(DaysProperty, value); }
        }

        //public static readonly DependencyProperty CurrentDateProperty = DependencyProperty.Register("CurrentDate",
        //    typeof(DateTime), typeof(Calendar));

        //public DateTime CurrentDate
        //{
        //    get { return (DateTime)GetValue(CurrentDateProperty); }
        //    set { SetValue(CurrentDateProperty, value); }
        //}

        public event EventHandler<DayChangedEventArgs> DayChanged;        

        public DateTime BuildCalendar(DateTime targetDate, ObservableCollection<Day> _Days)
        {
            _Days.Clear();

            DateTime d = new DateTime(targetDate.Year, targetDate.Month, 1);
            int offset = Convert.ToInt32(d.DayOfWeek);
            d = d.AddDays(-(offset == 0 ? 6 : --offset));

            for (int box = 1; box <= 42; box++)
            {
                Day day = new Day { Date = d, Enabled = true, IsTargetMonth = targetDate.Month == d.Month, Records = new ObservableCollection<Record>() };
                day.PropertyChanged += Day_Changed;
                day.IsToday = d == DateTime.Today;
                _Days.Add(day);
                d = d.AddDays(1);
            }

            return d;
        }

        private void Day_Changed(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName != "Notes") return;
            if (DayChanged == null) return;

            DayChanged(this, new DayChangedEventArgs(sender as Day));
        }
    }

    public class DayChangedEventArgs : EventArgs
    {
        public Day Day { get; private set; }

        public DayChangedEventArgs(Day day)
        {
            this.Day = day;
        }
    }
}
