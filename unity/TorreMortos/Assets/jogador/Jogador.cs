using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{

    public float vida = 100;
    public float ataque = 20;

    void Start(){
        if(vida <= 0){
            vida = 100;
        }
    }

}
