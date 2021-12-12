using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour
{    
    public void startGame()
    {
        /* metodo para iniciar a tela do jogo propriamente dito */
        SceneManager.LoadScene("Dificuldade");
    }

    public void goToRegras()
    {
        /* metodo para ir para a tela de regras */
        SceneManager.LoadScene("Regras");
    }
}
