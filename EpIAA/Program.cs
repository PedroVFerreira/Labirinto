using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EpIAA
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ValidaArgumentos(args);
                var arquivo = args[0];
                var modulo = int.Parse(args[1]);
                
                Mapa mapa = new Mapa(arquivo);
                Joga jogo = new Joga(mapa);
                jogo.Jogar();
                jogo.ImprimeResultado(modulo);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
            }
        }

        private static void ValidaArgumentos(string[] args)
        {
            if (args.Length != 2)
                throw new Exception("Número de argumentos inválido");

            string arquivo = args[0];

            if (!File.Exists(arquivo))
                throw new Exception("O arquivo informado não foi encontrado");

            string modulo = args[1];
            List<string> modulosAceitos = new List<string> { "1", "2", "3" };
            if (!modulosAceitos.Any(c => c == modulo))
                throw new Exception("O módulo informado deve ser 1, 2 ou 3");

        }
    }
}
