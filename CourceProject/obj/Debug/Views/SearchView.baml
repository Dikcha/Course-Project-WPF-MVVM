   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5      H DCourceProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:CourceProject.ViewModelsCourceProject.ViewModels 5      9!clr-namespace:CourceProject.ViewsCourceProject.Views 5	      C&clr-namespace:CourceProject.ConvertersCourceProject.Converters 5
      $   CourceProject.Views.SearchView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      H DMahApps.Metro, Version=1.6.1.4, Culture=neutral, PublicKeyToken=null;controls,http://metro.mahapps.com/winfx/xaml/controls  5      Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5      :d2http://schemas.microsoft.com/expression/blend/2008  5      /vm&clr-namespace:CourceProject.ViewModels  5      -local!clr-namespace:CourceProject.Views  5	      /dc&clr-namespace:CourceProject.Converters  5
        � DataContext  5      .  (CourceProject.ViewModels.SearchViewModel  5   
    J� InputBindings 5      �� 5   
   
 �� Key$ Enter��61    �� Command 6   �� *Search+61    � 	Resources 5      ;  5CourceProject.Converters.FromDateTimeToStingConverter    FromDateTimeToStingConverter 
 DefBtn �� 
TargetType �� Property �� Value �� 
TargetType	 �� Padding
 �� CornerRadius �� Triggers P� Property P� Value d� Gesture �� RelativeSource �� AncestorType �� CommandParameter � ColumnDefinitions �� 	Converter �� Triggers �� Binding  ZeroToVisibilityConverter �� Value��%s
  &	        &	      '�� �    7   7  7  7  7    5   
   6*   �� 5   
   " ��6   .�5      ��  � �� �	#FFF5F5F56+   �� 5       � �� �	#FFDCDCDC6,   �� 5       � ��$ 1q�60   �� 5       � �� �	#FF6666666+   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ 11(�6)   �� 5       � ��$ Arial)�6+   �� 5       � ��$ Center=�6;   �� 5       � ��$ Center=�69   �� 5        � ��$	 8,7q�6(   �� 5!       � �� 5"      �� 5#      " ��6&   .R�5$      �� $��border��6!   #��z��5%      #	 z��5&      #��z��5'      $
 1��5(      #��z��5)      ._�5*      �� #��z��6/   #��z��5+   '   5,       5-      P� 5/       �@J�IsMouseOver . . �50   "   ��  � �� �	#FF4788c86?   51      P� 52       � �� . 54      P� 55       � {� .  . �56   "   ��  � �� �#ADADAD6?   57      59      5;      5<      �� 5=   
   " ��6   .�5>      ��  � �� 5?      �� 5@      " ��6&   .R�5A      ]� $	��150��6$    5B      d� 5C   "   $ LeftDoubleClicka�6/    6I   �� *" DataContext.OpenDayDetailsWindow+ �� $ UserControlL� 5D   /   �� .+�5F      � $��Center=�6#   $	��Top=�6>    5G   "   �� 5H   &   $
��Auto �67   �� 5I   &   $
��Auto �67   �� 5J   &   $
��Auto �67   .+�5L   "   �� $��0��6(   $��Center=�6�   $��Lucida Console)�6�   $��14(�6�   
���������68   �� *Date+8   6�   �� 5M   "   $��1��6(   $��Lucida Console)�6Z   ���#fd264F$��15(�6�   $��20,0,0,0q�6�   ��68   �� *Records.Count+��5N   &   �� 5O   *   " ��60    5P   .   �� 5Q   2    6>   �� *Records.Count+8  	 False6�   .a�5R   6   ��  � y�$ 	Collapsed=�6S   5S   3   �� 5T   2    6>   �� *Records.Count+8   True6�   .a�5U   6   ��  � y�$ Visible=�6S   5V   3   5X   +   5Z   #   �� 5[   "   $��2��6(   $ ��/Resources/record_pink.png��68   $��16��6\   ��5\   &   �� 5]   *   " ��60    5^   .   �� 5_   2    6>   �� *Records.Count+8  	 False6�   .a�5`   6   ��  � y�$ 	Collapsed=�6S   5a   3   �� 5b   2    6>   �� *Records.Count+8   True6�   .a�5c   6   ��  � y�$ Visible=�6S   5d   3   5f   +   5h   #   5i      �� 5j      $	��Top=�6(   $
��Wrap=�6=   $	��100��6Q   $	��540��6a   $��Arial)�6�   $��15(�6�   $��	10,5,5,10q�6�   ��6p   �� *Notes+6�   �� 5k      $��Bottom=�6(   $��1��6@   $	��440��6K   $��Center=�6W   $��0,0,0,10q�6t   
�������6�   5l      5m      5o      5p      5q      .��5r      �  � RowDefinitions 5s   
   �� 5t      $
��Auto �6   �� 5u      $
��Auto �6   �� 5v      $��* �6   �� 5w      $��40 �6   .+�5y   
   )  #MahApps.Metro.Controls.ProgressRing  $��Center=�6    $��4��6[    K� ZIndex$ 1��6l   $��Center=�6~   ���	#FC4C90F6   IsActive 6=   �� *
IsActive+6�   �� 5z   
   $"��/Resources/nootes_search.png��6   $��0��66   $	��300��6C   $��0, 30, 0, 0q�6O   $	��100��6d   �� 5{   
    �� Orientation$ 
Horizontal=�6   $��1��6.   $��Center=�6;   .+�5|      �� $	��450��5}      $��Center=�6#   $��15(�5~      $��SemiBold"�6$   $��Arial)�5      $��35��6)   ��5|      �� *
SearchText+5   )   �� 5�      #��[ 5�      $	��100��66   $��35��5�      $��Search��6!   $��15(�62   ��5�      �� *Search+5�   2   5�      �� 5�   
   $��2��6   $��Center=�5�      $	��550��5�      $��	0,10,10,0q�6   ��5�      �� *Days+5�      5�      5�      