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

    public void alteraVida(float valor){
        float atual = float.Parse(texto_vida.text) + valor;
        texto_vida.text = atual.ToString("0");
    }

}
