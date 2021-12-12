using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBackend;
using UnityEngine.UI;

/// <summary>
/// Essa classe � respons�vel pelos objetos "Tiles", bem como
/// pelas intera��es do mouse do jogador com os objetos do jogo,
/// por meio dos cliques nas tiles e nos bot�es. Junto com a classe
/// ManageCartas, Tile � respons�vel pelo front-end do jogo
/// </summary>
public class Tile : MonoBehaviour
{
    public Sprite originalCarta;    // frente da carta
    public Sprite backCarta;        // verso da carta

    // declarando outras classes para permitir o uso de suas fun��es aqui
    public ManageCartas manageCartasScript;
    public Main mainScript;


    // Start is called before the first frame update
    void Start()
    {
        RevelaCarta();

        // instaciando objetos scripts de outras classes para permitir o uso de suas fun��es aqui
        manageCartasScript = new ManageCartas();
        mainScript = new Main();
    }

    // Update is called once per frame
    void Update()
    {
        // exibe a carta
        RevelaCarta();
    }

    /* fun��o respons�vel pelo funcionamento do bot�o "Passar a vez"
     * utilizado pelo jogador quando n�o tem carta para jogar, mesmo
     * ap�s ter comprado */
    public void ButtonInteract()
    {
        // s� pode passar a vez ap�s ter comprado uma carta
        if(PlayerPrefs.GetInt("jogadorJaComprou") == 0)
        {
            Debug.Log("Jogador deve comprar uma carta antes de poder passar a vez");
            
            // exibe instru��es na tela para o jogador
            GameObject.Find("Comentario").GetComponent<Text>().text = "Voc� deve comprar uma carta antes de poder passar a vez";
        }

        // caso j� tenha comprado, o clique do bot�o passa a vez para a m�quina
        else
        {   
            // atualiza flags de compra e vez
            PlayerPrefs.SetInt("jogadorJaComprou", 0);
            PlayerPrefs.SetInt("vezDoJogador", 0);
        }
    }

    /* essa fun��o � executada sempre que houver clique do mouse;
     * ap�s clicar no objeto, o identifica e atualiza as flags referentes
     * a esse objetos, utilizadas pela classe Main para manter o curso do game */
    public void OnMouseDown()
    {
        // recupera nome do objeto
        string clicked_obj = this.gameObject.name;
        Debug.Log("Voc� clicou no objeto " + this.gameObject.name);

        // processa o nome no objeto
        var tokens = clicked_obj.Split('_');

        // verifica se o clique foi na pilha de compra
        if (clicked_obj.Equals("Carta_TopoPilhaCompra"))
        {
            // se clicou na pilha de compra e ainda n�o comprou nessa rodada
            if (PlayerPrefs.GetInt("jogadorJaComprou") == 0)
            {
                // dispara compra
                PlayerPrefs.SetInt("triggerCompra", 1);
            }
            else
            {   
                // caso contr�rio, exibe informa��o na tela para o jogador passar a vez, se for o caso
                Debug.Log("Voc� j� comprou nesta rodada. Caso n�o possua a carta compat�vel, passe a vez");
                GameObject.Find("Comentario").GetComponent<Text>().text = "Voc� j� comprou nesta rodada. Caso n�o possua a carta compat�vel, passe a vez";
            }

        }
        // se clicar em objeto carta
        else if(tokens.Length == 3)
        {
            // e se essa carta for do Jogador
            if(tokens[1] == "J")
            {
                // recupera informa��es da carta clicada e da carta do topo da pilha de descarte
                int posicaoCartaAtual = int.Parse(tokens[2]);                               // carta clicada (posi��o na m�o)
                int cartaAnteriorValor = PlayerPrefs.GetInt("cartaAnteriorValorPP");        // carta da pilha (valor)
                char cartaAnteriorTipo = PlayerPrefs.GetString("cartaAnteriorTipoPP")[0];   // carta da piha (tipo)
                int cartaAtualValor = PlayerPrefs.GetInt("carta"+ posicaoCartaAtual + "jogador"); // carta clicada (valor)

                /* verifica se a carta � compat�vel selecionada e compat�vel com a pilha; 
                 * se for, atualiza a flag para realizar o descarte no script Main */
                if(manageCartasScript.VerificaCartaCompativel(cartaAnteriorValor, cartaAnteriorTipo, cartaAtualValor)){
                    Debug.Log("Essa carta [" + cartaAtualValor + "] � COMPAT�VEL. Ela ser� jogada");
                    PlayerPrefs.SetInt("cartaDescartada", posicaoCartaAtual);
                }
            }
        }
    }

    /* fun��o respons�vel por exibir a carta */
    public void RevelaCarta()
    {
        GetComponent<SpriteRenderer>().sprite = originalCarta;
    }

    /* fun��o respons�vel por setar a frente das cartas */ 
    public void setCartaOriginal(Sprite novaCarta)
    {
        originalCarta = novaCarta;
    }



    
}
