using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBackend;

public class Tile : MonoBehaviour
{
    private bool tileRevelada = true;
    public Sprite originalCarta;
    public Sprite backCarta;

    // Start is called before the first frame update
    void Start()
    {
        RevelaCarta();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /* essa função é executada sempre que houver clique do mouse */
    public void OnMouseDown()
    {
        Debug.Log("Você clicou no objeto " + this.gameObject.name);
        //Debug.Log(this.gameObject.name);

        /*
        string clicked_obj = this.gameObject.name;

        // processa o click no objeto
        var tokens = clicked_obj.Split('_');

        // se o clique foi na pilha de compra, retorna -1
        bool compra = clicked_obj.Equals("Carta_TopoPilhaCompra");
        if (compra) {
            Globals.MOUSE_CLICK = -1;
        }

        else if (tokens[1] == "J") {
            Globals.MOUSE_CLICK = int.Parse(tokens[2]);
        }

        //Globals.EM_ESPERA = false;
        /*
        if (tileRevelada)
        {
            EscondeCarta();
        }
        else
        {
            RevelaCarta();
        }
        */


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
