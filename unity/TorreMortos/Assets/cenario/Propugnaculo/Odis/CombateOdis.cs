using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class CombateOdis : MonoBehaviour
{

    public BoxCollider colisorArma;
    Jogador jogador;

    DroparItem droparItem;

    public GameObject zumbis;

    int qntVidas = 0;

    bool estaAtacando;
    bool estaVivo = true;

    float velocidadePadrao;

    // intervalo de tempo da a animação de receber dano
    float valorResfriamento = 2f;
    float tempoResfriamento;
    
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

        jogador = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Jogador>();
        droparItem = GameObject.FindGameObjectsWithTag("Drop")[0].GetComponent<DroparItem>();

        velocidadePadrao = navAgent.speed;

    }

    void Update()
    {
        // verifica se o inimigo está próximo do alvo e se está habilitado para seguir
        if(controleNavegacao.seguirPersonagem == true){
            if( Vector3.Distance(transform.position, jogador.transform.position) < 2f){
                inimigoAtacar();
            }
            olharJogador();

            tempoResfriamento -= Time.deltaTime;            
        }
        
    }

    /* **************
    *    PRIVATE    *
    *************** */

    // verifica se a colisão vem da arma do jogador
    private void OnTriggerEnter(Collider other) {

        // verifica se o inimigo está morto para não receber mais golpes
        if(!estaVivo){
            return;
        }

        if(other.name == "ArmaPersonagem"){
            sofrerDano(jogador.ataque);
            jogador.buscaArma().transform.Find("ArmaPersonagem").GetComponent<ArmaStatus>().somAtaque.Play();
        }else if(other.name == "Especial"){
            sofrerDano(jogador.especial);
        }

    }

    // olha para a direção do jogador suavemente
    private void olharJogador(){
        var targetRotation = Quaternion.LookRotation(jogador.transform.position - transform.position);
        // Smoothly rotate towards the target point.
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 2f * Time.deltaTime);
    }

    /* *************
    *    PUBLIC    *
    ************** */

    // recebe dano do personagem ou outra fonte
    public void sofrerDano(float dano){

        inimigo.apanhar.Play();

        // desconta o dano da vida do personagem
        inimigo.vida -= dano;

        // se a vida zerar, mata o personagem e chama game over
        if(inimigo.vida <= 0){
            mataInimigo(true);
        }else{
            // seta animação de receber dano se estiver dentro do tempo de resfriamento
            if(tempoResfriamento <= 0){
                animator.SetTrigger("receberDano");
                tempoResfriamento = valorResfriamento;
            }
        }

    }

    // zera a vida do personagem chamando game over ou não
    public void mataInimigo(bool destruir){

        inimigo.vida = 0;

        int essencia = Random.Range(inimigo.essencia_min, inimigo.essencia_max);
        droparItem.droparEssencia(transform, essencia);

        // exibe a animação de morte e seta o inimigo como morto
        if(estaVivo){
            animator.SetTrigger("morrer");
            inimigo.morrer.Play();
            estaVivo = false;
        }

    }

    // emite a animação de atacar
    public void inimigoAtacar(){
        if(!estaAtacando && estaVivo){

            int n = Random.Range(0, 100);
            if(n < 25){
                animator.SetTrigger("ataque1");
            }else if(n < 50){
                animator.SetTrigger("ataque2");
            }else if(n < 75){
                animator.SetTrigger("ataque3");
            }else{
                animator.SetTrigger("ataque4");
            }

            estaAtacando = true;
        }
    }

    // aguarda ou cancela o processo de animação de ataque
    public void processoAtaque(int i){
        if(i == 1){
            navAgent.velocity = Vector3.zero;
            navAgent.speed = 0f;
            estaAtacando = true;
        }else{
            estaAtacando = false;
            navAgent.speed = velocidadePadrao;
            
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
            habilitaColisaoArma(0);
        }else{
            if(qntVidas < 7){
                proximaVida();
            }else{
                SceneManager.LoadScene("Final");
            }
        }
    }

    public void proximaVida(){
        zumbis.transform.GetChild( qntVidas ).GetComponent<Animator>().SetTrigger("morrer");
        transform.Find("Audio").transform.Find("Zumbi").GetComponent<AudioSource>().Play();
    }

    public void reviver(int i){
        if(i == 1){
            inimigo.vida = 100 * (qntVidas/1.3f);
            estaVivo = true;
            processoAtaque(0);
            controleNavegacao.seguirPersonagem = true;
        }else{
            animator.SetTrigger("reviver");
            transform.Find("Audio").transform.Find("Renascer").GetComponent<AudioSource>().Play();
            qntVidas ++;
        }
    }

}
