using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBackend;
using UnityEngine.UI;

/// <summary>
/// Essa classe é responsável pelos objetos "Tiles", bem como
/// pelas interações do mouse do jogador com os objetos do jogo,
/// por meio dos cliques nas tiles e nos botões. Junto com a classe
/// ManageCartas, Tile é responsável pelo front-end do jogo
/// </summary>
public class Tile : MonoBehaviour
{
    public Sprite originalCarta;    // frente da carta
    public Sprite backCarta;        // verso da carta

    // declarando outras classes para permitir o uso de suas funções aqui
    public ManageCartas manageCartasScript;
    public Main mainScript;


    // Start is called before the first frame update
    void Start()
    {
        RevelaCarta();

        // instaciando objetos scripts de outras classes para permitir o uso de suas funções aqui
        manageCartasScript = new ManageCartas();
        mainScript = new Main();
    }

    // Update is called once per frame
    void Update()
    {
        // exibe a carta
        RevelaCarta();
    }

    /* função responsável pelo funcionamento do botão "Passar a vez"
     * utilizado pelo jogador quando não tem carta para jogar, mesmo
     * após ter comprado */
    public void ButtonInteract()
    {
        // só pode passar a vez após ter comprado uma carta
        if(PlayerPrefs.GetInt("jogadorJaComprou") == 0)
        {
            Debug.Log("Jogador deve comprar uma carta antes de poder passar a vez");
            
            // exibe instruções na tela para o jogador
            GameObject.Find("Comentario").GetComponent<Text>().text = "Você deve comprar uma carta antes de poder passar a vez";
        }

        // caso já tenha comprado, o clique do botão passa a vez para a máquina
        else
        {   
            // atualiza flags de compra e vez
            PlayerPrefs.SetInt("jogadorJaComprou", 0);
            PlayerPrefs.SetInt("vezDoJogador", 0);
        }
    }

    /* essa função é executada sempre que houver clique do mouse;
     * após clicar no objeto, o identifica e atualiza as flags referentes
     * a esse objetos, utilizadas pela classe Main para manter o curso do game */
    public void OnMouseDown()
    {
        // recupera nome do objeto
        string clicked_obj = this.gameObject.name;
        Debug.Log("Você clicou no objeto " + this.gameObject.name);

        // processa o nome no objeto
        var tokens = clicked_obj.Split('_');

        // verifica se o clique foi na pilha de compra
        if (clicked_obj.Equals("Carta_TopoPilhaCompra"))
        {
            // se clicou na pilha de compra e ainda não comprou nessa rodada
            if (PlayerPrefs.GetInt("jogadorJaComprou") == 0)
            {
                // dispara compra
                PlayerPrefs.SetInt("triggerCompra", 1);
            }
            else
            {   
                // caso contrário, exibe informação na tela para o jogador passar a vez, se for o caso
                Debug.Log("Você já comprou nesta rodada. Caso não possua a carta compatível, passe a vez");
                GameObject.Find("Comentario").GetComponent<Text>().text = "Você já comprou nesta rodada. Caso não possua a carta compatível, passe a vez";
            }

        }
        // se clicar em objeto carta
        else if(tokens.Length == 3)
        {
            // e se essa carta for do Jogador
            if(tokens[1] == "J")
            {
                // recupera informações da carta clicada e da carta do topo da pilha de descarte
                int posicaoCartaAtual = int.Parse(tokens[2]);                               // carta clicada (posição na mão)
                int cartaAnteriorValor = PlayerPrefs.GetInt("cartaAnteriorValorPP");        // carta da pilha (valor)
                char cartaAnteriorTipo = PlayerPrefs.GetString("cartaAnteriorTipoPP")[0];   // carta da piha (tipo)
                int cartaAtualValor = PlayerPrefs.GetInt("carta"+ posicaoCartaAtual + "jogador"); // carta clicada (valor)

                /* verifica se a carta é compatível selecionada e compatível com a pilha; 
                 * se for, atualiza a flag para realizar o descarte no script Main */
                if(manageCartasScript.VerificaCartaCompativel(cartaAnteriorValor, cartaAnteriorTipo, cartaAtualValor)){
                    Debug.Log("Essa carta [" + cartaAtualValor + "] é COMPATÍVEL. Ela será jogada");
                    PlayerPrefs.SetInt("cartaDescartada", posicaoCartaAtual);
                }
            }
        }
    }

    /* função responsável por exibir a carta */
    public void RevelaCarta()
    {
        GetComponent<SpriteRenderer>().sprite = originalCarta;
    }

    /* função responsável por setar a frente das cartas */ 
    public void setCartaOriginal(Sprite novaCarta)
    {
        originalCarta = novaCarta;
    }



    
}
