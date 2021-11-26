using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe responsável por controlar a movimentação do Player
/// </summary>
public class MovimentoPlayer : MonoBehaviour
{
    public float VelocidadeMovimento = 3.0f;    // equivale ao momento (impulso a ser dado ao player)
    Vector2 Movimento = new Vector2();            // detectar movimento pelo teclado

    Animator animator;      // guara a componente do Controlador de Animação
    string estadoAnimacao = "EstadoAnimacao";   // guarda o nome do parâmetro de Animação
    Rigidbody2D rb2D;       // guarda a componente CorpoRígido do Player

    // atribui o código da máquina de estados para cada um dos estados do Player
    enum EstadosCaractere
    {
        andaLeste = 1,
        andaOeste = 2,
        andaNorte = 3,
        andaSul = 4,
        idle = 5
    }

    /* Start is called before the first frame update */
    void Start()
    {
        animator = GetComponent<Animator>();    // chama a componente Animador
        rb2D = GetComponent<Rigidbody2D>();     // chama a componente CorpoRígido do Player
    }

    /* Update is called once per frame */
    void Update()
    {
        UpdateEstado();
    }

    /* atualiza a dinâmica do Player de acordo com o movimento */
    private void FixedUpdate()
    {
        MoveCaractere();
    }

    /* movimenta o player a partir da entrada do teclado */
    private void MoveCaractere()
    {
        Movimento.x = Input.GetAxisRaw("Horizontal");   // movimento no eixo x
        Movimento.y = Input.GetAxisRaw("Vertical");     // movimento no eixo y
        Movimento.Normalize();                          // normaliza o movimento
        rb2D.velocity = Movimento * VelocidadeMovimento;    // define a velocidade
    }

    /* atualiza a animação do caractere de acordo com a direção que ele está indo */
    private void UpdateEstado()
    {
        if(Movimento.x > 0)
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.andaLeste);
        }
        else if(Movimento.x < 0)
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.andaOeste);
        }
        else if(Movimento.y > 0)
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.andaSul);
        }
        else if(Movimento.y < 0)
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.andaNorte);
        }
        else
        {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaractere.idle);
        }
    }
}
