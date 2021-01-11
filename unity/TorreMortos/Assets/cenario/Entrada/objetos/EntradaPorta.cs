using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EntradaPorta : MonoBehaviour
{

    public bool chave = false;
    Text hud;

    // Start is called before the first frame update
    void Start()
    {
        hud = GameObject.FindGameObjectsWithTag("hud")[0].transform.Find("Chave").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other) {
        hud.gameObject.SetActive(true);
        if(chave){
            hud.text = "Destrancar e entrar (Q)";
        }else{
            hud.text = "Você não possui a chave.";
        }
    }
    private void OnTriggerStay(Collider other) {
        if(chave && Input.GetKeyDown(KeyCode.Q)){
            
            GameObject.FindGameObjectsWithTag("hud")[0].transform.Find("Loading").transform.gameObject.SetActive(true);

            Jogador p = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Jogador>();

            GameStatus.personagem_vida = p.vida;
            GameStatus.personagem_arma = p.buscaArma().Find("ArmaPersonagem").GetComponent<ArmaStatus>().nome;

            GameStatus.sceneAtual = "Biblioteca";

            SceneManager.LoadScene("Biblioteca");

        }
    }
    private void OnTriggerExit(Collider other) {
        hud.gameObject.SetActive(false);
    }
}
