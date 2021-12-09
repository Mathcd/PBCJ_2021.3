using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameBackend;
using System; 

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Construtor de Main");
        Carta carta = new Carta(1, 'M');
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
