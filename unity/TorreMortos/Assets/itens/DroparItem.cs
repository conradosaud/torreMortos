using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroparItem : MonoBehaviour
{

    Transform essenciaVida;
    Transform pai;

    // Start is called before the first frame update
    void Start()
    {
        essenciaVida = gameObject.transform.Find("essenciaVida").transform;
        pai = GetComponent<Transform>();
    }

    public void droparEssencia(Transform local, int quantidade){

        Vector3 posicao = local.position;
        posicao.y *= 1.2f;

        float forca_espalhamento = 5f;
        if(quantidade > 5 && quantidade < 10){
            forca_espalhamento = 3f;
        }else if(quantidade > 10){
            forca_espalhamento = 1.8f;
        }

        for (int i = 0; i < quantidade; i++)
        {
            Transform instancia = Instantiate(essenciaVida, posicao, Quaternion.Euler(new Vector3(-90, 0, 0)), pai);
            instancia.gameObject.SetActive(true);
            Rigidbody rig = instancia.GetComponent<Rigidbody>();

            float convert = (float) i;

            float x = posicao.x * Random.Range(-0.1f, 0.1f);
            float y = posicao.y * Random.Range(2.5f, 10f);
            float z = posicao.z * Random.Range(-5f, 5f);
            float forca = Random.Range(1f, 10f);

            Vector3 espalhar = new Vector3(x, y, z);

            rig.AddForce(espalhar);
            rig.maxDepenetrationVelocity = forca_espalhamento;
            
        }

        
    }

    public void droparItem(){

        GameObject jogador = GameObject.FindGameObjectsWithTag("Player")[0].gameObject;

        Transform armaAtual = jogador.GetComponent<Jogador>().buscaArma();
        ArmaStatus armaStatus = armaAtual.GetComponent<ArmaStatus>();

        Transform novaArma = Instantiate(armaAtual, jogador.transform.position, jogador.transform.rotation, pai.Find("Armas"));
        novaArma.gameObject.name = armaStatus.nome;

    }

}
