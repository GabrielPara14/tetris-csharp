using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace tetris
{
    internal class Tetris
    {
        static void FixarPeca(int[,] formato, int x, int y, int[,] campo,Tetrominos peca, Jogador j1)
        {
            for (int i = 0; i < formato.GetLength(0); i++) 
            {
                for (int j = 0; j < formato.GetLength(1); j++)
                {
                    if (formato[i,j] == 1)
                    {
                        int linha = i + x;
                        int coluna = j + y;
                        if (linha >= 0 && linha < 20 && coluna >= 0 && coluna < 10)
                        {
                            campo[linha, coluna] = 1;
                        }
                    }
                }
            }
            j1.Pontuacao += peca.pontos;
        }
        static bool PodeMover(int[,] formato, int novoX, int novoY, int[,] campo)
        {
            for (int i = 0; i < formato.GetLength(0); i++)
            {
                for (int j = 0; j < formato.GetLength(1); j++)
                {
                    if (formato[i, j] == 1)
                    {
                        int linha = novoX + i;
                        int coluna = novoY + j;
                        if (linha < 0 || linha >= 20)
                        {
                            return false;
                        }
                        else if (coluna < 0 || coluna >= 10)
                        {
                            return false;
                        }
                        else if (campo[linha, coluna] == 1)
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public static void ImprimirTabuleiro(int[,] tabuleiro, Jogador j1)
        {
            Console.Clear();

            int linhas = tabuleiro.GetLength(0);
            int colunas = tabuleiro.GetLength(1);

            Console.Write("|");
            for (int j = 0; j < colunas; j++)
                Console.Write("-");
            Console.WriteLine("|");

            for (int i = 0; i < linhas; i++)
            {
                Console.Write("|");
                for (int j = 0; j < colunas; j++)
                {
                    if (tabuleiro[i, j] == 1)
                    {
                        Console.Write("█");

                    }
                    else
                    {
                        Console.Write(" ");

                    }
                }
                Console.WriteLine("|");
            }

            Console.Write("|");
            for (int j = 0; j < colunas; j++)
                Console.Write("-");
            Console.WriteLine("|");
            Console.WriteLine($"{j1.Nome}");
            Console.WriteLine("Pontuação: " + j1.Pontuacao);
        }

        static void ImprimeTabuleiroComPeca(int[,] campo, int[,] formato, int x, int y, Jogador j1)
        {
            int[,] tabuleiroMod = new int[20, 10];

            for (int i = 0; i < campo.GetLength(0); i++)
            {
                for (int j = 0; j < campo.GetLength(1); j++)
                {
                    tabuleiroMod[i, j] = campo[i, j];
                }
            }

            int contL = 0;
            for (int i = x; i < x + 3; i++, contL++)
            {
                int contC = 0;
                for (int j = y; j < y + 3; j++, contC++)
                {
                    if (i >= 0 && i < 20 && j >= 0 && j < 10)
                    {
                        if (formato[contL, contC] == 1)
                        {
                            tabuleiroMod[i, j] = formato[contL, contC];
                        }
                    }
                }
            }
            ImprimirTabuleiro(tabuleiroMod,j1);
        }

        static int[,] SimularRotacaoHorario(int[,] formato)
        {
            int[,] rotacionada = new int[3, 3];
            for (int j = 0; j < rotacionada.GetLength(1); j++)
            {
                rotacionada[j, 2] = formato[0, j];
                rotacionada[j, 1] = formato[1, j];
                rotacionada[j, 0] = formato[2, j];
            }
            return rotacionada;
        }

        static void Descer(int[,] campo, int linhas)
        {
            for (int i = linhas - 1; i >= 0; i--) 
            {
                for (int j = 0; j < campo.GetLength(1); j++)
                {
                    campo[i+1, j] = campo[i, j];
                }
            }
            
        }

        static void VerificaLinhas( int[,] campo , Jogador j1 )
        {
            int linhasRemo = 0;
            for(int i = campo.GetLength(0)-1; i >=0; i--)
            {
                bool linhaCompleta = true;
                for (int j = 0;j < campo.GetLength(1); j++)
                {
                    if (campo[i,j] != 1)
                    {
                        linhaCompleta = false;
                        break;
                    }
                }
                if (linhaCompleta)
                {
                    linhasRemo++;
                    Descer(campo, i);
                    for (int colunasL0 = 0; colunasL0 < campo.GetLength(1); colunasL0++)
                    {
                        campo[0, colunasL0] = 0;
                    }

                    i++; 
                }
            }
            
            if (linhasRemo > 1)
            {
                j1.Pontuacao += 300 + (100 * linhasRemo - 1);
            }
            else if( linhasRemo == 1) 
            {
                j1.Pontuacao += 300;
            }
        }

        public static void Jogar(Jogador player)
        {
            int[,] tabuleiro = new int[20, 10];
            bool gameOver = false;
            int posX = 0, posY = 3;
            Random r = new Random();
            int numTetra = r.Next(0, 5);

            Tetrominos tetro = new Tetrominos(numTetra);

            ImprimeTabuleiroComPeca(tabuleiro, tetro.formato, posX, posY, player);
                
                while (!gameOver)
                {
                    ConsoleKeyInfo tecla = Console.ReadKey();

                    if (tecla.Key == ConsoleKey.LeftArrow)
                    {
                        if(PodeMover(tetro.formato, posX, posY - 1, tabuleiro))
                        {
                            posY--;
                            ImprimeTabuleiroComPeca(tabuleiro, tetro.formato, posX, posY, player);
                        }
                        
                        
                    }
                    else if (tecla.Key == ConsoleKey.DownArrow)
                    {
                        if (PodeMover(tetro.formato, posX + 1, posY, tabuleiro))
                        {
                            posX++;
                            ImprimeTabuleiroComPeca(tabuleiro, tetro.formato, posX, posY, player);
                        }
                        else
                        {
                            FixarPeca(tetro.formato, posX, posY, tabuleiro, tetro, player);

                            tetro = new Tetrominos(r.Next(0, 5));
                            posX = 0;
                            posY = 4;
                            VerificaLinhas(tabuleiro, player);

                            ImprimeTabuleiroComPeca(tabuleiro, tetro.formato, posX, posY, player);

                            if (!PodeMover(tetro.formato, posX, posY, tabuleiro))
                            {
                                gameOver = true;
                            }
                        }
                    }
                    else if (tecla.Key == ConsoleKey.RightArrow)
                    {
                        if (PodeMover(tetro.formato, posX, posY + 1, tabuleiro))
                        {
                            posY++;
                            ImprimeTabuleiroComPeca(tabuleiro, tetro.formato, posX, posY, player);
                        }

                    }
                    else if (tecla.Key == ConsoleKey.UpArrow)
                    {
                        int[,] simulaRota = SimularRotacaoHorario(tetro.formato);
                        if (PodeMover(simulaRota, posX, posY, tabuleiro))
                        {
                            tetro.RotacionaHorario();
                            ImprimeTabuleiroComPeca(tabuleiro, tetro.formato, posX, posY, player);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Jogada inválida");
                    }

                    if (gameOver)
                    {
                        StreamWriter arqW = new StreamWriter($"scores.txt", true, Encoding.UTF8);
                        arqW.WriteLine($"{player.Nome} sua pontuação foi de: {player.Pontuacao}");
                        arqW.Close();
                    }
                }
        }

        static void Main(string[] args)
        {
            Console.Write("Digite o seu nome: ");
            string nome = Console.ReadLine();

            Jogador j1 = new Jogador(nome, 0);
            Jogar(j1);

            Console.WriteLine("Game-over");
        }
    }
}