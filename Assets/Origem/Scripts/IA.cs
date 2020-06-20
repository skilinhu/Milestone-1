using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{
   public enum EstadoIA
    {
        Atacando,
        Andando,
    }

    public EstadoIA estado;
    public float dano;
    public Animator anim;
    bool bateu;

    NavMeshAgent agenteNM;
    Vida vida;
    Vida vidaJogador;


    void Awake()
    {
        agenteNM = GetComponent<NavMeshAgent>();
        vida = GetComponent<Vida>();
        vidaJogador = GameObject.FindWithTag("Player").GetComponent<Vida>();
    }

    void Update()
    {
        if (agenteNM.isStopped)
        {
            estado = EstadoIA.Atacando;
        } else
        {
            estado = EstadoIA.Andando;
        }

        if(estado == EstadoIA.Atacando)
        {
            anim.SetTrigger("atacou");
            bateu = true;
            
            if (bateu == true)
            {
                vidaJogador.vida = vidaJogador.vida - dano * Time.deltaTime;
            }
           
        }

        if (vida.vida <= 0)
        {
            anim.SetTrigger("morreu");
            Destroy(gameObject, 3f);

        }
    }
}

internal class Bateu
{
}