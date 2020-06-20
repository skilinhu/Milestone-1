using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Espada : MonoBehaviour
{
    public float dano;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Inimigo"))
        {
            Vida vidaInimigo = other.gameObject.GetComponent<Vida>();
            vidaInimigo.vida = vidaInimigo.vida - dano;
            
        }
    }
}
