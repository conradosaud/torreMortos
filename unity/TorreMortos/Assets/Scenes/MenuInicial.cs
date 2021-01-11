using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        bool comecar = Input.GetKeyDown(KeyCode.Return);
        bool sair = Input.GetKeyDown(KeyCode.Escape);
        if(comecar){
            transform.Find("Loading").gameObject.SetActive(true);
            SceneManager.LoadScene("FlorestaMaldita");
        }
        if(sair){
            Application.Quit();
        }
    }
}
