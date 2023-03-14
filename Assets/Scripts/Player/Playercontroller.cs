using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playercontroller : MonoBehaviour
{
    private bool canDoubleJump;
    public float moveSpeed;
    public Rigidbody2D theRB;
    public float jumpForce;
    public bool isGrounded;
    public bool isHurt;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    //private bool canDoubleJump;
    private Animator anim;
    private SpriteRenderer theSR;
    public bool isLeft;

    private bool canDash = true;
    private bool isDashing;
    public float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

    public float knockBackLength, knockBackForce;
    private float knockBackCounter;

    public GameObject lightHitbox, heavyHitbox, fireHitbox, iceHitbox;
    public float attackCooldown, iceAttackCD, fireAttackCD;

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
        if (moveSpeed > 18)
        {
            moveSpeed = 8;
        }
        if (isDashing)
        {
            return;
        }

        //ATAQUES

        attackCooldown -= Time.deltaTime;
        iceAttackCD -= Time.deltaTime;
        fireAttackCD -= Time.deltaTime;
        //Ligero
        if (Input.GetKeyDown(KeyCode.E) && isLeft == false && attackCooldown <= 0)
        {
            Instantiate(lightHitbox, new Vector3(this.transform.position.x + 1.5f, this.transform.position.y, this.transform.position.z), this.transform.rotation);
            attackCooldown = 0.25f;
        }

        if (Input.GetKeyDown(KeyCode.E) && isLeft == true && attackCooldown <= 0)
        {
            Instantiate(lightHitbox, new Vector3(this.transform.position.x - 1.5f, this.transform.position.y, this.transform.position.z), this.transform.rotation);
            attackCooldown = 0.25f;
        }

        //Pesado
        if (Input.GetKeyDown(KeyCode.Q) && isLeft == false && attackCooldown <= 0)
        {
            Instantiate(heavyHitbox, new Vector3(this.transform.position.x + 1.5f, this.transform.position.y, this.transform.position.z), this.transform.rotation);
            attackCooldown = 0.5f;
        }

        if (Input.GetKeyDown(KeyCode.Q) && isLeft == true && attackCooldown <= 0)
        {
            Instantiate(heavyHitbox, new Vector3(this.transform.position.x - 1.5f, this.transform.position.y, this.transform.position.z), this.transform.rotation);
            attackCooldown = 0.5f;
        }

        //Hielo
        if (Input.GetKeyDown(KeyCode.F) && isLeft == false && attackCooldown <= 0 && iceAttackCD <= 0)
        {
            Instantiate(iceHitbox, new Vector3(this.transform.position.x + 4.0f, this.transform.position.y + 1.5f, this.transform.position.z), this.transform.rotation);
            attackCooldown = 2f;
            iceAttackCD = 30f;
        }

        if (Input.GetKeyDown(KeyCode.F) && isLeft == true && attackCooldown <= 0 && iceAttackCD <= 0)
        {
            Instantiate(iceHitbox, new Vector3(this.transform.position.x - 4.0f, this.transform.position.y + 1.5f, this.transform.position.z), this.transform.rotation);
            attackCooldown = 2f;
            iceAttackCD = 30f;
        }

        //Fire
        if (Input.GetKeyDown(KeyCode.R) && isLeft == false && attackCooldown <= 0 && fireAttackCD <= 0)
        {
            Instantiate(fireHitbox, new Vector3(this.transform.position.x + 4.0f, this.transform.position.y + 1.5f, this.transform.position.z), this.transform.rotation);
            attackCooldown = 2f;
            fireAttackCD = 30f;
        }

        if (Input.GetKeyDown(KeyCode.R) && isLeft == true && attackCooldown <= 0 && fireAttackCD <= 0)
        {
            Instantiate(fireHitbox, new Vector3(this.transform.position.x - 4.0f, this.transform.position.y + 1.5f, this.transform.position.z), this.transform.rotation);
            attackCooldown = 2f;
            fireAttackCD = 30f;
        }

        //MOVIMIENTO

        if (knockBackCounter <= 0)
        {
            theRB.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), theRB.velocity.y);
            isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, 2f, whatIsGround); //OverlapCircle(Punto para generar circulo, radio, layer a detectar.)            
            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded)
                {
                    theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
                    //Cambiar esto si se incluye doble salto
                    canDoubleJump = false;

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

            if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
            {
                StartCoroutine(Dash());
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

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = theRB.gravityScale;
        theRB.gravityScale = 0f;
        if (isLeft)
        {
            PlayerHealthController.sharedInstance.invincibleCounter = dashingTime;
            theRB.velocity = new Vector2(-transform.localScale.x * dashingPower, 0f);
            yield return new WaitForSeconds(dashingTime);
            theRB.gravityScale = originalGravity;
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }
        else
        {
            PlayerHealthController.sharedInstance.invincibleCounter = dashingTime;
            theRB.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
            yield return new WaitForSeconds(dashingTime);
            theRB.gravityScale = originalGravity;
            isDashing = false;
            yield return new WaitForSeconds(dashingCooldown);
            canDash = true;
        }
    }
}
