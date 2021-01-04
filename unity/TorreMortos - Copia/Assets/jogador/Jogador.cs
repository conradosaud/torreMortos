using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jogador : MonoBehaviour
{

    public float vida = 135;
    public float ataque = 1;
    public float especial = 35;

    void Start(){
        if(vida <= 0){
            vida = 135;
        }
    }

    public Transform buscaArma(){

        Transform x;
        x = transform.Find("Armature");
        x = x.transform.Find("Hips");
        x = x.transform.Find("Spine");
        x = x.transform.Find("Spine1");
        x = x.transform.Find("Spine2");
        x = x.transform.Find("RightShoulder");
        x = x.transform.Find("RightArm");
        x = x.transform.Find("RightForeArm");
        x = x.transform.Find("RightHand");
        //x = x.transform.Find("ArmaPersonagem");
        

        return x;

    }

}
