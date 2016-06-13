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
    /// Interaction logic for cadastrarLote.xaml
    /// </summary>
    public partial class cadastrarLote : Window
    {
        public cadastrarLote()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            codigoLote.Clear();
            descricaoLote.Clear();
        }

        private void cadastrar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                string caminho = "c:\\dados\\lotes\\" + codigoLote.Text + ".xml";

                XmlTextWriter writer = new XmlTextWriter(caminho, null);
                writer.Formatting = Formatting.Indented;

                writer.WriteStartDocument();
                writer.WriteComment("Arquivo gerado pelo Software OtimizaCut, desenvolvido por Aislan Nadrowski - Ano: 2013 [LOTE FILE]");

                writer.WriteStartElement("lote");
                writer.WriteStartElement("cabecalho");

                writer.WriteElementString("codigoLote", codigoLote.Text);
                writer.WriteElementString("descricaoLote", descricaoLote.Text);

                writer.WriteEndElement();

                writer.WriteStartElement("pecas");
                writer.WriteEndElement();

                writer.Close();

                this.Close();

                MessageBox.Show("Lote cadastrado com sucesso!");


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }





    }
}
