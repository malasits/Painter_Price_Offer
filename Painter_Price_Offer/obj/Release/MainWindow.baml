   M S B A M L   `   `   `  ���� N  JPainter_Price_Offer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Painter_Price_OfferPainter_Price_Offer  $    Painter_Price_Offer.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     3local!clr-namespace:Painter_Price_Offer     -   $��Window��  � Title$  Árajánlat készítő��$	��900��$	��900�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ 	CanResize=�.���  � ColumnDefinitions �� $��* ��� $��* � � RowDefinitions �� $��30 ��� $	��180 ��� $
��290* ��� $
��255* ��� $��30 �.+�o� $��2��.@�m� $��Fájl��.@�m� -   $��Új árajánlat��m� -   $��
Nyomtatás��m� -   $��	Bezárás��m� $��Beállítás��.@�m� -   $��Mentés helye���� $��1��$��0��$��Saját adatok��.��]� .+��� $��80��.+��� .��
Lap címe:�� .��Név:�� .��
Lakcím:�� .��Telefonszám:�� .��e-mail cím:�� .+��� -   $��txtTitle��$��20��$��0,5,0,0q��� -   $��txtName��$��20��$��0,5,0,0q��� -   $��txtLocation��$��20��$��0,5,0,0q��� -	   $��txtTelNumber��$��20��$��0,5,0,0q��� -
   $��txtEmail��$��20��$��0,5,0,0q��� $��1��$��1��$��Megrendelő adatai��.���� .+�� .+��� .��#!Megrendelői adatok alkalmazása:�� -   $��
cbCustomer��$��10,5,0,0q� d� 	IsChecked$ FalseY�]� .+��� $��80��.+��� .��Név:�� .��
Lakcím:�� .��Munk. megn:�� .��Telefonszám:�� .��e-mail cím:�� .+��� -   $��txtCustomerName��$��20��$��0,5,0,0q�{�.  �� -   $��txtCustomerLocation��$��20��$��0,5,0,0q�{�.  �� -   $��txtCustomerWorkPlace��$��20��$��0,5,0,0q�{�.  �� -   $��txtCustomerPhoneNumber��$��20��$��0,5,0,0q�{�.  �� -   $��txtCustomerEmail��$��20��$��0,5,0,0q�{�.  �� $��2��$��2��$��Munkadíj felsorolása��.���� .��&   System.Windows.Controls.DataGrid  -   $��grdWorkflow��   	RowHeight$ 25Q�   AutoGenerateColumns .     Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$	 Megnevezés��
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 3* 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *Megnevezés+  $	 
Mennyiség��$
 1* -   Mennyiseg_Column�� �� *
Mennyiség+  $	 
Egységár��$
 1*  �� *
Egységár+4  .System.Windows.Controls.DataGridComboBoxColumn  $	 Fm / m2��$
 1* -   
Unital��   CanUserSort .     SelectedValueBinding �� *Fmm2+ �� Mode$ TwoWay=�   DisplayMemberPath �� *cbUnit+  $	 	Munkadíj��$
 1*    
IsReadOnly .  �� *	Munkadíj+�� $��3��$��2��$��Anyagszükséglet��.���� .��  -   $��grdConsumption��$ 25Q� .     $	 Megnevezés��$
 3*  �� *Megnevezés+  $	 
Mennyiség��$
 1*  �� *
Mennyiség+  $	 
Egységár��$
 1*  �� *
Egységár+  $	 	Anyagdíj��$
 1*  .  �� *	Anyagdíj+� $��4��$��0��$��2��$	��Top=�$
��Left=�$��0,0,20,0q�.+��� $
��Bold"�.��Munkadíj összesen:�� -   $��lblWork��
��� � �.��0�� .��FT�� $
��Bold"�.��Anyagdíj összesen:�� -   $��lblMaterial��
��� � �.��0�� .��FT