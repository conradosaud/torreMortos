using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovimentoPersonagem : MonoBehaviour
{
    
    public float velocidade = 7;
    public float velocidadeDiagonal = 5;
    public float velocidadeCostas = 3;

    float recargaRolagem;
    float valorRolagem = 2f;

    Text hudRolagem;
    Image hudRolagemFundo;

    Animator animator;
    Rigidbody rig;
    Vector3 movimento;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rig = GetComponent<Rigidbody>();

        hudRolagemFundo = GameObject.FindGameObjectsWithTag("hud")[0].GetComponent<Transform>().Find("hudContador").transform.Find("rolagem").GetComponent<Image>();
        hudRolagem = hudRolagemFundo.transform.Find("contador").GetComponent<Text>();
    }

    void Update(){
        movimento = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
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

    // Update is called once per frame
    void FixedUpdate()
    {

        bool correrFrente = Input.GetKey(KeyCode.W);
        bool correrTras = Input.GetKey(KeyCode.S);
        bool correrEsquerda = Input.GetKey(KeyCode.A);
        bool correrDireita = Input.GetKey(KeyCode.D);
        bool rolagem = Input.GetKeyDown(KeyCode.LeftShift);

        if(rolagem && recargaRolagem < 0){
            animator.SetTrigger("rolar");
            velocidade = 10;
            recargaRolagem = valorRolagem;
        }

        float velocidadeFlex = velocidade;
        if( (correrFrente && correrEsquerda) || (correrFrente && correrDireita) ){
            velocidadeFlex = velocidadeDiagonal;
        }
        if(correrTras) {
            if(correrEsquerda || correrDireita){
                velocidadeFlex = velocidadeDiagonal/2 - 0.3f;
            }else{
                velocidadeFlex = velocidadeCostas;
            }
        }

        transform.Translate(movimento * velocidadeFlex * Time.deltaTime, Space.Self);

    }

    public void estaAtacando(bool atacando, float velReduc){
        if(atacando){
            velocidade = velReduc;
            velocidadeDiagonal = velReduc;
        }else{
            velocidade = 7;
            velocidadeDiagonal = 5;
        }
    }

    public void alteraVel(float vel){
        velocidade = vel;
        velocidadeDiagonal = vel/2 * 1.40f;
    }
    public void resetarVel(){
        velocidade = 7;
        velocidadeDiagonal = 5;
    }

}
