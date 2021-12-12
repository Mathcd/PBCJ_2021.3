using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBackend;
using UnityEngine.UI;

public class Tile : MonoBehaviour
{
    private bool tileRevelada = true;
    public Sprite originalCarta;
    public Sprite backCarta;

    public ManageCartas manageCartasScript;
    public Main mainScript;


    // Start is called before the first frame update
    void Start()
    {
        RevelaCarta();

        manageCartasScript = new ManageCartas();
        mainScript = new Main();
    }

    // Update is called once per frame
    void Update()
    {
        RevelaCarta();
    }

    public void ButtonInteract()
    {
        if(PlayerPrefs.GetInt("jogadorJaComprou") == 0)
        {
            Debug.Log("Jogador deve comprar uma carta antes de poder passar a vez");
            GameObject.Find("Comentario").GetComponent<Text>().text = "Você deve comprar uma carta antes de poder passar a vez";
        }
        else
        {
            PlayerPrefs.SetInt("jogadorJaComprou", 0);
            PlayerPrefs.SetInt("vezDoJogador", 0);
        }
    }

    /* essa função é executada sempre que houver clique do mouse */
    public void OnMouseDown()
    {
        Debug.Log("Você clicou no objeto " + this.gameObject.name);
        //Debug.Log(this.gameObject.name);

        
        string clicked_obj = this.gameObject.name;

        // processa o click no objeto
        var tokens = clicked_obj.Split('_');

        // se clicar na pilha de compra, compra carta
        if (clicked_obj.Equals("Carta_TopoPilhaCompra"))
        {
            if (PlayerPrefs.GetInt("jogadorJaComprou") == 0)
            {
                // dispara compra
                PlayerPrefs.SetInt("triggerCompra", 1);
            }
            else
            {
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
                int posicaoCartaAtual = int.Parse(tokens[2]);
                int cartaAnteriorValor = PlayerPrefs.GetInt("cartaAnteriorValorPP");
                char cartaAnteriorTipo = PlayerPrefs.GetString("cartaAnteriorTipoPP")[0];

                Debug.Log("Essa eh a carta anterior " + cartaAnteriorTipo + cartaAnteriorValor);
                int cartaAtualValor = PlayerPrefs.GetInt("carta"+ posicaoCartaAtual + "jogador");

                Debug.Log("Esse eh o valor da carta atual: " + cartaAtualValor);
                Debug.Log("Essa eh a posicão da carta atual: " + posicaoCartaAtual);

                if(manageCartasScript.VerificaCartaCompativel(cartaAnteriorValor, cartaAnteriorTipo, cartaAtualValor)){
                    Debug.Log("Essa carta[" + cartaAtualValor + "]é COMPATÍVEL. Ela será jogada");
                    PlayerPrefs.SetInt("cartaDescartada", posicaoCartaAtual);
                }
                //// VERIFICA SE A CARTA EH COMPATIVEL;
                //// SE FOR, JOGA CARTA


            }
        }

        

    }

    public void EscondeCarta()
    {
        GetComponent<SpriteRenderer>().sprite = backCarta;
        tileRevelada = false;
    }

    public void RevelaCarta()
    {
        GetComponent<SpriteRenderer>().sprite = originalCarta;
        //tileRevelada = true;
    }

    public void setCartaOriginal(Sprite novaCarta)
    {
        originalCarta = novaCarta;
    }



    
}
