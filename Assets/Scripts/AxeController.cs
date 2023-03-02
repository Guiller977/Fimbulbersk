using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    private float speed = 10.0f;
    private Vector3 target;
    private Vector2 position;
    private Rigidbody theRB;

    void Start()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
        float step = speed * Time.deltaTime;

        // move sprite towards the target location
        //transform.position = Vector2.MoveTowards(new Vector2(0,0), new Vector2(10,0), step);
        theRB.velocity = new Vector3(step, 0, 0);
    }
}
