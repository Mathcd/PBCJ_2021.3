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

    /* as 15 cartas do jogador e da máquina são dispostas na tela em um grid 3x5;
     * essa função define a posição e renderiza o grid de cartas do jogador e
     * da máquina (uma carta por chamada) */
    public void AddUmaCarta(char player, int posicao, char tipoCarta, int valorCarta)
    {
        // centro da tela como refência
        GameObject centro = GameObject.Find("centroDaTela");
        
        // indicadores de posição de cada carta
        int linha = posicao / 5;
        int coluna = 0;

        // se forem cartas do Jogador, insere do lado direito da tela
        if (player == 'J')
            coluna = posicao % 5;
        // caso contrário, insere do lado esquerdo (i.e., deslocado em 6 posições para esquerda)
        else
            coluna = posicao % 5 - 6;

        // define os fatores de escala
        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (662 * escalaCartaOriginal) / 100.0f;
        float fatorEscalaY = (936 * escalaCartaOriginal) / 100.0f;

        // vetor de posição da figura na tela
        Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((1.15f*coluna + 1.5f) * fatorEscalaX), 
                                          centro.transform.position.y + ((1.15f*linha - 1.95f) * fatorEscalaY), 
                                          centro.transform.position.z);

        // instancia o GameObject da carta
        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));

        /* define o nome da carta no formato "Carta_X_#", 
         * onde X = 'J' para cartas do jogador e 'M' para máquina
         * e # é a posição da carta não mão (grid), variando de 0 a 14 */
        c.name = "Carta_" + player + "_" + posicao;

        string nomeDaCarta = "";
        string numeroCarta = "" + valorCarta;

        /* recupera o nome do arquivo das cartas no projeto, no formato "X-#"
         * onde X = 'M' para cartas 'múltiplo', 'D' para cartas 'divisor' e
         * 'A' para cartas 'MD' (i.e. ambos, múltiplo e divisor) */
        nomeDaCarta = tipoCarta + "-" + valorCarta;

        // se o modo de jogo for "fácil", mostra a frente das cartas da máquina
        if (PlayerPrefs.GetInt("modoJogo") == 0)
        {
            // carrega o Sprite da carta e atualiza na tile correta
            Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));
            GameObject.Find("Carta_" + player + "_" + posicao).GetComponent<Tile>().setCartaOriginal(s1);
        }

        // se o modo de jogo for "difícil", exibe apenas o verso das cartas da máquina
        else
        {
            if(player == 'M')
            {
                // carrega o Sprite "verso" das cartas e atualiza nas tiles da máquina
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

    /* função responsável por renderizar a pilha de compra;
     * é renderizada o apenas o verso carta do top da pilha */
    public void MostraPilhaCompra()
    {
        // centro da tela como refência
        GameObject centro = GameObject.Find("centroDaTela");

        // define os fatores de escala
        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (662 * escalaCartaOriginal) / 100.0f;
        float fatorEscalaY = (936 * escalaCartaOriginal) / 100.0f;

        // vetor de posição da figura na tela
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
        // centro da tela como refência
        GameObject centro = GameObject.Find("centroDaTela");

        // define os fatores de escala
        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (662 * escalaCartaOriginal) / 100.0f;
        float fatorEscalaY = (936 * escalaCartaOriginal) / 100.0f;

        // vetor de posição da figura na tela
        Vector3 novaPosicao = new Vector3(centro.transform.position.x + 0.46f,
                                          centro.transform.position.y + (0 * fatorEscalaY - 0.25f),
                                          centro.transform.position.z);

        // instancia o GameObject da carta
        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));

        // define o nome da carta
        c.name = "Carta_TopoPilhaDescarte";

        /* recupera o nome do arquivo das cartas no projeto, no formato "X-#"
         * onde X = 'M' para cartas 'múltiplo', 'D' para cartas 'divisor' e
         * 'A' para cartas 'MD' (i.e. ambos, múltiplo e divisor) */
        string nomeDaCarta = tipoCarta + "-" + valorCarta;

        // carrega o Sprite da carta e atualiza na tile correta
        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));
        GameObject.Find("Carta_TopoPilhaDescarte").GetComponent<Tile>().setCartaOriginal(s1);
    }

    /* essa função verifica se a carta selecionada (atual) e compatível com a pilha de descarte (anterior)
     * de acordo com seus valores e tipo (múltiplo, divisor ou ambos); retorna 'true' se forem compatívels
     * ou 'false' caso contrário */
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
