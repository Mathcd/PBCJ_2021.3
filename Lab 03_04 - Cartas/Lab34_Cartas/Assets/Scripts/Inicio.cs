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
        /*
        *
        * quando a scene atrelada a esse script acordar, roda essa metodo 
        * para verificar se ja ha um recorde e ultima partida salvos 
        * e colocar seus valores nos objetos do jogo para que o usuario os veja 
        *
        */
        VerificarRecordeEUltimaPartida();
        GameObject.Find("recordeText").GetComponent<Text>().text = "Recorde de tentativas: " + PlayerPrefs.GetInt("Recorde");
        GameObject.Find("ultimaPartidaText").GetComponent<Text>().text = "Ultima partida: " + PlayerPrefs.GetInt("Jogadas");
    }

    void Start()
    {
        /*
        *
        * ao iniciar a tela, toque a musica
        *
        */
        inicio = GetComponent<AudioSource>();
        inicio.Play();
    }

    public void VerificarRecordeEUltimaPartida()
    {
        /*
        *
        * metodo para verificar se existe recorde e ultima partida
        * salvos em player preferences. se nao houver, salva ambos como 0
        * isso garante que sempre apareca algo na tela para o usuario ver
        *
        */
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
        /*
        *
        * metodo atrelado ao botao onclick para iniciar a tela do jogo propriamente dito
        *
        */
        SceneManager.LoadScene("Lab3");
    }

    public void IrParaConfiguracoes()
    {
        /*
        *
        * metodo atrelado ao botao onclick para ir para as configuracoes
        *
        */
        SceneManager.LoadScene("Configuracoes");
    }
}
