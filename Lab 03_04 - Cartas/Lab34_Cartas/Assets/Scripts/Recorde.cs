using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Recorde : MonoBehaviour
{
    AudioSource ahlelek;
    
    // Start is called before the first frame update
    void Start()
    {
        ahlelek = GetComponent<AudioSource>();
        ahlelek.Play();   
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
