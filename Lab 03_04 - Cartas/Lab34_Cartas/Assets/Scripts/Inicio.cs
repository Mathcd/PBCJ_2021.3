using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Inicio : MonoBehaviour
{

    AudioSource inicio;

    void Awake()
    {
        VerificarRecordeEUltimaPartida()
        GameObject.Find("recordeText").GetComponent<Text>().text = "Recorde de tentativas: " + PlayerPrefs.GetInt("Recorde");
        GameObject.Find("ultimaPartidaText").GetComponent<Text>().text = "Ultima partida: " + PlayerPrefs.GetInt("Jogadas");
    }

    // Start is called before the first frame update
    void Start()
    {
        inicio = GetComponent<AudioSource>();
        inicio.Play();
    }

    public VerificarRecordeEUltimaPartida()
    {
        if(!PlayerPrefs.HasKey("Recorde"))
        {
            PlayerPrefs.SetInt("Recorde", 0);
        }
        if(!PlayerPrefs.HasKey("Jogadas"))
        {
            PlayerPrefs.SetInt("Jogadas", 0);
        }
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
