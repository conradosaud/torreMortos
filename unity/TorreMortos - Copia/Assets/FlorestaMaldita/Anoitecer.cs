using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anoitecer : MonoBehaviour
{

    public Light luz;
    public GameObject chuva;

    bool mudarLuz = false;

    void Update()
    {
        if(mudarLuz){
            if(luz.colorTemperature < 20000){
                luz.colorTemperature += Time.deltaTime * 4000;
            }
            if(luz.colorTemperature >= 20000){
                luz.colorTemperature = 20000;
                this.enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        mudarLuz = true;
        chuva.SetActive(true);
    }

}
