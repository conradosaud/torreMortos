using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleSala1 : MonoBehaviour
{

    public GameObject bloqueio;

    private void OnDestroy() {
        Destroy(bloqueio);
    }

    void OnTriggerEnter(Collider other){
        if(other.name == "ArmaPersonagem" || other.name == "Especial"){
            GameObject.FindGameObjectsWithTag("audio")[0].transform.Find("Inimigos").transform.Find("Apocalipse").transform.Find("Apanhar").GetComponent<AudioSource>().Play();
        }
    }

}
