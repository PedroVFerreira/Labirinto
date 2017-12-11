using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EpIAA
{
    public class Mapa
    {

        public char[][] _mapa;
        private int linhas = 0;
        private int colunas = 0;
        
        public List<Item> _itens = new List<Item>();
        public Posicao inicio;
        private Posicao fim;
        public int capacidadeMochila;

        private int linhasAtual = 0;

        public Mapa(string arquivo)
        {
            var linhasDoArquivo = File.ReadAllLines(arquivo);
            LeituraDasDimensoes(linhasDoArquivo);
            _mapa = new char[linhas][];
            PopulaMapaSimples(linhasDoArquivo);
            LeituraDosItens(linhasDoArquivo);
            LeituraCapacidadeMochila(linhasDoArquivo);
            LeituraInicioEFimDoArq(linhasDoArquivo);

        }

        public bool CheckItemColetado(Posicao pos)
        {
            return GetItem(pos) != null;
        }

        public Item GetItem(Posicao pos)
        {
            return _itens.Where(c => c.posicao.Compara(pos)).FirstOrDefault();
        }

        private void LeituraInicioEFimDoArq(string[] linhasDoArquivo)
        {
            var inicioArr = linhasDoArquivo[linhasAtual].Split(' ');
            inicio = new Posicao(int.Parse(inicioArr[0]), int.Parse(inicioArr[1]));
            linhasAtual++;
            var fimArr = linhasDoArquivo[linhasAtual].Split(' ');
            fim = new Posicao(int.Parse(fimArr[0]), int.Parse(fimArr[1]));
            linhasAtual = 0;
        }

        private void LeituraCapacidadeMochila(string[] linhasDoArquivo)
        {
            capacidadeMochila = int.Parse(linhasDoArquivo[linhasAtual]);
            linhasAtual++;
        }

        public bool CampoLivre(Posicao pAux)
        {
            if (pAux.linha >= linhas ||
                pAux.linha < 0 ||
                pAux.coluna >= colunas ||
                pAux.coluna < 0
                )
                return false;
                return GetValorPosicao(pAux) == '.';
        }

        private void LeituraDosItens(string[] linhasDoArquivo)
        {
            int numeroDeItens = int.Parse(linhasDoArquivo[linhasAtual]);
            for (int i = 1; i <= numeroDeItens; i++)
            {
                string l = linhasDoArquivo[linhasAtual + i];
                var valores = l.Split(' ');
                Item item = new Item(
                                       int.Parse(valores[0]),
                                       int.Parse(valores[1]),
                                       int.Parse(valores[2]),
                                       int.Parse(valores[3])
                                    );
                _itens.Add(item);
            }
            linhasAtual += numeroDeItens + 1;
        }

        private void PopulaMapaSimples(string[] linhasDoArquivo)
        {
            for (int i = 1; i <= linhas; i++)
            {
                var l = new char[colunas];
                for (int o = 0; o < colunas; o++)
                {
                    l[o] = linhasDoArquivo[i][o];
                }
                _mapa[i - 1] = l;
            }
            linhasAtual = linhas + 1;
        }

        private void LeituraDasDimensoes(string[] linhasDoArquivo)
        {
            var tamanhoCampo = linhasDoArquivo[0].Split(' ');
            linhas = int.Parse(tamanhoCampo[0]);
            colunas = int.Parse(tamanhoCampo[1]);
        }

        public char GetValorPosicao(Posicao posicao)
        {
            return _mapa[posicao.linha][posicao.coluna];
        }

        public bool CheckFim(Posicao pos)
        {
            return (
                    pos.linha == fim.linha &&
                    pos.coluna == fim.coluna
                    );
        }
        
    }
}
