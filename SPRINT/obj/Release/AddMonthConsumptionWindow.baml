   M S B A M L   `   `   `  ���� A  =SPRINT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:SPRINTSPRINT  &     SPRINT.AddMonthConsumptionWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:SPRINT       � Title$  AddMonthConsumptionWindow��$	��130��$	��210�� � 
ResizeMode$ NoResize=�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto �.+�� .+�(  "System.Windows.Controls.DatePicker  -   $��MonthCalendar��   SPRINT.DatePickerCalendar   IsMonthYear . !   SPRINT.DatePickerDateFormat   
DateFormat$ MMM-yyyy��   Text$ MMM-yyyy��� $��1�� � ColumnDefinitions �� $
��Auto ��� $
��70px �.+��� $��1��$#��Расход за месяц:��$��17,2,0,0q��� -   $��MonthConsumptionTB��$��1��$��1��$��20,2,0,0q�� $��2��.+��� -   $��AddMonthConsumptionBtn��$
��70px��$
��30px��$��Добавить��$��0,10,0,0q�