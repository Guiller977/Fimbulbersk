using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float movSpeed;
    private Rigidbody2D theRB;
    private SpriteRenderer theSR;
    private bool movingRight;

    public Transform leftPoint, rightPoint;
    // Start is called before the first frame update
    void Start()
    {
        theRB = GetComponent<Rigidbody2D>();
        theSR = GetComponentInChildren<SpriteRenderer>();

        leftPoint.parent = null;
        rightPoint.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (movingRight)
        {
            theRB.velocity = new Vector2(movSpeed, 0);

            if (transform.position.x > rightPoint.position.x)
            {
                movingRight = false;
            }
        }
        else
        {
            theRB.velocity = new Vector2(-movSpeed, 0);

            if (transform.position.x < leftPoint.position.x)
            {
                movingRight = true;
            }
        }
    }
}
