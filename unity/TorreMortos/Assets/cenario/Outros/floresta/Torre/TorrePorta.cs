using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TorrePorta : MonoBehaviour
{
    public GameObject guardiao;
    Text hud;

    void Start(){
        hud = GameObject.FindGameObjectsWithTag("hud")[0].transform.Find("Passagem").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other) {
        hud.gameObject.SetActive(true);
        if(guardiao != null){
            hud.text = "Inimigos bloqueiam a entrada.";
        }else{
            hud.text = "Entrar (Q)";
        }
    }
    private void OnTriggerStay(Collider other) {
        if(guardiao == null && Input.GetKeyDown(KeyCode.Q)){
            
            GameObject.FindGameObjectsWithTag("hud")[0].transform.Find("Loading").transform.gameObject.SetActive(true);

            Jogador p = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Jogador>();

            GameStatus.personagem_vida = p.vida;
            GameStatus.personagem_arma = p.buscaArma().transform.Find("ArmaPersonagem").GetComponent<ArmaStatus>().nome;

            SceneManager.LoadScene("Entrada");

        }
    }
    private void OnTriggerExit(Collider other) {
        hud.gameObject.SetActive(false);
    }

}
