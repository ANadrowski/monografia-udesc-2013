using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtimizaCut_v2
{
    class PecaParaPlano : IComparable
    {
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Comprimento { get; set; }
        public string Largura { get; set; }
        public string Quantidade { get; set; }
        public bool Rotacionar { get; set; }
        public double Area { get; set; }
        public bool Inserida { get; set; }
        
        public int CompareTo(object obj)
        {
            PecaParaPlano pecaOrdenada = obj as PecaParaPlano;
            if (pecaOrdenada == null)
            {
                throw new ArgumentException("Objeto nao eh uma peca");
            }
            return this.Area.CompareTo(pecaOrdenada.Area);
        }
    }
}
