using Random = System.Random;
using UnityEngine;
using System.Collections.Generic;

namespace GameBackend
{
    public static class Globals
    {
        public static int QTD_CARTAS_AO_CRIAR_BARALHO = 5;
        public static int QTD_MAX_CARTAS_MAO = 15;
        public static char[] TIPOS_DISPONIVEIS = new char[] {'M', 'D', 'A', 'A'};
        public static int[] MULTIPLOS_DISPONIVEIS = new int[] {1, 2, 3, 4, 5, 6, 8, 9, 10};
        public static int[] DIVISORES_DISPONIVEIS = new int[] {6, 8, 9, 10, 12, 15, 16, 18, 20, 24};
        public static int[] AMBOS_DISPONIVEIS = new int[] {6, 8, 10};
        public static Random RANDOM = new Random();

        // define se o jogador pode ou não comprar
        public static bool JOGADOR_PODE_COMPRAR = true;
    }

    public class Carta
    {
        private int valor;
        private char tipo; 

        public Carta(int valor, char tipo)
        {
            this.setValor(valor);
            this.setTipo(tipo);
        }

        public void setValor(int valor)
        {
            this.valor = valor;
        }

        public void setTipo(char tipo)
        {
            this.tipo = tipo;
        }

        public int getValor()
        {
            return this.valor;
        }

        public char getTipo()
        {
            return this.tipo;
        }

        public string getNome()
        {
            string carta = this.getValor() + "" + this.getTipo() + ", ";
            return carta;
        }
    }

    public abstract class Baralho
    {
        private List<Carta> cartas;
        private int qtdCartas;

        protected void setCartas(int qtd)
        {
            char tipo = 'M';
            int valor = 1;
            this.setQtdCartas(qtd);
            this.cartas = new List<Carta>();
            for (int i=0; i<qtd; i++)
            {
                tipo = Globals.TIPOS_DISPONIVEIS[Globals.RANDOM.Next(Globals.TIPOS_DISPONIVEIS.Length)];
                switch(tipo)
                {
                    case 'M':
                        valor = Globals.MULTIPLOS_DISPONIVEIS[Globals.RANDOM.Next(Globals.MULTIPLOS_DISPONIVEIS.Length)];
                        break;
                    case 'D':
                        valor = Globals.DIVISORES_DISPONIVEIS[Globals.RANDOM.Next(Globals.DIVISORES_DISPONIVEIS.Length)];
                        break;
                    case 'A':
                        valor = Globals.AMBOS_DISPONIVEIS[Globals.RANDOM.Next(Globals.AMBOS_DISPONIVEIS.Length)];
                        break;
                }
                this.cartas.Add(new Carta(valor, tipo));
            }
        }

        protected Carta getCartaTopo()
        {
            Carta cartaTopo = this.getCartaAt(this.getQtdCartas()-1);
            Debug.Log("Carta do topo: " + cartaTopo.getNome());
            return cartaTopo;
        }

        public void adicionarCartaComprada(Carta carta)
        {
            Debug.Log("Adicionando carta " + carta.getNome() + " ao baralho");
            this.cartas.Add(carta);
            this.setQtdCartas(this.getQtdCartas() + 1);
            if (this.getQtdCartas() > Globals.QTD_MAX_CARTAS_MAO)
            {
                Debug.Log("Quantidade máxima de cartas atingida. Uma aleatória será removida");
                this.cartas.RemoveAt(Globals.RANDOM.Next(Globals.QTD_MAX_CARTAS_MAO));
                this.setQtdCartas(Globals.QTD_MAX_CARTAS_MAO);
            }
        }

        public void jogarCarta(int index)
        {
            this.cartas.RemoveAt(index);
            this.setQtdCartas(this.getQtdCartas() - 1);
        }

        public void printarBaralho(string prefixo)
        {
            string baralho = prefixo + ": ";
            for (int i=0; i<this.getQtdCartas(); i++)
            {
                baralho += this.cartas[i].getValor();
                baralho += this.cartas[i].getTipo();
                baralho += ", ";
            }
            Debug.Log(baralho);
        }

        protected void setQtdCartas(int qtdCartas)
        {
            this.qtdCartas = qtdCartas;
        }

        public int getQtdCartas()
        {
            return this.qtdCartas;
        }

        public Carta getCartaAt(int index)
        {
            return this.cartas[index];
        }
    }

    public class PilhaCompra : Baralho
    {
        public PilhaCompra()
        {
            this.setCartas(Globals.QTD_CARTAS_AO_CRIAR_BARALHO);
        }

        public void verificarPilhaVazia()
        {
            if (this.getQtdCartas() == 0)
            {
                Debug.Log("Pilha vazia. Uma nova será gerada aleatóriamente");
                this.setCartas(Globals.QTD_CARTAS_AO_CRIAR_BARALHO);
            }
        }

        public Carta comprarCarta()
        {
            Carta topo = this.getCartaTopo();
            this.setQtdCartas(this.getQtdCartas() - 1);
            this.verificarPilhaVazia();
            Debug.Log("Carta comprada: " + topo.getNome());
            return topo;
        }
    }

    public class Maquina : Baralho
    {
        public Maquina()
        {
            this.setCartas(Globals.QTD_CARTAS_AO_CRIAR_BARALHO);
        }

        public int procurarCartaCompativel(Carta anterior)
        {
            char tipoAnterior = anterior.getTipo();
            int valorAnterior = anterior.getValor();
            int valorAtual;

            for (int i=0; i<this.getQtdCartas(); i++)
            {
                valorAtual = this.getCartaAt(i).getValor();

                switch(anterior.getTipo())
                {
                    case 'M':
                        if (valorAtual % valorAnterior == 0)
                            return i;
                        break;
                    case 'D':
                        if (valorAnterior % valorAtual == 0)
                            return i;
                        break;
                    case 'A':
                        if (valorAnterior % valorAtual == 0 | valorAtual % valorAnterior == 0)
                            return i;
                        break;
                }              
            }

            return -1;
        }
    }

    public class Jogador : Baralho
    {
        public Jogador()
        {
            this.setCartas(Globals.QTD_CARTAS_AO_CRIAR_BARALHO);
        }
    }
}