using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogoArmas : MonoBehaviour
{

    GameObject dialogos;
    DroparItem droparItem;

    bool interagindo;
    float contador = 1f;

    ArmaStatus armaStatus;

    void Start(){
        armaStatus = GetComponent<ArmaStatus>();
    }

    void Update(){

        if(contador > 0){
            contador -= Time.deltaTime;
        }

        dialogos = GameObject.FindGameObjectsWithTag("hud")[0].transform.Find("Dialogos").gameObject;
        droparItem = GameObject.FindGameObjectsWithTag("Drop")[0].GetComponent<DroparItem>();

    }

    void OnTriggerStay(Collider other) {        

        if(other.name != "Jogador"){
            return;
        }

        dialogoArmas();
        dialogos.SetActive(true);
        interagindo = true;

        bool botaoPermutar = Input.GetKeyDown(KeyCode.Q);
        if(botaoPermutar && interagindo && contador < 0){
            if(comprarArma()){
                trocarArma(armaStatus.nome);
            }
            contador = 1f;
        }

    }

    void OnTriggerExit(Collider other) {
        if(other.name == "Jogador"){
            dialogos.SetActive(false);
            dialogos.transform.Find("DialogoArmas").gameObject.SetActive(false);
        }
    }

    void dialogoArmas(){
        GameObject d = dialogos.transform.Find("DialogoArmas").gameObject;

        d.transform.Find("NomeArma").GetComponent<Text>().text = armaStatus.nome;
        d.transform.Find("ValorAtaque").GetComponent<Text>().text = armaStatus.ataque.ToString();
        d.transform.Find("ValorRecarga").GetComponent<Text>().text = (armaStatus.especial_recarga.ToString())+"s";
        d.transform.Find("ValorDanoEspecial").GetComponent<Text>().text = armaStatus.especial_ataque.ToString();
        d.transform.Find("ValorDescricao").GetComponent<Text>().text = armaStatus.descricao.ToString();
        d.transform.Find("ValorEssencias").GetComponent<Text>().text = armaStatus.essencias.ToString();

        d.SetActive(true);
    }

    void trocarArma(string nome){

        // Pega a instancia do gameobject do jogador
        GameObject jogador = GameObject.FindGameObjectsWithTag("Player")[0].gameObject;

        // Pega a arma que ele está equipando no momento com suas coordenadas
        Transform armaAtual = jogador.GetComponent<Jogador>().buscaArma();
        Vector3 armaAtualPosicao = armaAtual.transform.Find("ArmaPersonagem").transform.position;
        Quaternion armaAtualRotacao = armaAtual.transform.Find("ArmaPersonagem").transform.rotation;
        Vector3 armaAtualEscala = armaAtual.transform.Find("ArmaPersonagem").transform.localScale;

        // Pega os status da arma
        ArmaStatus armaStatus = armaAtual.transform.Find("ArmaPersonagem").GetComponent<ArmaStatus>();

        // Abre o catálogo de armas do gameobject drop
        Transform hudDrop = GameObject.FindGameObjectsWithTag("Drop")[0].transform;
        hudDrop = hudDrop.Find("Armas");

        // Passa a arma que estava equipada antes para o catálogo de drops possiveis para que o jogador possa retrocar depois caso queira
        Transform instanciaNovaArma = Instantiate(armaAtual.transform.Find("ArmaPersonagem"), jogador.transform.position, jogador.transform.rotation, hudDrop);
        corrigeArmaChao(armaStatus.nome, instanciaNovaArma);
        instanciaNovaArma.gameObject.name = armaStatus.nome;
        instanciaNovaArma.gameObject.AddComponent<DialogoArmas>();
        instanciaNovaArma.gameObject.GetComponent<DialogoArmas>().dialogos = dialogos;
        instanciaNovaArma.gameObject.GetComponent<DialogoArmas>().droparItem = droparItem;
        instanciaNovaArma.gameObject.GetComponent<BoxCollider>().enabled = true;

        // Destroi a arma que ele estava antes
        Destroy(armaAtual.transform.Find("ArmaPersonagem").gameObject);

        // Encontra a nova arma na posicao de drops e cria uma instancia dela na mão do personagem
        Transform novaArma = hudDrop.transform.Find(nome);
        novaArma = Instantiate(novaArma, armaAtualPosicao, armaAtualRotacao, armaAtual);
        corrigeArma(nome, novaArma);
        novaArma.gameObject.name = "ArmaPersonagem";
        Destroy(novaArma.GetComponent<DialogoArmas>());
        dialogos.SetActive(false);
        novaArma.GetComponent<BoxCollider>().enabled = false;

        // Remove as particulas da arma
        Transform particulas = novaArma.Find("Particulas");
        if(particulas != null){
            Destroy(particulas.GetComponent<ParticleSystem>().gameObject);
        }

        // Altera os status do jogador para os da nova arma
        jogador.GetComponent<Jogador>().ataque = novaArma.GetComponent<ArmaStatus>().ataque;
        jogador.GetComponent<Jogador>().especial = novaArma.GetComponent<ArmaStatus>().especial_ataque;

        // Destrói o item que estava no mapa antes de pego
        Destroy(hudDrop.transform.Find(nome).gameObject);

        jogador.GetComponent<CombatePersonagem>().atualizaArma(novaArma.GetComponent<ArmaStatus>(), novaArma.GetComponent<BoxCollider>());

        interagindo = false;
    }

    void corrigeArma(string nome, Transform arma){
        if(nome == "Arco de Guerra"){
            arma.localPosition = new Vector3(-2.1f, 11.7f, 5.2f);
            arma.localRotation = Quaternion.Euler(70f, -193f, 95f);
            arma.localScale = new Vector3(15, 15, 15);
        }else if(nome == "Lâmina do Algoz"){
            arma.localPosition = new Vector3(-2.33f, 7.96f, 1.52f);
            arma.localRotation = Quaternion.Euler(240f, 330f, -235f);
            arma.localScale = new Vector3(70f, 50f, 50f);
        }else if(nome == "Crepúsculo"){
            arma.localPosition = new Vector3(-36f, 5f, -16f);
            arma.localRotation = Quaternion.Euler(6f, -121f, 80f);
            arma.localScale = new Vector3(5555f, 5555f, 5555f);
        }else if(nome == "Cajado Mortífero"){
            arma.localPosition = new Vector3(-7.3f, 14f, 10f);
            arma.localRotation = Quaternion.Euler(36f, -351f, 86f);
            arma.localScale = new Vector3(70f, 70f, 70f);
        }else if(nome == "Machado Telúrico"){
            arma.localPosition = new Vector3(-17.2f, 9.9f, -0.7f);
            arma.localRotation = Quaternion.Euler(-0.2f, 163.1f, 276f);
            arma.localScale = new Vector3(10000f, 10000f, 10000f);
        }else{
            arma.localPosition = new Vector3(-33f, 1.4f, -10f);
            arma.localRotation = Quaternion.Euler(-12f, -27f, 250);
            arma.localScale = new Vector3(100, 85, 100);
        }
    }

    void corrigeArmaChao(string nome, Transform arma){
        if(nome == "Arco de Guerra"){
            arma.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        }else if(nome == "Lâmina do Algoz"){
            arma.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }else if(nome == "Crepúsculo"){
            //arma.localPosition = new Vector3(-107.733f, 2.2f, 83.394f );
            arma.localScale = new Vector3(55, 55, 55);
        }else{
            arma.localScale = new Vector3(1, 1, 1);
        }
    }

    bool comprarArma(){

        if(armaStatus.essencias == 0){
            return true;
        }

        Text tEssencia = GameObject.FindGameObjectsWithTag("hud")[0].transform.Find("hudVida").GetComponent<Text>();
        float nEssencia = float.Parse(tEssencia.text);
        if(nEssencia > armaStatus.essencias){
            GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<CombatePersonagem>().sofrerDano(armaStatus.essencias);
            armaStatus.essencias = 0;
            return true;
        }
        return false;
    }


    


}
