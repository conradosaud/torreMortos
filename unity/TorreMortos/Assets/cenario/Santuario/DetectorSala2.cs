using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectorSala2 : MonoBehaviour
{

    public GameObject bloqueio;
    public GameObject mago;

    private void OnTriggerEnter(Collider other) {
        if(other.name == "Jogador"){
            bloqueio.SetActive(true);
            mago.GetComponent<CombateInvocador>().enabled = true;
            Destroy(transform.gameObject);
        }
    }
}
