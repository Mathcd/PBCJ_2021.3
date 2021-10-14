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

    // Start is called before the first frame update
    void Start()
    {
        EscondeCarta();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        print("Vou pressionou num Tile");
        /*if (tileRevelada)
        {
            EscondeCarta();
        }
        else
        {
            RevelaCarta();
        }*/                     // aqui não se guardava número de cartas

        GameObject.Find("gameManager").GetComponent<ManageCartas>().CartaSelecionada(gameObject);

    }

    public void EscondeCarta()
    {
        if (fundoAzulOuVermelho == 0){
            GetComponent<SpriteRenderer>().sprite = backCarta;
        } else {
            GetComponent<SpriteRenderer>().sprite = backCarta2;
        }
        tileRevelada = false;
    }

    public void RevelaCarta()
    {
        GetComponent<SpriteRenderer>().sprite = originalCarta;
        tileRevelada = true;
    }

    public void setCartaOriginal(Sprite novaCarta, int idFundo)
    {
        originalCarta = novaCarta;
        fundoAzulOuVermelho = idFundo;
    }


}
