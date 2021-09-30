using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// classe para gerenciar o funcionamento dos botões responsáveis pelas transições de telas do game
public class ManageBotoes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("score", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // função para iniciar a scene da tela principal do game
    public void StartMundoGame()
    {
        SceneManager.LoadScene("Lab1");
    }

    // função para iniciar a tela de game over e créditos
    public void EndMundoGame()
    {
        SceneManager.LoadScene("Lab1_creditos");
    }
}
