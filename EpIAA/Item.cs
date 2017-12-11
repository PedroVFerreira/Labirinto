using System;
using System.Collections.Generic;
using System.Text;

namespace EpIAA
{
    public class Item
    {
        public Posicao posicao { get; set; }
        public int valor { get; set; }
        public int peso { get; set; }

        public Item(int linha, int coluna, int _valor, int _peso)
        {
            posicao = new Posicao(linha, coluna);
            valor = _valor;
            peso = _peso;
        }
    }
}
