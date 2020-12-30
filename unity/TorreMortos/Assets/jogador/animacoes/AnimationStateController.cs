using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateController : MonoBehaviour
{

    Animator animacao;
    
    float velocidade = 0.0f;
    float aceleracao = 0.1f;
    float desaceleracao = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        animacao = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("w") && velocidade < 1){
            velocidade += Time.deltaTime * aceleracao;
        }

        if(!Input.GetKey("w") && velocidade > 0){
            velocidade -= Time.deltaTime * desaceleracao;
        }

        if(velocidade < 0){
            velocidade = 0.0f;
        }

        //animacao.SetFloat("velocidade", velocidade);
      
    }
}
