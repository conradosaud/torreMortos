using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarregaGameStatus : MonoBehaviour
{

    Jogador jogador;

    void Start()
    {
       jogador = GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<Jogador>();

        if(GameStatus.personagem_vida <= 0){
            GameStatus.personagem_vida = 100;
        }
        jogador.vida = GameStatus.personagem_vida;
        GameObject.FindGameObjectsWithTag("hud")[0].GetComponent<HUDController>().atualizaVida();

        atualizaScene();

    }

    void atualizaScene(){
        // Pega a instancia do gameobject do jogador
        GameObject jogador = GameObject.FindGameObjectsWithTag("Player")[0].gameObject;

        // Abre o catálogo de armas do gameobject drop
        Transform hudDrop = GameObject.FindGameObjectsWithTag("Drop")[0].transform;
        hudDrop = hudDrop.Find("Armas");

        // Encontra a nova arma na posicao de drops e cria uma instancia dela na mão do personagem
        string nome;
        if(GameStatus.personagem_arma == null){
            nome = "Espada Amaldiçoada";
        }else{
            nome = GameStatus.personagem_arma;
        }
        Destroy(jogador.GetComponent<Jogador>().buscaArma().transform.Find("ArmaPersonagem").gameObject);

        Transform novaArma = hudDrop.transform.Find(nome);
        novaArma = Instantiate(novaArma, jogador.GetComponent<Jogador>().buscaArma());
        corrigeArma(nome, novaArma);
        novaArma.gameObject.name = "ArmaPersonagem";
        Destroy(novaArma.GetComponent<DialogoArmas>());
        GameObject.FindGameObjectsWithTag("hud")[0].transform.Find("Dialogos").gameObject.SetActive(false);
        novaArma.GetComponent<BoxCollider>().enabled = false;
        novaArma.gameObject.SetActive(true);

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

}
