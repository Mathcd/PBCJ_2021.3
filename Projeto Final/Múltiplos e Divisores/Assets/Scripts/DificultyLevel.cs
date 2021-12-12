using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe responsável pela seleção do modo de jogo (fácil ou difícil)
/// </summary>
public class DificultyLevel : MonoBehaviour
{
    /* método para executar o jogo no modo fácil */
    public void PlayEasy()
    {
        PlayerPrefs.SetInt("modoJogo", 0);
        SceneManager.LoadScene("Desenvolvimento");
    }

    /* método para executar o jogo no modo difícil */
    public void PlayHard()
    {
        PlayerPrefs.SetInt("modoJogo", 1);
        SceneManager.LoadScene("Desenvolvimento");
    }
}
