using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void alteraVel(float vel){
        velocidade = vel;
        velocidadeDiagonal = vel/2 * 1.40f;
        velocidadeCostas = vel/2 * 1.1f;
    }
    public void resetarVel(){
        velocidade = 7;
        velocidadeDiagonal = 5;
        velocidadeCostas = 3;
    }

}
