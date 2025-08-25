using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    internal class Jogador
    {
        private string nome;
        private int pontuacao;

        public Jogador(string nome, int pontuacao)
        {
            this.nome = nome;
            this.pontuacao = pontuacao;
        }

        public int Pontuacao
        {
            get { return this.pontuacao; }
            set { this.pontuacao = value; }
        }
        public string Nome
        {
            get { return this.nome; }
            set { this.nome = value; }
        }
    }
}
