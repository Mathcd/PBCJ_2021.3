using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class ManageCartas : MonoBehaviour
{
    public GameObject carta;    // A carta a ser descartada
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* as 15 cartas do jogador e da m�quina s�o dispostas na tela em um grid 3x5;
     * essa fun��o define a posi��o e renderiza o grid de cartas do jogador e
     * da m�quina (uma carta por chamada) */
    public void AddUmaCarta(char player, int posicao, char tipoCarta, int valorCarta)
    {
        // centro da tela como ref�ncia
        GameObject centro = GameObject.Find("centroDaTela");
        
        // indicadores de posi��o de cada carta
        int linha = posicao / 5;
        int coluna = 0;

        // se forem cartas do Jogador, insere do lado direito da tela
        if (player == 'J')
            coluna = posicao % 5;
        // caso contr�rio, insere do lado esquerdo (i.e., deslocado em 6 posi��es para esquerda)
        else
            coluna = posicao % 5 - 6;

        // define os fatores de escala
        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (662 * escalaCartaOriginal) / 100.0f;
        float fatorEscalaY = (936 * escalaCartaOriginal) / 100.0f;

        // vetor de posi��o da figura na tela
        Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((1.15f*coluna + 1.5f) * fatorEscalaX), 
                                          centro.transform.position.y + ((1.15f*linha - 1.95f) * fatorEscalaY), 
                                          centro.transform.position.z);

        // instancia o GameObject da carta
        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));

        /* define o nome da carta no formato "Carta_X_#", 
         * onde X = 'J' para cartas do jogador e 'M' para m�quina
         * e # � a posi��o da carta n�o m�o (grid), variando de 0 a 14 */
        c.name = "Carta_" + player + "_" + posicao;

        string nomeDaCarta = "";
        string numeroCarta = "" + valorCarta;

        /* recupera o nome do arquivo das cartas no projeto, no formato "X-#"
         * onde X = 'M' para cartas 'm�ltiplo', 'D' para cartas 'divisor' e
         * 'A' para cartas 'MD' (i.e. ambos, m�ltiplo e divisor) */
        nomeDaCarta = tipoCarta + "-" + valorCarta;

        // se o modo de jogo for "f�cil", mostra a frente das cartas da m�quina
        if (PlayerPrefs.GetInt("modoJogo") == 0)
        {
            // carrega o Sprite da carta e atualiza na tile correta
            Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));
            GameObject.Find("Carta_" + player + "_" + posicao).GetComponent<Tile>().setCartaOriginal(s1);
        }

        // se o modo de jogo for "dif�cil", exibe apenas o verso das cartas da m�quina
        else
        {
            if(player == 'M')
            {
                // carrega o Sprite "verso" das cartas e atualiza nas tiles da m�quina
                Sprite s1 = (Sprite)(Resources.Load<Sprite>("verso"));
                GameObject.Find("Carta_" + player + "_" + posicao).GetComponent<Tile>().setCartaOriginal(s1);
            }
            else
            {
                // carrega o Sprite da carta e atualiza na tile correta
                Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));
                GameObject.Find("Carta_" + player + "_" + posicao).GetComponent<Tile>().setCartaOriginal(s1);
            }
        }

        
        
    }

    /* fun��o respons�vel por renderizar a pilha de compra;
     * � renderizada o apenas o verso carta do top da pilha */
    public void MostraPilhaCompra()
    {
        // centro da tela como ref�ncia
        GameObject centro = GameObject.Find("centroDaTela");

        // define os fatores de escala
        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (662 * escalaCartaOriginal) / 100.0f;
        float fatorEscalaY = (936 * escalaCartaOriginal) / 100.0f;

        // vetor de posi��o da figura na tela
        Vector3 novaPosicao = new Vector3(centro.transform.position.x + 4.46f, 
                                          centro.transform.position.y + (1.2f * fatorEscalaY + 1.2f), 
                                          centro.transform.position.z);

        // instancia o GameObject da carta
        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));

        // define o nome da carta
        c.name = "Carta_TopoPilhaCompra";

        // obtem o nome do arquivo da carta ("verso")
        string nomeDaCarta = "verso";

        // carrega o Sprite da carta e atualiza na tile correta
        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));
        GameObject.Find("Carta_TopoPilhaCompra").GetComponent<Tile>().setCartaOriginal(s1);
    }



    public void MostraPilhaDescarte(char tipoCarta, int valorCarta)
    {
        // centro da tela como ref�ncia
        GameObject centro = GameObject.Find("centroDaTela");

        // define os fatores de escala
        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (662 * escalaCartaOriginal) / 100.0f;
        float fatorEscalaY = (936 * escalaCartaOriginal) / 100.0f;

        // vetor de posi��o da figura na tela
        Vector3 novaPosicao = new Vector3(centro.transform.position.x + 0.46f,
                                          centro.transform.position.y + (0 * fatorEscalaY - 0.25f),
                                          centro.transform.position.z);

        // instancia o GameObject da carta
        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));

        // define o nome da carta
        c.name = "Carta_TopoPilhaDescarte";

        /* recupera o nome do arquivo das cartas no projeto, no formato "X-#"
         * onde X = 'M' para cartas 'm�ltiplo', 'D' para cartas 'divisor' e
         * 'A' para cartas 'MD' (i.e. ambos, m�ltiplo e divisor) */
        string nomeDaCarta = tipoCarta + "-" + valorCarta;

        // carrega o Sprite da carta e atualiza na tile correta
        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));
        GameObject.Find("Carta_TopoPilhaDescarte").GetComponent<Tile>().setCartaOriginal(s1);
    }

    /* essa fun��o verifica se a carta selecionada (atual) e compat�vel com a pilha de descarte (anterior)
     * de acordo com seus valores e tipo (m�ltiplo, divisor ou ambos); retorna 'true' se forem compat�vels
     * ou 'false' caso contr�rio */
    public bool VerificaCartaCompativel(int cartaAnteriorValor, char cartaAnteriorTipo, int cartaAtualValor)
    {
        // switch no tipo de carta
        switch (cartaAnteriorTipo)
        {
            case 'M':
                if (cartaAtualValor % cartaAnteriorValor == 0)
                {
                    return true;
                }
                break;
            case 'D':
                if (cartaAnteriorValor % cartaAtualValor == 0)
                {
                    return true;
                }
                break;
            case 'A':
                if (cartaAnteriorValor % cartaAtualValor == 0 | cartaAtualValor % cartaAnteriorValor == 0)
                {
                    return true;
                }
                break;
        }
        return false;
    }
}
