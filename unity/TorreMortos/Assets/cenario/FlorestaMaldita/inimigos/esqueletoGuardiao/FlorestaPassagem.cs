using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlorestaPassagem : MonoBehaviour
{

    public GameObject passagem;
    public TorrePorta torrePorta;

    private void OnDestroy() {
        passagem.SetActive(true);
        torrePorta.passagem = true;
    }
}
