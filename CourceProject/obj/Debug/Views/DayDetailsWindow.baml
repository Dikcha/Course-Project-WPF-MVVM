   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5   	   H DCourceProject, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullC&clr-namespace:CourceProject.ViewModelsCourceProject.ViewModels 5	   	   9!clr-namespace:CourceProject.ViewsCourceProject.Views 5
   	   C&clr-namespace:CourceProject.ConvertersCourceProject.Converters 5   	   9!clr-namespace:CourceProject.ViewsCourceProject.Views 5   	   *   $CourceProject.Views.DayDetailsWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5   	   X TXceed.Wpf.Toolkit, Version=3.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   :d2http://schemas.microsoft.com/expression/blend/2008  5   	   H DMahApps.Metro, Version=1.6.1.4, Culture=neutral, PublicKeyToken=null;controls,http://metro.mahapps.com/winfx/xaml/controls  5   	   Dmc;http://schemas.openxmlformats.org/markup-compatibility/2006  5   	   /vm&clr-namespace:CourceProject.ViewModels  5	   	   -local!clr-namespace:CourceProject.Views  5
   	   7
converters&clr-namespace:CourceProject.Converters  5   	   *mp!clr-namespace:CourceProject.Views  5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$ 	Day Notes��5   	   (  "MahApps.Metro.Controls.MetroWindow   WindowTitleBrush �	#FFFD264F$	��500��5   	   $	��500��6   $	��500��6"   $	��420��61    � DataContext 5      8  2CourceProject.ViewModels.DayDetailsWindowViewModel  5   
    � 	Resources 5      :  4CourceProject.Converters.TotalSecondsToTimeConverter   TotalSecondsToTimeConverter��%   &	          5   
   61   5      .��5      �  � RowDefinitions 5   
   �� 5      $	��.6* �6   �� 5      $
��.45* �6   �� 5      $	��.2* �6   �� 5      $
��Auto �6   .+�5   
   �� $��0��6   $��Disabled=�5       �� AcceptsReturn .  �� TextWrapping$
 Wrap=�5"      $��Lucida Console)�5#      $��14(�5$      $��20q�5%      $��5q�5&      ��5       �� *Notes+5&      �� 5'   
   $��1��6   $
��Auto=�6$   $��	20,5,20,5q�6G   .��5(      �� ��6   �� *	Records+ 5)       �� 
TargetType	 �� Property
 �� Value �� 
TargetType��%�   '��       �� 5*      " ��6    .�5+      �� 	 � ��
 5,   "   �� 5-   &   " ��66   .R�5.   *   � #��z��6/   .+�5/   .   �� #��z��50   1   #��z��51   1   #��z��52   1   #��z��53   1   54   /   55   +   56   '   58      59      5:      ��5;      �� 5<      " ��6     �� Triggers 5=      �� 5>   "    �� Binding 6.   ��  �� Self �� RelativeSource# Z  �� Path$ Items.Count� �� Value 06�   .a�5?   &   �� 	 � ��
 5@   *   �� 5A   .   .R�5B   2   �� $��No Records Found��6<   $��25(�6T   $��Center=�6b   $��Center=�6   ���#fd264f$��Lucida Calligraphy)�6�   5C   /   5E   '   5F   #   .�5H      �� 	 � ��$
 0q�6@   �� 5I      	 � ��
 5J   "   �� 5K   &   .R�5L   *   �  � ColumnDefinitions 5M   .   �� 5N   2   $
��Auto �6C   �� 5O   2   $��* �6C   .+�5Q   .   '  !CourceProject.Views.MyMediaPlayer    Name$ MyMediaPlayer��6O   $��0��6?   $��Center=�6�      Record 6g   �� *DataContext+ ��  �� AncestorType$
 GridL�6�   �� 5R   .   $��1��65     IconButtonStyle#��[ 6E   $��15,0,0,0q�6n   $��Bottom=�6�   ��5S   9   �� *DataContext.DeleteRecord+ �� $ local:DayDetailsWindowL���5T   9   �� *DataContext+ �� $
 GridL�.��5U   2   �� $��/Resources/delete_32.png��68   5V   /   5W   +   5X   '   5Z      5[      5]      5^      �� 5_   
   $��2��6   $��	20,0,20,0q�6"   .+�5`      ��  �� Orientation$ 
Horizontal=�6   .+�5a      �� $��Record Name: ��6   ���#fd264F$��Lucida Console)�5b      $��Center=�5c      �� 5d      $	��150��6   $��20��6&   ���#fd264F$��Lucida Console)�6G   $��Center=�5e   6   $��Center=�5f      $��0,0,20,0q�6<   ��5e      �� *
RecordName+5f   <   �� 5h      -   5         ��Name$ StartRecord��5h      $��3,0,0,3q�6.   $��Center=�6?   $��32��5i      $��32��6    ��6,   �� *StartRecord+��5j      �� 5k      " ��6     �� BasedOn# [ 64    5l      �� 5m   "    6.   �� *	CanRecord+	 False6L   .a�5n   &   �� 	 � y�$
 	Collapsed=�6C   5o   #   5q      .��5s      �� $$��/Resources/microphone_pink.png��6   5t      �� 5u      -   5        $ 
StopRecord��5u      $��3,0,0,3q�6-   $��Center=�6>   $��32��5v      $��32��6    ��6,   �� *
StopRecord+��5w      �� 5x      " ��6    # [ 64    5y      �� 5z   "    6.   �� *	CanRecord+ True6L   .a�5{   &   �� 	 � y�$
 	Collapsed=�6C   5|   #   5~      .��5�      �� $%��/Resources/microphone_black.png��6   5�      �� 5�      ���#fd264F$��Lucida Console)�5�   -   $��15(�6I   $��Center=�5�      $��20,0,0,0q�63   ��5�      �� *
RecordTime+ �� 	Converter# [  5�   3   5�      5�      �� 5�   
   $��3��6   $ 
Horizontal=�6"   $��Right=�6<   $��10q�6X   .+�5�      �� $	��100��6   $��30��6!   $
��Save��5�      ��5�      �� *Save+��5�      �� *.+ �� $ WindowL��� 5�      $	��100��6   $��30��6!   $��Cancel��5�      $��20,0,5,0q�5�      ��5�      �� *Cancel+��5�      �� *.+ �� $ WindowL�5�      5�      5�      