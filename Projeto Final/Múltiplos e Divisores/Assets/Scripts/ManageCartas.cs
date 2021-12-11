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
        //MostraCartas();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    public void MostraCartas(char player, int posicao, char tipoCarta, int valorCarta)
    {
        AddUmaCarta(player, posicao, tipoCarta, valorCarta);
    }*/


    public void AddUmaCarta(char player, int posicao, char tipoCarta, int valorCarta)
    {
        //Debug.Log(codCarta);
        GameObject centro = GameObject.Find("centroDaTela");
        
        int linha = posicao / 5;
        int coluna = 0;

        // se forem cartas do Jogador, insere do lado direito da tela
        if (player == 'J')
            coluna = posicao % 5;
        // caso contrário, insere do lado esquerdo (i.e., deslocado em 6 posições para esquerda)
        else
            coluna = posicao % 5 - 6;

        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (662 * escalaCartaOriginal) / 100.0f;
        float fatorEscalaY = (936 * escalaCartaOriginal) / 100.0f;

        Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((1.15f*coluna + 1.5f) * fatorEscalaX), 
                                          centro.transform.position.y + ((1.15f*linha - 1.5f) * fatorEscalaY), 
                                          centro.transform.position.z);

        

        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));
        c.name = "Carta_" + player + "_" + posicao;

        string nomeDaCarta = "";
        string numeroCarta = "" + valorCarta;

        
        nomeDaCarta = tipoCarta + "-" + valorCarta;

        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));
        GameObject.Find("Carta_" + player + "_" + posicao).GetComponent<Tile>().setCartaOriginal(s1);
    }

    public void MostraPilhaCompra()
    {
        GameObject centro = GameObject.Find("centroDaTela");

        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (662 * escalaCartaOriginal) / 100.0f;
        float fatorEscalaY = (936 * escalaCartaOriginal) / 100.0f;

        Vector3 novaPosicao = new Vector3(centro.transform.position.x, 
                                          centro.transform.position.y + (1.2f * fatorEscalaY), 
                                          centro.transform.position.z);

        

        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));
        c.name = "Carta_TopoPilhaCompra";

        string nomeDaCarta = "verso";

        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));
        GameObject.Find("Carta_TopoPilhaCompra").GetComponent<Tile>().setCartaOriginal(s1);
    }



    public void MostraPilhaDescarte(char tipoCarta, int valorCarta)
    {
        GameObject centro = GameObject.Find("centroDaTela");

        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (662 * escalaCartaOriginal) / 100.0f;
        float fatorEscalaY = (936 * escalaCartaOriginal) / 100.0f;

        Vector3 novaPosicao = new Vector3(centro.transform.position.x,
                                          centro.transform.position.y + (0 * fatorEscalaY),
                                          centro.transform.position.z);



        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));
        c.name = "Carta_TopoPilhaDescarte";

        string nomeDaCarta = tipoCarta + "-" + valorCarta;

        Sprite s1 = (Sprite)(Resources.Load<Sprite>(nomeDaCarta));
        GameObject.Find("Carta_TopoPilhaDescarte").GetComponent<Tile>().setCartaOriginal(s1);
    }

    
}
