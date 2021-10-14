using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fim : MonoBehaviour
{

    AudioSource somVitoria;

    void Start()
    {
        /*
        *
        * ao iniciar, tocar a musica do fim
        *
        */
        somVitoria = GetComponent<AudioSource>();
        somVitoria.Play();
    }

    public void VoltarParaInicio()
    {
        /*
        *
        * metodo onclick do botao para voltar para o inicio
        *
        */
        SceneManager.LoadScene("Inicio");
    }
}
