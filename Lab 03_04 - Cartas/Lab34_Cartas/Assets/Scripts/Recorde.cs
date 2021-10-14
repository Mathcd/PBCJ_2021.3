using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Recorde : MonoBehaviour
{
    AudioSource ahlelek;
    
    void Start()
    {
        /*
        *
        * ao iniciar a tela atrelada a essse scriot, toque a musica
        *
        */
        ahlelek = GetComponent<AudioSource>();
        ahlelek.Play();   
    }

    public void VoltarParaInicio()
    {
        /*
        *
        * metodo onclick do botao para voltar para a tela inicial
        *
        */
        SceneManager.LoadScene("Inicio");
    }
}
