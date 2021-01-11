using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleZumbi : MonoBehaviour
{
   
    public CombateOdis co;
    Inimigo inimigo;
    DroparItem droparItem;

    void Start(){
        inimigo = GetComponent<Inimigo>();
        droparItem = GameObject.FindGameObjectsWithTag("Drop")[0].GetComponent<DroparItem>();
    }

    public void reviver(int i){
        co.reviver(0);

        transform.GetComponent<BoxCollider>().enabled = false;

        int essencia = Random.Range(inimigo.essencia_min, inimigo.essencia_max);
        droparItem.droparEssencia(transform, essencia);

        Destroy(transform.Find("luz").gameObject);

        

    }

}
