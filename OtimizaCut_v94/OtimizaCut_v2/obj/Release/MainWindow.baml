   M S B A M L   `   `   `  ����   OtimizaCut_v2    OtimizaCut_v2.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -     � Title$!  OtimizaCut - Aislan Systems��$��749.328��$��1381.166�� � WindowState$ 	Maximized=� � Icon$ Logo.png�� � DataContext ��  �� RelativeSource �� *Self+.��� .+��� .@��� $��	Materiais��.��� 
�������.+��� $��Biblioteca de Materiais��$
��Left=�$��	10,10,0,0q�$	��Top=�$	��581��$
��1343���� -    ��Name$ adicionarMaterial��$��	Cadastrar��$
��Left=�$��
31,530,0,0q�$	��Top=�$��75��$��33���� -   $ excluirMaterial��$��Excluir��$
��Left=�$��227,530,0,0q�$	��Top=�$��75��$��33���� -   $ editarMaterial��$��Editar��$
��Left=�$��128,530,0,0q�$	��Top=�$��75��$��33���� -    ��Name$ listview��$	��456��$��
31,45,10,0q�$	��Top=� �� View �� .P���  �� Width$	 140��$��Código��	 �� DisplayMemberBinding	 �� *Codigo+�� $	 140��$��Descrição��	 �� *	Descricao+�� $	 140��$��Comprimento��	 �� *Comprimento+�� $	 140��$��Largura��	 �� *	Largura+�� $	 140��$��	Espessura��	 �� *	Espessura+�� $	 140��$	��Cor��	 �� *Cor+�� $	 140��$
��Tipo��	 �� *Tipo+�� $	 140��$��Tem veio��	 �� *	Rotaciona+�� -   $ atualizarMaterial��$��Refresh��$
��Left=�$��326,530,0,0q�$	��Top=�$��74��$��33���� -   $ 	botaoSair��$
��Sair��$
��Left=�$��502,530,0,0q�$	��Top=�$��75��$��33���� $��Lotes��.��� 
�������
 � RowDefinitions
 �� .+��� $��Lotes��$
��Left=�$	��582��$��	10,10,0,0q�$	��Top=�$
��1343��.���� -   $ listviewLotes��$
��Left=�$	��453��$��	15,19,0,0q�$	��Top=�$	��915�� �� .P��� $	 140��$��Código��	 �� *Codigo+�� $	 450��$��Descrição��	 �� *	Descricao+�� -	   $ cadastrarLote��$��	Cadastrar��$
��Left=�$��
31,530,0,0q�$	��Top=�$��75��$��33���� -
   $ 
editarLote��$��Editar��$
��Left=�$��128,530,0,0q�$	��Top=�$��75��$��33���� -   $ excluirLote��$��Excluir��$
��Left=�$��227,530,0,0q�$	��Top=�$��75��$��33���� -   $ refreshLote��$��Refresh��$
��Left=�$��326,530,0,0q�$	��Top=�$��75��$��33���� -   $ sairLote��$
��Sair��$
��Left=�$��502,530,0,0q�$	��Top=�$��75��$��33���� $��Configurações��.��� 
�������.+��� $��Espessura da serra:��$
��Left=�$��	46,49,0,0q�$	��Top=��� $��Refilos laterais:��$
��Left=�$��	67,75,0,0q�$	��Top=��� -    ��Name$ espessuraSerra��$
��Left=�$��23��$��
161,51,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��120���� -   $ refilosLaterais��$
��Left=�$��23��$��
161,79,0,0q�$
 Wrap=�$	��Top=�$	��120���� $#��Parâmetros de configuração��$
��Left=�$��	34,29,0,0q�$	��Top=�$	��177��$	��460��.���� -    ��Name$ mostraComLarg��$4��.Exibir comprimento e largura no plano de corte��$
��Left=�$��27��$	��Top=�$	��393��$��0,84,0,0q��� -   $ salvarConfiguracoes��$��Salvar��$
��Left=�$��419,401,0,0q�$	��Top=�$��75���� $
��(mm)��$
��Left=�$��
282,78,0,0q�$	��Top=��� -   $ mostraRefilos��$:��4Exibir linhas de refilos lateriais no plano de corte��$
��Left=�$��
40,149,0,0q�$	��Top=�$	��290���� -   $ mostraCortesIniciais��$U��OExibir apenas os cortes do primeiro ciclo (cortes verticais/cortes horizontais)��$
��Left=�$��
40,169,0,0q�$	��Top=��� -    ��Name$ orientacaoCortesIniciais��$/��)Orientação dos cortes iniciais da chapa��$
��Left=�$��68��$��
34,219,0,0q�$	��Top=�$	��460��	� -    	�Name$ corteHorizontalSetado��$��Horizontais��$
��Left=�$��
40,241,0,0q�$	��Top=� 	� 	GroupName$% orientacaoCortesIniciaisDaChapa��	� -   $ corteVerticalSetado��$��	Verticais��$
��Left=�$��
40,261,0,0q�$	��Top=�$% orientacaoCortesIniciaisDaChapa���� -   $ opcaoRelatorio��$<��6Configurações para geração do relatório de cortes��$
��Left=�$��
34,305,0,0q�$	��Top=�$��84��$	��460���� -   $ mostraDesenho��$4��.Exibir desenho do plano de corte no relatório��$
��Left=�$��
40,333,0,0q�$	��Top=��� -   $ mostraEstatisticas��$,��&Exibir estatísticas do plano de corte��$
��Left=�$��
40,353,0,0q�$	��Top=��� $
��(mm)��$
��Left=�$��
282,50,0,0q�$	��Top=��� $��Sobre��.��� 
�������.+��� $3��-Desenvolvido por Aislan Nadrowski - Ano: 2013��$��
83,275,0,0q�$��Bottom=�$	��281��$��Center=�