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

        for (int i = 0; i < quantidade; i++)
        {
            Transform instancia = Instantiate(essenciaVida, posicao, Quaternion.Euler(new Vector3(-90, 0, 0)), pai);
            Rigidbody rig = instancia.GetComponent<Rigidbody>();

            float convert = (float) i;

            float x = posicao.x * Random.Range(-0.1f, 0.1f);
            float y = posicao.y * Random.Range(10f, 50f);
            float z = posicao.z * Random.Range(-0.1f, 0.1f);
            float forca = Random.Range(1f, 10f);

            Vector3 espalhar = new Vector3(x, y, z);

            rig.AddForce(espalhar);
            rig.maxDepenetrationVelocity = 4f;
            
        }

        
    }

}
