using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BibliotecaPorta : MonoBehaviour
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
            
            Debug.Log("Entrou");

        }
    }
    private void OnTriggerExit(Collider other) {
        hud.gameObject.SetActive(false);
    }

}
