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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Xml;
using System.Data;
using System.Xml.Linq;


namespace OtimizaCut_v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CarregaParametros();
        }


        //Eventos
        private void salvarConfiguracoes_Click(object sender, RoutedEventArgs e)
        {
            string caminho = @"c:\dados\configuracoes\configuracoes.xml";

            XmlDocument doc = new XmlDocument();
            doc.Load(caminho);

            XmlNodeList trocarValorEspessuraSerra = doc.GetElementsByTagName("serra");

            for (int i = 0; i < trocarValorEspessuraSerra.Count; i++)
            {
                trocarValorEspessuraSerra[i].InnerText = espessuraSerra.Text;
            }

            XmlNodeList trocarValorRefilosLaterais = doc.GetElementsByTagName("refilos");

            for (int i = 0; i < trocarValorRefilosLaterais.Count; i++)
            {
                trocarValorRefilosLaterais[i].InnerText = refilosLaterais.Text;
            }




            XmlNodeList mostraComprimentoLargura = doc.GetElementsByTagName("mostraCompLarg");

            for (int i = 0; i < mostraComprimentoLargura.Count; i++)
            {
                if (mostraComLarg.IsChecked == true)            
                    mostraComprimentoLargura[i].InnerText = "True";


                if (mostraComLarg.IsChecked == false)
                    mostraComprimentoLargura[i].InnerText = "False";

            }



            XmlNodeList mostraRefilosLaterais = doc.GetElementsByTagName("mostraLinhasRefilos");

            for (int i = 0; i < mostraRefilosLaterais.Count; i++)
            {
                if (mostraRefilos.IsChecked == true)
                    mostraRefilosLaterais[i].InnerText = "True";


                if (mostraRefilos.IsChecked == false)
                    mostraRefilosLaterais[i].InnerText = "False";

            }


            XmlNodeList mostraCortesIniciaisNaChapa = doc.GetElementsByTagName("mostraCortesIniciais");

            for (int i = 0; i < mostraCortesIniciaisNaChapa.Count; i++)
            {
                if (mostraCortesIniciais.IsChecked == true)
                    mostraCortesIniciaisNaChapa[i].InnerText = "True";


                if (mostraCortesIniciais.IsChecked == false)
                    mostraCortesIniciaisNaChapa[i].InnerText = "False";

            }


            XmlNodeList orientacaoDosCortesIniciais = doc.GetElementsByTagName("orientacaoCortesIniciaisDaChapa");

            for (int i = 0; i < mostraCortesIniciaisNaChapa.Count; i++)
            {
                if (corteHorizontalSetado.IsChecked == true)
                    orientacaoDosCortesIniciais[i].InnerText = "Horizontais";


                if (corteVerticalSetado.IsChecked == true)
                    orientacaoDosCortesIniciais[i].InnerText = "Verticais";

            }

            XmlNodeList mostraDesenhoNoRelatorio = doc.GetElementsByTagName("mostraDesenhoNoRelatorio");

            for (int i = 0; i < mostraCortesIniciaisNaChapa.Count; i++)
            {
                if (mostraDesenho.IsChecked == true)
                    mostraDesenhoNoRelatorio[i].InnerText = "True";


                if (mostraDesenho.IsChecked == false)
                    mostraDesenhoNoRelatorio[i].InnerText = "False";

            }

            XmlNodeList mostraEstatisticasNoRelatorio = doc.GetElementsByTagName("mostraEstatisticasNoRelatorio");

            for (int i = 0; i < mostraCortesIniciaisNaChapa.Count; i++)
            {
                if (mostraEstatisticas.IsChecked == true)
                    mostraEstatisticasNoRelatorio[i].InnerText = "True";


                if (mostraEstatisticas.IsChecked == false)
                    mostraEstatisticasNoRelatorio[i].InnerText = "False";

            }




            doc.Save(caminho);
            
            MessageBox.Show("Parâmetros salvos com sucesso!");
        }


        private void CarregaParametros()
        {

            string caminho = @"c:\dados\configuracoes\configuracoes.xml";

            XmlTextReader tr = new XmlTextReader(caminho);

            while (tr.Read())
            {

                if (tr.NodeType == XmlNodeType.Element)
                {

                    if (tr.Name == "serra")
                    {
                        espessuraSerra.Text = tr.ReadString();

                    }

                    if (tr.Name == "refilos")
                    {
                        refilosLaterais.Text = tr.ReadString();
                    }

                    if (tr.Name == "mostraCompLarg")
                    {
                        if (tr.ReadString().Equals("True")) 
                            mostraComLarg.IsChecked = true;

                        if (tr.ReadString().Equals("False"))
                            mostraComLarg.IsChecked = false;
                    }

                    if (tr.Name == "mostraLinhasRefilos")
                    {
                        if (tr.ReadString().Equals("True"))
                            mostraRefilos.IsChecked = true;

                        if (tr.ReadString().Equals("False"))
                            mostraRefilos.IsChecked = false;
                    }

                 /*   if (tr.Name == "mostraCortes")
                    {
                        if (tr.ReadString().Equals("True"))
                            mostraCortes.IsChecked = true;

                        if (tr.ReadString().Equals("False"))
                            mostraCortes.IsChecked = false;
                    }
                    */
                    if (tr.Name == "mostraCortesIniciais")
                    {
                        if (tr.ReadString().Equals("True"))
                            mostraCortesIniciais.IsChecked = true;

                        if (tr.ReadString().Equals("False"))
                            mostraCortesIniciais.IsChecked = false;
                    }

                    if (tr.Name == "orientacaoCortesIniciaisDaChapa")
                    {
                        if (tr.ReadString().Equals("Horizontais"))
                            corteHorizontalSetado.IsChecked = true;

                        else
                            corteVerticalSetado.IsChecked = true;
                    }

                    if (tr.Name == "mostraDesenhoNoRelatorio")
                    {
                        if (tr.ReadString().Equals("True"))
                            mostraDesenho.IsChecked = true;

                        if (tr.ReadString().Equals("False"))
                            mostraDesenho.IsChecked = false;
                    }

                    if (tr.Name == "mostraEstatisticasNoRelatorio")
                    {
                        if (tr.ReadString().Equals("True"))
                            mostraEstatisticas.IsChecked = true;

                        if (tr.ReadString().Equals("False"))
                            mostraEstatisticas.IsChecked = false;
                    }

                }
            }

            tr.Close();
        }



        private void adicionarMaterial_Click(object sender, RoutedEventArgs e)
        {
            cadastrarMaterial janela = new cadastrarMaterial();
            janela.Show();

        }



        private void atualizarMaterial_Click(object sender, RoutedEventArgs e)
        {

            DirectoryInfo diretorio = new DirectoryInfo(@"c:\dados\materiais\");
            FileInfo[] Arquivos = diretorio.GetFiles("*.xml*");

            listview.Items.Clear();

            foreach (FileInfo fileinfo in Arquivos)
            {

                string nome = fileinfo.FullName;

                XmlTextReader tr = new XmlTextReader(nome);
               
                LVData row = new LVData();
               
                while (tr.Read())
                {
                    if (tr.NodeType == XmlNodeType.Element)
                    {
                        if (tr.Name == "codigo")
                            row.Codigo = tr.ReadString();

                        if (tr.Name == "descricao")
                            row.Descricao = tr.ReadString();

                        if (tr.Name == "comprimento")
                            row.Comprimento = tr.ReadString();

                        if (tr.Name == "largura")
                            row.Largura = tr.ReadString();

                        if (tr.Name == "espessura")
                            row.Espessura = tr.ReadString();

                        if (tr.Name == "cor")
                            row.Cor = tr.ReadString();

                        if (tr.Name == "tipo")
                            row.Tipo = tr.ReadString();

                        if (tr.Name == "rotaciona")
                            row.Rotaciona = tr.ReadString();

                    }

                }
                listview.Items.Add(row);
                tr.Close();
            }

            System.GC.Collect();

            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        
        private void excluirMaterial_Click(object sender, RoutedEventArgs e)
        {
            
            if (MessageBox.Show("Deseja apagar esse material?", "Apagar material", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                LVData s = (LVData)listview.SelectedItems[0];
                string caminho1 = "C:\\dados\\materiais\\" + s.Codigo + ".xml";
                File.Delete(caminho1);

                
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           // View janela = new View();
           // janela.Show();

        }

        private void editarMaterial_Click(object sender, RoutedEventArgs e)
        {
            LVData s = (LVData)listview.SelectedItems[0];
            string caminho2 = "C:\\dados\\materiais\\" + s.Codigo + ".xml";
            int valorLinha = Convert.ToInt32(s.Codigo);

            EditarMaterial janela = new EditarMaterial(valorLinha);
            janela.Show();
            
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void cadastrarLote_Click(object sender, RoutedEventArgs e)
        {
            cadastrarLote janela = new cadastrarLote();
            janela.Show();
        }

        private void editarLote_Click(object sender, RoutedEventArgs e)
        {

            LVDataLote s = (LVDataLote)listviewLotes.SelectedItems[0];

       //     string caminho2 = "C:\\dados\\materiais\\" + s.Codigo + ".xml";
            int valorLinha = Convert.ToInt32(s.Codigo);
            string valorLinha2 = Convert.ToString(s.Descricao);

            cadastrarPecasLote janela = new cadastrarPecasLote(valorLinha, valorLinha2);
            janela.Show();
        }

        private void refreshLote_Click(object sender, RoutedEventArgs e)
        {
            DirectoryInfo diretorio = new DirectoryInfo(@"c:\dados\lotes\");
            FileInfo[] Arquivos = diretorio.GetFiles("*.xml*");

            listviewLotes.Items.Clear();

            foreach (FileInfo fileinfo in Arquivos)
            {

                string nome = fileinfo.FullName;

                XmlTextReader tr = new XmlTextReader(nome);

                LVDataLote row = new LVDataLote();

                while (tr.Read())
                {
                    if (tr.NodeType == XmlNodeType.Element)
                    {
                        if (tr.Name == "codigoLote")
                            row.Codigo = tr.ReadString();

                        if (tr.Name == "descricaoLote")
                            row.Descricao = tr.ReadString();

                    }

                }
                listviewLotes.Items.Add(row);
                tr.Close();
            }

            
        }

        private void excluirLote_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Deseja apagar esse lote?", "Apagar lote", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {

                LVDataLote s = (LVDataLote)listviewLotes.SelectedItems[0];
                string caminho1 = "C:\\dados\\lotes\\" + s.Codigo + ".xml";
                File.Delete(caminho1);


            }
        }


    }
}
