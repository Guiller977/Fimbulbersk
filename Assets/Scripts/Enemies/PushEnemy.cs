using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Muevete");
        collision.rigidbody.AddForce(new Vector2(-10, 0));
    }
}
