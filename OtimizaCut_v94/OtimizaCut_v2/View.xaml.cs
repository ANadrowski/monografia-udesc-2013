using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Xml;
using System.Data;
using System.Printing;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Collections.ObjectModel;




namespace OtimizaCut_v2
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : Window
    {

        public View(string codigoLote)
        {
            InitializeComponent();
            retangulo(codigoLote);

        }



        // Inicio da rotina responsável por transformar cada TabItem num arquivo de imagem.

        private static void SaveUsingEncoder(string fileName, FrameworkElement UIElement, BitmapEncoder encoder)
        {
            int height = (int)UIElement.ActualHeight;
            int width = (int)UIElement.ActualWidth;


            UIElement.Measure(new System.Windows.Size(width, height));
            UIElement.Arrange(new Rect(0, 0, width, height));
            RenderTargetBitmap bitmap = new RenderTargetBitmap(width,

                                                                    height,
                                                                    96, // These decides the dpi factors 
                                                                    96,// The can be changed when we'll have preview options.
                                                                    PixelFormats.Pbgra32);
            bitmap.Render(UIElement);

            SaveUsingBitmapTargetRenderer(fileName, bitmap, encoder);
        }


        private static void SaveUsingBitmapTargetRenderer(string fileName, RenderTargetBitmap renderTargetBitmap, BitmapEncoder bitmapEncoder)
        {
            BitmapFrame frame = BitmapFrame.Create(renderTargetBitmap);
            bitmapEncoder.Frames.Add(frame);
            // Save file .
            string caminhoCompleto = @"C:\dados\imagens\relatorio\";
            string caminhoCompleto2 = caminhoCompleto + fileName;

            using (var stream = File.Create(caminhoCompleto2))
            {
                bitmapEncoder.Save(stream);
            }
        }

        // Final da rotina responsável por transformar cada TabItem num arquivo de imagem.



        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //  Inserir código para gerar um PDF do plano gerado.
        }

        private void fechar(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void gerarPlano(object sender, RoutedEventArgs e)
        {
            try
            {


                //Carrega valores de configuracoes

                string caminhoConf = "C:\\dados\\configuracoes\\configuracoes.xml";

                XmlTextReader arqConf = new XmlTextReader(caminhoConf);

                bool mostraPlanosDeCorteNoRelatorioBool = false;
                bool mostraEstatisticasNoRelatorioBool = false;

                while (arqConf.Read())
                {
                    if (arqConf.NodeType == XmlNodeType.Element)
                    {

                        if (arqConf.Name == "mostraDesenhoNoRelatorio")
                        {

                            if (arqConf.ReadString().Equals("True"))
                                mostraPlanosDeCorteNoRelatorioBool = true;

                            if (arqConf.ReadString().Equals("False"))
                                mostraPlanosDeCorteNoRelatorioBool = false;
                        }

                        if (arqConf.Name == "mostraEstatisticasNoRelatorio")
                        {

                            if (arqConf.ReadString().Equals("True"))
                                mostraEstatisticasNoRelatorioBool = true;

                            if (arqConf.ReadString().Equals("False"))
                                mostraEstatisticasNoRelatorioBool = false;
                        }


                    }
                }

                arqConf.Close();


                //Carrega valores do material e lote para mostrar na capa do relatório
                LVData informacoesParaRelatorio = new LVData();
                string descricaoLote = ""; 
                string codigoLote = "";

                string caminhoEst = "C:\\dados\\planos_otimizados\\estatisticas.xml";

                XmlTextReader leitorCapa = new XmlTextReader(caminhoEst);

                while (leitorCapa.Read())
                {
                    if (leitorCapa.NodeType == XmlNodeType.Element)
                    {
                        if (leitorCapa.Name == "codigo")
                            informacoesParaRelatorio.Codigo = leitorCapa.ReadString();

                        if (leitorCapa.Name == "descricao")
                            informacoesParaRelatorio.Descricao = leitorCapa.ReadString();

                        if (leitorCapa.Name == "comprimento")
                            informacoesParaRelatorio.Comprimento = leitorCapa.ReadString();

                        if (leitorCapa.Name == "largura")
                            informacoesParaRelatorio.Largura = leitorCapa.ReadString();

                        if (leitorCapa.Name == "espessura")
                            informacoesParaRelatorio.Espessura = leitorCapa.ReadString();

                        if (leitorCapa.Name == "cor")
                            informacoesParaRelatorio.Cor = leitorCapa.ReadString();

                        if (leitorCapa.Name == "tipo")
                            informacoesParaRelatorio.Tipo = leitorCapa.ReadString();

                        if (leitorCapa.Name == "descricaoLote")
                            descricaoLote = leitorCapa.ReadString();

                        if (leitorCapa.Name == "codigoLote")
                            codigoLote = leitorCapa.ReadString();

                    }
                }
                    string codigoMatString = informacoesParaRelatorio.Codigo;
                    string descricaoMatString = informacoesParaRelatorio.Descricao;
                    string comprimentoMatString = informacoesParaRelatorio.Comprimento;
                    string larguraMatString = informacoesParaRelatorio.Largura;
                    string espessuraMatString = informacoesParaRelatorio.Espessura;
                    string corMatString = informacoesParaRelatorio.Cor;
                    string tipoMatString = informacoesParaRelatorio.Tipo;

                    leitorCapa.Close();


                //INFORMAÇÕES DA CAPA DO RELATÓRIO.
                // Cria objeto PrintDialog
                PrintDialog pd = new PrintDialog();
                if (pd.ShowDialog() != true) return;

                // Cria documento
                FixedDocument document = new FixedDocument();
                document.DocumentPaginator.PageSize = new Size(pd.PrintableAreaWidth, pd.PrintableAreaHeight);

                //Cria pagina da capa
                FixedPage page1 = new FixedPage();
                page1.Width = document.DocumentPaginator.PageSize.Width;
                page1.Height = document.DocumentPaginator.PageSize.Height;

                //Titulo da capa
                TextBlock tituloCapa = new TextBlock();
                tituloCapa.Text = "Relatório de Cortes (OtimizaCut)";
                tituloCapa.FontWeight = FontWeights.UltraBold;
                tituloCapa.FontSize = 30; 
                tituloCapa.Margin = new Thickness(300, 30, 0, 0);
                page1.Children.Add(tituloCapa);


                TextBlock propriedadesMatCapa = new TextBlock();
                propriedadesMatCapa.Text = "Propriedades do material utilizado: ";
                propriedadesMatCapa.FontWeight = FontWeights.UltraBold;
                propriedadesMatCapa.FontSize = 16;
                propriedadesMatCapa.Margin = new Thickness(96, 100, 0, 0); 
                page1.Children.Add(propriedadesMatCapa);

                TextBlock codigoMatCapa = new TextBlock();
                codigoMatCapa.Text = "Código: " + codigoMatString;
                codigoMatCapa.FontSize = 14;
                codigoMatCapa.Margin = new Thickness(96, 120, 0, 0);
                page1.Children.Add(codigoMatCapa);

                TextBlock descricaoMatCapa = new TextBlock();
                descricaoMatCapa.Text = "Descrição: " + descricaoMatString;
                descricaoMatCapa.FontSize = 14; 
                descricaoMatCapa.Margin = new Thickness(96, 135, 0, 0); 
                page1.Children.Add(descricaoMatCapa);

                TextBlock comprimentoMatCapa = new TextBlock();
                comprimentoMatCapa.Text = "Comprimento: " + comprimentoMatString;
                comprimentoMatCapa.FontSize = 14; 
                comprimentoMatCapa.Margin = new Thickness(96, 150, 0, 0); 
                page1.Children.Add(comprimentoMatCapa);

                TextBlock larguraMatCapa = new TextBlock();
                larguraMatCapa.Text = "Largura: " + larguraMatString;
                larguraMatCapa.FontSize = 14; 
                larguraMatCapa.Margin = new Thickness(96, 165, 0, 0); 
                page1.Children.Add(larguraMatCapa);

                TextBlock espessuraMatCapa = new TextBlock();
                espessuraMatCapa.Text = "Espessura: " + espessuraMatString;
                espessuraMatCapa.FontSize = 14; 
                espessuraMatCapa.Margin = new Thickness(96, 180, 0, 0); 
                page1.Children.Add(espessuraMatCapa);

                TextBlock corMatCapa = new TextBlock();
                corMatCapa.Text = "Cor: " + corMatString;
                corMatCapa.FontSize = 14; 
                corMatCapa.Margin = new Thickness(96, 195, 0, 0); 
                page1.Children.Add(corMatCapa);

                TextBlock tipoMatCapa = new TextBlock();
                tipoMatCapa.Text = "Tipo: " + tipoMatString;
                tipoMatCapa.FontSize = 14; 
                tipoMatCapa.Margin = new Thickness(96, 210, 0, 0); 
                page1.Children.Add(tipoMatCapa);


                Image finalImage = new Image();
                finalImage.Margin = new Thickness(850,60,0,0);               
                BitmapImage logo = new BitmapImage();
                logo.BeginInit();
                logo.UriSource = new Uri("C:/dados/imagens/logo.jpg");
                logo.EndInit();               
                finalImage.Source = logo;
                page1.Children.Add(finalImage);


                TextBlock propriedadesLoteCapa = new TextBlock();
                propriedadesLoteCapa.Text = "Propriedades do lote otimizado: ";
                propriedadesLoteCapa.FontWeight = FontWeights.UltraBold;
                propriedadesLoteCapa.FontSize = 16;
                propriedadesLoteCapa.Margin = new Thickness(96, 240, 0, 0);
                page1.Children.Add(propriedadesLoteCapa);

                TextBlock codigoLoteCapa = new TextBlock();
                codigoLoteCapa.Text = "Código: " + codigoLote;
                codigoLoteCapa.FontSize = 14;
                codigoLoteCapa.Margin = new Thickness(96, 260, 0, 0);
                page1.Children.Add(codigoLoteCapa);

                TextBlock descricaoLoteCapa = new TextBlock();
                descricaoLoteCapa.Text = "Descrição: " + descricaoLote;
                descricaoLoteCapa.FontSize = 14;
                descricaoLoteCapa.Margin = new Thickness(96, 275, 0, 0);
                page1.Children.Add(descricaoLoteCapa);

                TextBlock rodapeCapa = new TextBlock();
                rodapeCapa.Text = "Otimizador de Cortes OtimizaCut - Desenvolvido por Aislan Nadrowski (2013)";
                rodapeCapa.FontSize = 12;
                rodapeCapa.Margin = new Thickness(96, 750, 0, 0);
                page1.Children.Add(rodapeCapa);


                // Adiciona página da capa no documento
                PageContent page1Content = new PageContent();
                ((IAddChild)page1Content).AddChild(page1);
                document.Pages.Add(page1Content);


                //PLANOS DE CORTE (INSERÇÃO DOS DESENHOS NO RELATÓRIO).


                if (mostraPlanosDeCorteNoRelatorioBool == true)
                {

                    int quantidadeDeTabs = tabcontrolView.Items.Count;

                    for (int i = 0; i < quantidadeDeTabs; i++)
                    {
                        tabcontrolView.SelectedIndex = i;
                        string nomeArquivoImagem = i.ToString() + ".png";
                        int numeroDaChapa = i + 1;
                        string mensagemDeOk = "Coordenadas geradas para a chapa " + numeroDaChapa.ToString() + " com sucesso. Pressione Ok!";
                        MessageBox.Show(mensagemDeOk);
                        SaveUsingEncoder(nomeArquivoImagem, tabcontrolView, new PngBitmapEncoder());

                    }



                    int quantidadeDeTabsMais1 = 0;
                    for (int i = 0; i < quantidadeDeTabs; i++)
                    {
                        quantidadeDeTabsMais1 = i + 1;
                        //Insere titulo na pagina do desenho
                        FixedPage page2 = new FixedPage();
                        page2.Width = document.DocumentPaginator.PageSize.Width;
                        page2.Height = document.DocumentPaginator.PageSize.Height;
                        TextBlock txt1 = new TextBlock();
                        txt1.Text = "Chapa " + quantidadeDeTabsMais1 + ":";
                        txt1.FontSize = 30;
                        txt1.Margin = new Thickness(20, 10, 0, 0);
                        page2.Children.Add(txt1);
                        PageContent page2Content = new PageContent();
                        ((IAddChild)page2Content).AddChild(page2);
                        document.Pages.Add(page2Content);

                        string auxUriPlano = @"C:/dados/imagens/relatorio/" + i.ToString() + ".png";
                        Image planoDeCorteImage = new Image();
                        planoDeCorteImage.Margin = new Thickness(96, 40, 0, 0);
                        BitmapImage planoDeCorte1 = new BitmapImage();
                        planoDeCorte1.BeginInit();
                        planoDeCorte1.UriSource = new Uri(auxUriPlano);
                        planoDeCorte1.EndInit();
                        planoDeCorteImage.Source = planoDeCorte1;
                        page2.Children.Add(planoDeCorteImage);


                        Image logoEscrito = new Image();
                        logoEscrito.Margin = new Thickness(880, 10, 0, 0);
                        BitmapImage logoEscrito1 = new BitmapImage();
                        logoEscrito1.BeginInit();
                        logoEscrito1.UriSource = new Uri("C:/dados/imagens/logo_escrito.jpg");
                        logoEscrito1.EndInit();
                        logoEscrito.Source = logoEscrito1;
                        page2.Children.Add(logoEscrito);



                        TextBlock rodapePaginaPlano = new TextBlock();
                        rodapePaginaPlano.Text = "Otimizador de Cortes OtimizaCut - Desenvolvido por Aislan Nadrowski (2013)";
                        rodapePaginaPlano.FontSize = 12;
                        rodapePaginaPlano.Margin = new Thickness(96, 750, 0, 0);
                        page2.Children.Add(rodapePaginaPlano);
                    }
                }




                //ESTATÍSTICAS DOS PLANOS.

                if (mostraEstatisticasNoRelatorioBool == true)
                {
                    string caminhoPlano = "c:\\dados\\planos_otimizados\\" + codigoLote + ".xml";
                    XmlDocument doc = new XmlDocument();
                    doc.Load(caminhoPlano);

                    int contadorChapasParaRelatorio = 0;
                    int contadorPecasNaChapa = 0;

                    double areaDaPeca = 0;
                    double areaTotalDasPecasNaChapa = 0;

                    XmlNodeList listaPlano = doc.SelectNodes("otimizado/planos/plano");

                    foreach (XmlNode node1 in listaPlano)
                    {

                        FixedPage page3 = new FixedPage();
                        page3.Width = document.DocumentPaginator.PageSize.Width;
                        page3.Height = document.DocumentPaginator.PageSize.Height;


                        contadorChapasParaRelatorio++;



                        TextBlock codigoListaPecas = new TextBlock();
                        codigoListaPecas.Text = "CÓDIGO";
                        codigoListaPecas.FontSize = 12;
                        codigoListaPecas.Margin = new Thickness(96, 130, 0, 0);
                        codigoListaPecas.FontWeight = FontWeights.UltraBold;
                        page3.Children.Add(codigoListaPecas);


                        TextBlock descricaoListaPecas = new TextBlock();
                        descricaoListaPecas.Text = "DESCRIÇÃO";
                        descricaoListaPecas.FontSize = 12;
                        descricaoListaPecas.Margin = new Thickness(180, 130, 0, 0);
                        descricaoListaPecas.FontWeight = FontWeights.UltraBold;
                        page3.Children.Add(descricaoListaPecas);


                        TextBlock comprimentoListaPecas = new TextBlock();
                        comprimentoListaPecas.Text = "COMPRIMENTO";
                        comprimentoListaPecas.FontSize = 12;
                        comprimentoListaPecas.Margin = new Thickness(350, 130, 0, 0);
                        comprimentoListaPecas.FontWeight = FontWeights.UltraBold;
                        page3.Children.Add(comprimentoListaPecas);

                        TextBlock larguraListaPecas = new TextBlock();
                        larguraListaPecas.Text = "LARGURA";
                        larguraListaPecas.FontSize = 12;
                        larguraListaPecas.Margin = new Thickness(500, 130, 0, 0);
                        larguraListaPecas.FontWeight = FontWeights.UltraBold;
                        page3.Children.Add(larguraListaPecas);

                        TextBlock quantidadeListaPecas = new TextBlock();
                        quantidadeListaPecas.Text = "QUANTIDADE";
                        quantidadeListaPecas.FontSize = 12;
                        quantidadeListaPecas.Margin = new Thickness(610, 130, 0, 0);
                        quantidadeListaPecas.FontWeight = FontWeights.UltraBold;
                        page3.Children.Add(quantidadeListaPecas);


                        TextBlock areaDaPecaListaPecas = new TextBlock();
                        areaDaPecaListaPecas.Text = "ÁREA (M²)";
                        areaDaPecaListaPecas.FontSize = 12;
                        areaDaPecaListaPecas.Margin = new Thickness(750, 130, 0, 0);
                        areaDaPecaListaPecas.FontWeight = FontWeights.UltraBold;
                        page3.Children.Add(areaDaPecaListaPecas);



                        XmlNodeList listaPeca = node1.SelectNodes("peca");


                     //   var listaDePecaParaRelatorio = new ObservableCollection<PecaParaPlano>()
                      //  {
                       // };

                        var listaDePecaParaRelatorio = new List<PecaParaPlano>
                        {
                        };

                        var lista = new List<PecaParaPlano>
                        {

                        };



                        int posLinha = 145;

                        foreach (XmlNode node in listaPeca)
                        {
                            XmlElement elementoPeca = (XmlElement)node;

                            string codigoP = elementoPeca.GetElementsByTagName("codigoP")[0].InnerText;
                            string descricaoP = elementoPeca.GetElementsByTagName("descricaoP")[0].InnerText;
                            string comprimentoP = elementoPeca.GetElementsByTagName("comprimentoP")[0].InnerText;
                            string larguraP = elementoPeca.GetElementsByTagName("larguraP")[0].InnerText;
                            string rotacionaP = elementoPeca.GetElementsByTagName("rotacionaP")[0].InnerText;
                            string quantidadeP = elementoPeca.GetElementsByTagName("quantidadeP")[0].InnerText;
                            string coordenadaXP = elementoPeca.GetElementsByTagName("coordenadaXP")[0].InnerText;
                            string coordenadaYP = elementoPeca.GetElementsByTagName("coordenadaYP")[0].InnerText;

                            string primeirosCortesP = elementoPeca.GetElementsByTagName("coordenadaPrimeirosCortes")[0].InnerText;


                            double lar = Convert.ToDouble(larguraP);
                            double comp = Convert.ToDouble(comprimentoP);
                            double coorY = Convert.ToDouble(coordenadaXP);
                            double coorX = Convert.ToDouble(coordenadaYP);

                            areaDaPeca = lar * comp;
                            areaTotalDasPecasNaChapa = areaTotalDasPecasNaChapa + areaDaPeca;
                            contadorPecasNaChapa++;


                            PecaParaPlano pecaNaLista = new PecaParaPlano();
                            pecaNaLista.Codigo = codigoP;
                            pecaNaLista.Descricao = descricaoP;
                            pecaNaLista.Comprimento = comprimentoP;
                            pecaNaLista.Largura = larguraP;
                            pecaNaLista.Rotacionar = false;
                            pecaNaLista.Quantidade = quantidadeP;

                            listaDePecaParaRelatorio.Add(pecaNaLista);
                            lista.Add(pecaNaLista);

              
                        }


                        List<PecaParaPlano> list = lista.GroupBy(a => a.Codigo).Select(g => g.First()).ToList();

                        int tamanhoDaLista = list.Count();

                        for (int i = 1; i <= tamanhoDaLista; i++)
                        {
                         //   MessageBox.Show(list[i].Codigo.ToString());
                            //ver quantas vezes o valor de list[i] aparece na lista toda de pecas do plano e multiplicar para mostrar valor.
                            int repeticao = 0;
                            foreach (PecaParaPlano p in listaDePecaParaRelatorio)
                            {
                                if (p.Codigo.Equals(list[i-1].Codigo))
                                    repeticao++;


                            }

                        //    MessageBox.Show(i.ToString() + " : " + repeticao.ToString());

                            TextBlock codigoListaPecasInfo = new TextBlock();
                            codigoListaPecasInfo.Text = list[i-1].Codigo;
                            codigoListaPecasInfo.FontSize = 12;
                            codigoListaPecasInfo.Margin = new Thickness(96, posLinha, 0, 0);
                            page3.Children.Add(codigoListaPecasInfo);


                            TextBlock descricaoListaPecasInfo = new TextBlock();
                            descricaoListaPecasInfo.Text = list[i-1].Descricao;
                            descricaoListaPecasInfo.FontSize = 12;
                            descricaoListaPecasInfo.Margin = new Thickness(180, posLinha, 0, 0);
                            page3.Children.Add(descricaoListaPecasInfo);


                            TextBlock comprimentoListaPecasInfo = new TextBlock();
                            comprimentoListaPecasInfo.Text = list[i-1].Comprimento;
                            comprimentoListaPecasInfo.FontSize = 12;
                            comprimentoListaPecasInfo.Margin = new Thickness(350, posLinha, 0, 0);
                            page3.Children.Add(comprimentoListaPecasInfo);


                            TextBlock larguraListaPecasInfo = new TextBlock();
                            larguraListaPecasInfo.Text = list[i-1].Largura;
                            larguraListaPecasInfo.FontSize = 12;
                            larguraListaPecasInfo.Margin = new Thickness(500, posLinha, 0, 0);
                            page3.Children.Add(larguraListaPecasInfo);


                            TextBlock quantidadeListaPecasInfo = new TextBlock();
                            quantidadeListaPecasInfo.Text = repeticao.ToString();
                            quantidadeListaPecasInfo.FontSize = 12;
                            quantidadeListaPecasInfo.Margin = new Thickness(610, posLinha, 0, 0);
                            page3.Children.Add(quantidadeListaPecasInfo);


                            double areaPeca2 = ( (Convert.ToDouble(list[i-1].Comprimento) * Convert.ToDouble(list[i-1].Largura) * repeticao )  ) / 1000000;
                            TextBlock areaListaPecasInfo = new TextBlock();
                            areaListaPecasInfo.Text = areaPeca2.ToString("0.00");
                            areaListaPecasInfo.FontSize = 12;
                            areaListaPecasInfo.Margin = new Thickness(750, posLinha, 0, 0);
                            page3.Children.Add(areaListaPecasInfo);
                            posLinha = posLinha + 15;

                            repeticao = 0;


                        }



                        //Insere titulo na pagina de estatistica

                        TextBlock tituloPagEst = new TextBlock();
                        tituloPagEst.Text = "Chapa " + contadorChapasParaRelatorio + " - Estatísticas:";
                        tituloPagEst.FontSize = 30;
                        tituloPagEst.Margin = new Thickness(20, 10, 0, 0);
                        page3.Children.Add(tituloPagEst);
                        PageContent page3Content = new PageContent();
                        ((IAddChild)page3Content).AddChild(page3);
                        document.Pages.Add(page3Content);


                        Image logoEscrito = new Image();
                        logoEscrito.Margin = new Thickness(880, 10, 0, 0);
                        BitmapImage logoEscrito1 = new BitmapImage();
                        logoEscrito1.BeginInit();
                        logoEscrito1.UriSource = new Uri("C:/dados/imagens/logo_escrito.jpg");
                        logoEscrito1.EndInit();
                        logoEscrito.Source = logoEscrito1;
                        page3.Children.Add(logoEscrito);



                        TextBlock rodapePaginaPlano = new TextBlock();
                        rodapePaginaPlano.Text = "Otimizador de Cortes OtimizaCut - Desenvolvido por Aislan Nadrowski (2013)";
                        rodapePaginaPlano.FontSize = 12;
                        rodapePaginaPlano.Margin = new Thickness(96, 750, 0, 0);
                        page3.Children.Add(rodapePaginaPlano);


                        SolidColorBrush corDoSeparador = new SolidColorBrush();
                        corDoSeparador.Color = Colors.Black;

                        Line separador = new Line();
                        separador.X1 = 2750;
                        separador.Y1 = 60;
                        separador.Y2 = 60;
                        separador.StrokeThickness = 1;
                        separador.Stroke = corDoSeparador;
                        page3.Children.Add(separador);


                        Line separador1 = new Line();
                        separador1.X1 = 2750;
                        separador1.Y1 = 120;
                        separador1.Y2 = 120;
                        separador1.StrokeThickness = 1;
                        separador1.Stroke = corDoSeparador;
                        page3.Children.Add(separador1);

                        TextBlock infoMatRel = new TextBlock();
                        infoMatRel.Text = "Material: ";
                        infoMatRel.FontSize = 12;
                        infoMatRel.Margin = new Thickness(10, 65, 0, 0);
                        infoMatRel.FontWeight = FontWeights.UltraBold;
                        page3.Children.Add(infoMatRel);

                        TextBlock infoMatRelInfo = new TextBlock();
                        infoMatRelInfo.Text = comprimentoMatString + "mm x " + larguraMatString + "mm x " + espessuraMatString + "mm";
                        infoMatRelInfo.FontSize = 12;
                        infoMatRelInfo.Margin = new Thickness(65, 65, 0, 0);                       
                        page3.Children.Add(infoMatRelInfo);


                        TextBlock quantidadePecas = new TextBlock();
                        quantidadePecas.Text = "Quantidade de peças: ";
                        quantidadePecas.FontSize = 12;
                        quantidadePecas.Margin = new Thickness(10, 80, 0, 0);
                        quantidadePecas.FontWeight = FontWeights.UltraBold;
                        page3.Children.Add(quantidadePecas);


                        TextBlock quantidadePecasInfo = new TextBlock();
                        quantidadePecasInfo.Text = contadorPecasNaChapa.ToString();
                        quantidadePecasInfo.FontSize = 12;
                        quantidadePecasInfo.Margin = new Thickness(135, 80, 0, 0);
                        page3.Children.Add(quantidadePecasInfo);
                        contadorPecasNaChapa = 0;

                        /*
                        TextBlock quantidadeCortes = new TextBlock();
                        quantidadeCortes.Text = "Quantidade de cortes: ";
                        quantidadeCortes.FontSize = 12;
                        quantidadeCortes.Margin = new Thickness(10, 95, 0, 0);
                        quantidadeCortes.FontWeight = FontWeights.UltraBold;
                        page3.Children.Add(quantidadeCortes);
                        */

                        TextBlock areaMatRel = new TextBlock();
                        areaMatRel.Text = "Área total da chapa: ";
                        areaMatRel.FontSize = 12;
                        areaMatRel.Margin = new Thickness(300, 65, 0, 0);
                        areaMatRel.FontWeight = FontWeights.UltraBold;
                        page3.Children.Add(areaMatRel);

                        double areaMaterial = (Convert.ToDouble(comprimentoMatString) * Convert.ToDouble(larguraMatString)) / 1000000;

                        TextBlock areaMatRelInfo = new TextBlock();
                        areaMatRelInfo.Text = areaMaterial.ToString() + "m²";
                        areaMatRelInfo.FontSize = 12;
                        areaMatRelInfo.Margin = new Thickness(417, 65, 0, 0);
                        page3.Children.Add(areaMatRelInfo);

                        TextBlock aproveitamentoMatRel = new TextBlock();
                        aproveitamentoMatRel.Text = "Área total de peças na chapa: ";
                        aproveitamentoMatRel.FontSize = 12;
                        aproveitamentoMatRel.Margin = new Thickness(300, 80, 0, 0);
                        aproveitamentoMatRel.FontWeight = FontWeights.UltraBold;
                        page3.Children.Add(aproveitamentoMatRel);


                        areaTotalDasPecasNaChapa = areaTotalDasPecasNaChapa / 1000000;

                        TextBlock aproveitamentoMatRelInfo = new TextBlock();
                        aproveitamentoMatRelInfo.Text = areaTotalDasPecasNaChapa.ToString() + "m²"; 
                        aproveitamentoMatRelInfo.FontSize = 12;
                        aproveitamentoMatRelInfo.Margin = new Thickness(469, 80, 0, 0);
                        page3.Children.Add(aproveitamentoMatRelInfo);

                        


                        TextBlock aproveitamentoMatRelPorcentagem = new TextBlock();
                        aproveitamentoMatRelPorcentagem.Text = "Aproveitamento da chapa: ";
                        aproveitamentoMatRelPorcentagem.FontSize = 12;
                        aproveitamentoMatRelPorcentagem.Margin = new Thickness(300, 95, 0, 0);
                        aproveitamentoMatRelPorcentagem.FontWeight = FontWeights.UltraBold;
                        page3.Children.Add(aproveitamentoMatRelPorcentagem);
                         

                        double aproveitamentoChapa = areaTotalDasPecasNaChapa / areaMaterial * 100;

                        TextBlock aproveitamentoMatRelPorcentagemInfo = new TextBlock();
                        aproveitamentoMatRelPorcentagemInfo.Text = aproveitamentoChapa.ToString("0.00") + "%";
                        aproveitamentoMatRelPorcentagemInfo.FontSize = 12;
                        aproveitamentoMatRelPorcentagemInfo.Margin = new Thickness(453, 95, 0, 0);
                        page3.Children.Add(aproveitamentoMatRelPorcentagemInfo);

                        areaTotalDasPecasNaChapa = 0;

                    }

                }


                // Imprime documento
                string nomeParaSalvar = "Mat " + codigoMatString + " - " + descricaoMatString + " (Lote " + codigoLote + " - " +descricaoLote + ")";
                pd.PrintDocument(document.DocumentPaginator, nomeParaSalvar );


            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao imprimir plano de corte.", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        private Rectangle material(string codigoLote)
        {
            Canvas meuCanvas = new Canvas();

            //Cria objeto para o material.
            Rectangle material = new Rectangle();

            string caminhoPlano = "c:\\dados\\planos_otimizados\\" + codigoLote + ".xml";

            XmlTextReader tr = new XmlTextReader(caminhoPlano);

            while (tr.Read())
            {

                if (tr.NodeType == XmlNodeType.Element)
                {

                    if (tr.Name == "largura")
                    {
                        double lar = Convert.ToDouble(tr.ReadString());
                     //   larguraMatGrid.Text = "Medida Y: " + lar.ToString() + "mm";
                        material.Height = lar / 3;

                    }

                    if (tr.Name == "comprimento")
                    {
                        double comp = Convert.ToDouble(tr.ReadString());
                     //   comprimentoMatGrid.Text = "Medida X: " + comp.ToString() + "mm";
                        material.Width = comp / 3;

                    }
                }
            }

            material.Fill = Brushes.SaddleBrown;
            Canvas.SetTop(material, 0);
            Canvas.SetLeft(material, 0);

            return material;

        }


        public Rectangle cria(double a, double b, double c, double d, string codigo, string descricao, string rotaciona, string quantidade)
        {

            StackPanel meuSPanel = new StackPanel();
            Canvas meuCanvas = new Canvas();

            Rectangle peca = new Rectangle();
            peca.Height = a / 3;
            peca.Width = b / 3;

            double teste1 = c / 3;
            Canvas.SetTop(peca, teste1);
            double teste2 = d / 3;
            Canvas.SetLeft(peca, teste2);

            peca.Fill = Brushes.Linen;

            ToolTip tooltip = new ToolTip { Content = " ~~~~~~ PEÇA  ~~~~~~\nCódigo: " + codigo + "\nDescrição: " + descricao + "\nComprimento: " + b + " mm\nLargura: " + a + " mm\nRotacionou: " + rotaciona + " \nQuantidade: " + quantidade + "\nCoord Y: " + c + "\nCoord X: " + d };
            tooltip.Background = Brushes.Black;
            tooltip.Foreground = Brushes.White;

            tooltip.IsOpen = false;
            peca.ToolTip = tooltip;
            //teste de inserção de texto dentro da peça.

            return peca;
        }


        private void retangulo(string codigoLote)
        {

            //Rotina para fazer a leitura dos parametros de configuração (espessura da serra, refilos, etc).

            string caminhoConf = "C:\\dados\\configuracoes\\configuracoes.xml";

            XmlTextReader arqConf = new XmlTextReader(caminhoConf);

            bool mostraComprimentoLarguraBool = false;
            bool mostraRefilosLateriais = false;
            double refilosLaterais = 0;
            double espessuraSerra = 0;
            bool mostraPrimeirosCortesDaChapa = false;
            bool indicaCortesIniciaisHorizontais = false;


            while (arqConf.Read())
            {
                if (arqConf.NodeType == XmlNodeType.Element)
                {
                    if (arqConf.Name == "mostraCompLarg")
                    {

                        if (arqConf.ReadString().Equals("True"))
                            mostraComprimentoLarguraBool = true;

                        if (arqConf.ReadString().Equals("False"))
                            mostraComprimentoLarguraBool = false;
                    }

                    if (arqConf.Name == "mostraLinhasRefilos")
                    {

                        if (arqConf.ReadString().Equals("True"))
                            mostraRefilosLateriais = true;

                        if (arqConf.ReadString().Equals("False"))
                            mostraRefilosLateriais = false;
                    }

                    if (arqConf.Name == "refilos")
                    {
                        refilosLaterais = Convert.ToDouble( arqConf.ReadString());
                    }

                    if (arqConf.Name == "serra")
                    {
                        espessuraSerra = Convert.ToDouble(arqConf.ReadString());
                    }

                    if (arqConf.Name == "mostraCortesIniciais")
                    {

                        if (arqConf.ReadString().Equals("True"))
                            mostraPrimeirosCortesDaChapa = true;

                        if (arqConf.ReadString().Equals("False"))
                            mostraPrimeirosCortesDaChapa = false;
                    }

                    if (arqConf.Name == "orientacaoCortesIniciaisDaChapa")
                    {

                        if (arqConf.ReadString().Equals("Horizontais"))
                            indicaCortesIniciaisHorizontais = true;

                        if (arqConf.ReadString().Equals("Verticais"))
                            indicaCortesIniciaisHorizontais = false;
                    }


                }
            }

            arqConf.Close();

            //Rotina para fazer a leitura dos valores do mateiral.

            LVData materialExib = new LVData();
            string caminhoPlanoMaterial = "c:\\dados\\planos_otimizados\\" + codigoLote + ".xml";

            XmlTextReader tr = new XmlTextReader(caminhoPlanoMaterial);

            while (tr.Read())
            {

                if (tr.NodeType == XmlNodeType.Element)
                {

                    if (tr.Name == "largura")
                    {
                        double lar = Convert.ToDouble(tr.ReadString());
                        materialExib.Largura = lar.ToString();

                    }

                    if (tr.Name == "comprimento")
                    {
                        double comp = Convert.ToDouble(tr.ReadString());
                        materialExib.Comprimento = comp.ToString();
                    }
                }
            }

            //Restante das funcoes.

            string caminhoPlano = "c:\\dados\\planos_otimizados\\" + codigoLote + ".xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(caminhoPlano);

            int contadorChapas = 0;

            XmlNodeList listaPlano = doc.SelectNodes("otimizado/planos/plano");

            foreach (XmlNode node1 in listaPlano)
            {

                StackPanel meuSPanel2 = new StackPanel();
                Canvas meuCanvas2 = new Canvas();
                TabItem TesteTab = new TabItem();

                contadorChapas++;

                meuCanvas2.Children.Add(material(codigoLote));

                XmlNodeList listaPeca = node1.SelectNodes("peca");


                foreach (XmlNode node in listaPeca)
                {
                    XmlElement elementoPeca = (XmlElement)node;

                    string codigoP = elementoPeca.GetElementsByTagName("codigoP")[0].InnerText;
                    string descricaoP = elementoPeca.GetElementsByTagName("descricaoP")[0].InnerText;
                    string comprimentoP = elementoPeca.GetElementsByTagName("comprimentoP")[0].InnerText;
                    string larguraP = elementoPeca.GetElementsByTagName("larguraP")[0].InnerText;
                    string rotacionaP = elementoPeca.GetElementsByTagName("rotacionaP")[0].InnerText;
                    string quantidadeP = elementoPeca.GetElementsByTagName("quantidadeP")[0].InnerText;
                    string coordenadaXP = elementoPeca.GetElementsByTagName("coordenadaXP")[0].InnerText;
                    string coordenadaYP = elementoPeca.GetElementsByTagName("coordenadaYP")[0].InnerText;

                    string primeirosCortesP = elementoPeca.GetElementsByTagName("coordenadaPrimeirosCortes")[0].InnerText;


                    double lar = Convert.ToDouble(larguraP);
                    double comp = Convert.ToDouble(comprimentoP);
                    double coorX = Convert.ToDouble(coordenadaXP);
                    double coorY = Convert.ToDouble(coordenadaYP);

                    //Adiciona retangulo da peca.
                    meuCanvas2.Children.Add(cria(lar, comp, coorY, coorX, codigoP, descricaoP, rotacionaP, quantidadeP));

                    
                    //Adiciona codigo em cima da peca.
                    TextBlock textoCodigo = new TextBlock();
                    textoCodigo.Text = codigoP;

                    Canvas.SetLeft(textoCodigo, ((coorX/3) + 1));
                    Canvas.SetTop(textoCodigo, ((coorY/3) + 1));

                    meuCanvas2.Children.Add(textoCodigo);


                    if (mostraComprimentoLarguraBool == true)
                    {

                        //Adiciona comprimento em cima da peca.
                        TextBlock textoComprimento = new TextBlock();
                        textoComprimento.Text = comprimentoP;
                        textoComprimento.FontSize = 10;

                        Canvas.SetLeft(textoComprimento, ((coorX / 3) + 18));
                        Canvas.SetTop(textoComprimento, (coorY / 3 ) + 1);


                        meuCanvas2.Children.Add(textoComprimento);

                        //Adiciona largura em cima da peca.
                        TextBlock textoLargura = new TextBlock();
                        textoLargura.Text = larguraP;
                        textoLargura.FontSize = 10;
                        
                        Canvas.SetLeft(textoLargura, ((coorX / 3) + 18));
                        Canvas.SetTop(textoLargura, (coorY / 3) + 10);


                        meuCanvas2.Children.Add(textoLargura);
                    }
           
                }

                if (mostraRefilosLateriais == true)
                {

                    //Linha refilo lateral esquerda.
                    Line linhaRefLatEsq = new Line();
                    linhaRefLatEsq.X1 = refilosLaterais / 3;
                    linhaRefLatEsq.Y1 = Convert.ToDouble(materialExib.Largura) / 3;
                    linhaRefLatEsq.X2 = refilosLaterais / 3;

                    SolidColorBrush corDaLinhaRefilo = new SolidColorBrush();
                    corDaLinhaRefilo.Color = Colors.White;

                    linhaRefLatEsq.StrokeThickness = 2;
                    linhaRefLatEsq.Stroke = corDaLinhaRefilo;

                    meuCanvas2.Children.Add(linhaRefLatEsq);


                    //Linha refilo lateral direita.
                    Line linhaRefLatDit = new Line();
                    linhaRefLatDit.X1 = (Convert.ToDouble(materialExib.Comprimento) - refilosLaterais) / 3;
                    linhaRefLatDit.Y1 = Convert.ToDouble(materialExib.Largura) / 3;
                    linhaRefLatDit.X2 = (Convert.ToDouble(materialExib.Comprimento) - refilosLaterais) / 3;

                    linhaRefLatDit.StrokeThickness = 2;
                    linhaRefLatDit.Stroke = corDaLinhaRefilo;

                    meuCanvas2.Children.Add(linhaRefLatDit);

                    //Linha refilo superior.
                    Line linhaRefSup = new Line();
                    linhaRefSup.X1 = Convert.ToDouble(materialExib.Comprimento) / 3;
                    linhaRefSup.Y1 = refilosLaterais / 3;
                    linhaRefSup.Y2 = refilosLaterais / 3;

                    linhaRefSup.StrokeThickness = 2;
                    linhaRefSup.Stroke = corDaLinhaRefilo;

                    meuCanvas2.Children.Add(linhaRefSup);

                    //Linha refilo inferior.
                    Line linhaRefInf = new Line();
                    linhaRefInf.X1 = Convert.ToDouble(materialExib.Comprimento) / 3;
                    linhaRefInf.Y1 = (Convert.ToDouble(materialExib.Largura) - refilosLaterais) / 3;
                    linhaRefInf.Y2 = (Convert.ToDouble(materialExib.Largura) - refilosLaterais) / 3;

                    linhaRefInf.StrokeThickness = 2;
                    linhaRefInf.Stroke = corDaLinhaRefilo;

                    meuCanvas2.Children.Add(linhaRefInf);

                }

                //Adiciona dimensões da chapa - largura.

                TextBlock textoLarguraMat = new TextBlock();
                textoLarguraMat.Text = materialExib.Largura + " mm";

                textoLarguraMat.LayoutTransform = new RotateTransform(-90);

                Canvas.SetLeft(textoLarguraMat, ( Convert.ToDouble(materialExib.Comprimento) /3) + 1);
                Canvas.SetTop(textoLarguraMat, ((Convert.ToDouble(materialExib.Largura) / 3) / 2.2) );

                meuCanvas2.Children.Add(textoLarguraMat);

                //Adiciona dimensões da chapa - comprimento.

                TextBlock textoComprimentoMat = new TextBlock();
                textoComprimentoMat.Text = materialExib.Comprimento + " mm";

                Canvas.SetLeft(textoComprimentoMat, ((Convert.ToDouble(materialExib.Comprimento) / 3) / 2.2));
                Canvas.SetTop(textoComprimentoMat, ((Convert.ToDouble(materialExib.Largura) / 3) - 2));

                meuCanvas2.Children.Add(textoComprimentoMat);



                //Adiciona as linhas que representam o primeiro corte.
                if (mostraPrimeirosCortesDaChapa == true)
                {

                    foreach (XmlNode node in listaPeca)
                    {
                        XmlElement elementoPeca = (XmlElement)node;


                        string primeirosCortesP = elementoPeca.GetElementsByTagName("coordenadaPrimeirosCortes")[0].InnerText;
                        string primeirosCortesP2 = elementoPeca.GetElementsByTagName("coordenadaPrimeirosCortes2")[0].InnerText;


                        if (primeirosCortesP != "0")
                        {

                            if (indicaCortesIniciaisHorizontais)
                            {

                                //Adiciona cortes antes de peça.
                                Line linhasDoPrimeiroCorte = new Line();
                                linhasDoPrimeiroCorte.X1 = Convert.ToDouble(materialExib.Comprimento) / 3;
                                linhasDoPrimeiroCorte.Y1 = Convert.ToDouble(primeirosCortesP) / 3;
                                linhasDoPrimeiroCorte.Y2 = Convert.ToDouble(primeirosCortesP) / 3;


                                SolidColorBrush corDaLinhaPrimeiroCorte = new SolidColorBrush();
                                corDaLinhaPrimeiroCorte.Color = Colors.Orange;

                                linhasDoPrimeiroCorte.StrokeThickness = espessuraSerra;
                                linhasDoPrimeiroCorte.Stroke = corDaLinhaPrimeiroCorte;

                                meuCanvas2.Children.Add(linhasDoPrimeiroCorte);

                                //Adiciona cortes no final da ultima tira.
                                Line linhasDoPrimeiroCorte2 = new Line();
                                linhasDoPrimeiroCorte2.X1 = Convert.ToDouble(materialExib.Comprimento) / 3;
                                linhasDoPrimeiroCorte2.Y1 = Convert.ToDouble(primeirosCortesP2) / 3;
                                linhasDoPrimeiroCorte2.Y2 = Convert.ToDouble(primeirosCortesP2) / 3;

                                linhasDoPrimeiroCorte2.StrokeThickness = espessuraSerra;
                                linhasDoPrimeiroCorte2.Stroke = corDaLinhaPrimeiroCorte;

                                meuCanvas2.Children.Add(linhasDoPrimeiroCorte2);

                            }
                            else if (!indicaCortesIniciaisHorizontais)
                            {
                                //Adiciona cortes antes de peça.
                                Line linhasDoPrimeiroCorte = new Line();
                                linhasDoPrimeiroCorte.X1 = Convert.ToDouble(primeirosCortesP) / 3;
                                linhasDoPrimeiroCorte.X2 = Convert.ToDouble(primeirosCortesP) / 3;
                                linhasDoPrimeiroCorte.Y1 = Convert.ToDouble(materialExib.Largura) / 3;

                                SolidColorBrush corDaLinhaPrimeiroCorte = new SolidColorBrush();
                                corDaLinhaPrimeiroCorte.Color = Colors.Orange;

                                linhasDoPrimeiroCorte.StrokeThickness = espessuraSerra;
                                linhasDoPrimeiroCorte.Stroke = corDaLinhaPrimeiroCorte;

                                meuCanvas2.Children.Add(linhasDoPrimeiroCorte);

                                //Adiciona cortes no final da ultima tira.
                                Line linhasDoPrimeiroCorte2 = new Line();
                                linhasDoPrimeiroCorte2.X1 = Convert.ToDouble(primeirosCortesP2) / 3;
                                linhasDoPrimeiroCorte2.X2 = Convert.ToDouble(primeirosCortesP2) / 3;
                                linhasDoPrimeiroCorte2.Y1 = Convert.ToDouble(materialExib.Largura) / 3;

                                linhasDoPrimeiroCorte2.StrokeThickness = espessuraSerra;
                                linhasDoPrimeiroCorte2.Stroke = corDaLinhaPrimeiroCorte;

                                meuCanvas2.Children.Add(linhasDoPrimeiroCorte2);
                            }
                        }


                    }
                }


                meuSPanel2.Children.Add(meuCanvas2);
                TesteTab.Header = "Chapa " + contadorChapas.ToString();
                TesteTab.Content = meuSPanel2;
                tabcontrolView.Items.Add(TesteTab);

            }
        }


    }
}