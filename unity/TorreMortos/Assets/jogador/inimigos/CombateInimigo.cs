using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CombateInimigo : MonoBehaviour
{

    public BoxCollider colisorArma;
    public Jogador jogador;

    public DroparItem droparItem;

    bool estaAtacando;

    bool estaVivo = true;
    
    Inimigo inimigo;
    Animator animator;
    ControleNavegacao controleNavegacao;
    NavMeshAgent navAgent;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        controleNavegacao = GetComponent<ControleNavegacao>();
        inimigo = GetComponent<Inimigo>();
    }

    void Update()
    {
        // verifica se o inimigo está próximo do alvo e se está habilitado para seguir
        if( Vector3.Distance(transform.position, jogador.transform.position) < 3f && controleNavegacao.seguirPersonagem == true){
            inimigoAtacar();
        }
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

        int essencia = Random.Range(inimigo.essencia_min, inimigo.essencia_max);
        droparItem.droparEssencia(transform, essencia);

        // exibe a animação de morte e seta o inimigo como morto
        if(estaVivo){
            animator.SetTrigger("morrer");
            estaVivo = false;
        }

    }

    // emite a animação de atacar
    public void inimigoAtacar(){
        if(!estaAtacando && estaVivo){
            animator.SetTrigger("atacar");
            estaAtacando = true;
        }
    }

    // aguarda ou cancela o processo de animação de ataque
    public void processoAtaque(int i){
        if(i == 1){
            estaAtacando = true;
        }else{
            estaAtacando = false;
        }
    }

    // habilita/desabilita a colisão da arma
    public void habilitaColisaoArma(int i){
        if(i == 1){
            colisorArma.enabled = true;
        }else{
            colisorArma.enabled = false;
        }
    }

    public void inimigoMorto(int i){
        if(i == 0){
            controleNavegacao.pararSeguir();
        }else{
            Destroy(gameObject);
        }
    }
}
