using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageCartas : MonoBehaviour
{
    private bool primeiraCartaSelecionada, segundaCartaSelecionada, terceiraCartaSelecionada, quartaCartaSelecionada; // indicadores para cada carta escolhida em cada linha
    private GameObject carta1, carta2, carta3, carta4; // gameObjects da 1a e 2a carta selecionada
    private string linhaCarta1, linhaCarta2, linhaCarta3, linhaCarta4; // linha da carta selecionada
    public GameObject carta; // A carta a ser descartada
    public bool timerPausado, timerAcionado; // indicador de pausa no Timer ou Start Timer
    public float timer; // variável de tempo
    public int modoDeJogo = 0; // modo de jogo escolhido nas configuracoes
    public int numTentativas = 0; // número de tentativas na rodada
    public int numAcertos = 0; // número de match de pares acertados
    public AudioSource somOK; // som de acerto
    public AudioSource myLittleBrownBookMP3; // Coltrane eh top demais se eh louco cachorreira

    void Awake()
    {
        /*
        *
        * quando a scene atrelada a esse script acordar, roda essa metodo 
        * para colocar recorde e ultima partida nos objetos do jogo para que o usuario os veja 
        *
        */
        GameObject.Find("ultimaJogada").GetComponent<Text>().text = "Ultima partida: " + PlayerPrefs.GetInt("Jogadas");
        GameObject.Find("recordeText").GetComponent<Text>().text = "Recorde: " + PlayerPrefs.GetInt("Recorde");
    }

    void Start()
    {
        /*
        *
        * ao iniciar, setar o modo de jogo escolhido nas configuracoes
        * mostrar as cartas na tela, tocar a musica do jogo, e setar o som 
        * de acerto. tambem coloca o numero de tentativas em 0 pq acabou de comecar
        *
        */
        modoDeJogo = PlayerPrefs.GetInt("ModoDeJogo");
        MostraCartas();
        UpDateTentativas();
        somOK = GetComponent<AudioSource>();
        myLittleBrownBookMP3 = GetComponent<AudioSource>();
        myLittleBrownBookMP3.Play();
    }

    void Update()
    {
        /*
        *
        * toda vez que o timer for acionado, verifica se o usuario conseguiu 
        * acertar, ou se ele errou. Dependendo do que for, pode esconder as cartas, ou destrui-las
        * se o usuario tiver os 13 acertos requeridos, verifica se bateu o recorde e manda o usuario
        * para a tela de recorde, ou senao, para a tela de fim
        *
        */
        if (timerAcionado)
        {
            timer += Time.deltaTime;
            print(timer);

            if (timer > 1)
            {
                timerPausado = true;
                timerAcionado = false;

                if(
                    (modoDeJogo!=4 && carta1.tag==carta2.tag) || 
                    (modoDeJogo==4 && carta1.tag==carta2.tag && carta2.tag==carta3.tag && carta3.tag==carta4.tag)
                ){
                    Destroy(carta1);
                    Destroy(carta2);
                    if (modoDeJogo==4){
                        Destroy(carta3);
                        Destroy(carta4);
                    }                        
                    numAcertos++;
                    somOK.Play();

                    if (numAcertos == 13)
                    {
                        // ajusta a qtd de jogadas da partida que acabou de se encerrar
                        PlayerPrefs.SetInt("Jogadas", numTentativas);

                        // verifica se bateu recorde
                        if (PlayerPrefs.GetInt("Jogadas") < PlayerPrefs.GetInt("Recorde") || PlayerPrefs.GetInt("Recorde") == 0)
                        {
                            PlayerPrefs.SetInt("Recorde", PlayerPrefs.GetInt("Jogadas"));
                            SceneManager.LoadScene("Recorde");
                        }
                        else
                        {
                            SceneManager.LoadScene("Fim");
                        }                        
                    }   
                }
                else
                {
                    carta1.GetComponent<Tile>().EscondeCarta();
                    carta2.GetComponent<Tile>().EscondeCarta();
                    if (modoDeJogo==4){
                        carta3.GetComponent<Tile>().EscondeCarta();
                        carta4.GetComponent<Tile>().EscondeCarta();
                    }
                }
                primeiraCartaSelecionada = false;
                segundaCartaSelecionada = false;
                terceiraCartaSelecionada = false;
                quartaCartaSelecionada = false;
                carta1 = null;
                carta2 = null;
                carta3 = null;
                carta4 = null;
                linhaCarta1 = "";
                linhaCarta2 = "";
                linhaCarta3 = "";
                linhaCarta4 = "";
                timer = 0;
            }
        }
    }

    void MostraCartas()
    {
        /*
        *
        * funcao que roda no comeco para mostrar as cartas na tela
        * ela cria um array de inteiros embaralhados (ids das cartas)
        * e as coloca na tela de acordo com o modo de jogo escolhido nas configuracoes
        * um array para cada linha
        *
        */
        int[] arrayEmbaralhado1 = criaArrayEmbaralhado();
        int[] arrayEmbaralhado2 = criaArrayEmbaralhado();
        int[] arrayEmbaralhado3 = criaArrayEmbaralhado();
        int[] arrayEmbaralhado4 = criaArrayEmbaralhado();

        for (int i=0; i<13; i++)
        {
            switch (modoDeJogo)
            {
                // apenas naipes vermelhos
                case 0:
                    AddUmaCarta(0.0f, i, arrayEmbaralhado1[i], 1, 0);
                    AddUmaCarta(1.0f, i, arrayEmbaralhado2[i], 3, 0);
                    break;

                // apenas naipes pretos
                case 1:
                    AddUmaCarta(0.0f, i, arrayEmbaralhado1[i], 0, 1);
                    AddUmaCarta(1.0f, i, arrayEmbaralhado2[i], 2, 1);
                    break;

                // um naipe com traseiras iguais
                case 2:
                    AddUmaCarta(0.0f, i, arrayEmbaralhado1[i], 2, 0);
                    AddUmaCarta(1.0f, i, arrayEmbaralhado2[i], 2, 0);
                    break;

                // um naipe com traseiras diferentes
                case 3:
                    AddUmaCarta(0.0f, i, arrayEmbaralhado1[i], 3, 0);
                    AddUmaCarta(1.0f, i, arrayEmbaralhado2[i], 3, 1);
                    break;

                // quatro naipes com traseiras intercaladas
                case 4:
                    AddUmaCarta(-0.5f, i, arrayEmbaralhado1[i], 0, 0);
                    AddUmaCarta(0.5f, i, arrayEmbaralhado2[i], 1, 1);
                    AddUmaCarta(1.5f, i, arrayEmbaralhado3[i], 2, 0);
                    AddUmaCarta(2.5f, i, arrayEmbaralhado4[i], 3, 1);
                    break;
                
            }            
        }
    }

    void AddUmaCarta(float linha, int rank, int valor, int idNaipe, int idFundo)
    {
        /*
        *
        *
        *
        */
        GameObject centro = GameObject.Find("centroDaTela");
        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (650 * escalaCartaOriginal) / 110.0f;
        float fatorEscalaY = (945 * escalaCartaOriginal) / 110.0f;
        Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13/2) * fatorEscalaX), centro.transform.position.y + ((linha - 2.0f/2.0f) * fatorEscalaY), centro.transform.position.z);
        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));
        string nomeDaCarta = "";
        string numeroCarta = "";
        c.tag = "" + (valor+1);
        c.name = "" + linha + "_" + valor;

        if (valor == 0)
            numeroCarta = "ace";
        else if (valor == 10)
            numeroCarta = "jack";
        else if (valor == 11)
            numeroCarta = "queen";
        else if (valor == 12)
            numeroCarta = "king";
        else
            numeroCarta = "" + (valor + 1);

        switch(idNaipe){
            case 0:
                nomeDaCarta = numeroCarta + "_of_clubs";
                break;
            case 1:
                nomeDaCarta = numeroCarta + "_of_hearts";
                break;
            case 2:
                nomeDaCarta = numeroCarta + "_of_spades";
                break;
            case 3:
                nomeDaCarta = numeroCarta + "_of_diamonds";
                break;
        }

        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));
        print("S1: " + s1);

        GameObject.Find("" + linha + "_" + valor).GetComponent<Tile>().setCartaOriginal(s1, idFundo);
    }

    public int[] criaArrayEmbaralhado()
    {
        /*
        *
        * metodo para criar um array de inteiros embaralhados
        * vai servir para saber a ordem em que colocar as cartas em cada linha
        * do jogo
        *
        */
        int[] novoArray = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        int temp;

        for(int t=0; t < 13; t++)
        {
            temp = novoArray[t];
            int r = Random.Range(t, 13);
            novoArray[t] = novoArray[r];
            novoArray[r] = temp;
        }

        return novoArray;
    }

    public void CartaSelecionada(GameObject carta)
    {
        /*
        *
        * metodo para manejar as cartas selecionadas pelo player conforme ele clica
        * primeiro verifica se a primeira foi selecionada, depois a segunda, e assim por diante
        * dependendo do modo de jogo escolhido nas configuracoes.
        * somente quando o player clica em todas as cartas e finaliza uma jogada, verifica-se
        * se ele acertou ou errou chamando o metodo VerificaCartas()
        *
        */
        if (modoDeJogo != 4){
            if (!primeiraCartaSelecionada)
            {
                string linha = carta.name.Substring(0, 1);
                linhaCarta1 = linha;
                primeiraCartaSelecionada = true;
                carta1 = carta;
                carta1.GetComponent<Tile>().RevelaCarta();
            }
            else if(primeiraCartaSelecionada && !segundaCartaSelecionada)
            {
                string linha = carta.name.Substring(0, 1);
                linhaCarta2 = linha;
                segundaCartaSelecionada = true;
                carta2 = carta;
                carta2.GetComponent<Tile>().RevelaCarta();
                VerificaCartas();
            }
        }
        else
        {
            if (!primeiraCartaSelecionada){
                linhaCarta1 = carta.name.Substring(0, 1);
                primeiraCartaSelecionada = true;
                carta1 = carta;
                carta1.GetComponent<Tile>().RevelaCarta();
            } 
            else if (
                primeiraCartaSelecionada &&
                !segundaCartaSelecionada
            ){
                linhaCarta2 = carta.name.Substring(0, 1);
                segundaCartaSelecionada = true;
                carta2 = carta;
                carta2.GetComponent<Tile>().RevelaCarta();
            }
            else if (
                primeiraCartaSelecionada &&
                segundaCartaSelecionada &&
                !terceiraCartaSelecionada
            ){
                linhaCarta3 = carta.name.Substring(0, 1);
                terceiraCartaSelecionada = true;
                carta3 = carta;
                carta3.GetComponent<Tile>().RevelaCarta();
            }
            else if (
                primeiraCartaSelecionada &&
                segundaCartaSelecionada &&
                terceiraCartaSelecionada &&
                !quartaCartaSelecionada
            ){
                linhaCarta4 = carta.name.Substring(0, 1);
                quartaCartaSelecionada = true;
                carta4 = carta;
                carta4.GetComponent<Tile>().RevelaCarta();
                VerificaCartas();
            }
        }   
    }

    public void VerificaCartas()
    {
        /*
        *
        * esse metodo eh chamado quando um player encerra sua jogada. O timer eh disparado 
        * para que a funcao Update faca o que deve fazer. esse metodo tambem acrescenta
        * uma jogada ao numero de tentativas e chama UpDateTentativas() para mostrar na tela
        * a qtd de tentativas do usuario
        *
        */
        DisparaTimer();
        numTentativas++;
        UpDateTentativas();
    }

    public void DisparaTimer()
    {
        /*
        *
        * Dispara o timer para o metodo Update passar pelo seu primeiro if
        *
        */
        timerPausado = false;
        timerAcionado = true;
    }

    public void UpDateTentativas()
    {
        /*
        *
        * mostra a quantidade de tentativas do usuario na tela
        *
        */
        GameObject.Find("numTentativas").GetComponent<Text>().text = "Tentativas: " + numTentativas;
    }

    public void VoltarParaInicio()
    {
        /*
        *
        * metodo atrelado ao botao onclick para voltar a tela de inicio
        *
        */
        primeiraCartaSelecionada = false;
        segundaCartaSelecionada = false;
        terceiraCartaSelecionada = false;
        quartaCartaSelecionada = false;
        carta1 = null;
        carta2 = null;
        carta3 = null;
        carta4 = null;
        linhaCarta1 = "";
        linhaCarta2 = "";
        linhaCarta3 = "";
        linhaCarta4 = "";
        timer = 0;
        numTentativas = 0;
        numAcertos = 0;
        SceneManager.LoadScene("Inicio");
    }
}
