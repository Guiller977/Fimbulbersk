using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public Transform farBackground, middleBackground;

    public float minHeight, maxHeight; 

    private float lastXPos, lastYPos;
    // Start is called before the first frame update
    void Start()
    {
        lastXPos = transform.position.x;
        lastYPos = transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(target.position.x, transform.position.y, target.position.z - 10);

        //float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);

        ////Pone limites a la posición Y de la camara
        //transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);
        //Pone limites a la posición Y de la camara.
        transform.position = new Vector3(target.position.x, Mathf.Clamp(target.position.y, minHeight, maxHeight), transform.position.z);

        //Usada para saber la cantidad en la que debe moverse la camara en X
        float amountToMoveX = transform.position.x - lastXPos;
        float amountToMoveY = transform.position.y - lastYPos;
        
        farBackground.position = farBackground.position + new Vector3(amountToMoveX, amountToMoveY,0f);
        middleBackground.position = middleBackground.position + new Vector3(amountToMoveX * 0.5f, amountToMoveY * 0.5f, 0f);

        lastXPos = transform.position.x;
        lastYPos = transform.position.y;
    }
}
