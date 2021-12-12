using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe responsável pela tela de início do jogo
/// </summary>
public class Inicio : MonoBehaviour
{
    /* método para iniciar a tela de seleção do modo de jogo (fácil ou difícil) */
    public void startGame()
    {   
        SceneManager.LoadScene("Dificuldade");
    }

    /* método para ir para a tela de regras */
    public void goToRegras()
    {
        SceneManager.LoadScene("Regras");
    }
}
