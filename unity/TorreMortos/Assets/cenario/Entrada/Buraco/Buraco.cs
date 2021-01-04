using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buraco : MonoBehaviour
{

    public Collider chao;

    private void OnTriggerEnter(Collider other) {
        chao.enabled = false;
    }
    private void OnTriggerExit(Collider other) {
        chao.enabled = true;
    }

}
