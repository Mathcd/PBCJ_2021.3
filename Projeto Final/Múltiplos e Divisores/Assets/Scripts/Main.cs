using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBackend;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Essa é principal classe do Jogo. Ela é responsável por integrar
/// o back-end com o front-end do jogo e por controlar o desenvolvimento
/// do game, tais como de quem a é vez de jogar, chamar as funções de compra
/// e descarte, e inicializar vários dos objetos do jogo.
/// </summary>
public class Main : MonoBehaviour
{

    public ManageCartas manageCartasScript; // cria objeto manageCartasScript da classe ManageCartas
    

    // aqui são declarados os objetos do backend que serão utilizados ao longo do jogo
    Maquina maquina1;       // objeto referente à máquina do jogo (jogador IA)
    Jogador jogador1;       // objeto referente à ao jogador
    PilhaCompra pilha;      // objeto referente à pilha de compra das cartas
    Carta cartaAnterior;    // objeto referente à pilha de descarte (somente a carta superior)
     
    int indexCarta; // variável indicadora da posição da carta no array das cartas na "mão"


    // Start is called before the first frame update
    void Start()
    {

        // criando variáveis para cada uma 15 possíveis cartas do jogador
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

        // de acordo com o modo de jogo (0-Fácil / 1-Difícil), o jogador ou a máquina joga a primeira carta 
        if (PlayerPrefs.GetInt("modoJogo") == 0)
        {
            PlayerPrefs.SetInt("vezDoJogador", 1); // flag da vez do jogador; se 1 : é a vez do jogador
        }
        else
        {
            PlayerPrefs.SetInt("vezDoJogador", 0); // flag da vez do jogador; se 0 : não é a vez do jogador
        }
            
        // grava mensagem com instruções para o jogador na tela
        GameObject.Find("Comentario").GetComponent<Text>().text = "Faça a primeira jogada! Suas cartas são as da direita";

        /* variável com o valor carta que o jogador escolheu para descartar na pilha;
         * "-1" indica que o jogador ainda clicou escolheu (clicou) na carta */
        PlayerPrefs.SetInt("cartaDescartada", -1);

        PlayerPrefs.SetInt("jogadorJaComprou", 0); // variável indica se o jogador já comprou carta nesta rodada
        PlayerPrefs.SetInt("triggerCompra", 0);    // variável responsável por realizar compra quando solicitada pelo jogador

        

        // criando um baralho para a pilha de compra
        pilha = new PilhaCompra();
        pilha.printarBaralho("pilha");

        // criando um baralho para a máquina 1 
        maquina1 = new Maquina();
        maquina1.printarBaralho("máquina 1");

        // criando um baralho para a máquina 2
        jogador1 = new Jogador();
        jogador1.printarBaralho("jogador 1");

        // renderiza pilha de compra
        manageCartasScript.MostraPilhaCompra();

        // chamamos a carta que está no topo da pilha de compra para iniciar o jogo renderizamos essa carta 
        cartaAnterior = pilha.comprarCarta();
        manageCartasScript.MostraPilhaDescarte(cartaAnterior.getTipo(), cartaAnterior.getValor());

        // salvamos informações da carta anterior (valor e tipo) para ser acessada em outras instâncias do jogo
        PlayerPrefs.SetInt("cartaAnteriorValorPP", cartaAnterior.getValor());
        PlayerPrefs.SetString("cartaAnteriorTipoPP", cartaAnterior.getTipo().ToString());

        // renderiza os objetos das cartas do jogador e máquina 
        RenderizaTodasCartas(jogador1);
        RenderizaTodasCartas(maquina1);

        // atualiza os objetos com as cartas corretas da mão do jogador e máquina e espaços vazios 
        AtualizaTodasCartas(jogador1);
        AtualizaTodasCartas(maquina1);
    }



    // Update is called once per frame
    void Update()
    {
        if (maquina1.getQtdCartas() == 0)   // se a máquina descartar todas as cartas, exibe tela de derrota
        {
            SceneManager.LoadScene("FimDerrota");
        }
        else if (jogador1.getQtdCartas() == 0)  // se o jogador descartar todas as cartas, exibe tela de vitória
        {
            SceneManager.LoadScene("Fim");
        }

        // se ninguém tiver ganhado, verifica se é a vez do jogador e se ele já comprou nesta rodada
        else if (PlayerPrefs.GetInt("vezDoJogador") == 1) { 
            if (PlayerPrefs.GetInt("triggerCompra") == 1)
            {
                // jogador compra carta
                Debug.Log("Jogador 1 não encontrou carta compatível e está comprando uma carta da pilha.");
                Carta novaCarta = pilha.comprarCarta();
                jogador1.adicionarCartaComprada(novaCarta);

                // atualiza a mão do jogador com a nova carta comprada
                AtualizaTodasCartas(jogador1);

                // atualiza as flags de compra
                PlayerPrefs.SetInt("triggerCompra", 0);
                PlayerPrefs.SetInt("jogadorJaComprou", 1);
            }

            // caso valor de cartaDescartada seja != -1, significa que alguma carta foi jogada
            else if (PlayerPrefs.GetInt("cartaDescartada") != -1)
            {
                indexCarta = PlayerPrefs.GetInt("cartaDescartada"); // indice na mão do jogador da carta clicada para descarte

                // obtém dados da carta de descarte
                cartaAnterior = jogador1.getCartaAt(indexCarta);
                PlayerPrefs.SetInt("cartaAnteriorValorPP", cartaAnterior.getValor());
                PlayerPrefs.SetString("cartaAnteriorTipoPP", cartaAnterior.getTipo().ToString());

                Debug.Log("Os PlayerPrefs foram setados com sucesso");
                Debug.Log("Pilha de descarte atualizada");

                // insere na pilha de descarte a carta selecionada pelo jogador
                manageCartasScript.MostraPilhaDescarte(cartaAnterior.getTipo(), cartaAnterior.getValor());

                // remove carta selecionada da mão do jogador
                jogador1.jogarCarta(indexCarta);
                Debug.Log("Jogador1 jogou a carta");

                // atualiza as Tiles das cartas na mão do jogador
                AtualizaTodasCartas(jogador1);
                Debug.Log("Atualizando as cartas do jogador1");

                // atualiza as flags de carta, vez e compra do jogador
                PlayerPrefs.SetInt("cartaDescartada", -1);
                PlayerPrefs.SetInt("jogadorJaComprou", 0);
                PlayerPrefs.SetInt("vezDoJogador", 0);
            }

            // não faz nada
            else {}
        }

        /* caso não seja vez do jogador, significa que é a vez da máquina;
         * Maquina 1 verifica se tem alguma carta compativel em seu baralho
         * se nao tiver, entao compra uma carta da pilha de compra            
         * e verifica se consegue jogar com a carta comprada, senão passa a vez */
        else
        {
            Debug.Log("Máquina 1 iniciando sua jogada");

            // procura nas cartas da mão por uma carta compativel
            indexCarta = maquina1.procurarCartaCompativel(cartaAnterior);

            // caso seja encontrada carta compatível na mão
            if (indexCarta >= 0)
            {
                Debug.Log("Máquina 1 encontrou carta no index " + indexCarta);

                // a carta compatível encontrada é armazenada para ser enviada para a pilha de descarte
                cartaAnterior = maquina1.getCartaAt(indexCarta);

                // atualiza as flags da carta no topo da pilha
                PlayerPrefs.SetInt("cartaAnteriorValorPP", cartaAnterior.getValor());
                PlayerPrefs.SetString("cartaAnteriorTipoPP", cartaAnterior.getTipo().ToString());
                maquina1.jogarCarta(indexCarta);

                // atualizando Tiles das cartas após alteração na mão
                AtualizaTodasCartas(maquina1);

                // atualizamos a pilha de descarte com a carta jogada
                manageCartasScript.MostraPilhaDescarte(cartaAnterior.getTipo(), cartaAnterior.getValor());
                
                // escreve status/instruções na tela do jogo
                GameObject.Find("Comentario").GetComponent<Text>().text = "Máquina acabou de jogar. Agora é a sua vez";
            }

            // se não for encontrada carta compatível, compra
            else
            {
                Debug.Log("Máquina 1 não encontrou carta compatível e comprará uma carta da pilha.");
                
                // compra carta
                Carta novaCarta = pilha.comprarCarta();
                maquina1.adicionarCartaComprada(novaCarta);

                // depois de comprar, verifica se consegue jogar, senão passa a vez
                indexCarta = maquina1.procurarCartaCompativel(cartaAnterior);
                if (indexCarta >= 0)
                {
                    // caso carta comprada seja compatível, joga ela
                    Debug.Log("Máquina 1 encontrou carta no index " + indexCarta);
                    cartaAnterior = maquina1.getCartaAt(indexCarta);

                    // atualiza flags do topo da pilha de descarte
                    PlayerPrefs.SetInt("cartaAnteriorValorPP", cartaAnterior.getValor());
                    PlayerPrefs.SetString("cartaAnteriorTipoPP", cartaAnterior.getTipo().ToString());

                    // remove carta jogada mão da máquina
                    maquina1.jogarCarta(indexCarta);

                    // atualizamos a pilha de descarte com a carta jogada
                    manageCartasScript.MostraPilhaDescarte(cartaAnterior.getTipo(), cartaAnterior.getValor());
                }

                // atualizando cartas após alteração na mão
                AtualizaTodasCartas(maquina1);

                // escreve status/instruções na tela do jogo
                GameObject.Find("Comentario").GetComponent<Text>().text = "A Máquina já foi. Agora é a sua vez";
            }

            // após máquina jogar, atualiza os Tiles das cartas
            AtualizaTodasCartas(maquina1);

            // passa a vez para o jogador
            PlayerPrefs.SetInt("vezDoJogador", 1);
        }
    }

    /* chama método responsável por instanciar cada uma das cartas do jogador */
    public void RenderizaTodasCartas(Jogador jogador1)
    {
        // instancia os tiles de cada uma das cartas do jogador
        for (int i = 0; i < 15; i++)
        {
            /* carta M-7 foi usada como modelo apenas para instanciar o objeto; 
             * ela é sobrescrita pela carta correta logo em seguida */
            manageCartasScript.AddUmaCarta('J', i, 'M', 7);
        }
    }

    /* chama método responsável por instanciar cada uma das cartas da máquina */
    public void RenderizaTodasCartas(Maquina maquina1)
    {
        // instancia os tiles de cada uma das cartas da máquina
        for (int i = 0; i < 15; i++)
        {
            /* carta M-7 foi usada como modelo apenas para instanciar o objeto; 
             * ela é sobrescrita pela carta correta logo em seguida */
            manageCartasScript.AddUmaCarta('M', i, 'M', 7);
        }
    }

    /* função que atualiza todas as cartas renderizadas do jogador de acordo com o backend; 
     * deve ser chamada sempre que houver uma alteração nas cartas do jogo (ex. compra ou descarte) */
    public void AtualizaTodasCartas(Jogador jogador1)
    {
        int qtdCartas = jogador1.getQtdCartas();

        // preenche as posições das cartas com as cartas da mão
        for (int i = 0; i < qtdCartas; i++)
        {
            // get das informações da carta
            Carta cartaAtual = jogador1.getCartaAt(i);
            string nomeCartaAtual = "" + cartaAtual.getTipo() + "-" + cartaAtual.getValor();

            // set da tile e informações da carta
            Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeCartaAtual));
            GameObject.Find("Carta_" + "J" + "_" + i).GetComponent<Tile>().setCartaOriginal(s1);
            PlayerPrefs.SetInt("carta"+i+"jogador", cartaAtual.getValor());
        }
        // preenche as posições vazias com a carta "blank"
        for (int i = qtdCartas; i < 15; i++)
        {
            // set das posições "blank"
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

        // preenche as posições das cartas com as cartas da mão
        for (int i = 0; i < qtdCartas; i++)
        {
            // get das informações da carta
            Carta cartaAtual = maquina1.getCartaAt(i);
            string nomeCartaAtual = "" + cartaAtual.getTipo() + "-" + cartaAtual.getValor();

            // se o modo de jogo for "fácil", exibe a frente das cartas da máquina
            if (PlayerPrefs.GetInt("modoJogo") == 0)
            {
                // set da tile e informações da carta
                Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeCartaAtual));
                GameObject.Find("Carta_" + "M" + "_" + i).GetComponent<Tile>().setCartaOriginal(s1);
            }

            // se o modo de jogo for "difícil", exibe o verso das cartas da máquina
            else
            {
                // set da tile e informações da carta
                Sprite s1 = (Sprite)(Resources.Load<Sprite>("verso"));
                GameObject.Find("Carta_" + "M" + "_" + i).GetComponent<Tile>().setCartaOriginal(s1);
            }
            
        }
        // preenche as posições vazias com a carta "blank"
        for (int i = qtdCartas; i<15; i++)
        {
            // set das posições "blank"
            Sprite s1 = (Sprite)(Resources.Load<Sprite>("blank"));
            GameObject.Find("Carta_" + "M" + "_" + i).GetComponent<Tile>().setCartaOriginal(s1);
        }
    }
}
