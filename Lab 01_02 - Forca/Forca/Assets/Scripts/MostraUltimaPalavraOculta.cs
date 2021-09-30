using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// classe para mostrar a palavra oculta após acertar todas as letras
public class MostraUltimaPalavraOculta : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Text>().text = PlayerPrefs.GetString("ultimaPalavraOculta");
    }

}
