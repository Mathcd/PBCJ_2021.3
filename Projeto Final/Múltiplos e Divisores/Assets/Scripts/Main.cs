using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBackend;
using System; 

public class Main : MonoBehaviour
{

    public ManageCartas manageCartasScript; // cria objeto manageCartasScript da classe ManageCartas

    // Criando um baralho para a máquina 1
    Maquina maquina1;
    Jogador jogador1;
    PilhaCompra pilha;
    Carta cartaAnterior;
    //maquina1.printarBaralho("máquina 1");

    int indexCarta;
    int rodadas = 0;
    bool vezDoJogador = true;



    // Start is called before the first frame update
    void Start()
    {
        // criando variáveis para cada uma das cartas do jogador
        PlayerPrefs.SetInt("carta0jogador", -1);
        PlayerPrefs.SetInt("carta1jogador", -1);
        PlayerPrefs.SetInt("carta2jogador", -1);
        PlayerPrefs.SetInt("carta3jogador", -1);
        PlayerPrefs.SetInt("carta4jogador", -1);
        PlayerPrefs.SetInt("carta5jogador", -1);
        PlayerPrefs.SetInt("carta6jogador", -1);
        PlayerPrefs.SetInt("carta7jogador", -1);
        PlayerPrefs.SetInt("carta8jogador", -1);
        PlayerPrefs.SetInt("carta9jogador", -1);
        PlayerPrefs.SetInt("carta10jogador", -1);
        PlayerPrefs.SetInt("carta11jogador", -1);
        PlayerPrefs.SetInt("carta12jogador", -1);
        PlayerPrefs.SetInt("carta13jogador", -1);
        PlayerPrefs.SetInt("carta14jogador", -1);

        // flag da vez do jogador; se 0 : não é a vez, se 1 : não é a vez
        PlayerPrefs.SetInt("vezDoJogador", 0);

        // Criando um baralho para a pilha de compra
        pilha = new PilhaCompra();
        pilha.printarBaralho("pilha");

        // Criando um baralho para a máquina 1
        maquina1 = new Maquina();
        maquina1.printarBaralho("máquina 1");

        // Criando um baralho para a máquina 2
        jogador1 = new Jogador();
        jogador1.printarBaralho("jogador 1");

        // renderiza pilha de compra
        manageCartasScript.MostraPilhaCompra();

        // chamamos a carta que está no topo da pilha de compra para iniciar o jogo e renderizamos ela
        cartaAnterior = pilha.comprarCarta();
        manageCartasScript.MostraPilhaDescarte(cartaAnterior.getTipo(), cartaAnterior.getValor());
        PlayerPrefs.SetInt("cartaAnteriorValorPP", cartaAnterior.getValor());
        PlayerPrefs.SetString("cartaAnteriorTipoPP", cartaAnterior.getTipo().ToString());


        // inicializa os objetos das cartas do jogador e máquina
        RenderizaTodasCartas(jogador1);
        RenderizaTodasCartas(maquina1);

        // atualiza os objetos com as cartas corretas da mão do jogador e máquina e espaços vazios
        AtualizaTodasCartas(jogador1);
        AtualizaTodasCartas(maquina1);



        // Definindo a carta que vai comecar o jogo
        //Carta cartaAnterior = cartaInicial;


    }



        // Update is called once per frame
    void Update()
    {
        if (vezDoJogador)
        {
            int abc = 1;
        }
        else
        {
            // Maquina 1 verifica se tem alguma carta compativel em seu baralho
            // se nao tiver, entao compra uma carta da pilha de compra
            // e verifica se consegue jogar com a carta comprada, senao passa a vez
            Debug.Log("Máquina 1 iniciando sua jogada");

            // Procuro uma carta compativel
            indexCarta = maquina1.procurarCartaCompativel(cartaAnterior);

            // Se encontrou uma carta...
            if (indexCarta >= 0)
            {
                //maquina1.printarBaralho("maquina 1 antes do destroy");
                //DestroiTodasCartas(maquina1);
                //maquina1.printarBaralho("maquina 1 depois do destroy");

                // Aqui achamos uma carta no baralho, vou jogar a carta encontrada
                // e definir que agr a carta anterior eh a carta encontrada
                Debug.Log("Máquina 1 encontrou carta no index " + indexCarta);
                cartaAnterior = maquina1.getCartaAt(indexCarta);
                maquina1.jogarCarta(indexCarta);


                // atualizando cartas após alteração na mão
                AtualizaTodasCartas(maquina1);

                // atualizamos a pilha de descarte com a carta jogada
                manageCartasScript.MostraPilhaDescarte(cartaAnterior.getTipo(), cartaAnterior.getValor());

                Debug.Log("E assim ficou o baralho da maquina:");
                maquina1.printarBaralho("máquina 1");
            }

            // Se nao encontrou, compra
            else
            {


                Debug.Log("Máquina 1 não encontrou carta compatível e comprará uma carta da pilha.");
                Carta novaCarta = pilha.comprarCarta();
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

                    // atualizamos a pilha de descarte com a carta jogada
                    manageCartasScript.MostraPilhaDescarte(cartaAnterior.getTipo(), cartaAnterior.getValor());
                }

                // atualizando cartas após alteração na mão
                AtualizaTodasCartas(maquina1);

                Debug.Log("E assim ficou o baralho da maquina:");
                maquina1.printarBaralho("máquina 1");
            }
            AtualizaTodasCartas(maquina1);
            vezDoJogador = true;

        }
    }

    /* chama método responsável por instanciar cada uma das cartas do jogador */
    public void RenderizaTodasCartas(Jogador jogador1)
    {
        for (int i = 0; i < 15; i++)
        {
            /* carta M-7 foi usada apenas para instanciar o objeto; ela é sobrescrita logo em seguida */
            manageCartasScript.AddUmaCarta('J', i, 'M', 7);
        }
    }

    /* chama método responsável por instanciar cada uma das cartas da máquina */
    public void RenderizaTodasCartas(Maquina maquina1)
    {
        for (int i = 0; i < 15; i++)
        {
            /* carta M-7 foi usada apenas para instanciar o objeto; ela é sobrescrita logo em seguida */
            manageCartasScript.AddUmaCarta('M', i, 'M', 7);
        }
    }

    /* função que atualiza todas as cartas renderizadas do jogador de acordo com o backend, 
     * deve ser chamada sempre que houver uma alteração nas cartas do jogo (ex. compra ou descarte) */
    public void AtualizaTodasCartas(Jogador jogador1)
    {
        int qtdCartas = jogador1.getQtdCartas();

        // preenche as posições com as cartas da mão
        for (int i = 0; i < qtdCartas; i++)
        {
            Carta cartaAtual = jogador1.getCartaAt(i);
            string nomeCartaAtual = "" + cartaAtual.getTipo() + "-" + cartaAtual.getValor();

            Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeCartaAtual));
            GameObject.Find("Carta_" + "J" + "_" + i).GetComponent<Tile>().setCartaOriginal(s1);
            PlayerPrefs.SetInt("carta"+i+"jogador", cartaAtual.getValor());
        }
        // preenche as posições vazias com a carta "blank"
        for (int i = qtdCartas; i < 15; i++)
        {
            Sprite s1 = (Sprite)(Resources.Load<Sprite>("blank"));
            GameObject.Find("Carta_" + "J" + "_" + i).GetComponent<Tile>().setCartaOriginal(s1);
            PlayerPrefs.SetInt("carta" + i + "jogador", -1);
        }
    }

    /* função que atualiza todas as cartas renderizadas da máquina de acordo com o backend, 
     * atualizando as posições vazias para a carta "blank";
     * deve ser chamada sempre que houver uma alteração nas cartas do jogo (ex. compra ou descarte) */
    public void AtualizaTodasCartas(Maquina maquina1)
    {
        int qtdCartas = maquina1.getQtdCartas();

        // preenche as posições com as cartas da mão
        for (int i = 0; i < qtdCartas; i++)
        {
            Carta cartaAtual = maquina1.getCartaAt(i);
            string nomeCartaAtual = "" + cartaAtual.getTipo() + "-" + cartaAtual.getValor();

            Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeCartaAtual));
            GameObject.Find("Carta_" + "M" + "_" + i).GetComponent<Tile>().setCartaOriginal(s1);
        }
        // preenche as posições vazias com a carta "blank"
        for (int i = qtdCartas; i<15; i++)
        {
            Sprite s1 = (Sprite)(Resources.Load<Sprite>("blank"));
            GameObject.Find("Carta_" + "M" + "_" + i).GetComponent<Tile>().setCartaOriginal(s1);
        }
    }

   

    public int jogoCompleto(int qtdMaxRodadas)
    {
        /*
         
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
        Jogador jogador1= new Jogador();
        jogador1.printarBaralho("jogador 1");

        // renderiza pilha de compra
        manageCartasScript.MostraPilhaCompra();

        // chamamos a carta que está no topo da pilha de compra para iniciar o jogo e renderizamos ela
        Carta cartaInicial = pilha.comprarCarta();
        manageCartasScript.MostraPilhaDescarte(cartaInicial.getTipo(), cartaInicial.getValor());


        // inicializa os objetos das cartas do jogador e máquina
        RenderizaTodasCartas(jogador1);
        RenderizaTodasCartas(maquina1);

        // atualiza os objetos com as cartas corretas da mão do jogador e máquina e espaços vazios
        AtualizaTodasCartas(jogador1);
        AtualizaTodasCartas(maquina1);

        

        // Definindo a carta que vai comecar o jogo
        Carta cartaAnterior = cartaInicial;

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
                maquina1.printarBaralho("maquina 1 antes do destroy");
                //DestroiTodasCartas(maquina1);
                maquina1.printarBaralho("maquina 1 depois do destroy");

                // Aqui achamos uma carta no baralho, vou jogar a carta encontrada
                // e definir que agr a carta anterior eh a carta encontrada
                Debug.Log("Máquina 1 encontrou carta no index " + indexCarta);
                cartaAnterior = maquina1.getCartaAt(indexCarta);
                maquina1.jogarCarta(indexCarta);


                /* ISSO DEVE SER APAGADO !!!!!!!!
                print("Qtd cartas antes do Destroy: " + maquina1.getQtdCartas());
                maquina1.printarBaralho("maquina 1 antes do render");
                RenderizaTodasCartas(maquina1);
                maquina1.printarBaralho("maquina 1 depois do render");
                DestroiTodasCartas(maquina1);
                RenderizaTodasCartas(maquina1);
                

                // atualizando cartas após alteração na mão
                AtualizaTodasCartas(maquina1);
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

                // atualizando cartas após alteração na mão
                AtualizaTodasCartas(maquina1);

                
            }

            // Finaliza a jogada
            Debug.Log("Máquina 1 finalizou sua jogada");
            maquina1.printarBaralho("máquina 1");

            break;
                */

                // Mesma coisa pra máquina 2
                //Debug.Log("Jogador 1 iniciando sua jogada");



        /*
        indexCarta = jogador1.procurarCartaCompativel(cartaAnterior);
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



    } while (maquina1.getQtdCartas() > 0 & jogador1.getQtdCartas() > 0 & rodadas < qtdMaxRodadas);

    if (maquina1.getQtdCartas() == 0 )
    {
        Debug.Log("MÁQUINA 1 VENCEU O JOGO EM " + rodadas + " RODADAS!");
    }
    else if (jogador1.getQtdCartas() == 0 )
    {
        Debug.Log("MÁQUINA 2 VENCEU O JOGO EM " + rodadas + " RODADAS!");
    }
    else
    {
        Debug.Log("JOGO PARADO POIS ATINGIMOS " + rodadas + " RODADAS");
    }

    return rodadas;

    */

        return 0;
    }
}
