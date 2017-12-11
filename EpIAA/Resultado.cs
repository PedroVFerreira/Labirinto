using System;
using System.Collections.Generic;
using System.Text;

namespace EpIAA
{
    public class Resultado
    {
        public List<Posicao> caminho { get; set; } = new List<Posicao>();
        public List<Item> itensColetados { get; set; } = new List<Item>();

    }
}
