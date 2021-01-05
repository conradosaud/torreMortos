using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoCamera : MonoBehaviour
{

    public float velocidadeRotacao = 2;

    public Transform target;
    public Transform player;

    float mouseX;
    float mouseY;

    public float suavidadeTarget = 0.5f;
    public float suavidadePlayer = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        mouseX += Input.GetAxis("Mouse X") * velocidadeRotacao;
        mouseY -= Input.GetAxis("Mouse Y") * velocidadeRotacao;
        mouseY -= Mathf.Clamp(mouseY, -35, 60);

        transform.LookAt(target);

    }

    void FixedUpdate() {
        
        if(Input.GetKey(KeyCode.Mouse1)){
            target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            
        }else{
            //target.rotation = Quaternion.Euler(mouseY, mouseX, 0);
            Quaternion rTargetNova = Quaternion.Euler(mouseY, mouseX, 0);
            Quaternion rTargetOrig = target.rotation;

            target.rotation = Quaternion.Lerp(rTargetOrig, rTargetNova, suavidadeTarget);
            
            //player.rotation = Quaternion.Euler(0, mouseX, 0);

            Quaternion rPlayerNova = Quaternion.Euler(0, mouseX, 0);
            Quaternion rPlayerOrig = player.rotation;

            player.rotation = Quaternion.Lerp(rPlayerOrig, rPlayerNova, suavidadePlayer);

        }
    }

}
