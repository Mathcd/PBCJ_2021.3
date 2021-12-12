using Random = System.Random;
using UnityEngine;
using System.Collections.Generic;

/* Este arquivo contém as classes que implementam o back-end do jogo
 * Para referência auxiliar, consulte o arquivo "Classe UML.pdf" na 
 * pasta raiz do projeto para consultar o diagrama de classes UML
 * deste projeto*/

namespace GameBackend
{
    /// <summary>
    /// Classe definindo os valores de constantes que serão utilizadas pelo back-end do jogo
    /// </summary>
    public static class Globals
    {
        public static int QTD_CARTAS_AO_CRIAR_BARALHO = 5;
        public static int QTD_MAX_CARTAS_MAO = 15;
        public static char[] TIPOS_DISPONIVEIS = new char[] {'M', 'D', 'A', 'A'}; // 25% de chance de cartas M ou D e 50% de cartas ambos (MD)
        public static int[] MULTIPLOS_DISPONIVEIS = new int[] {1, 2, 3, 4, 5, 6, 8, 9, 10};
        public static int[] DIVISORES_DISPONIVEIS = new int[] {6, 8, 9, 10, 12, 15, 16, 18, 20, 24};
        public static int[] AMBOS_DISPONIVEIS = new int[] {6, 8, 10};
        public static Random RANDOM = new Random();
    }

    /// <summary>
    /// Classe implementa o objeto "Carta", com os atributos valor (int) e tipo (char)
    /// </summary>
    public class Carta
    {
        private int valor;
        private char tipo; 

        // construtor da Carta
        public Carta(int valor, char tipo)
        {
            this.setValor(valor);
            this.setTipo(tipo);
        }

        // setters
        public void setValor(int valor)
        {
            this.valor = valor;
        }

        public void setTipo(char tipo)
        {
            this.tipo = tipo;
        }

        // getters
        public int getValor()
        {
            return this.valor;
        }

        public char getTipo()
        {
            return this.tipo;
        }

        // retorna o nome de uma carta, formados por seus valor e tipo
        public string getNome()
        {
            string carta = this.getValor() + "" + this.getTipo() + ", ";
            return carta;
        }
    }

    /// <summary>
    /// Classe abstrata que implementa métodos a serem herdados pela Pilha de Compra,
    /// Máquina e Jogador; inclui funções para adicionar carta comprada e jogar carta
    /// </summary>
    public abstract class Baralho
    {
        private List<Carta> cartas;
        private int qtdCartas;

        /* define o baralho de cartas, de acordo com as possibilidades definidas na classe Global, 
         * para cada tipo de carta */
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

        /* método para retornar a carta do topo da pilha de compras */
        protected Carta getCartaTopo()
        {
            Carta cartaTopo = this.getCartaAt(this.getQtdCartas()-1);
            Debug.Log("Carta do topo: " + cartaTopo.getNome());
            return cartaTopo;
        }

        /* método para adicionar uma carta comprada */
        public void adicionarCartaComprada(Carta carta)
        {
            Debug.Log("Adicionando carta " + carta.getNome() + " ao baralho");
            this.cartas.Add(carta);
            this.setQtdCartas(this.getQtdCartas() + 1);
            if (this.getQtdCartas() > Globals.QTD_MAX_CARTAS_MAO)
            {
                // caso uma carta seja comprada após o baralho atingir o limite, uma carta é removida aleatoriamente
                Debug.Log("Quantidade máxima de cartas atingida. Uma aleatória será removida");
                this.cartas.RemoveAt(Globals.RANDOM.Next(Globals.QTD_MAX_CARTAS_MAO));
                this.setQtdCartas(Globals.QTD_MAX_CARTAS_MAO);
            }
        }

        /* método para jogar a carta de um índice especificado, removendo-a da mão e retornando-a */
        public void jogarCarta(int index)
        {
            this.cartas.RemoveAt(index);
            this.setQtdCartas(this.getQtdCartas() - 1);
        }

        /* método para printar o baralho (máquina, jogador ou pilha), retornando seus valores e tipos */
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

        // setters
        protected void setQtdCartas(int qtdCartas)
        {
            this.qtdCartas = qtdCartas;
        }

        // getters
        public int getQtdCartas()
        {
            return this.qtdCartas;
        }

        public Carta getCartaAt(int index)
        {
            return this.cartas[index];
        }
    }

    /// <summary>
    /// Classe implementa as funções a serem utilizadas pela Pilha de Compra, que
    /// deve fornecer cartas a serem compradas (pelo jogador e máquina) e verificar
    /// se a pilha ficou vazia, gerando uma nova pilha com cartas aleatórias.
    /// Herda da classe Baralho
    /// </summary>
    public class PilhaCompra : Baralho
    {
        /* define pilha de compra */
        public PilhaCompra()
        {
            this.setCartas(Globals.QTD_CARTAS_AO_CRIAR_BARALHO);
        }

        /* método para checar se a pilha está vazia */
        public void verificarPilhaVazia()
        {
            if (this.getQtdCartas() == 0)
            {
                // gera nova pilha aleatória caso pilha esteja vazia
                Debug.Log("Pilha vazia. Uma nova será gerada aleatóriamente");
                this.setCartas(Globals.QTD_CARTAS_AO_CRIAR_BARALHO);
            }
        }

        /* método para comprar carta, removendo a carta do topo da lista e retornando-a */
        public Carta comprarCarta()
        {
            Carta topo = this.getCartaTopo();
            this.setQtdCartas(this.getQtdCartas() - 1);
            this.verificarPilhaVazia();
            Debug.Log("Carta comprada: " + topo.getNome());
            return topo;
        }
    }

    /// <summary>
    /// Classe responsável pelo objeto Máquina, herdando da classe Baralho,
    /// implementa função para verificar se uma de suas cartas é compatível
    /// com a carta da pilha de descarte
    /// </summary>
    public class Maquina : Baralho
    {
        public Maquina()
        {
            this.setCartas(Globals.QTD_CARTAS_AO_CRIAR_BARALHO);
        }

        /* método recebe como entrada a carta que esta na mesa, na pilha de descarte,
         * e verifica se possui uma de suas cartas compatíveis de acordo com os critérios
         * de valor e tipo (divisor, múltiplo ou ambos) */
        public int procurarCartaCompativel(Carta anterior)
        {
            char tipoAnterior = anterior.getTipo();
            int valorAnterior = anterior.getValor();
            int valorAtual;

            // verifica para cada uma das cartas da mão
            for (int i=0; i<this.getQtdCartas(); i++)
            {
                valorAtual = this.getCartaAt(i).getValor();

                /* checa se há algumas carta compatível, de acordo com tipo, retornando o índice
                 * da mão caso encontre carta compatível */
                switch (anterior.getTipo())
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

    /// <summary>
    /// Classe responsável pelo objeto Jogador, herdando da classe Baralho
    /// </summary>
    public class Jogador : Baralho
    {
        public Jogador()
        {
            this.setCartas(Globals.QTD_CARTAS_AO_CRIAR_BARALHO);
        }
    }
}