   M S B A M L   `   `   `  ����   OtimizaCut_v2&     OtimizaCut_v2.cadastrarPecasLote   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$-  'Cadastro de peças do lote - OtimizaCut��$��669.791��$��811.94�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � DataContext ��  �� RelativeSource �� *Self+.��� $��-1,0,1,0q� � RowDefinitions �� $
��245* ��� $
��398* �.+��� $��Identificação��$
��Left=�$��72��$��	10,26,0,0q�$	��Top=�$
��1011��.��� $
��Left=�$��38��$��	10,10,0,0q�$	��Top=�$	��853��.+��� $��Código:��$
��Left=�$��26��$��0,1,0,0q�$	��Top=�$��98��$
��Bold"��� $��Descrição:��$
��Left=�$��	138,1,0,0q�$	��Top=� J� RenderTransformOrigin$ 0.079,0.154&�$
��Bold"��� -    ��Name$ 
codigoLote��$
��Left=�$��54,6,0,0q�$
��Wrap=�$��cod do lote��$	��Top=��� -   $ descricaoLote��$
��Left=�$��	205,6,0,0q�$
��Wrap=�$��descrição do lote��$	��Top=��� $*��$Escolha do material (matéria-prima)��$
��Left=�$��72��$��
10,103,0,0q�$	��Top=�$
��1011��.��� $
��Left=�$��38��$��	10,10,0,0q�$	��Top=�$	��853��.+��� $��	Material:��$
��Left=�$��26��$��0,1,0,0q�$	��Top=�$��61��$
��Bold"��� -    ��Name$ selecionaMat��$
��Left=�$��21��$��62,6,0,0q�$	��Top=�$	��314��&   System.Windows.Controls.DataGrid  -   	  Name$	 datagridLote��$
��Left=�$	��293��$��	54,37,0,0q�$	��Top=�$	��691��
   SelectionUnit$

 Cell=�$ �� IsSynchronizedWithCurrentItem$
 TrueY�   CanUserAddRows .    CanUserDeleteRows . $��1��   
IsReadOnly .     Columns �� -    ��Name$ excluirLinhas��$%��Excluir todas as peças do lote��$
��Left=�$��30��$��
54,241,0,0q�$	��Top=�$	��172��$��2���� -   $
 sair��$
��Sair��$
��Left=�$��66��$��682,192,0,0q�$	��Top=�$��63��$��2���� -   $ 
salvarLote��$��Salvar��$
��Left=�$��66��$��614,192,0,0q�$	��Top=�$��63��$��2���� -   $ calcular��$��	Calcular!��$
��Left=�$��423,192,0,0q�$	��Top=�$	��134��$��66��$��2�� � ToolTip$< 6Clique aqui para executar o calculo do plano de corte!��