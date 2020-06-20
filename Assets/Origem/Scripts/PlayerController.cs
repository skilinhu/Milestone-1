using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float velocidadeMov;
    public float deslocamentoAltura;
    public float intensidadePulo;
    public LayerMask camadaChao;
    public Animator anim;

    Transform tr;
    Transform trCam;
    Rigidbody rb;

    bool estaempulo;
    bool estaemmovimento;
    bool estanochao;

    public static Vector3 pontochao;

    void Awake()
    {
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        trCam = GameObject.FindWithTag("Tripe").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        // receber dados de entrada do jogador
        bool apertouPulo = Input.GetButtonDown("Jump");
        bool apertouAtaque = Input.GetButtonDown("Fire1");
        float movH = Input.GetAxis("Horizontal");
        float movV = Input.GetAxis("Vertical");

        Vector3 mov = new Vector3(movH, 0, movV);

        if (mov.magnitude > 1f)
            mov.Normalize();

        // detectar estados
        RaycastHit chaohit;
        estanochao = Physics.Raycast(tr.position, Vector3.down, out chaohit, deslocamentoAltura, camadaChao);


        estaempulo = apertouPulo || !estanochao;
        estaemmovimento = mov.magnitude > 0.1f;

        // ataque
        if (apertouAtaque && !estaempulo)
        {
            anim.SetTrigger("Atacou");
        }

        
        // desempenhar pulo
        rb.useGravity = estaempulo;
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        if (!estaempulo)
            rb.constraints = rb.constraints | RigidbodyConstraints.FreezePositionY;

        if (apertouPulo && estanochao)
        {
            rb.AddForce(Vector3.up * intensidadePulo, ForceMode.Impulse);
        }

        // rotacionar o jogador na direção do movimento
        if (estaemmovimento)
            tr.LookAt(tr.position + trCam.TransformDirection(mov) * 5);

        // fazer o player andar
        if (estaemmovimento)
            tr.Translate(0, 0, mov.magnitude * velocidadeMov * Time.deltaTime);

        //alimentando parâmetro anim
        anim.SetFloat("Velocidade", mov.magnitude);

        // acompanhar chão
        if (!estaempulo)
        {
            RaycastHit hit;
            bool rcBateuNoChao = Physics.Raycast(tr.position, Vector3.down, out hit, Mathf.Infinity, camadaChao);

            if (rcBateuNoChao)
            {
                Vector3 pos = tr.position;
                pos.y = hit.point.y + deslocamentoAltura;
                tr.position = pos;

                pontochao = hit.point;
            }

            // zerar inércia
            rb.velocity = Vector3.zero;
        }

    }

}
