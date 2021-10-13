using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Fim : MonoBehaviour
{

    AudioSource somVitoria;

    // Start is called before the first frame update
    void Start()
    {
        somVitoria = GetComponent<AudioSource>();
        somVitoria.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VoltarParaInicio()
    {
        SceneManager.LoadScene("Inicio");
    }
}
