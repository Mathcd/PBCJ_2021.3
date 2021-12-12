using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe respons�vel pela tela de in�cio do jogo
/// </summary>
public class Inicio : MonoBehaviour
{
    /* m�todo para iniciar a tela de sele��o do modo de jogo (f�cil ou dif�cil) */
    public void startGame()
    {   
        SceneManager.LoadScene("Dificuldade");
    }

    /* m�todo para ir para a tela de regras */
    public void goToRegras()
    {
        SceneManager.LoadScene("Regras");
    }
}
