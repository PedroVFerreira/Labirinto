using System;
using System.Collections.Generic;
using System.Text;

namespace EpIAA
{
    public class Posicao
    {
        public int linha { get; set; }
        public int coluna { get; set; }

        public Posicao(int _linha, int _coluna)
        {
            linha = _linha;
            coluna = _coluna;
        }

        public Posicao CopiaPosicao()
        {
            return new Posicao(linha, coluna);
        }

        public bool Compara(Posicao obj)
        {
            return (linha == obj.linha &&
                coluna == obj.coluna);
        }
    }
}
