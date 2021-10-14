using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageCartas : MonoBehaviour
{
    public GameObject carta;                    // A carta a ser descartada
    private bool primeiraCartaSelecionada, segundaCartaSelecionada, terceiraCartaSelecionada, quartaCartaSelecionada; // indicadores para cada carta escolhida em cada linha
    private GameObject carta1, carta2, carta3, carta4; // gameObjects da 1ª e 2ª carta selecionada
    private string linhaCarta1, linhaCarta2, linhaCarta3, linhaCarta4;    // linha da carta selecionada

    bool timerPausado, timerAcionado;           // indicador de pausa no Timer ou Start Timer
    float timer;                                // variável de tempo

    int modoDeJogo = 0;                         // modo de jogo escolhido nas configuracoes
    int numTentativas = 0;                      // número de tentativas na rodada
    int numAcertos = 0;                         // número de match de pares acertados
    AudioSource somOK;                          // som de acerto
    AudioSource myLittleBrownBookMP3;           // Coltrane eh top demais se eh louco cachorro

    // Start is called before the first frame update
    void Start()
    {
        modoDeJogo = PlayerPrefs.GetInt("ModoDeJogo");
        MostraCartas();
        UpDateTentativas();
        somOK = GetComponent<AudioSource>();
        myLittleBrownBookMP3 = GetComponent<AudioSource>();
        myLittleBrownBookMP3.Play();
        GameObject.Find("ultimaJogada").GetComponent<Text>().text = "Ultima partida: " + PlayerPrefs.GetInt("Jogadas");
    }

    // Update is called once per frame
    void Update()
    {
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
        int[] arrayEmbaralhado1 = criaArrayEmbaralhado();
        int[] arrayEmbaralhado2 = criaArrayEmbaralhado();
        int[] arrayEmbaralhado3 = criaArrayEmbaralhado();
        int[] arrayEmbaralhado4 = criaArrayEmbaralhado();

        //Instantiate(carta, new Vector3(0, 0, 0), Quaternion.identity);
        //AddUmaCarta();
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
        GameObject centro = GameObject.Find("centroDaTela");
        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (650 * escalaCartaOriginal) / 110.0f;
        float fatorEscalaY = (945 * escalaCartaOriginal) / 110.0f;

        // Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13 / 2) * 1.5f), centro.transform.position.y, centro.transform.position.z);        
        Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13/2) * fatorEscalaX), centro.transform.position.y + ((linha - 2.0f/2.0f) * fatorEscalaY), centro.transform.position.z);

        // GameObject c = (GameObject)(Instantiate(carta, new Vector3(rank*1.5f, 0, 0), Quaternion.identity));
        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));

        c.tag = "" + (valor+1);
        c.name = "" + linha + "_" + valor;
        string nomeDaCarta = "";
        string numeroCarta = "";

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
        DisparaTimer();
        numTentativas++;
        UpDateTentativas();
    }

    public void DisparaTimer()
    {
        timerPausado = false;
        timerAcionado = true;
    }

    void UpDateTentativas()
    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = "Tentativas: " + numTentativas;
    }
}
