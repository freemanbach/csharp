   M S B A M L   `   `   `  ����C  ?ParseCSV, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:ParseCSVParseCSV  5   	   O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem 5   	       ParseCSV.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:ParseCSV     5   	   9System&clr-namespace:System;assembly=mscorlib     5   	   -   5          � Title$  ParseCSV��5
   	   $	��560��6   $	��680��6'   .��5      � .+�5   
   �� -   5         ��Name$ lblText1��5      $
��Item��6"   $
��Left=�61   $��30��6L   $��	45,66,0,0q�6X   $	��Top=�6k   $��50��6�   $��Arial)�6�   $
��Bold"�6�    J� RenderTransformOrigin$ 0.5,0.5&�6�   �� 5   
   -   5         ��Name$ txtBox1��5      $
��Left=�6#   $��30��6>   $��
118,62,0,0q�6J    �� TextWrapping$
 Wrap=�6^   $	��Top=�6r   $	��150��6�   �� 5   
   $��Visible=�6   .��5      �� $	��Top=�6   $
��Left=�62   .+�5      �� -   5         ��Name$ txtBox2��5      $
��Wrap=�6-   $	��160��6A   $	��320��6N   $��
321,66,0,0q�6Z   6p   5      5      �� 5   
   -   5         ��Name$ lstBox1��5      $
��Left=�6#   $��52��6>   $��
79,146,0,0q�6J   $	��Top=�6^   $	��181��6v   ��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� ��6$   �� 5      5      5      �� 5    
   -   5         ��Name$ 
btnButton1��5       $��Clear��6%   $
��Left=�65   $��30��6P   $��
79,237,0,0q�6\   $	��Top=�6p   $��55��6�   6�   �� 5!   
   -   5        $ 
btnButton2��5!      $
��Exit��6%   $
��Left=�64   $��30��6O   $��180,237,0,0q�6[   $	��Top=�6p   $��55��6�   6�   �� 5"   
   -   5        $ 
btnButton3��5"      $��Submit��6%   $
��Left=�66   $��30��6Q   $��575,237,0,0q�6]   $	��Top=�6r   $��55��6�   $ 0.531,0.328&�6�   6�   o� 5%   
   $
��Left=�6   $��32��6*   $��10,0,0,0q�66   $	��Top=�6H   $��60��6`   .@�5&      m� $��_File��6   .@�5'      m� 	 m� Command$	 ApplicationCommands.Copy��6   5(      m� 5)      $��_Edit��6   .@�5*      m� $	 ApplicationCommands.Paste��6   5+      5,      5.      5/      