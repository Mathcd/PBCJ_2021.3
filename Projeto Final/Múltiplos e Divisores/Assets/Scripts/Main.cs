using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBackend;
using System; 

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int rodadas;
        for(int i=0; i<1; i++)
        {
            rodadas = jogoCompleto(500);
            Debug.Log("JOGO FINALIZADO EM: " + rodadas + " RODADAS");
        }
    }

    public int jogoCompleto(int qtdMaxRodadas)
    {
        // Variaveis 
        int indexCarta, rodadas=0;
        Carta novaCarta;

        // Criando um baralho para a pilha de compra
        PilhaCompra pilha = new PilhaCompra();
        pilha.printarBaralho("pilha");

        // Criando um baralho para a máquina 1
        Maquina maquina1 = new Maquina();
        maquina1.printarBaralho("máquina 1");

        // Criando um baralho para a máquina 2
        Maquina maquina2 = new Maquina();
        maquina2.printarBaralho("máquina 2");

        // Definindo a carta que vai comecar o jogo (fazer isso ser aleatorio depois)
        Carta cartaAnterior = new Carta(2, 'A');

        // Rodadas do jogo
        do
        {
            rodadas++;

            // Maquina 1 verifica se tem alguma carta compativel em seu baralho
            // se nao tiver, entao compra uma carta da pilha de compra
            // e verifica se consegue jogar com a carta comprada, senao passa a vez
            Debug.Log("Máquina 1 iniciando sua jogada");
            
            // Procuro uma carta compativel
            indexCarta = maquina1.procurarCartaCompativel(cartaAnterior);
            
            // Se encontrou uma carta...
            if (indexCarta >= 0)
            {
                // Aqui achamos uma carta no baralho, vou jogar a carta encontrada
                // e definir que agr a carta anterior eh a carta encontrada
                Debug.Log("Máquina 1 encontrou carta no index " + indexCarta);
                cartaAnterior = maquina1.getCartaAt(indexCarta);
                maquina1.jogarCarta(indexCarta);
            }

            // Se nao encontrou, compra
            else
            {
                Debug.Log("Máquina 1 não encontrou carta compatível e comprará uma carta da pilha.");
                novaCarta = pilha.comprarCarta();
                maquina1.adicionarCartaComprada(novaCarta);

                // Depois de comprar, verifica se consegue jogar, senao passa a vez
                indexCarta = maquina1.procurarCartaCompativel(cartaAnterior);
                if (indexCarta >= 0)
                {
                    // Aqui achamos uma carta no baralho, vou jogar a carta encontrada
                    // e definir que agr a carta anterior eh a carta encontrada
                    Debug.Log("Máquina 1 encontrou carta no index " + indexCarta);
                    cartaAnterior = maquina1.getCartaAt(indexCarta);
                    maquina1.jogarCarta(indexCarta);
                }
            }

            // Finaliza a jogada
            Debug.Log("Máquina 1 finalizou sua jogada");
            maquina1.printarBaralho("máquina 1");

            // Mesma coisa pra máquina 2
            Debug.Log("Máquina 2 iniciando sua jogada");
            indexCarta = maquina2.procurarCartaCompativel(cartaAnterior);
            if (indexCarta >= 0)
            {
                Debug.Log("Máquina 2 encontrou carta no index " + indexCarta);
                cartaAnterior = maquina2.getCartaAt(indexCarta);
                maquina2.jogarCarta(indexCarta);
            }
            else
            {
                Debug.Log("Máquina 2 não encontrou carta compatível e comprará uma carta da pilha.");
                novaCarta = pilha.comprarCarta();
                maquina2.adicionarCartaComprada(novaCarta);
                indexCarta = maquina2.procurarCartaCompativel(cartaAnterior);
                if (indexCarta >= 0)
                {
                    Debug.Log("Máquina 2 encontrou carta no index " + indexCarta);
                    cartaAnterior = maquina2.getCartaAt(indexCarta);
                    maquina2.jogarCarta(indexCarta);
                }
            }
            Debug.Log("Máquina 2 finalizou sua jogada");
            maquina2.printarBaralho("máquina 2");
        } while (maquina1.getQtdCartas() > 0 & maquina2.getQtdCartas() > 0 & rodadas < qtdMaxRodadas);

        if (maquina1.getQtdCartas() == 0 )
        {
            Debug.Log("MÁQUINA 1 VENCEU O JOGO EM " + rodadas + " RODADAS!");
        }
        else if (maquina2.getQtdCartas() == 0 )
        {
            Debug.Log("MÁQUINA 2 VENCEU O JOGO EM " + rodadas + " RODADAS!");
        }
        else
        {
            Debug.Log("JOGO PARADO POIS ATINGIMOS " + rodadas + " RODADAS");
        }

        return rodadas;
    }
}
