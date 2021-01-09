using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CombatePersonagem : MonoBehaviour
{

    Transform sons;

    public BoxCollider colisorArma;    
    BoxCollider colisorEspecial;

    public ArmaStatus armaStatus;    
    HUDController hudController;

    Text recargaEspecial;
    Image fundoEspecial;

    Text hudRolagem;
    Image hudRolagemFundo;
    float recargaRolagem;
    float valorRolagem = 2f;

    public float especialRecarga;
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

        hudController = GameObject.FindGameObjectsWithTag("hud")[0].GetComponent<HUDController>();
        fundoEspecial = hudController.GetComponent<Transform>().Find("hudContador").transform.Find("especial").GetComponent<Image>();
        recargaEspecial = fundoEspecial.transform.Find("contador").GetComponent<Text>();

        hudRolagemFundo = GameObject.FindGameObjectsWithTag("hud")[0].GetComponent<Transform>().Find("hudContador").transform.Find("rolagem").GetComponent<Image>();
        hudRolagem = hudRolagemFundo.transform.Find("contador").GetComponent<Text>();

        sons = GameObject.FindGameObjectsWithTag("audio")[0].transform.Find("Personagem").transform;

    }

    void Update()
    {

        // botão e identificador do ataque
        bool botaoAtacar = Input.GetKeyDown(KeyCode.Mouse0);
        bool botaoEspecial = Input.GetKeyDown(KeyCode.Mouse1);

        bool rolagem = Input.GetKeyDown(KeyCode.LeftShift);
        if(rolagem && recargaRolagem < 0){
            animator.SetTrigger("rolar");
            //movimentoPersonagem.alteraVel(10);
            recargaRolagem = valorRolagem;
        }

        if(!estaAtacando){
            if(botaoAtacar){
                personagemAtacar("atacar");
                movimentoPersonagem.alteraVel( armaStatus.reducao_vel_ataque );
            }
            if(botaoEspecial && especialRecarga < 0){
                personagemAtacar(armaStatus.animacao_especial);
                statusPersonagem.buscaArma().transform.Find("ArmaPersonagem").GetComponent<ArmaStatus>().somEspecial.Play();
                especialRecarga = valorRecarga;
                movimentoPersonagem.alteraVel( armaStatus.reducao_vel_especial );
            }
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

        if(recargaRolagem >= 0){
            recargaRolagem -= Time.deltaTime;
            hudRolagem.gameObject.SetActive(true);
            hudRolagem.text = recargaRolagem.ToString("0.0");
            hudRolagemFundo.color = new Color32(255,255,225,10);
        }
        if(recargaRolagem <= 0){
            hudRolagem.gameObject.SetActive(false);
            hudRolagemFundo.color = new Color32(255,255,225,60);
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

        hudController.atualizaVida();
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
            movimentoPersonagem.alteraVel( 5f );
            processoAtaque(1);
        }else{
            estaRolando = false;
            movimentoPersonagem.resetarVel();
            processoAtaque(0);
        }
        habilitaColisaoArma(0);
        habilitaColisaoEspecial(0);
    }

    // habilita/desabilita a colisão da arma
    public void habilitaColisaoArma(int i){
        if(i == 1){
            //sons.Find("Personagem").transform.Find("ArmaVento").GetComponent<AudioSource>().Play();
            colisorArma.enabled = true;
        }else{
            colisorArma.enabled = false;
        }
    }

    // habilita/desabilita a colisão da área do especial
    public void habilitaColisaoEspecial(int i){
        if(i == 1){
            colisorEspecial.enabled = true;
            if(colisorEspecial.GetComponent<MeshRenderer>()){
                colisorEspecial.GetComponent<MeshRenderer>().enabled = true;
            }
            //colisorEspecial.gameObject.SetActive(true);
        }else{
            
            colisorEspecial.enabled = false;
            if(colisorEspecial.GetComponent<MeshRenderer>()){
                colisorEspecial.GetComponent<MeshRenderer>().enabled = false;
            }
            //colisorEspecial.gameObject.SetActive(false);
        }
    }
    
    public void atualizaArma(ArmaStatus aStatus, BoxCollider colisor){
        armaStatus = aStatus;
        colisorArma = colisor;
        colisorEspecial = colisorArma.transform.Find("Especial").GetComponent<BoxCollider>();
        valorRecarga = aStatus.especial_recarga;
    }

    public void emiteSom(string acao){
        sons.Find(acao).GetComponent<AudioSource>().Play();
    }

}
