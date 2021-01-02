using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogoArmas : MonoBehaviour
{

    public GameObject dialogos;
    public DroparItem droparItem;

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
            trocarArma(armaStatus.nome);
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
        d.transform.Find("ValorVelocidade").GetComponent<Text>().text = armaStatus.velocidade.ToString();
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

        // Pega os status da arma
        ArmaStatus armaStatus = armaAtual.transform.Find("ArmaPersonagem").GetComponent<ArmaStatus>();

        // Abre o catálogo de armas do gameobject drop
        Transform hudDrop = GameObject.FindGameObjectsWithTag("Drop")[0].transform;
        hudDrop = hudDrop.Find("Armas");

        // Passa a arma que estava equipada antes para o catálogo de drops possiveis para que o jogador possa retrocar depois caso queira
        Transform instanciaNovaArma = Instantiate(armaAtual.transform.Find("ArmaPersonagem"), jogador.transform.position, jogador.transform.rotation, hudDrop);
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
        jogador.GetComponent<Jogador>().velocidade = novaArma.GetComponent<ArmaStatus>().velocidade;
        jogador.GetComponent<Jogador>().especial = novaArma.GetComponent<ArmaStatus>().especial_ataque;

        // Altera a colisão do jogador para a da nova arma
        jogador.GetComponent<CombatePersonagem>().colisorArma = novaArma.GetComponent<BoxCollider>();

        // Destrói o item que estava no mapa antes de pego
        Destroy(hudDrop.transform.Find(nome).gameObject);

        interagindo = false;
    }

}
