﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleSala2 : MonoBehaviour
{
    public GameObject bloqueio;
    public GameObject escadaria;

    private void OnDestroy() {
        if(bloqueio){
            Destroy(bloqueio);
            escadaria.SetActive(true);
        }
    }
}
