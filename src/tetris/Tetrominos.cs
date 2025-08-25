using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    internal class Tetrominos
    {
        public int[,] formato = new int[3, 3];
        public int pontos;

        public Tetrominos(int num)
        {
            if (num == 2)
            {
                i0g(formato);
            }
            else if (num == 1)
            {
                l0g(formato);
            }
            else if (num == 0)
            {
                t0g(formato);
            }
            else if(num == 3) 
            {
                c0g(formato);
            }
            else if( num == 4)
            {
                e0g(formato);
            }
        }

        public void i0g(int[,] peca)
        {
            peca[0, 1] = 1;
            peca[1, 1] = 1;
            peca[2, 1] = 1;
            pontos = 3;
        }
        public void l0g(int[,] peca)
        {
            peca[0, 1] = 1;
            peca[1, 1] = 1;
            peca[2, 1] = 1;
            peca[2, 2] = 1;
            pontos = 4;
        }
        public void t0g(int[,] peca)
        {
            peca[0, 0] = 1;
            peca[0, 1] = 1;
            peca[0, 2] = 1;
            peca[1, 1] = 1;
            pontos = 5;
        }

        public void c0g(int[,] peca)
        {
            peca[0,0] = 1;
            peca[0, 1] = 1;
            peca[1, 0] = 1;
            peca[1, 1] = 1;
            pontos = 4;
        }
        public void e0g(int[,] peca)
        {
            peca[2,0] = 1;
            peca[2,1] = 1;
            peca[1, 1] = 1;
            peca[1, 2] = 1;
            pontos = 5;
        }

        public void RotacionaHorario()
        {
            int[,] rotacionada = new int[3, 3];
            for (int j = 0; j < rotacionada.GetLength(1); j++)
            {
                rotacionada[j, 2] = formato[0, j];
                rotacionada[j, 1] = formato[1, j];
                rotacionada[j, 0] = formato[2, j];
            }

            for (int i = 0; i < formato.GetLength(0); i++)
            {
                for (int j = 0; j < formato.GetLength(1); j++)
                {
                    formato[i, j] = rotacionada[i, j];
                }
            }
        }

        public void RotacionaAntHorario()
        {
            int[,] rotacionada = new int[3, 3];
            int cont = 0;
            for (int j = rotacionada.GetLength(1) - 1; j >= 0; j--, cont++)
            {
                rotacionada[cont, 0] = formato[0, j];
                rotacionada[cont, 1] = formato[1, j];
                rotacionada[cont, 2] = formato[2, j];
            }

            for (int i = 0; i < formato.GetLength(0); i++)
            {
                for (int j = 0; j < formato.GetLength(1); j++)
                {
                    formato[i, j] = rotacionada[i, j];
                }
            }
        }
    }
}
