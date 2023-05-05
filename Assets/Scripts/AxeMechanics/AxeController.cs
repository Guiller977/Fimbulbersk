using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeController : MonoBehaviour
{
    private Vector3 target;
    private Vector2 position;
    public Rigidbody2D theRB;
    public Transform playerpos;
    public float diference;
    public static AxeController sharedInstance;
    public float damage;
    public float speed;

    private float rotZ;
    public float rotationSpeed;
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        playerpos = Playercontroller.sharedInstance.transform;
        theRB = GetComponent<Rigidbody2D>();
        Throw();
    }

    void FixedUpdate()
    {
        rotZ += -Time.deltaTime * rotationSpeed;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    void Throw()
    {
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(playerpos.position.x, playerpos.position.y, playerpos.position.z);
        theRB.AddForce(target.normalized * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rotationSpeed = 0;
        speed = 0;
    }


    //target = Camera.main.ScreenToWorldPoint(Input.mousePosition) - new Vector3(playerpos.position.x, playerpos.position.y, playerpos.position.z);
    //theRB.AddForce(target, ForceMode2D.Impulse);
}
