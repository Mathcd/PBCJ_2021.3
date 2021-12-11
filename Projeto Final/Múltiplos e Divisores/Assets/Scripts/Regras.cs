using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Regras : MonoBehaviour
{
    public void voltar()
    {
        /* metodo para voltar a tela de inicio */
        SceneManager.LoadScene("Inicio");
    }
}
