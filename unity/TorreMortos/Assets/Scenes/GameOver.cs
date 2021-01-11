using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public string scene;

    void Start(){
        scene = GameStatus.sceneAtual;
        if(scene == "" || scene == null){
            scene = "FlorestaMaldita";
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool continuar = Input.GetKeyDown(KeyCode.Return);
        bool sair = Input.GetKeyDown(KeyCode.Escape);
        
        if(continuar){
            SceneManager.LoadScene(scene);
        }
        if(sair){
            SceneManager.LoadScene("Menu");
        }

    }


}
