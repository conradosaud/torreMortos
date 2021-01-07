using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControleSala1 : MonoBehaviour
{

    public GameObject bloqueio;

    private void OnDestroy() {
        Destroy(bloqueio);
    }
}
