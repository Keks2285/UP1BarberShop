   M S B A M L   `   `   `  ���� E  ABarberShop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:BarberShop.ModelsBarberShop.Models  ?$clr-namespace:BarberShop.ClientPagesBarberShop.ClientPages  .    (BarberShop.ClientPages.RecordServicePage   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QScottPlot.WPF, Version=4.1.59.0, Culture=neutral, PublicKeyToken=e53b06131e34a3aae aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  (vmclr-namespace:BarberShop.Models  :d2http://schemas.microsoft.com/expression/blend/2008  0local$clr-namespace:BarberShop.ClientPages  -     N� Title$  RecordServicePage��.��� 
������� � ColumnDefinitions �� $	��22* ��� $
��176* ��� $
��270* ��� $
��163* ��� $��4* ��� $
��165* � � RowDefinitions �� $	��27* ��� $	��46* ��� $
��189* ��� $
��188* �.+�&   System.Windows.Controls.DataGrid  -     Name$ 
ServicesDg��   CanUserSortColumns$
 True��$   HorizontalScrollBarVisibility$
 Auto=�"   VerticalScrollBarVisibility$
 Auto=�   AutoGenerateColumns$ False��   CanUserDeleteRows$
 True��	   CanUserAddRows$	 False��$��
4,10,10,10q�$��1��$��1��$��3��$��2��
   Columns
 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ *    
IsReadOnly$
 True��   CanUserSort$
 True��   Header$ Название��1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Name_Service+  $ * $
 True��$
 True��$ Стоимость�� �� *Price_Service+�� -    ��Name$ 	RecordBtn��$��1��$(��"Добавить  материал��$	��Top=�$��3�� � Cursor$
 Hand��$��46��$��
4,10,202,0q�$��2���� $��2��$��Наши услуги��$
��Left=�$��8,0,0,0q�$��Center=�$��24��$��72��(  "System.Windows.Controls.DatePicker  -     Name$ 
DateRecord��$��3��$
��Left=�$��	10,10,0,0q�$��3��$	��Top=�$��46��$	��147��)  #MaterialDesignThemes.Wpf.TimePicker  -     Name$ 
TimeRecord��$��3��$
��Left=�$��10,0,0,0q�$��3��$��36��$	��147��