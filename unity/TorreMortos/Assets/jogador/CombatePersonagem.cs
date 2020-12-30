using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatePersonagem : MonoBehaviour
{

    public BoxCollider colisorArma;
    public HUDController hudController;

    // refazer ************************
    public Inimigo inimigo;

    bool estaAtacando;

    Animator animator;

    MovimentoPersonagem movimentoPersonagem;
    Jogador statusPersonagem;

    void Start()
    {
        animator = GetComponent<Animator>();
        statusPersonagem = GetComponent<Jogador>();
        movimentoPersonagem = GetComponent<MovimentoPersonagem>();
    }

    void Update()
    {
        // botão e identificador do ataque
        bool botaoAtacar = Input.GetKeyDown(KeyCode.Mouse0);
        if(botaoAtacar){
            personagemAtacar();
        }

        // corrige a velocidade do personagem enquanto ataca
        movimentoPersonagem.estaAtacando(estaAtacando);

    }

    /* **************
    *    PRIVATE    *
    *************** */

    private void OnTriggerEnter(Collider other) {

        // verifica se a colisão vem da arma de um inimigo
        if(other.name == "ArmaInimigo"){

            // desativa a colisao da arma do inimigo
            other.enabled = false;

            // seta a animação de tomar dano
            animator.SetTrigger("recebeDano");

        }
    }
    

    /* *************
    *    PUBLIC    *
    ************** */

    // recebe dano de um inimigo ou outra fonte
    public void sofrerDano(float dano){
        // desconta o dano da vida do personagem
        statusPersonagem.vida -= dano;

        // se a vida zerar, mata o personagem e chama game over
        if(statusPersonagem.vida <= 0){
            mataPersonagem(true);
        }

        hudController.alteraVida(-dano);
    }

    // zera a vida do personagem chamando game over ou não
    public void mataPersonagem(bool gameOver){
        statusPersonagem.vida = 0;
        if(gameOver){
            //SceneManager.LoadScene("gameover");
        }
    }

    // emite a animação de atacar
    public void personagemAtacar(){
        if(estaAtacando == false){
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
    

}
