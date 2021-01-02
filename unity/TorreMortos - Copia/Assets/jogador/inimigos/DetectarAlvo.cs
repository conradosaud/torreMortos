using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectarAlvo : MonoBehaviour
{

    Animator anim;
    ControleNavegacao controleNavegacao;

    void Start()
    {
        anim = transform.parent.GetComponent<Animator>();
        controleNavegacao = transform.parent.GetComponent<ControleNavegacao>();
    }

    // Habilita seguir o personagem ao entrar no range de detecção
    void OnTriggerEnter(Collider other) {
        controleNavegacao.seguirPersonagem = true;   
    }

    // Deixa de seguir o personagem ao sai do range de detecção
    void OnTriggerExit(Collider other) {
        controleNavegacao.pararSeguir();  
        anim.SetFloat("velocidade", 0);
    }

}
