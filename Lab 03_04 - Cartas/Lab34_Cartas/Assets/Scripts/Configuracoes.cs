using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Configuracoes : MonoBehaviour
{

    AudioSource inicio;

    // Start is called before the first frame update
    void Start()
    {
        inicio = GetComponent<AudioSource>();
        inicio.Play();
    }

    public void SalvarEVoltarParaInicio()
    {
        int modoDeJogo = GameObject.Find("Dropdown").GetComponent<Dropdown>().value;
        PlayerPrefs.SetInt("ModoDeJogo", modoDeJogo);
        SceneManager.LoadScene("Inicio");
    }

    public void ResetarPreferencias()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Recorde", 0);
        PlayerPrefs.SetInt("Jogadas", 0);
        PlayerPrefs.SetInt("ModoDeJogo", 0);
    }
}
