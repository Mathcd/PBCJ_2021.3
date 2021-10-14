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

    // Update is called once per frame
    void Update()
    {
    }

    public void SalvarEVoltarParaInicio()
    {
        int modoDeJogo = GameObject.Find("Dropdown").GetComponent<Dropdown>().value;
        PlayerPrefs.SetInt("ModoDeJogo", modoDeJogo);
        SceneManager.LoadScene("Inicio");
    }
}
