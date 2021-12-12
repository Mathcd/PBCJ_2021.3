using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DificultyLevel : MonoBehaviour
{
    public void PlayEasy()
    {
        PlayerPrefs.SetInt("modoJogo", 0);
        SceneManager.LoadScene("Desenvolvimento");
    }

    public void PlayHard()
    {
        PlayerPrefs.SetInt("modoJogo", 1);
        SceneManager.LoadScene("Desenvolvimento");
    }
}
