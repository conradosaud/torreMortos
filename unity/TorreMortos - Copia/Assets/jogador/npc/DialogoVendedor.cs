using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogoVendedor : MonoBehaviour
{

    public GameObject dialogos;

    bool interagindo;

    GameObject dialogoVendedor;

    void Start()
    {
        dialogoVendedor = dialogos.transform.Find("DialogoVendedor").gameObject;
    }

    void Update()
    {
        if(interagindo){
            conversar();
        }
    }

    void OnTriggerStay(Collider other) {        
        if(other.name == "Jogador"){
            dialogos.SetActive(true);
            dialogoVendedor.SetActive(true);
            interagindo = true;
        }
    }

    void OnTriggerExit(Collider other) {
        if(other.name == "Jogador"){
            dialogos.SetActive(false);
            dialogoVendedor.SetActive(false);
            interagindo = false;
        }
    }

    void conversar(){
        bool tecla1 = Input.GetKeyDown(KeyCode.Alpha1);
        bool tecla2 = Input.GetKeyDown(KeyCode.Alpha2);
        bool tecla3 = Input.GetKeyDown(KeyCode.Alpha3);

        string fala = "";
        if(tecla1){
            fala = "Um singelo comerciante que permuta armas por essências de vida como moeda de troca. Fique a vontade para olhar minhas mercadorias.";
        }
        if(tecla2){
            fala = "Quatro andares populados de tropas mortas, e no quinto e último, seu líder, Odis, o necromante.";
        }
        if(tecla3){
            fala = "Você sabe que mortos não recup... ah! Me desculpe, parece que você retomou a consiciência por alguns instantes.";
        }

        if(fala != ""){
            dialogoVendedor.transform.Find("TextoPrincipal").GetComponent<Text>().text = fala;
        }
    }

}
