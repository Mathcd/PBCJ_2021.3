using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Classe respons�vel pela sele��o do modo de jogo (f�cil ou dif�cil)
/// </summary>
public class DificultyLevel : MonoBehaviour
{
    /* m�todo para executar o jogo no modo f�cil */
    public void PlayEasy()
    {
        PlayerPrefs.SetInt("modoJogo", 0);
        SceneManager.LoadScene("Desenvolvimento");
    }

    /* m�todo para executar o jogo no modo dif�cil */
    public void PlayHard()
    {
        PlayerPrefs.SetInt("modoJogo", 1);
        SceneManager.LoadScene("Desenvolvimento");
    }
}
