using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inicio : MonoBehaviour
{

    AudioSource inicio;

    // Start is called before the first frame update
    void Start()
    {
        inicio = GetComponent<AudioSource>();
        inicio.Play();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Recorde", 0);
        PlayerPrefs.SetInt("Jogadas", 0);
        GameObject.Find("recordeText").GetComponent<Text>().text = "Recorde de tentativas: " + PlayerPrefs.GetInt("Recorde");   
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("recordeText").GetComponent<Text>().text = "Recorde de tentativas: " + PlayerPrefs.GetInt("Recorde");
    }

    public void StartMundoGame()
    {
        SceneManager.LoadScene("Lab3");
    }

    public void IrParaConfiguracoes()
    {
        SceneManager.LoadScene("Configuracoes");
    }
}
