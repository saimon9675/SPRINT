   M S B A M L   `   `   `  ���� A  =SPRINT, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:SPRINTSPRINT      SPRINT.RefillDBWizard   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     &localclr-namespace:SPRINT     X TXceed.Wpf.Toolkit, Version=3.3.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit    � Title$4  .Мастер перезаполнения БД��$	��350��$	��450�� � 
ResizeMode$ NoResize=�.��� .+�  Xceed.Wpf.Toolkit.Wizard  -   $��WizardWindow��   FinishButtonClosesWindow .    HelpButtonVisibility$ 	Collapsed=�   ExteriorPanelMinWidth$ 20Q�   NextButtonContent$ Вперед >��   CancelButtonContent$ Отменить��   BackButtonContent$ < Назад��   FinishButtonContent$ Завершить��.@�"  Xceed.Wpf.Toolkit.WizardPage  -   	  Name$	 	IntroPage��
   Title$�
 �Добро пожаловать в мастер перезаполнения базы данных биллинговой системы водокачки "СПРИНТ"��   Description$K EПредыдущая база данных не сохранится!��  -   $	 Page1��   PageType$ Interior=�$-
 'Цена за кубометр воды��$< 6Введите цену за кубометр воды��   CanSelectNextPage$ FalseX�   NextPage ��  �� ElementName$ Page2��   PreviousPage �� $ 	IntroPage��.���  � ColumnDefinitions �� $
��35px ��� $
��50px ��� $��230px � � RowDefinitions �� $
��Auto ��� $��* �.+��� $��	Цена:���� -   $��CostOfWaterTextBox��$��1���� $��2��$'��!(в формате руб,коп)��$��5,0,0,0q�  -   $	 Page2��$ Interior=�$
 Дачники��$O IДобавьте дачников и площади их участков��.���  � IsSharedSizeScope .  �� $
��Auto ��� $��* �.+�� $��0�� �� $	��10* ��� $��5* ��� $��4* � �� $
��Auto ��� $
��Auto �.+��� $��ФИО дачника:���� $��1��$$��Площадь участка:��$	��1,0q��� -   $��AuthorOfCottageTextBox��$��1��$	��0,1q��� -   $��SquareTextBox��$��1��$��1��$	��2,1q��� -   $��AddCottagerButton��$��1��$��2��$��Добавить��$��1,0,0,0q�� $��1�� � ShowGridLines .  �� $��* �.+�&   System.Windows.Controls.DataGrid  -	   $��AuthorsDataGrid��$	��0,5q�   
IsReadOnly .    SelectionMode$ Single=� � 	Resources    RowMenu��%1   &	        �� .@�m� -
   $��Удалить��   RowStyle ��  �� 
TargetType)  #System.Windows.Controls.DataGridRow"  .���  �� Property �@�ContextMenu �� Value# [    -   $	 Page3��$ Interior=�$:
 4Удаление предыдущих записей��$ FalseX�.���  �� $
��Auto � �� $
��Auto �.+��� -   $!��DeleteAllFromTablesCheckBox��$��RightToLeft=�$t��nЯ понимаю, что все предыдущие записи из базы данных удалятся��  -   $	 LastPage��$ Interior=�$J
 DСоздание новой базы данных завершено��   CanSelectPreviousPage$ FalseX�   	CanFinish$
 TrueX�   CancelButtonVisibility$ 	Collapsed=�   BackButtonVisibility$ 	Collapsed=�.���  �� $
��Auto ��� $
��Auto �.+��� -   $��costTextBlock��$��0��$/��)Цена за кубометр воды: ���� -   $��countCottagersTextBlock��$��1��$+��%Добавлено дачников: ��