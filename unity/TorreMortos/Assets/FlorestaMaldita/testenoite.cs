using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testenoite : MonoBehaviour
{

    public Light luz;
    bool mudarLuz = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mudarLuz){
            if(luz.colorTemperature < 20000){
                luz.colorTemperature += Time.deltaTime * 3000;
            }
            if(luz.colorTemperature >= 20000){
                luz.colorTemperature = 20000;
                this.enabled = false;
            }
        }
    }

    void OnTriggerEnter(Collider other) {
        mudarLuz = true;
    }

}
