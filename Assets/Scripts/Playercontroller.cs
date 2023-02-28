using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D theRB;
    public float jumpForce;
    private bool isGrounded;
    public bool isHurt;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    private bool canDoubleJump;
    private Animator anim;
    private SpriteRenderer theSR;
    public bool isLeft;

    public float knockBackLength, knockBackForce;
    private float knockBackCounter;

    public static Playercontroller sharedInstance;
    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (knockBackCounter <= 0)
        {
            theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 2f, whatIsGround); //OverlapCircle(Punto para generar circulo, radio, layer a detectar.)            
            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    canDoubleJump = true;
                }
            }
            else
            {
                if (canDoubleJump)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    canDoubleJump = false;
                }
            }
            if (theRB.velocity.x < 0)
            {
                theSR.flipX = false;
                isLeft = true;
            }
            else if (theRB.velocity.x > 0)
            {
                theSR.flipX = true;
                isLeft = false;
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;
            if (!theSR.flipX)
            {
                theRB.velocity = new Vector2(knockBackForce, theRB.velocity.y);
            }
            else
            {
                theRB.velocity = new Vector2(-knockBackForce, theRB.velocity.y);
            }
        } 
        anim.SetFloat("movSpeed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
        anim.SetBool("isHurt", isHurt);
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackLength;
        theRB.velocity = new Vector2(0f, knockBackForce);
    }
}
