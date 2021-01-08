using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscadariaPorta : MonoBehaviour
{
    Text hud;

    void Start(){
        hud = GameObject.FindGameObjectsWithTag("hud")[0].transform.Find("Porta").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other) {
        hud.gameObject.SetActive(true);
        hud.text = "Entrar (Q)";
    }
    private void OnTriggerStay(Collider other) {
        if(Input.GetKeyDown(KeyCode.Q)){
            
            GameObject.FindGameObjectsWithTag("hud")[0].transform.Find("Loading").transform.gameObject.SetActive(true);

            Jogador p = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Jogador>();

            GameStatus.personagem_vida = p.vida;
            GameStatus.personagem_arma = p.buscaArma().Find("ArmaPersonagem").GetComponent<ArmaStatus>().nome;

            SceneManager.LoadScene("Santuario");

        }
    }
    private void OnTriggerExit(Collider other) {
        hud.gameObject.SetActive(false);
    }
}
