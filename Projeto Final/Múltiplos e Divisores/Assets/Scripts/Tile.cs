using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBackend;

public class Tile : MonoBehaviour
{
    private bool tileRevelada = true;
    public Sprite originalCarta;
    public Sprite backCarta;

    public ManagePlayer managePlayerScript;
    public Main mainScript;


    // Start is called before the first frame update
    void Start()
    {
        RevelaCarta();

        managePlayerScript = new ManagePlayer();
        mainScript = new Main();
    }

    // Update is called once per frame
    void Update()
    {
        RevelaCarta();
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
        if (clicked_obj.Equals("Carta_TopoPilhaCompra") && Globals.JOGADOR_PODE_COMPRAR)
        {
            if (!Globals.JOGADOR_PODE_COMPRAR)
            {
                Debug.Log("Jogador acabou de comprar e deve jogar a carta comprada");
            }
            else
            {
                //// FUNÇÃO COMPRA CARTA
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

                if(managePlayerScript.VerificaCartaCompativel(cartaAnteriorValor, cartaAnteriorTipo, cartaAtualValor)){
                    Debug.Log("Essa carta é COMPATÍVEL");
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
