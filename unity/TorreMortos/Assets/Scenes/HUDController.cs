using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{

    public Text texto_vida;

    /* *************
    *    PUBLIC    *
    ************** */


    public void atualizaVida(){
        texto_vida.text = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Jogador>().vida.ToString("0");
    }

}
