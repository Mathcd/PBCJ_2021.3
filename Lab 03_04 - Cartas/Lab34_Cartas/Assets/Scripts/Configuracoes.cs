using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Configuracoes : MonoBehaviour
{

    AudioSource inicio;

    void Start()
    {
        /*
        *
        * ao iniciar a tela atrelada a esse script, inicie a musica
        *
        */
        inicio = GetComponent<AudioSource>();
        inicio.Play();
    }

    public void SalvarEVoltarParaInicio()
    {
        /*
        *
        * verifica o modo de jogo, salva nas preferencias do usuario e volta para 
        * a tela inicial
        *
        */
        int modoDeJogo = GameObject.Find("Dropdown").GetComponent<Dropdown>().value;
        PlayerPrefs.SetInt("ModoDeJogo", modoDeJogo);
        SceneManager.LoadScene("Inicio");
    }

    public void ResetarPreferencias()
    {
        /*
        *
        * metodo onclick do botao de reset. ele vai deletar todas as preferencias do usuario
        * incluindo seu recorde e seu numero de jogadas da ultima partida
        * depois, o metodo seta tudo para zero para que haja o que mostrar na tela depois
        *
        */
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Recorde", 0);
        PlayerPrefs.SetInt("Jogadas", 0);
        PlayerPrefs.SetInt("ModoDeJogo", 0);
    }
}
