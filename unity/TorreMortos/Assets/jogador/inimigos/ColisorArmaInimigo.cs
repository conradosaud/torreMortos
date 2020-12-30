using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisorArmaInimigo : MonoBehaviour
{

    public CombatePersonagem combatePersonagem;
    public Inimigo inimigo;

    // termpo mínimo de receber mais de um dano da mesma fonte
    float tempoResfriamento = 1f;

    void Update(){
        tempoResfriamento -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other) {
        if(tempoResfriamento <= 0){
            combatePersonagem.sofrerDano(inimigo.ataque);
            tempoResfriamento = 1f;
        }
    }

}
