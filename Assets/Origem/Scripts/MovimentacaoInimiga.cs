using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovimentacaoInimiga : MonoBehaviour
{
    NavMeshAgent agenteNM;
    public float distanciaMinima;

    void Awake()
    {
        agenteNM = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        Vector3 posicaoJogador = PlayerController.pontochao;
        agenteNM.SetDestination(posicaoJogador);

        float distanciaEntreJogadoreInimigo = Vector3.Distance(transform.position, posicaoJogador);
        if(distanciaEntreJogadoreInimigo <= distanciaMinima)
        {
            agenteNM.isStopped = true;
        } else
        {
            agenteNM.isStopped = false;
        }
    }
}
