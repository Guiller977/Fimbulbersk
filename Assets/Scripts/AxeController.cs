using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    public float speed = 300.0f;
    private Vector3 target;
    private Vector2 position;
    public Rigidbody2D theRB;

    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void Update()
    {
        Throw();
    }

    void Throw()
    {
        float step = speed * Time.deltaTime;
        theRB.AddForce(target, ForceMode2D.Impulse);
    }
}
