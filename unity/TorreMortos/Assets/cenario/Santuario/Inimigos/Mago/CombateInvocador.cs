using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombateInvocador : MonoBehaviour
{

    float tempoInvocacao;
    float recargaInvocacao = 3f;

    int numeroInvocacoes = 0;
    bool estaVivo = true;
    public GameObject invocacao;

    Inimigo inimigo;

    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        inimigo = GetComponent<Inimigo>();
        anim = GetComponent<Animator>();
        tempoInvocacao = 0;
    }

    // Update is called once per frame
    void Update()
    {
        tempoInvocacao -= Time.deltaTime;
        if(tempoInvocacao < 0 && numeroInvocacoes < 20 && estaVivo){
            anim.SetTrigger("atacar");
            tempoInvocacao = recargaInvocacao;
            GameObject.FindGameObjectsWithTag("audio")[0].transform.Find("Inimigos").transform.Find("InvocadorDesalmado").transform.Find("Ataque").GetComponent<AudioSource>().Play();
        }
    }

    public void invocar(int i){

        GameObject invoc = Instantiate(invocacao, new Vector3(130, 63, 4), transform.rotation, transform.Find("Invocacoes"));
        invoc.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        invoc.gameObject.SetActive(true);

        numeroInvocacoes++;

        if(numeroInvocacoes >= 5){
            invoc.GetComponent<Inimigo>().vida = 40;
        }
        if(numeroInvocacoes >= 12 && transform.Find("Bloqueio") != null){
            if(transform.Find("Invocacoes").transform.childCount < 15){
                Destroy(transform.Find("Bloqueio").transform.gameObject);
            }
        }

    }

    private void OnTriggerEnter(Collider other){
        if(other.name == "ArmaPersonagem"){
            anim.SetTrigger("morrer");
            GameObject.FindGameObjectsWithTag("audio")[0].transform.Find("Inimigos").transform.Find("InvocadorDesalmado").transform.Find("Morte").GetComponent<AudioSource>().Play();
        }
    }

    // zera a vida do personagem chamando game over ou não
    public void inimigoMorto(int destruir){
        if(destruir == 0){
            int essencia = Random.Range(inimigo.essencia_min, inimigo.essencia_max);
            GameObject.FindGameObjectsWithTag("Drop")[0].GetComponent<DroparItem>().droparEssencia(transform, essencia);
            estaVivo = false;
        }else{
            Destroy(transform.gameObject);
        }

    }

}
