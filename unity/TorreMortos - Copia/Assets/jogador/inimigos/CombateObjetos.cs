using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombateObjetos : MonoBehaviour
{

    Jogador jogador;

    public DroparItem droparItem;

    bool estaVivo = true;
    
    Inimigo inimigo;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        inimigo = GetComponent<Inimigo>();

        jogador = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Jogador>();
    }

    /* **************
    *    PRIVATE    *
    *************** */

    // verifica se a colisão vem da arma do jogador
    private void OnTriggerEnter(Collider other) {
        if(other.name == "ArmaPersonagem"){
            sofrerDano(jogador.ataque);
        }
    }

    /* *************
    *    PUBLIC    *
    ************** */

    // recebe dano do personagem ou outra fonte
    public void sofrerDano(float dano){

        // verifica se o inimigo está morto para não receber mais golpes
        if(!estaVivo){
            return;
        }

        // desconta o dano da vida do personagem
        inimigo.vida -= dano;

        // se a vida zerar, mata o personagem e chama game over
        if(inimigo.vida <= 0){
            mataInimigo(true);
        }else{
            // seta animação de receber dano
            animator.SetTrigger("receberDano");
        }

    }

    // zera a vida do personagem chamando game over ou não
    public void mataInimigo(bool destruir){

        inimigo.vida = 0;

        // tira a colisão do corpo para o personagem poder passar por cima
        transform.GetComponent<BoxCollider>().enabled = false;

        

        // exibe a animação de morte e seta o inimigo como morto
        if(estaVivo){
            animator.SetTrigger("morrer");
            estaVivo = false;
        }

    }


    public void inimigoMorto(int i){
        if(i == 1){
            Destroy(gameObject);
        }else{
            int essencia = Random.Range(inimigo.essencia_min, inimigo.essencia_max);
            droparItem.droparEssencia(transform, essencia);
        }
    }
}
