   M S B A M L   `   `   `  ����H  DCourceProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:CourceProject.CalendarCourceProject.Calendar  5      C&clr-namespace:CourceProject.ViewModelsCourceProject.ViewModels  5      &     CourceProject.Views.CalendarView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9calendar$clr-namespace:CourceProject.Calendar     5      5vm&clr-namespace:CourceProject.ViewModels     5        � DataContext  5
      0   *CourceProject.ViewModels.CalendarViewModel  5   
    � 	Resources 5       �� 
TargetType   DownArrowButtonTemplate �� Stretch J� RenderTransformOrigin �� Angle �� 
TargetType �� Triggers P� Property	 P� Value
 �� Property �� Value  UpArrowButtonTemplate  BaseComboBoxStyle  ButtonlessComboBoxStyle �� BasedOn  BaseArrowButtonStyle  BaseInfoLabelStyle��%�  &	        &	 �    &	 �    7  &	     &	 �    �� 5   
   " ��6   .R�5      F� #��z��6   #��z��5      $
 Fill=�5      +���� G j%р�y?р��^� G j%$ 0.5,0.5&�5      z�5      �� 5      $	 -90Q�6&   ��5      �� 5      " F�6    5      P� 5       �@J�IsMouseOver	 . . �5   "   �� 
 � �� �#FD264F69   5      P� 5       �@J�IsMouseOver	 .  . �5   "   �� 
 � ��
 �����69   5      5!      5#      5$      �� 5%   
   " ��6   .R�5&      F� #��z��6   #��z��5'      $
 Fill=�5(      +������j%� G �^� G y?р�j%$ 0.5,0.5&�5*      z�5+      �� 5,      $	 -90Q�6&   ��5.      �� 5/      " F�6    50      P� 51       �@J�IsMouseOver	 . . �52   "   �� 
 � �� �#FD264F69   53      P� 54       �@J�IsMouseOver	 .  . �55   "   �� 
 � ��
 �����69   56      58      5:      5;      �� 5<   
   " ��6   8   .�5=      �� 
 � ��$ 35��6'   �� 5>      
 � ��$	 120��6&   �� 5?      
 � ��$ Center=�6;   �� 5@      
 � ��$ Center=�62   �� 5A      
 � ��$ Center=�69   �� 5B      
 � ��$ 18(�6)   �� 5C      
 � �� �#FD264F6+   �� 5D      
 � ��$ Lucida Console)�6+   5E      �� 5F   
   " ��6   .�5G      �� 
 � ��$ 5q�6'   �� 5H      
 � ��$ 10��6&   �� 5I      
 � ��$ 8��6'   5J      �� 5K   
   " ��6   .�5L      �� 
 � ��$ 18(�6)   �� 5M      
 � ��$ Lucida Console)�6+   �� 5N      
 � ��$ Center=�64   �� 5O      
 � ��$ Center=�62   5P      5Q      .��5R      �  � RowDefinitions 5S   
   �� 5T      $
��Auto �6   �� 5U      $��* �6   .+�5W   
   � $��0��6    � ColumnDefinitions 5X      �� 5Y      $��* �6#   �� 5Z      $��* �6#   �� 5[      $��* �6#   �� 5\      $��2* �6#   .+�5^      �� #��[ 5_      $
��Left=�5`      {�.  $��30,20,20,20q�5b      $��20(�5c      ��5^      �� *CurrentTime+5c      �� 5d      $��1��6    �� Orientation$ 
Horizontal=�6)   $��Center=�6B   .+�5e      �� #��[ 6    �� SelectedItem 5f      �� *SelectedMonth+��5g      �� *MonthesNames+�� 5h      $��Center=�6   .+�5i      �� #��[ 6   #��[ 5k      ��5j      �� *	MonthUp+5k      �� 5l      #��[  6   #��[ 5n      ��5m      �� *	MonthDown+5n      5o      5p      �� 5q      $��2��6   $ 
Horizontal=�6)   $��Center=�6B   .+�5r      �� #��[ 6   $��70��5s       5t      �� *SelectedYear+��5u      �� *
YearsNames+�� 5v      $��Center=�6   .+�5w      �� #��[ 6   #��[ 5y      ��5x      �� *YearUp+5y      �� 5z      #��[  6   #��[ 5|      ��5{      �� *
YearDown+5|      5}      5      �� 5�      $��3��6   $ 
Horizontal=�5�      $��Center=�5�      .+�5�      (  "System.Windows.Controls.DatePicker  $	��140��6   $��40��5�      $��Center=�5�         Text 5�      �� *SelectedDate+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5�      �� 5�      $��40��6   $	��100��6%   $��ПЕРЕЙТИ��5�      $��Segoe UI Light)�5�      $��13(�5�      
����������#FD264F$��10,0,0,0q�5�      ��5�      �� *
GoToDate+5�      5�      5�      %   CourceProject.Calendar.Calendar  5�   
   $��1��6     CalendarTemplate#��[ 5�         Days 5�      �� *Days+$ PropertyChanged=�5�      5�      5�      