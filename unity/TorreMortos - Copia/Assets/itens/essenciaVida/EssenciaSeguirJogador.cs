using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssenciaSeguirJogador : MonoBehaviour
{

    public Transform jogador;
    public HUDController hud;

    bool imantar = false;
    float tempoSpawn = 0.5f;

    Transform essenciaVida;
    


    // Start is called before the first frame update
    void Start()
    {
        essenciaVida = transform.parent.GetComponent<Transform>();
    }

    void Update()
    {
        
        // tempo antes de poder pegar as essencias na tela
        if (tempoSpawn > 0){
            tempoSpawn -= Time.deltaTime;
        }

        if(imantar && tempoSpawn <= 0){
            // se estiverem próximos ganha a essencia e destroi a instancia
            if(Vector3.Distance(essenciaVida.position, jogador.position) < 1f){
                ganhaEssencia();
                imantar = false;
                Destroy(essenciaVida.gameObject);
            }else{
                // persegue o jogador
                essenciaVida.position = Vector3.MoveTowards(essenciaVida.position, jogador.position, 0.2f );
            }
        }
    }

    /* **************
    *    PRIVATE    *
    *************** */

    // ativa a imantação da essencia e desativa as colisões para seguir o jogador
    void OnTriggerStay(Collider other) {
        if(other.name == "Jogador" && tempoSpawn <= 0){
            imantar = true;
        }
    }

    /* *************
    *    PUBLIC    *
    ************** */

    public void ganhaEssencia(){
        ganhaEssencia(1);
    }
    public void ganhaEssencia(int quantidade){
        float valor = quantidade * 3.5f;
        hud.alteraVida(valor);
    }


}
