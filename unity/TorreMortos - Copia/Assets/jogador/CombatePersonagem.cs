using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatePersonagem : MonoBehaviour
{


    public BoxCollider colisorArma;    
    BoxCollider colisorEspecial;

    public ArmaStatus armaStatus;    
    public HUDController hudController;

    Text recargaEspecial;
    Image fundoEspecial;

    float especialRecarga;
    float valorRecarga;

    // tempo de respiro entre tomar animações de ataque
    float recargaDano = -0.1f;
    float valorRecargaDano = 1.2f;

    bool estaAtacando;
    bool estaRolando;

    Animator animator;
    MovimentoPersonagem movimentoPersonagem;
    Jogador statusPersonagem;

    void Start()
    {
        animator = GetComponent<Animator>();
        statusPersonagem = GetComponent<Jogador>();
        movimentoPersonagem = GetComponent<MovimentoPersonagem>();

        colisorEspecial = colisorArma.transform.Find("Especial").GetComponent<BoxCollider>();

        valorRecarga = armaStatus.especial_recarga;
        fundoEspecial = hudController.GetComponent<Transform>().Find("hudContador").transform.Find("especial").GetComponent<Image>();
        recargaEspecial = fundoEspecial.transform.Find("contador").GetComponent<Text>();
    }

    void Update()
    {
        // botão e identificador do ataque
        bool botaoAtacar = Input.GetKeyDown(KeyCode.Mouse0);
        bool botaoEspecial = Input.GetKeyDown(KeyCode.Mouse1);

        if(botaoAtacar){
            personagemAtacar("atacar");
            movimentoPersonagem.alteraVel( armaStatus.reducao_vel_ataque );
        }

        if(botaoEspecial && especialRecarga < 0){
            personagemAtacar("esp-espada-barao");
            especialRecarga = valorRecarga;
            movimentoPersonagem.alteraVel( armaStatus.reducao_vel_especial );
        }

        if(especialRecarga >= 0){ 
            recargaEspecial.gameObject.SetActive(true);
            especialRecarga -= Time.deltaTime;
            recargaEspecial.text = especialRecarga.ToString("0.0");
            fundoEspecial.color = new Color32(255,255,225,10);
        }
        if(especialRecarga <= 0){
            recargaEspecial.gameObject.SetActive(false);
            fundoEspecial.color = new Color32(255,255,225,60);
        }

        if(recargaDano > 0){
            recargaDano -= Time.deltaTime;
        }

    }

    /* **************
    *    PRIVATE    *
    *************** */

    private void OnTriggerEnter(Collider other) {
        // verifica se a colisão vem da arma de um inimigo / se estiver rolando fica imune
        if(other.name == "ArmaInimigo" && !estaRolando && recargaDano < 0){
            // seta a animação de tomar dano
            animator.SetTrigger("recebeDano");
            recargaDano = valorRecargaDano;
        }
    }
    

    /* *************
    *    PUBLIC    *
    ************** */

    // recebe dano de um inimigo ou outra fonte
    public void sofrerDano(float dano){

        if(estaRolando){
            return;
        }

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
    public void personagemAtacar(string animacao){
        if(estaAtacando == false){
            animator.SetTrigger(animacao);
            estaAtacando = true;
        }
    }

    // aguarda ou cancela o processo de animação de ataque
    public void processoAtaque(int i){
        if(i == 1){
            estaAtacando = true;
        }else{
            estaAtacando = false;
            movimentoPersonagem.resetarVel();
        }
    }

    // aguarda o processo de rolagem do personagem
    public void processoRolagem(int i){
        if(i == 1){
            estaRolando = true;
            movimentoPersonagem.alteraVel( 4f );
        }else{
            estaRolando = false;
            movimentoPersonagem.resetarVel();
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

    // habilita/desabilita a colisão da área do especial
    public void habilitaColisaoEspecial(int i){
        if(i == 1){
            colisorEspecial.enabled = true;
        }else{
            colisorEspecial.enabled = false;
        }
    }
    

}
