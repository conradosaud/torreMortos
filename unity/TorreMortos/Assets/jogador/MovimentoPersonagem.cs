using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPersonagem : MonoBehaviour
{
    
    public float velocidade = 7;
    public float velocidadeDiagonal = 5;
    public float velocidadeCostas = 3;

    Animator animator;
    Rigidbody rig;
    Vector3 movimento;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();
    }

    void Update(){
        movimento = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        bool correrFrente = Input.GetKey(KeyCode.W);
        bool correrTras = Input.GetKey(KeyCode.S);
        bool correrEsquerda = Input.GetKey(KeyCode.A);
        bool correrDireita = Input.GetKey(KeyCode.D);

        float velocidadeFlex = velocidade;
        if( (correrFrente && correrEsquerda) || (correrFrente && correrDireita) ){
            velocidadeFlex = velocidadeDiagonal;
        }
        if(correrTras) {
            if(correrEsquerda || correrDireita){
                velocidadeFlex = velocidadeDiagonal/2 - 0.3f;
            }else{
                velocidadeFlex = velocidadeCostas;
            }
        }

        transform.Translate(movimento * velocidadeFlex * Time.deltaTime, Space.Self);

    }

    public void estaAtacando(bool atacando){
        if(atacando){
            velocidade = 3.5f;
        }else{
            velocidade = 7;
        }
    }

}
