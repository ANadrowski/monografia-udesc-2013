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
using System.Xml.Linq;

namespace OtimizaCut_v2
{
    /// <summary>
    /// Interaction logic for EditarMaterial.xaml
    /// </summary>
    public partial class EditarMaterial : Window
    {
        public EditarMaterial(int valor)
        {
            InitializeComponent();
            carregarValores(valor);
        }

 
        private void limpar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void salvar_Click(object sender, RoutedEventArgs e)
        {

            //Abre arquivo .xml
            int valor = Convert.ToInt32(codigoMat.Text);
            string caminho = "C:\\dados\\materiais\\" + valor + ".xml";

            XmlDocument doc = new XmlDocument();
            doc.Load(caminho);

            
            //Modifica o conteudo do arquivo .xml.
            XmlNodeList descricaoMat1 = doc.GetElementsByTagName("descricao");

            for (int i = 0; i < descricaoMat1.Count; i++)
            {
                descricaoMat1[i].InnerText = descricaoMat.Text;
            }


            XmlNodeList comprimentoMat1 = doc.GetElementsByTagName("comprimento");

            for (int i = 0; i < comprimentoMat1.Count; i++)
            {
                comprimentoMat1[i].InnerText = comprimentoMat.Text;
            }


            XmlNodeList larguraMat1 = doc.GetElementsByTagName("largura");

            for (int i = 0; i < larguraMat1.Count; i++)
            {
                larguraMat1[i].InnerText = larguraMat.Text;
            }


            XmlNodeList espessuraMat1 = doc.GetElementsByTagName("espessura");

            for (int i = 0; i < espessuraMat1.Count; i++)
            {
                espessuraMat1[i].InnerText = espessuraMat.Text;
            }


            XmlNodeList corMat1 = doc.GetElementsByTagName("cor");

            for (int i = 0; i < corMat1.Count; i++)
            {
                corMat1[i].InnerText = corMat.Text;
            }


            XmlNodeList tipoMat1 = doc.GetElementsByTagName("tipo");

            for (int i = 0; i < tipoMat1.Count; i++)
            {
                tipoMat1[i].InnerText = tipoMat.Text;
            }


            XmlNodeList rotacionaMat1 = doc.GetElementsByTagName("rotaciona");

            for (int i = 0; i < rotacionaMat1.Count; i++)
            {

                if (rotacionaMat.SelectedIndex == 0)
                    rotacionaMat1[i].InnerText = "Sim";

                if (rotacionaMat.SelectedIndex == 1)
                    rotacionaMat1[i].InnerText = "Não";
            }

            //Salva as modificações realizadas no arquivo .xml.
            this.Close();

            doc.Save(caminho);
            MessageBox.Show("Alterações realizadas com sucesso!");

           
          
        }

        private void carregarValores(int valor)
        {
            LVData s = new LVData();
            string caminho = "C:\\dados\\materiais\\" + valor + ".xml";

            XmlTextReader tr = new XmlTextReader(caminho);
            
            while (tr.Read())
            {
                if (tr.NodeType == XmlNodeType.Element)
                {
                    if (tr.Name == "codigo")
                        s.Codigo = tr.ReadString();

                    if (tr.Name == "descricao")
                        s.Descricao = tr.ReadString();

                    if (tr.Name == "comprimento")
                        s.Comprimento = tr.ReadString();

                    if (tr.Name == "largura")
                        s.Largura = tr.ReadString();

                    if (tr.Name == "espessura")
                        s.Espessura = tr.ReadString();

                    if (tr.Name == "cor")
                        s.Cor = tr.ReadString();

                    if (tr.Name == "tipo")
                        s.Tipo = tr.ReadString();

                    if (tr.Name == "rotaciona")
                    {
                        s.Rotaciona = tr.ReadString();
                            if (s.Rotaciona.Equals("Sim"))
                                rotacionaMat.SelectedIndex = 0;
                            if (s.Rotaciona.Equals("Não"))
                                rotacionaMat.SelectedIndex = 1;
                    }

                }

                codigoMat.Text = s.Codigo;
                descricaoMat.Text = s.Descricao;
                comprimentoMat.Text = s.Comprimento;
                larguraMat.Text = s.Largura;
                espessuraMat.Text = s.Espessura;
                corMat.Text = s.Cor;
                tipoMat.Text = s.Tipo;
                
            }
            tr.Close();
            

        }

    }
}
