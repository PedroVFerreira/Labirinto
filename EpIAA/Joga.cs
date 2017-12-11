using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpIAA
{
    public class Joga
    {
        private Mapa _mapa = null;
        public List<Resultado> resultados { get; set; }
         = new List<Resultado>();


        public Joga(Mapa mapa)
        {
            _mapa = mapa;
        }

        public void Jogar()
        {
            if (_mapa == null)
                throw new Exception("O mapa não foi carregado corretamente");

            List<Posicao> caminho = new List<Posicao>();
            caminho.Add(_mapa.inicio);
            Jogada(caminho, new List<Item>(), _mapa.inicio);

        }

        public void ImprimeResultado(int modulo)
        {
            Resultado r = DecideResultado(modulo);

            Console.Out.WriteLine(r.caminho.Count);
            foreach (var item in r.caminho)
            {
                Console.Out.WriteLine(item.linha + " " + item.coluna);
            }
            Console.Out.WriteLine(r.itensColetados.Count + " " + r.itensColetados.Sum(c => c.valor) + " " + r.itensColetados.Sum(c => c.peso));

            foreach (var item in r.itensColetados)
            {
                Console.Out.WriteLine(item.posicao.linha + " " + item.posicao.coluna);
            }
        }

        private Resultado DecideResultado(int modulo)
        {
            if (modulo == 1)
                return resultados
                    .OrderBy(c => c.caminho.Count)
                    .First();
            if (modulo == 2)
                return resultados
                    .OrderByDescending(c => c.itensColetados.Sum(x => x.valor))
                    .First();
            
                return resultados
                    .Where(c => c.itensColetados.Sum(x => x.peso) <= _mapa.capacidadeMochila )
                    .OrderByDescending(c => c.itensColetados.Sum(x => x.valor))
                    .First();
        }

        public void Jogada(List<Posicao> caminho, List<Item> itensColetados, Posicao pos)
        {
            if (_mapa.CheckFim(pos))
            {
                SalvaResultado(caminho, itensColetados);
                return;
            }

            List<Posicao> movimentos = MovimentosPossiveis(caminho, pos);
            
            foreach (var item in movimentos)
            {
                caminho.Add(item);
                if (_mapa.CheckItemColetado(item))
                    itensColetados.Add(_mapa.GetItem(item));

                Jogada(caminho, itensColetados, item);

                caminho.Pop();
                if (_mapa.CheckItemColetado(item))
                    itensColetados.Pop();
            }
        }

        private List<Posicao> MovimentosPossiveis(List<Posicao> caminho, Posicao pos)
        {
            var result = new List<Posicao>();
            int[] xS = { -1, 0, 1 };
            int[] yS = { -1, 0, 1 };

            foreach (var x in xS)
                foreach (var y in yS)
                {
                    if (Modulo(y) == Modulo(x)) continue;

                    Posicao pAux = new Posicao(pos.linha + x, pos.coluna + y);

                    if (_mapa.CampoLivre(pAux) && CaminhoLivre(pAux, caminho))
                        result.Add(pAux);
                }


            return result;
        }

        private bool CaminhoLivre(Posicao pAux, List<Posicao> caminho)
        {
            return !(caminho.Exists(c => c.Compara(pAux)));
        }

        public int Modulo(int x)
        {
            return x < 0 ? -x : x;
        }

        private void SalvaResultado(List<Posicao> caminho, List<Item> itensColetados)
        {
            Resultado r = new Resultado();

            foreach (var passo in caminho)
                r.caminho.Add(passo);
            foreach (var item in itensColetados)
                r.itensColetados.Add(item);

            resultados.Add(r);
        }
    }
}
