using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool tileRevelada = false;      // indicador da carta virada ou não
    public Sprite originalCarta;            // Sprite da carta desejada
    public Sprite backCarta;                // Sprite do avesso da carta
    public Sprite backCarta2;
    public int fundoAzulOuVermelho;

    void Start()
    {
        /*
        *
        * ao criar um objeto dessa classe, esconda-o (eh uma carta)
        *
        */
        EscondeCarta();
    }

    public void OnMouseDown()
    {
        /*
        *
        * Ao clicar numa carta, roda o metodo CartaSelecionada de ManageCartas
        *
        */
        GameObject.Find("gameManager").GetComponent<ManageCartas>().CartaSelecionada(gameObject);
    }

    public void EscondeCarta()
    {
        /*
        *
        * quando for pra esconder a carta, precisa apenas verificar se o fundo deve ser vermelho ou azul
        *
        */
        if (fundoAzulOuVermelho == 0){
            GetComponent<SpriteRenderer>().sprite = backCarta;
        } else {
            GetComponent<SpriteRenderer>().sprite = backCarta2;
        }
        tileRevelada = false;
    }

    public void RevelaCarta()
    {
        /*
        *
        * revela a carta na tela
        *
        */
        GetComponent<SpriteRenderer>().sprite = originalCarta;
        tileRevelada = true;
    }

    public void setCartaOriginal(Sprite novaCarta, int idFundo)
    {
        /*
        *
        * seta a carta e seu fundo (vermelho ou azul)
        *
        */
        originalCarta = novaCarta;
        fundoAzulOuVermelho = idFundo;
    }

}
