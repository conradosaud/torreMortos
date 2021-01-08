using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EntradaChave : MonoBehaviour
{

    public EntradaPorta entradaPorta;

    Text hud;

    void Start(){
        hud = GameObject.FindGameObjectsWithTag("hud")[0].transform.Find("Chave").GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other) {
        hud.gameObject.SetActive(true);
        hud.text = "Pegar chave (Q)";
    }
    private void OnTriggerStay(Collider other) {
        if(Input.GetKeyDown(KeyCode.Q)){
            entradaPorta.chave = true;
            hud.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit(Collider other) {
        hud.gameObject.SetActive(false);
    }

}
