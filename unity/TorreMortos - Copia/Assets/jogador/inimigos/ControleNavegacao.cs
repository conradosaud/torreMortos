using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControleNavegacao : MonoBehaviour
{

    public bool seguirPersonagem;

    Transform alvo;

    NavMeshAgent inimigo;
    Animator anim;

    void Start()
    {
        inimigo = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        alvo = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Transform>();

    }

    void Update()
    {
        // segue o personagem se a variável estiver habilitada
        if(seguirPersonagem){
            comecarSeguir(alvo.position);
            float distanciaAnimacao = inimigo.remainingDistance - 1.9f;
            anim.SetFloat("velocidade", distanciaAnimacao);
        }

    }

    /* **************
    *    PRIVATE    *
    *************** */


    /* *************
    *    PUBLIC    *
    ************** */

    public void comecarSeguir(Vector3 posicao){
        inimigo.SetDestination(posicao);
    }

    public void pararSeguir(){
        inimigo.ResetPath();
        seguirPersonagem = false;
    }

}
