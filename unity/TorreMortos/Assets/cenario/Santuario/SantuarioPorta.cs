using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SantuarioPorta : MonoBehaviour
{

    private void OnTriggerEnter(Collider other) {
        GameObject.FindGameObjectsWithTag("hud")[0].transform.Find("Loading").transform.gameObject.SetActive(true);

        Jogador p = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Jogador>();

        GameStatus.personagem_vida = p.vida;
        GameStatus.personagem_arma = p.buscaArma().Find("ArmaPersonagem").GetComponent<ArmaStatus>().nome;

        GameStatus.sceneAtual = "Propugnaculo";

        SceneManager.LoadScene("Propugnaculo");
    }
   
}
