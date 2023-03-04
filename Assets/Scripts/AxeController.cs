using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public float speed = 300.0f;
    private Vector3 target;
    private Vector2 position;
    public Rigidbody2D theRB;
    public Transform playerpos;
    public float diference;

    void Start()
    {
        playerpos = Playercontroller.sharedInstance.transform;
        theRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Throw();
        Debug.Log(target);
    }

    void Throw()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(playerpos.position.x, playerpos.position.y, playerpos.position.z);
        float step = speed * Time.deltaTime;
        theRB.AddForce(target, ForceMode2D.Impulse);
    }
}
