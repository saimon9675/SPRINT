   M S B A M L   `   `   `  ����A  =SPRINT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:SPRINTSPRINT  5   	   &     SPRINT.AddMonthConsumptionWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:SPRINT     5   	     � Title$)  #Добавление периода��5   	   $	��108��6$   $	��295��61    � 
ResizeMode$ NoResize=�5	   	   .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �  � ColumnDefinitions 5      �� 5      $	��5px �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   �� 5      $
��Auto �6#   .+�5      (  "System.Windows.Controls.DatePicker  -   5        $��MonthCalendar��5   )   $��1��6      SPRINT.DatePickerCalendar   IsMonthYear . !   SPRINT.DatePickerDateFormat   
DateFormat$ MMM-yyyy��5         Text$ MMM-yyyy��5      $��0,5,0,5q�6   61   �� 5      $��2��6   $"��Расход за месяц��6(   $��13,7,0,0q�6?   �� 5      -   5        $��MonthConsumptionTB��5   &   $��3��6   $��5,7,0,5q�6@   $��60��6Q   5      � 5   
   $��1��6   .+�5      �� -   5        $��AddMonthConsumptionBtn��5      $
��70px��63   $��Right=�6@   $��Добавить��6\   $��0,5,5,5q�6o   6�   5      5      5       