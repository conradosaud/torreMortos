using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogoVendedor : MonoBehaviour
{

    public GameObject dialogos;

    bool interagindo;

    GameObject dialogoVendedor;

    public string fase;

    void Start()
    {
        dialogoVendedor = dialogos.transform.Find("DialogoVendedor").gameObject;

        if(fase == "floresta" || fase == ""){
            dialogoVendedor.transform.Find("TextoPrincipal").GetComponent<Text>().text = "Você tem coragem de se aventurar por essas terras.\nJá faz muito tempo que niguém passa por aqui... não com vida, pelo menos.";

            dialogoVendedor.transform.Find("Escolha1").GetComponent<Text>().text = "Quem é você?";
            dialogoVendedor.transform.Find("Escolha2").GetComponent<Text>().text = "O que há na torre?";
            dialogoVendedor.transform.Find("Escolha3").GetComponent<Text>().text = "Como recupero vida?";

        }else if(fase == "entrada"){
            dialogoVendedor.transform.Find("TextoPrincipal").GetComponent<Text>().text = "Nossa, que bagunça está esse lugar. Você realmente precisa quebrar tudo assim?";
            dialogoVendedor.transform.Find("Escolha1").GetComponent<Text>().text = "Como chegou aqui?";
            dialogoVendedor.transform.Find("Escolha2").GetComponent<Text>().text = "Por onde eu vou?";
            dialogoVendedor.transform.Find("Escolha3").GetComponent<Text>().text = "O que sou eu?";
        }else if(fase == "biblioteca"){
            dialogoVendedor.transform.Find("TextoPrincipal").GetComponent<Text>().text = "Estou impressionado por você ter chegado até aqui, pois eu mesmo não posso subir mais. Isso é uma despedida.";
            dialogoVendedor.transform.Find("Escolha1").GetComponent<Text>().text = "Não vai subir?";
            dialogoVendedor.transform.Find("Escolha2").GetComponent<Text>().text = "O que há nos livros?";
            dialogoVendedor.transform.Find("Escolha3").GetComponent<Text>().text = "Quem são os\nnecromantes?";
        }

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
        if(fase == "entrada"){

            if(tecla1){
                fala = "A pergunta não é como eu cheguei aqui, e sim, por que você está aqui?";
            }
            if(tecla2){
                fala = "Essa torre está ruindo, muitas entradas e escadas estão destroçadas. Procure pelas entradas abertas, são mais seguras.";
            }
            if(tecla3){
                fala = "Seu corpo não lhe pertence mais, mas sua consicência continua se manifestando. Incrível, necromantes são fascinantes!";
            }

        }else if(fase == "biblioteca"){

            if(tecla1){
                fala = "Meus poderes não vêm de mim, e nesse momento não tenho tanto força como você para continuar, além disso, eu e Odis temos uma relação complicada.";
            }
            if(tecla2){
                fala = "A pesquisa e o estudo sobre os três necromantes, Odis, Endus e o terceiro, que pouco se sabe quem é. Nada que você já não saiba.";
            }
            if(tecla3){
                fala = "Sabemos que Odis tem o poder de controlar inúmeros cadáveres com poderes limitados, enquanto Endus, apenas um, que se fortifica absorvendo essências de vida. ";
            }

        }else{

            if(tecla1){
                fala = "Um singelo comerciante que permuta armas por essências de vida como moeda de troca. Fique a vontade para olhar minhas mercadorias.";
            }
            if(tecla2){
                fala = "Quatro andares populados de tropas mortas, e no quinto e último, seu líder, Odis, o necromante.";
            }
            if(tecla3){
                fala = "Você sabe que mortos não recup... ah! Me desculpe, parece que você retomou a consiciência por alguns instantes.";
            }

        }

        if(fala != ""){
            dialogoVendedor.transform.Find("TextoPrincipal").GetComponent<Text>().text = fala;
        }

    }

}
