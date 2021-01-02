using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inimigo : MonoBehaviour
{
    public string nome;

    public float vida;
    public float ataque;

    public int essencia_min;
    public int essencia_max;

    void Start(){
        // destroi os modelos originais
        if(transform.tag == "Original"){
            Destroy(gameObject);
        }
    }

}
