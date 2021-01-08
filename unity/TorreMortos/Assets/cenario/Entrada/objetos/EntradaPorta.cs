using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            
            Debug.Log("Passa de fase");

        }
    }
    private void OnTriggerExit(Collider other) {
        hud.gameObject.SetActive(false);
    }
}
