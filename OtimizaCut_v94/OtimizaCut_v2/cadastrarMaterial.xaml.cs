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
using System.Xml;
using System.Data;


namespace OtimizaCut_v2
{
    /// <summary>
    /// Interaction logic for cadastrarMaterial.xaml
    /// </summary>
    public partial class cadastrarMaterial : Window
    {
        public cadastrarMaterial()
        {
            InitializeComponent();
        }

        private void limpar_Click(object sender, RoutedEventArgs e)
        {
            codigoMat.Clear();
            descricaoMat.Clear();
            comprimentoMat.Clear();
            larguraMat.Clear();
            espessuraMat.Clear();
            corMat.Clear();
            tipoMat.Clear();         

        }

        private void salvar_Click(object sender, RoutedEventArgs e)
        {

            try
            {

                string caminho = "c:\\dados\\materiais\\" + codigoMat.Text + ".xml";

                XmlTextWriter writer = new XmlTextWriter(caminho, null);
                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();
                writer.WriteComment("Arquivo gerado pelo Software OtimizaCut, desenvolvido por Aislan Nadrowski - Ano: 2013");

                writer.WriteStartElement("material");

                writer.WriteElementString("codigo", codigoMat.Text);
                writer.WriteElementString("descricao", descricaoMat.Text);
                writer.WriteElementString("comprimento", comprimentoMat.Text);
                writer.WriteElementString("largura", larguraMat.Text);
                writer.WriteElementString("espessura", espessuraMat.Text);
                writer.WriteElementString("cor", corMat.Text);
                writer.WriteElementString("tipo", tipoMat.Text);

                if (rotacionaMat.SelectedIndex.ToString().Equals("1"))
                    writer.WriteElementString("rotaciona", "Não");


                if (rotacionaMat.SelectedIndex.ToString().Equals("0"))
                    writer.WriteElementString("rotaciona", "Sim");

               
                writer.WriteEndElement();

                writer.Close();

                MessageBox.Show("Material cadastrado com sucesso!");

                this.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

    }
}
