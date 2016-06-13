using System;
using System.Collections.Generic;
using System.Collections;
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
using System.Windows.Shapes;
using System.IO;
using System.Xml;
using System.Data;
using System.Xml.Linq;



namespace OtimizaCut_v2
{
    /// <summary>
    /// Interaction logic for cadastrarPecasLote.xaml
    /// </summary>
    public partial class cadastrarPecasLote : Window
    {
        public cadastrarPecasLote(int valor1, string valor2)
        {
            InitializeComponent();
            carregarValores(valor1, valor2);
        }


        private void sair_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void carregarValores(int valor1, string valor2)
        {

            //Carrega valores para identificação do lote
            codigoLote.Text = valor1.ToString();
            descricaoLote.Text = valor2;

  
            //Carrega valores para identicaçao do material


            DirectoryInfo diretorio = new DirectoryInfo(@"c:\dados\materiais\");
            FileInfo[] Arquivos = diretorio.GetFiles("*.xml*");

            foreach (FileInfo fileinfo in Arquivos)
            {

                string nome = fileinfo.FullName;

                XmlTextReader tr = new XmlTextReader(nome);


                string valorMat = "clear";
                string valorMat2 = "clear";
                while (tr.Read())
                {
                    if (tr.NodeType == XmlNodeType.Element)
                    {
                        if (tr.Name == "codigo")
                        {
                            valorMat = tr.ReadString();

                        }

                        if (tr.Name == "descricao")
                        {
                            valorMat2 = tr.ReadString();

                        }
                    }
                }

                string valorMat3 = String.Concat(valorMat, " - ", valorMat2);
                selecionaMat.Items.Add(valorMat3);


                tr.Close();    

            }

            //Carrega valores para identificação do material (se existe algo cadastrado dentro do arquivo de lote)

            string caminhoLote2 = "c:\\dados\\lotes\\" + codigoLote.Text + ".xml";
            XmlDocument lote = new XmlDocument();
            lote.Load(caminhoLote2);

            int quantidadeMatCadastrados = selecionaMat.Items.Count;

            int jaExisteMaterialCadastrado = lote.GetElementsByTagName("codigo").Count;

            if (jaExisteMaterialCadastrado > 0)
            {

                LVData procuraMaterial = new LVData();

                XmlTextReader lerLoteParaProcurarMaterial = new XmlTextReader(caminhoLote2);

                while (lerLoteParaProcurarMaterial.Read())
                {
                    if (lerLoteParaProcurarMaterial.NodeType == XmlNodeType.Element)
                    {
                        if (lerLoteParaProcurarMaterial.Name == "codigo")
                            procuraMaterial.Codigo = lerLoteParaProcurarMaterial.ReadString();
                    }

                }

                //Pega o valor do codigo cadastrado e faz a exibição do item na tela.
            
                int linhaSelecionada = 0;
                string linhaMat = "tst";
                string[] linhaMatSplit;

                for (int i = 0; i < quantidadeMatCadastrados; i++)
                {
                    selecionaMat.SelectedIndex = i;
                    linhaMat = selecionaMat.SelectedValue.ToString();

                    linhaMatSplit = linhaMat.Split();

                    if (linhaMatSplit[0].Equals(procuraMaterial.Codigo))
                        linhaSelecionada = i;

                      selecionaMat.SelectedIndex = linhaSelecionada;                 
                }
            }

            //mostra descrição do material

        }


        private void atualizarDataGridPecasDoLote_Click(object sender, RoutedEventArgs e)
        {

            try
            {

               
                string caminhoLote = "c:\\dados\\lotes\\" + codigoLote.Text + ".xml";
                 XmlDocument doc = new XmlDocument();
                 doc.Load(caminhoLote);

                 XmlNodeList listaPeca = doc.GetElementsByTagName("peca");


                 var pecasParaLote = new ObservableCollection<LVDataLote>()
                 {
                    
                 };

                 foreach (XmlNode node in listaPeca)
                 {
                    

                     XmlElement elementoPeca = (XmlElement)node;

                     string codigoP = elementoPeca.GetElementsByTagName("codigoP")[0].InnerText;
                     string descricaoP = elementoPeca.GetElementsByTagName("descricaoP")[0].InnerText;
                     string comprimentoP = elementoPeca.GetElementsByTagName("comprimentoP")[0].InnerText;
                     string larguraP = elementoPeca.GetElementsByTagName("larguraP")[0].InnerText;
                     string quantidadeP = elementoPeca.GetElementsByTagName("quantidadeP")[0].InnerText;
               //      bool rotacionaP = false;
                     string auxiliarRotacionaP = elementoPeca.GetElementsByTagName("rotacionaP")[0].InnerText;

                     LVDataLote pecaParaAddNaLista = new LVDataLote();
                     pecaParaAddNaLista.Codigo = codigoP;
                     pecaParaAddNaLista.Descricao = descricaoP;
                     pecaParaAddNaLista.Comprimento = comprimentoP;
                     pecaParaAddNaLista.Largura = larguraP;
                     pecaParaAddNaLista.Quantidade = quantidadeP;

                     if (auxiliarRotacionaP.Equals("True"))
                     {
                         pecaParaAddNaLista.Rotacionar = true;
                     }

                     if (auxiliarRotacionaP.Equals("False"))
                     {
                         pecaParaAddNaLista.Rotacionar = false;
                     }

                    pecasParaLote.Add(pecaParaAddNaLista);
                                      
                 }
                 this.datagridLote.ItemsSource = pecasParaLote;

                 
              

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }       
        }

        private void calcular_Click(object sender, RoutedEventArgs e)
        {
            //Input teste:
            //InputLote inplote = new InputLote();
            //inplote.Show();

            Calculo novoCalculo = new Calculo();
            novoCalculo.Calcula(codigoLote.Text);
            novoCalculo.ExibePlanoCalculado(codigoLote.Text);

        }

        private void salvarLote_Click(object sender, RoutedEventArgs e)
        {
            //Rotina para salvar as alterações feitas no arquivo de lote.

            string codigoMaterialSelecionado = selecionaMat.SelectedValue.ToString();
            string[] codigoMaterialSelecionadoSlit = codigoMaterialSelecionado.Split();

            string materialUtilizadoNoPlano = codigoMaterialSelecionadoSlit[0];


            string codigoMatString;
            string descricaoMatString;
            string comprimentoMatString;
            string larguraMatSgring;
            string espessuraMatString;
            string corMatString;
            string tipoMatString;
            string rotacionaMatString;


            LVData material = new LVData();


            string caminho = "C:\\dados\\materiais\\" + materialUtilizadoNoPlano + ".xml";

            XmlTextReader tr = new XmlTextReader(caminho);

            while (tr.Read())
            {
                if (tr.NodeType == XmlNodeType.Element)
                {
                    if (tr.Name == "codigo")
                        material.Codigo = tr.ReadString();

                    if (tr.Name == "descricao")
                        material.Descricao = tr.ReadString();

                    if (tr.Name == "comprimento")
                        material.Comprimento = tr.ReadString();

                    if (tr.Name == "largura")
                        material.Largura = tr.ReadString();

                    if (tr.Name == "espessura")
                        material.Espessura = tr.ReadString();

                    if (tr.Name == "cor")
                        material.Cor = tr.ReadString();

                    if (tr.Name == "tipo")
                        material.Tipo = tr.ReadString();

                    if (tr.Name == "rotaciona")
                    {
                        material.Rotaciona = tr.ReadString();
                        if (material.Rotaciona.Equals("Sim"))
                            rotacionaMatString = "0";
                        if (material.Rotaciona.Equals("Não"))
                            rotacionaMatString = "1";
                    }

                }

                codigoMatString = material.Codigo;
                descricaoMatString = material.Descricao;
                comprimentoMatString = material.Comprimento;
                larguraMatSgring = material.Largura;
                espessuraMatString = material.Espessura;
                corMatString = material.Cor;
                tipoMatString = material.Tipo;
                rotacionaMatString = material.Rotaciona;



                //Escrever no arquivo.

                try
                {

                    string caminhoLote = "c:\\dados\\lotes\\" + codigoLote.Text + ".xml";

                    XmlTextWriter writer = new XmlTextWriter(caminhoLote, null);
                    writer.Formatting = Formatting.Indented;

                    writer.WriteStartDocument();
                    writer.WriteComment("Arquivo gerado pelo Software OtimizaCut, desenvolvido por Aislan Nadrowski - Ano: 2013 [LOTE FILE]");

                    writer.WriteStartElement("lote");
                    writer.WriteStartElement("cabecalho");

                    writer.WriteElementString("codigoLote", codigoLote.Text);
                    writer.WriteElementString("descricaoLote", descricaoLote.Text);

                    writer.WriteEndElement();

                    writer.WriteStartElement("material");
                    writer.WriteElementString("codigo", codigoMatString);
                    writer.WriteElementString("descricao", descricaoMatString);
                    writer.WriteElementString("comprimento", comprimentoMatString);
                    writer.WriteElementString("largura", larguraMatSgring);
                    writer.WriteElementString("espessura", espessuraMatString);
                    writer.WriteElementString("cor", corMatString);
                    writer.WriteElementString("tipo", tipoMatString);
                    writer.WriteElementString("rotaciona", rotacionaMatString);
                    writer.WriteEndElement();

                    writer.WriteStartElement("pecas");

                    int qtdaLinhasNoDatagrid = datagridLote.Items.Count;
                
                    //colecao povoada com conteudo do datagrid.
                    ObservableCollection<LVDataLote> listaTeste1 = new ObservableCollection<LVDataLote>(datagridLote.ItemsSource as ObservableCollection<LVDataLote>);

                    for (int i = 0; i < (qtdaLinhasNoDatagrid-1); i++)
                    {                        
                        writer.WriteStartElement("peca");

                        writer.WriteElementString("codigoP", listaTeste1[i].Codigo.ToString());
                        writer.WriteElementString("descricaoP", listaTeste1[i].Descricao.ToString());
                        writer.WriteElementString("comprimentoP", listaTeste1[i].Comprimento.ToString());
                        writer.WriteElementString("larguraP", listaTeste1[i].Largura.ToString());
                        writer.WriteElementString("quantidadeP", listaTeste1[i].Quantidade.ToString());
                        writer.WriteElementString("rotacionaP", listaTeste1[i].Rotacionar.ToString());

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();

                    writer.Close();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            MessageBox.Show("Lote cadastrado com sucesso!");

        }

        private void excluirLinha_Click(object sender, RoutedEventArgs e)
        {

            var pecasParaLote = new List<LVDataLote>
            {
            };

            LVDataLote novaLinha = new LVDataLote();

            novaLinha.Codigo = "";
            novaLinha.Descricao = "";
            novaLinha.Comprimento = "";
            novaLinha.Largura = "";
            novaLinha.Quantidade = "";
            novaLinha.Rotacionar = false;

            pecasParaLote.Add(novaLinha);

            datagridLote.ItemsSource = pecasParaLote;

        }

    }
}
