using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoDimensionalAnimationController : MonoBehaviour
{

    Animator animator;

    float velocidadeX = 0.0f;
    float velocidadeZ = 0.0f;

    float aceleracao = 2.0f;
    float desaceleracao = 2.0f;

    float velMax = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey("w") && velocidadeZ < velMax){
            velocidadeZ += Time.deltaTime * aceleracao;
        }
        if(Input.GetKey("s") && velocidadeZ > -velMax){
            velocidadeZ -= Time.deltaTime * aceleracao;
        }
        if(Input.GetKey("a") && velocidadeX > -velMax){
            velocidadeX -= Time.deltaTime * aceleracao;
        }
        if(Input.GetKey("d") && velocidadeX < velMax){
            velocidadeX += Time.deltaTime * aceleracao;
        }

        if(!Input.GetKey("w") && velocidadeZ > 0.0f){
            velocidadeZ -= Time.deltaTime * desaceleracao;
        }
        if(!Input.GetKey("s") && velocidadeZ < 0.0f){
            velocidadeZ += Time.deltaTime * desaceleracao;
        }
        if(!Input.GetKey("w") && !Input.GetKey("s") && velocidadeZ != 0.0f && (velocidadeZ > -0.05f && velocidadeZ < 0.05f) ){
            velocidadeZ = 0.0f;
        }
        
        if(!Input.GetKey("a") && velocidadeX < 0.0f){
            velocidadeX += Time.deltaTime * desaceleracao;
        }
        if(!Input.GetKey("d") && velocidadeX > 0.0f){
            velocidadeX -= Time.deltaTime * desaceleracao;
        }

        if(!Input.GetKey("a") && !Input.GetKey("d") && velocidadeX != 0.0f && (velocidadeX > -0.05f && velocidadeX < 0.05f)){
            velocidadeX = 0.0f;
        }

        animator.SetFloat("velocidade z", velocidadeZ);
        animator.SetFloat("velocidade x", velocidadeX);


    }
}
