using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TorrePorta : MonoBehaviour
{
    public bool passagem = false;
    Text hud;

    void Start(){
        hud = GameObject.FindGameObjectsWithTag("hud")[0].transform.Find("Passagem").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other) {
        hud.gameObject.SetActive(true);
        if(passagem){
            hud.text = "Entrar (Q)";
        }else{
            hud.text = "Inimigos bloqueiam a entrada.";
        }
    }
    private void OnTriggerStay(Collider other) {
        if(passagem && Input.GetKeyDown(KeyCode.Q)){
            
            Debug.Log("Entrou");

        }
    }
    private void OnTriggerExit(Collider other) {
        hud.gameObject.SetActive(false);
    }
}
