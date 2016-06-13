using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Xml;
using System.Data;
using System.Xml.Linq;

namespace OtimizaCut_v2
{
    class Calculo
    {
        public Calculo()
        {
          
        }


        public void Calcula(string codigoLote)
        {

            string codigoMatString;
            string descricaoMatString;
            string comprimentoMatString;
            string larguraMatString;
            string espessuraMatString;
            string corMatString;
            string tipoMatString;
            string rotacionaMatString;

            

            //Rotina para fazer a leitura dos parametros de configuração (espessura da serra, refilos, etc).

            string caminhoConf = "C:\\dados\\configuracoes\\configuracoes.xml";

            XmlTextReader arqConf = new XmlTextReader(caminhoConf);

            string espessuraSerraString = "0.0";
            string refilosLateraisString = "0.0";
            bool corteHorizontal = false;

            while (arqConf.Read())
            {
               if (arqConf.NodeType == XmlNodeType.Element)
                {
                    if (arqConf.Name == "serra")
                        espessuraSerraString= arqConf.ReadString();

                    if (arqConf.Name == "refilos")
                        refilosLateraisString = arqConf.ReadString();

                    if (arqConf.Name == "orientacaoCortesIniciaisDaChapa")
                    {
                        if (arqConf.ReadString().Equals("Horizontais"))
                            corteHorizontal = true;
                        if (arqConf.ReadString().Equals("Verticais"))
                            corteHorizontal = false;
                    }

                }
            }

            arqConf.Close();

            double espessuraSerra = Convert.ToDouble(espessuraSerraString);
            double refilosLaterais = Convert.ToDouble(refilosLateraisString);

            //Parte da rotina que lê o arquivo .xml do lote - informaçoes sobre o tipo de material utilizado.

            LVData planoDeCorte = new LVData();
            string descricaoLote = ""; //descricaoLote

            string caminho = "C:\\dados\\lotes\\" + codigoLote + ".xml";


            XmlTextReader tr = new XmlTextReader(caminho);


            while (tr.Read())
            {
                if (tr.NodeType == XmlNodeType.Element)
                {
                    if (tr.Name == "codigo")
                        planoDeCorte.Codigo = tr.ReadString();

                    if (tr.Name == "descricao")
                        planoDeCorte.Descricao = tr.ReadString();

                    if (tr.Name == "comprimento")
                        planoDeCorte.Comprimento = tr.ReadString();

                    if (tr.Name == "largura")
                        planoDeCorte.Largura = tr.ReadString();

                    if (tr.Name == "espessura")
                        planoDeCorte.Espessura = tr.ReadString();

                    if (tr.Name == "cor")
                        planoDeCorte.Cor = tr.ReadString();

                    if (tr.Name == "tipo")
                        planoDeCorte.Tipo = tr.ReadString();

                    if (tr.Name == "rotaciona")
                    {
                        planoDeCorte.Rotaciona = tr.ReadString();
                        if (planoDeCorte.Rotaciona.Equals("Sim"))
                            rotacionaMatString = "0";
                        if (planoDeCorte.Rotaciona.Equals("Não"))
                            rotacionaMatString = "1";
                    }

                    if (tr.Name == "descricaoLote")
                        descricaoLote = tr.ReadString();

                }

                codigoMatString = planoDeCorte.Codigo;
                descricaoMatString = planoDeCorte.Descricao;
                comprimentoMatString = planoDeCorte.Comprimento;
                larguraMatString = planoDeCorte.Largura;
                espessuraMatString = planoDeCorte.Espessura;
                corMatString = planoDeCorte.Cor;
                tipoMatString = planoDeCorte.Tipo;
                rotacionaMatString = planoDeCorte.Rotaciona;

                //Rotina que lê as peças do arquivo e lote e transforma cada peça num objeto da classe PecaParaPlano.


                string caminhoPlano5 = "c:\\dados\\lotes\\" + codigoLote + ".xml";
                XmlDocument doc5 = new XmlDocument();
                doc5.Load(caminhoPlano5);

                XmlNodeList listaPeca = doc5.GetElementsByTagName("peca");

                var listaDePecaParaOtimizar = new ObservableCollection<PecaParaPlano>()
                {
                };

                foreach (XmlNode node in listaPeca)
                {
                    XmlElement elementoPeca = (XmlElement)node;

                    string quantidadeP = elementoPeca.GetElementsByTagName("quantidadeP")[0].InnerText;
                    int quantidadePecas = Convert.ToInt16(quantidadeP);

                    for (int i = quantidadePecas; i > 0; i--)
                    {

                        string codigoP = elementoPeca.GetElementsByTagName("codigoP")[0].InnerText;
                        string descricaoP = elementoPeca.GetElementsByTagName("descricaoP")[0].InnerText;
                        string comprimentoP = elementoPeca.GetElementsByTagName("comprimentoP")[0].InnerText;
                        string larguraP = elementoPeca.GetElementsByTagName("larguraP")[0].InnerText;
                        string rotacionaP = elementoPeca.GetElementsByTagName("rotacionaP")[0].InnerText;

                        PecaParaPlano pecaNoPlano = new PecaParaPlano();
                        pecaNoPlano.Codigo = codigoP;
                        pecaNoPlano.Descricao = descricaoP;
                        pecaNoPlano.Comprimento = comprimentoP;
                        pecaNoPlano.Largura = larguraP;


                        if (rotacionaP.Equals("True"))
                            pecaNoPlano.Rotacionar = true;
                        else
                            pecaNoPlano.Rotacionar = false;

                        pecaNoPlano.Quantidade = quantidadeP;

                        listaDePecaParaOtimizar.Add(pecaNoPlano);
                    }

                }

                //Rotina que cria o arquivo estatisticas.xml, utilizado para geração do relatório de cortes.

                try
                {
                    string caminhoLoteEstatisticas = "c:\\dados\\planos_otimizados\\estatisticas.xml";

                    XmlTextWriter writer1 = new XmlTextWriter(caminhoLoteEstatisticas, null);
                    writer1.Formatting = Formatting.Indented;

                    writer1.WriteStartDocument();
                    writer1.WriteComment("Arquivo gerado pelo Software OtimizaCut, desenvolvido por Aislan Nadrowski - Ano: 2013 [INFORMACOES PARA RELATÓRIO]");

                    writer1.WriteStartElement("estatisticas");

                    //Escreve as caracteristicas do material dentro do arquivo .xml.
                    writer1.WriteStartElement("material");

                    writer1.WriteElementString("codigo", codigoMatString);
                    writer1.WriteElementString("descricao", descricaoMatString);
                    writer1.WriteElementString("comprimento", comprimentoMatString);
                    writer1.WriteElementString("largura", larguraMatString);
                    writer1.WriteElementString("espessura", espessuraMatString);
                    writer1.WriteElementString("cor", corMatString);
                    writer1.WriteElementString("tipo", tipoMatString);
                    writer1.WriteElementString("rotaciona", rotacionaMatString);
                    writer1.WriteEndElement();

                    writer1.WriteStartElement("lote");
                    writer1.WriteElementString("codigoLote", codigoLote);
                    writer1.WriteElementString("descricaoLote", descricaoLote);
                    writer1.WriteEndElement();

                    writer1.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                //Calcula a area de cada peça.
                int quantidadePecaNaLista = listaDePecaParaOtimizar.Count;

                double compAux = 0.0;
                double largAux = 0.0;

                //Variaveis uteis no posicionamento de peças:
                bool todasPecasInseridas = true;


                for (int i = 0; i < quantidadePecaNaLista; i++)
                {
                    compAux = Convert.ToDouble(listaDePecaParaOtimizar[i].Comprimento);
                    largAux = Convert.ToDouble(listaDePecaParaOtimizar[i].Largura);

                    listaDePecaParaOtimizar[i].Area = compAux * largAux;
                    listaDePecaParaOtimizar[i].Inserida = false;
                }

                //Ordena a lista de peças utilizando a area. Ordenado de maior para menor:
                ObservableCollection<PecaParaPlano> listaDePecasSort = new ObservableCollection<PecaParaPlano>(
                    listaDePecaParaOtimizar.OrderByDescending(pecaOrdenada => pecaOrdenada.Area)
                    );




                //Escreve no arquivo as coordenadas de geração dos planos de corte.
                if (corteHorizontal == true)
                {

                    //Faz a inversão das coordenadas se a largura for maior que o comprimento e se a peça permitir rotacioção:
                    string auxComprimentoRot = "";
                    string auxLarguraRot = "";


                    for (int t = 0; t < quantidadePecaNaLista; t++)
                    {
                        if (listaDePecasSort[t].Rotacionar)
                        {
                            if ((Convert.ToDouble(listaDePecasSort[t].Largura) > Convert.ToDouble(listaDePecasSort[t].Comprimento)))
                            {
                                auxComprimentoRot = listaDePecasSort[t].Comprimento;
                                auxLarguraRot = listaDePecasSort[t].Largura;

                                listaDePecasSort[t].Comprimento = auxLarguraRot;
                                listaDePecasSort[t].Largura = auxComprimentoRot;

                                listaDePecasSort[t].Rotacionar = true;
                            }

                        }
                    }

                    try
                    {
                        string caminhoLote = "c:\\dados\\planos_otimizados\\" + codigoLote + ".xml";

                        XmlTextWriter writer = new XmlTextWriter(caminhoLote, null);
                        writer.Formatting = Formatting.Indented;

                        writer.WriteStartDocument();
                        writer.WriteComment("Arquivo gerado pelo Software OtimizaCut, desenvolvido por Aislan Nadrowski - Ano: 2013 [PLANO OTIMIZADO]");

                        writer.WriteStartElement("otimizado");

                        //Escreve as caracteristicas do material dentro do arquivo .xml.
                        writer.WriteStartElement("material");

                        writer.WriteElementString("codigo", codigoMatString);
                        writer.WriteElementString("descricao", descricaoMatString);
                        writer.WriteElementString("comprimento", comprimentoMatString);
                        writer.WriteElementString("largura", larguraMatString);
                        writer.WriteElementString("espessura", espessuraMatString);
                        writer.WriteElementString("cor", corMatString);
                        writer.WriteElementString("tipo", tipoMatString);
                        writer.WriteElementString("rotaciona", rotacionaMatString);
                        writer.WriteEndElement();

                        writer.WriteStartElement("planos");

                        //Escreve as peças dentro do arquivo .xml com as coordenadas X e Y para posicionamento dentro do plano de corte.

                        //Ponto zero da chapa:
                        double pontoZero = refilosLaterais;
                        double coordenadaX = pontoZero;
                        double coordenadaY = pontoZero;

                        //Dimensões da chapa:
                        double comprimentoMatD = Convert.ToDouble(comprimentoMatString);
                        double larguraMatD = Convert.ToDouble(larguraMatString);
                        double areaUtilX = comprimentoMatD - (refilosLaterais * 2);
                        double areaUtilY = larguraMatD - (refilosLaterais * 2);

                        //Dimensoes do retangulo util:

                        double areaX = areaUtilX;
                        double areaY = areaUtilY;

                        writer.WriteStartElement("plano");

                        //Faz a iteração para inserção das peças dentro do plano de corte:
                        bool primeiraPecaNaTira = true;
                        double comprimentoAnterior = 0;
                        double larguraAnterior = 0;
                        bool cabePeca = false;
                        double larguraPrimeiraPecaNaTira = 0;
                        bool jogaPecaNoInicioEmYDaTira = false;
                        bool informaVoltouInicioYdaTira = false;
                        bool primeiraPecaNaTiraEmY = true;
                        double coordenadaYPrimeiraPecaDaTira = 0;
                        double comprimentoPrimeiraPecaNaTiraEmY = 0; // antiga variavel "teste5".
                        double sobraTiraY = areaY;
                        bool chegouNoFinalDaTira = false;
                        double medidaFinalDaTira = 0;

                        while (todasPecasInseridas & (comprimentoMatD > 0) & (areaX > 0) & (areaY > 0))
                        {

                            //Percorre a lista de peças ordenadas:
                            for (int i = 0; i < quantidadePecaNaLista; i++)
                            {
                                if (!listaDePecasSort[i].Rotacionar)
                                {
                               //     MessageBox.Show(" ########## DEBUG GRAFICO ########## \n\n " + "Comprimento: " + listaDePecasSort[i].Comprimento + " <= areaX + 2x serra: " + (areaX + espessuraSerra + espessuraSerra).ToString() + " - areaX: " + areaX.ToString() + "\n\n Largura: " + listaDePecasSort[i].Largura + " <= areaY: " + areaY.ToString() + "\n\n larg: " + listaDePecasSort[i].Largura + " <= sobraTiraY: " + sobraTiraY.ToString() + "\n\n i = " + i.ToString() + " \n\n coord X: " + coordenadaX.ToString() + " - coord Y: " + coordenadaY.ToString() + "\n\n\n Peca inserida: " + listaDePecasSort[i].Rotacionar.ToString() + " \n\n LarguradaTiraemY: " + larguraPrimeiraPecaNaTira.ToString() + " \n\n coordenadaYPrimeiraPecaDaTira: " + coordenadaYPrimeiraPecaDaTira.ToString());
                                }


                                if ((Convert.ToDouble(listaDePecasSort[i].Comprimento) <= (areaX + espessuraSerra + espessuraSerra)) & (Convert.ToDouble(listaDePecasSort[i].Largura) <= areaY) & (Convert.ToDouble(listaDePecasSort[i].Largura) <= sobraTiraY) & (listaDePecasSort[i].Inserida == false))
                                {

                                 //   MessageBox.Show("comp: " + listaDePecasSort[i].Comprimento + " <= areaX + 2x serra: " + (areaX + espessuraSerra + espessuraSerra).ToString() + "\n\n larg: " + listaDePecasSort[i].Largura + " <= areaY: " + areaY.ToString() + "\n\n larg: " + listaDePecasSort[i].Largura + " <= sobraTiraY: " + sobraTiraY.ToString());
                                  //  MessageBox.Show("Entrou no if. i = " + i.ToString());

                                    //  Se todas as peças não couberam na chapa, cria uma nova chapa:
                                    if ( Convert.ToDouble(listaDePecasSort[i].Largura) >  (areaUtilY - coordenadaY + espessuraSerra)) 
                                    {
                                        //Fecha a chapa atual.
                                        writer.WriteEndElement();

                                        coordenadaX = pontoZero;
                                        coordenadaY = pontoZero;

                                        areaX = areaUtilX;
                                        areaY = areaUtilY;

                                        //Faz a iteração para inserção das peças dentro do plano de corte:
                                        primeiraPecaNaTira = true;
                                        comprimentoAnterior = 0;
                                        larguraAnterior = 0;
                                        cabePeca = false;
                                        larguraPrimeiraPecaNaTira = 0;
                                        jogaPecaNoInicioEmYDaTira = false;
                                        informaVoltouInicioYdaTira = false;
                                        primeiraPecaNaTiraEmY = true;
                                        coordenadaYPrimeiraPecaDaTira = 0;
                                        comprimentoPrimeiraPecaNaTiraEmY = 0; // antiga variavel "teste5".
                                        sobraTiraY = areaY;
                                        chegouNoFinalDaTira = false;
                                        medidaFinalDaTira = 0;

                                        //Abre nova chapa:
                                        writer.WriteStartElement("plano");
                                    }


                                    if ((coordenadaX + Convert.ToDouble(listaDePecasSort[i].Comprimento) <=  (areaX + espessuraSerra + espessuraSerra)))
                                    {

                                        //Escreve no arquivo XML o conteudo da peça:
                                        writer.WriteStartElement("peca");
                                        writer.WriteElementString("codigoP", listaDePecasSort[i].Codigo);
                                        writer.WriteElementString("descricaoP", listaDePecasSort[i].Descricao);
                                        writer.WriteElementString("comprimentoP", listaDePecasSort[i].Comprimento);
                                        writer.WriteElementString("larguraP", listaDePecasSort[i].Largura);

                                        if (listaDePecasSort[i].Rotacionar.Equals(true))
                                            writer.WriteElementString("rotacionaP", "Sim");
                                        else
                                            writer.WriteElementString("rotacionaP", "Não");
                                        
                                        writer.WriteElementString("quantidadeP", listaDePecasSort[i].Quantidade);
                                        writer.WriteElementString("coordenadaXP", coordenadaX.ToString());
                                        writer.WriteElementString("coordenadaYP", coordenadaY.ToString());



                                        if (primeiraPecaNaTira)
                                        {
                                            larguraPrimeiraPecaNaTira = Convert.ToDouble(listaDePecasSort[i].Largura);
                                            sobraTiraY = larguraPrimeiraPecaNaTira;
                                            coordenadaYPrimeiraPecaDaTira = coordenadaY;

                                            writer.WriteElementString("coordenadaPrimeirosCortes", coordenadaX.ToString());
                                            medidaFinalDaTira = Convert.ToDouble(listaDePecasSort[i].Largura);
                                            writer.WriteElementString("coordenadaPrimeirosCortes2", (coordenadaY + medidaFinalDaTira).ToString());
                                        }
                                        else
                                        {
                                            writer.WriteElementString("coordenadaPrimeirosCortes", "0");
                                            writer.WriteElementString("coordenadaPrimeirosCortes2", "0");
                                        }


                                        writer.WriteEndElement();

                                        listaDePecasSort[i].Inserida = true;
                                        primeiraPecaNaTira = false;

                                        comprimentoAnterior = Convert.ToDouble(listaDePecasSort[i].Comprimento);
                                        larguraAnterior = Convert.ToDouble(listaDePecasSort[i].Largura);

                                        sobraTiraY = sobraTiraY - Convert.ToDouble(listaDePecasSort[i].Largura) - espessuraSerra;

                                      //  MessageBox.Show("sobraTiraY: " + sobraTiraY.ToString() + " - larguraPrimeiraPecaNaTira: " + larguraPrimeiraPecaNaTira.ToString());

                                        if (primeiraPecaNaTiraEmY.Equals(true))
                                        {
                                            comprimentoPrimeiraPecaNaTiraEmY = Convert.ToDouble(listaDePecasSort[i].Comprimento);
                                            primeiraPecaNaTiraEmY = false;
                                          
                                        }

                                        if (sobraTiraY == 0)
                                        {
                                            sobraTiraY = larguraPrimeiraPecaNaTira;
                                            primeiraPecaNaTiraEmY = true;
                                            
                                            i = 0;
                                        }
                                        else if (sobraTiraY <= Convert.ToDouble(listaDePecasSort[i].Largura))
                                        {
                                            sobraTiraY = larguraPrimeiraPecaNaTira;
                                            jogaPecaNoInicioEmYDaTira = true;
                                            primeiraPecaNaTiraEmY = true;
                                            chegouNoFinalDaTira = true;

                                        }

                                        if (primeiraPecaNaTira.Equals(false))
                                        {


                                            if (jogaPecaNoInicioEmYDaTira.Equals(true))
                                            {
                                             //   i = 0 - se deixar essa linha descomentada da problema na sobraTiraY!;
                                                coordenadaX = coordenadaX + comprimentoPrimeiraPecaNaTiraEmY + espessuraSerra;
                                                coordenadaY = coordenadaYPrimeiraPecaDaTira;
                                                jogaPecaNoInicioEmYDaTira = false;
                                                informaVoltouInicioYdaTira = true;
                                                cabePeca = false;
                                                
                                            }
                                            else
                                            {
                                                coordenadaX = coordenadaX + comprimentoAnterior + espessuraSerra;

                                            }


                                            if ((Convert.ToDouble(listaDePecasSort[i].Largura) < areaY) & cabePeca.Equals(true) & informaVoltouInicioYdaTira.Equals(false))
                                            {
                                                coordenadaY = coordenadaY + espessuraSerra + larguraAnterior;

                                                coordenadaX = coordenadaX - comprimentoAnterior - espessuraSerra;
                                                cabePeca = false;
                                               
                                            }
                                            
                                            informaVoltouInicioYdaTira = false;


                                        }


                                        if (areaY >= Convert.ToDouble(listaDePecasSort[i].Largura))
                                        {
                                            cabePeca = true;
                                        }

                                        areaY = larguraPrimeiraPecaNaTira;



                                    }
                                    else
                                    {
                                        //Se acabou espaço na tira, inicia a rotina abaixo para iniciar um nova tira:
                                        coordenadaX = pontoZero;
                                        coordenadaY = coordenadaYPrimeiraPecaDaTira + larguraPrimeiraPecaNaTira + espessuraSerra;
                                        areaX = areaUtilX;
                                        areaY = Convert.ToDouble(listaDePecasSort[i].Largura);
                                       primeiraPecaNaTira = true;
                                       i = 0;

                                    }
                                }
                                else
                                {
                                    //rotina serve como alternativa para quando a peça que passou e nao foi inserida no primeiro momento
                                    //criou-se um alternativa para criar uma nova tira.


                                    if (chegouNoFinalDaTira)
                                    {
                                        areaX = areaUtilX;
                                        areaY = Convert.ToDouble(listaDePecasSort[i].Largura);
                                        sobraTiraY = Convert.ToDouble(listaDePecasSort[i].Largura);
                                        //     MessageBox.Show("Entrou no if chegouNoFinalDaTira == true");
                                    }


                                    chegouNoFinalDaTira = false;

                                }


                                }
             
                                //Confere se todas as peças foram inseridas no plano de corte:
                                int cont = 0;
                                for (int j = 0; j < quantidadePecaNaLista; j++)
                                {

                                if (listaDePecasSort[j].Inserida)
                                {
                                    cont = cont + 1;
                                }
                                if (cont.Equals(quantidadePecaNaLista))
                                {
                                    todasPecasInseridas = false;
                                }

                            }
                            cont = 0;
                        
                            //Para rodar a logica sem fazer o looping na lista de peças, basta descomentar a linha abaixo:
                           // todasPecasInseridas = false;
                        }
                        
                        writer.WriteEndElement();
                        writer.WriteEndElement();

                        writer.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }


                //###############################################################################################################

                //Inicio do cálculo em vertical:

                if (corteHorizontal == false)
                {
                    //Faz a inversão das coordenadas se a largura for maior que o comprimento e se a peça permitir rotacioção:
                    string auxComprimentoRot = "";
                    string auxLarguraRot = "";


                    for (int t = 0; t < quantidadePecaNaLista; t++)
                    {
                        if (listaDePecasSort[t].Rotacionar)
                        {
                            if ((Convert.ToDouble(listaDePecasSort[t].Comprimento) > Convert.ToDouble(listaDePecasSort[t].Largura)))
                            {
                                auxComprimentoRot = listaDePecasSort[t].Comprimento;
                                auxLarguraRot = listaDePecasSort[t].Largura;

                                listaDePecasSort[t].Comprimento = auxLarguraRot;
                                listaDePecasSort[t].Largura = auxComprimentoRot;

                                listaDePecasSort[t].Rotacionar = true;
                            }

                        }
                    }

                    try
                    {
                        string caminhoLote = "c:\\dados\\planos_otimizados\\" + codigoLote + ".xml";

                        XmlTextWriter writer = new XmlTextWriter(caminhoLote, null);
                        writer.Formatting = Formatting.Indented;

                        writer.WriteStartDocument();
                        writer.WriteComment("Arquivo gerado pelo Software OtimizaCut, desenvolvido por Aislan Nadrowski - Ano: 2013 [PLANO OTIMIZADO]");

                        writer.WriteStartElement("otimizado");

                        //Escreve as caracteristicas do material dentro do arquivo .xml.
                        writer.WriteStartElement("material");

                        writer.WriteElementString("codigo", codigoMatString);
                        writer.WriteElementString("descricao", descricaoMatString);
                        writer.WriteElementString("comprimento", comprimentoMatString);
                        writer.WriteElementString("largura", larguraMatString);
                        writer.WriteElementString("espessura", espessuraMatString);
                        writer.WriteElementString("cor", corMatString);
                        writer.WriteElementString("tipo", tipoMatString);
                        writer.WriteElementString("rotaciona", rotacionaMatString);
                        writer.WriteEndElement();

                        writer.WriteStartElement("planos");

                        //Escreve as peças dentro do arquivo .xml com as coordenadas X e Y para posicionamento dentro do plano de corte.

                        //Ponto zero da chapa:
                        double pontoZero = refilosLaterais;
                        double coordenadaX = pontoZero;
                        double coordenadaY = pontoZero;

                        //Dimensões da chapa:
                        double comprimentoMatD = Convert.ToDouble(comprimentoMatString);
                        double larguraMatD = Convert.ToDouble(larguraMatString);
                        double areaUtilX = comprimentoMatD - (refilosLaterais * 2);
                        double areaUtilY = larguraMatD - (refilosLaterais * 2);

                        //Dimensoes do retangulo util:

                        double areaX = areaUtilX;
                        double areaY = areaUtilY;

                        writer.WriteStartElement("plano");

                        //Faz a iteração para inserção das peças dentro do plano de corte:
                        bool primeiraPecaNaTira = true;
                        double comprimentoAnterior = 0;
                        double larguraAnterior = 0;
                        bool cabePeca = false;
                        double larguraPrimeiraPecaNaTira = 0;
                        bool jogaPecaNoInicioEmXDaTira = false;
                        bool informaVoltouInicioXdaTira = false;
                        bool primeiraPecaNaTiraEmX = true;
                        double coordenadaXPrimeiraPecaDaTira = 0;
                        double comprimentoPrimeiraPecaNaTiraEmX = 0; // antiga variavel "teste5".
                        double sobraTiraX = areaX;
                        bool chegouNoFinalDaTira = false;
                        double medidaFinalDaTira = 0;

                        while (todasPecasInseridas & (comprimentoMatD > 0) & (areaX > 0) & (areaY > 0))
                        {

                            //Percorre a lista de peças ordenadas:
                            for (int i = 0; i < quantidadePecaNaLista; i++)
                            {


                                if ((Convert.ToDouble(listaDePecasSort[i].Largura) <= (areaY + espessuraSerra + espessuraSerra)) & (Convert.ToDouble(listaDePecasSort[i].Comprimento) <= areaX) & (Convert.ToDouble(listaDePecasSort[i].Comprimento) <= sobraTiraX) & (listaDePecasSort[i].Inserida == false))
                                {

                                    //  Se todas as peças não couberam na chapa, cria uma nova chapa:
                                    if (Convert.ToDouble(listaDePecasSort[i].Comprimento) > (areaUtilX - coordenadaX + espessuraSerra))
                                    {
                                        //Fecha a chapa atual.
                                        writer.WriteEndElement();

                                        coordenadaX = pontoZero;
                                        coordenadaY = pontoZero;

                                        areaX = areaUtilX;
                                        areaY = areaUtilY;

                                        //Faz a iteração para inserção das peças dentro do plano de corte:
                                        primeiraPecaNaTira = true;
                                        comprimentoAnterior = 0;
                                        larguraAnterior = 0;
                                        cabePeca = false;
                                        larguraPrimeiraPecaNaTira = 0;
                                        jogaPecaNoInicioEmXDaTira = false;
                                        informaVoltouInicioXdaTira = false;
                                        primeiraPecaNaTiraEmX = true;
                                        coordenadaXPrimeiraPecaDaTira = 0;
                                        comprimentoPrimeiraPecaNaTiraEmX = 0; // antiga variavel "teste5".
                                        sobraTiraX = areaX;
                                        chegouNoFinalDaTira = false;
                                        medidaFinalDaTira = 0;

                                        //Abre nova chapa:
                                        writer.WriteStartElement("plano");
                                    }


                                    if ((coordenadaY + Convert.ToDouble(listaDePecasSort[i].Largura) <= (areaY + espessuraSerra + espessuraSerra)))
                                    {

                                        //Escreve no arquivo XML o conteudo da peça:
                                        writer.WriteStartElement("peca");
                                        writer.WriteElementString("codigoP", listaDePecasSort[i].Codigo);
                                        writer.WriteElementString("descricaoP", listaDePecasSort[i].Descricao);
                                        writer.WriteElementString("comprimentoP", listaDePecasSort[i].Comprimento);
                                        writer.WriteElementString("larguraP", listaDePecasSort[i].Largura);

                                        if (listaDePecasSort[i].Rotacionar.Equals(true))
                                            writer.WriteElementString("rotacionaP", "Sim");
                                        else
                                            writer.WriteElementString("rotacionaP", "Não");

                                        writer.WriteElementString("quantidadeP", listaDePecasSort[i].Quantidade);
                                        writer.WriteElementString("coordenadaXP", coordenadaX.ToString());
                                        writer.WriteElementString("coordenadaYP", coordenadaY.ToString());



                                        if (primeiraPecaNaTira)
                                        {
                                            larguraPrimeiraPecaNaTira = Convert.ToDouble(listaDePecasSort[i].Comprimento);
                                            sobraTiraX = larguraPrimeiraPecaNaTira;
                                            coordenadaXPrimeiraPecaDaTira = coordenadaX;

                                            writer.WriteElementString("coordenadaPrimeirosCortes", coordenadaY.ToString());
                                            medidaFinalDaTira = Convert.ToDouble(listaDePecasSort[i].Comprimento);
                                            writer.WriteElementString("coordenadaPrimeirosCortes2", (coordenadaX + medidaFinalDaTira).ToString());
                                        }
                                        else
                                        {
                                            writer.WriteElementString("coordenadaPrimeirosCortes", "0");
                                            writer.WriteElementString("coordenadaPrimeirosCortes2", "0");
                                        }


                                        writer.WriteEndElement();

                                        listaDePecasSort[i].Inserida = true;
                                        primeiraPecaNaTira = false;

                                        comprimentoAnterior = Convert.ToDouble(listaDePecasSort[i].Comprimento);
                                        larguraAnterior = Convert.ToDouble(listaDePecasSort[i].Largura);

                                        sobraTiraX = sobraTiraX - Convert.ToDouble(listaDePecasSort[i].Comprimento) - espessuraSerra;

                                        //  MessageBox.Show("sobraTiraY: " + sobraTiraY.ToString() + " - larguraPrimeiraPecaNaTira: " + larguraPrimeiraPecaNaTira.ToString());

                                        if (primeiraPecaNaTiraEmX.Equals(true))
                                        {
                                            comprimentoPrimeiraPecaNaTiraEmX = Convert.ToDouble(listaDePecasSort[i].Largura);
                                            primeiraPecaNaTiraEmX = false;

                                        }

                                        if (sobraTiraX == 0)
                                        {
                                            sobraTiraX = larguraPrimeiraPecaNaTira;
                                            primeiraPecaNaTiraEmX = true;

                                            i = 0;
                                        }
                                        else if (sobraTiraX <= Convert.ToDouble(listaDePecasSort[i].Comprimento))
                                        {
                                            sobraTiraX = larguraPrimeiraPecaNaTira;
                                            jogaPecaNoInicioEmXDaTira = true;
                                            primeiraPecaNaTiraEmX = true;
                                            chegouNoFinalDaTira = true;

                                        }

                                        if (primeiraPecaNaTira.Equals(false))
                                        {


                                            if (jogaPecaNoInicioEmXDaTira.Equals(true))
                                            {
                                                //   i = 0 - se deixar essa linha descomentada da problema na sobraTiraY!;
                                                coordenadaX = coordenadaXPrimeiraPecaDaTira;
                                                coordenadaY = coordenadaY + larguraAnterior + espessuraSerra;
                                                jogaPecaNoInicioEmXDaTira = false;
                                                informaVoltouInicioXdaTira = true;
                                                cabePeca = false;

                                            }
                                            else
                                            {
                                                coordenadaY = coordenadaY + larguraAnterior + espessuraSerra;

                                            }


                                            if ((Convert.ToDouble(listaDePecasSort[i].Comprimento) < areaX) & cabePeca.Equals(true) & informaVoltouInicioXdaTira.Equals(false))
                                            {
                                               // coordenadaY = coordenadaY + espessuraSerra + larguraAnterior;
                                                coordenadaY = coordenadaY - larguraAnterior - espessuraSerra;
                                              //  coordenadaX = coordenadaX - comprimentoAnterior - espessuraSerra;
                                                coordenadaX = coordenadaX + comprimentoAnterior + espessuraSerra;
                                                cabePeca = false;

                                            }

                                            informaVoltouInicioXdaTira = false;


                                        }


                                        if (areaX >= Convert.ToDouble(listaDePecasSort[i].Comprimento))
                                        {
                                            cabePeca = true;
                                        }

                                        areaX = larguraPrimeiraPecaNaTira;



                                    }
                                    else
                                    {
                                        //Se acabou espaço na tira, inicia a rotina abaixo para iniciar um nova tira:
                                            // coordenadaX = pontoZero;
                                            // coordenadaY = coordenadaYPrimeiraPecaDaTira + larguraPrimeiraPecaNaTira + espessuraSerra;
                                        coordenadaX = coordenadaXPrimeiraPecaDaTira + larguraPrimeiraPecaNaTira + espessuraSerra;
                                        coordenadaY = pontoZero;

                                            //areaX = areaUtilX;
                                            // areaY = Convert.ToDouble(listaDePecasSort[i].Largura);
                                        areaX = Convert.ToDouble(listaDePecasSort[i].Comprimento);
                                        areaY = areaUtilY;

                                        primeiraPecaNaTira = true;
                                        i = 0;

                                    }
                                }
                                else
                                {
                                    //rotina serve como alternativa para quando a peça que passou e nao foi inserida no primeiro momento
                                    //criou-se um alternativa para criar uma nova tira.

                                    if (chegouNoFinalDaTira)
                                    {
                                        //areaX = areaUtilX;
                                       // areaY = Convert.ToDouble(listaDePecasSort[i].Largura);
                                       // sobraTiraY = Convert.ToDouble(listaDePecasSort[i].Largura);
                                        areaX = Convert.ToDouble(listaDePecasSort[i].Comprimento);
                                        areaY = areaUtilY;
                                        sobraTiraX = Convert.ToDouble(listaDePecasSort[i].Comprimento);

                                    }

                                    chegouNoFinalDaTira = false;

                                }

                            }

                            //Confere se todas as peças foram inseridas no plano de corte:
                            int cont = 0;
                            for (int j = 0; j < quantidadePecaNaLista; j++)
                            {

                                if (listaDePecasSort[j].Inserida)
                                {
                                    cont = cont + 1;
                                }
                                if (cont.Equals(quantidadePecaNaLista))
                                {
                                    todasPecasInseridas = false;
                                }

                            }
                            cont = 0;

                            //Para rodar a logica sem fazer o looping na lista de peças, basta descomentar a linha abaixo:
                            // todasPecasInseridas = false;
                        }

                        writer.WriteEndElement();
                        writer.WriteEndElement();

                        writer.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }


            }
            tr.Close();
        }

        public void ExibePlanoCalculado(string codigoPlano)
        {
            View planoDeCorteOtimizado = new View(codigoPlano);
            planoDeCorteOtimizado.Show();
        }

    }
}
