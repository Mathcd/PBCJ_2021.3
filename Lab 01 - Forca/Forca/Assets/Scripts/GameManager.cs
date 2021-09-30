using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int numTentativas;              // Armazena as tentativas válidas da rodada
    private int maxNumTentativas;           // Número máximo de tentativas para Forca ou Salvação
    int score = 0;

    public GameObject letra;                // prefab da letra no Game
    public GameObject centro;               // objeto de texto que indica o centro da tela

    private string palavraOculta = "";      // palavra oculta a ser descoberta
    // private string[] palavrasOcultas = new string[] {"carro", "elefante", "futebol"}; // array de palavras ocultas (Lab2 - Parte A)
    
    private int tamanhoPalavraOculta;       // tamanho da palavra oculta
    char[] letrasOcultas;                   // letras da palavra oculta
    bool[] letrasDescobertas;               // indicador de quais letras foram descobertas

    // Start is called before the first frame update
    void Start()
    {
        centro = GameObject.Find("centroDaTela");
        InitGame();
        InitLetras();
        numTentativas = 0;
        maxNumTentativas = 10;
        UpdateNumTentativas();
        UpdateScore();
    }

    // Update is called once per frame
    void Update()
    {
       checkTeclado(); 
    }

    void InitLetras()
    {
        int numLetras = tamanhoPalavraOculta;
        for (int i=0; i<numLetras; i++){
            Vector3 novaPosicao;
            novaPosicao = new Vector3(centro.transform.position.x + ((i-numLetras/2.0f)*80), centro.transform.position.y, transform.position.z);
            GameObject l = (GameObject)Instantiate(letra, novaPosicao, Quaternion.identity);
            l.name = "letra" + (i + 1);         // nomeia na hierarquia a GameObject com letra-(iésima+1), i = 1..numLetras
            l.transform.SetParent(GameObject.Find("Canvas").transform); // posiciona-se como filho do GameObject Canvas
        }
    }

    void InitGame()
    {
        // palavraOculta = "Elefante";                             // definição da palavra oculta a ser descoberta (Lab1 - Parte A)
        // int numeroAleatorio = Random.Range(0, palavrasOcultas.Length); // sorteamos um número dentro do número de palavras do array (Lab2 - Parte A)
        // palavraOculta = palavrasOcultas[numeroAleatorio];       // selecionamos uma palavra sorteada

        palavraOculta = PegaUmaPalavraDoArquivo();

        tamanhoPalavraOculta = palavraOculta.Length;            // determina-se o número de letras da palavra oculta
        palavraOculta = palavraOculta.ToUpper();                // transforma-se a palavra em maiúscula
        letrasOcultas = new char[tamanhoPalavraOculta];         // instanciamos o array char das letras da palavra
        letrasDescobertas = new bool[tamanhoPalavraOculta];     // instaciamos o array bool do indicador de letras certas
        letrasOcultas = palavraOculta.ToCharArray();            // copia-se a palavra no array de letras
    }

    // função para captura das letras digitadas no teclado
    void checkTeclado()
    {
        if(Input.anyKeyDown)
        {
            char letraTeclada = Input.inputString.ToCharArray()[0];         // obtém a letra digitada
            int letraTecladaComoInt = System.Convert.ToInt32(letraTeclada);

            if(letraTecladaComoInt >= 97 && letraTecladaComoInt <= 122)
            {
                numTentativas++;        // incrementa o número de tentativas
                UpdateNumTentativas();

                if(numTentativas > maxNumTentativas)    // caso exceda o limite de tentativas, vai para tela de "enforcado"
                {
                    SceneManager.LoadScene("Lab1_forca");
                }

                for (int i=0; i<=tamanhoPalavraOculta; i++)
                {
                    if (!letrasDescobertas[i])
                    {
                        letraTeclada = System.Char.ToUpper(letraTeclada);   // converte para letras maiúsculas
                        if (letrasOcultas[i] == letraTeclada)
                        {
                            letrasDescobertas[i] = true;
                            GameObject.Find("letra" + (i+1)).GetComponent<Text>().text = letraTeclada.ToString();
                            score = PlayerPrefs.GetInt("score");
                            score++;
                            PlayerPrefs.SetInt("score", score);
                            UpdateScore();
                            VerificaSePalavraDescoberta();
                        }
                    }
                }
            }
        }
    }

    // atualiza o contador de tentativas após cada letra ser digitada
    void UpdateNumTentativas()
    {
        GameObject.Find("numTentativas").GetComponent<Text>().text = numTentativas + " | " + maxNumTentativas;
    }

    // atualiza o contador de letras acertadas
    void UpdateScore()
    {
        GameObject.Find("scoreUI").GetComponent<Text>().text = "Score " + score;
    }

    // verifica constantemente se todas as letras de uma palavra foram descobertas
    void VerificaSePalavraDescoberta()
    {
        bool condicao = true;

        for(int i = 0; i < tamanhoPalavraOculta; i++) // varre cada uma das letras da palavra
        {
            condicao = condicao && letrasDescobertas[i];
        }
        if(condicao)            // caso a palavra tenha sido descoberta, direciona para a tela de vitória
        {
            PlayerPrefs.SetString("ultimaPalavraOculta", palavraOculta);
            SceneManager.LoadScene("Lab1_salvo");
        }
    }

    // seleciona aleatoriamente uma das palavras do arquivo "palavras1" para o game
    string PegaUmaPalavraDoArquivo()
    {
        TextAsset t1 = (TextAsset)Resources.Load("palavras1", typeof(TextAsset));
        string s = t1.text;
        string[] palavras = s.Split(' ');
        int palavraAleatoria = Random.Range(0, palavras.Length - 1);
        return (palavras[palavraAleatoria]);
    }
}
