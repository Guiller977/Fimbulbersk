using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Velocidad del enemigo
    public float moveSpeed, moveSpeerAux;
    public float hp;
    public bool isFrozen;
    public bool isOnFire;
    public bool inmuneToIce, inmuneToFire;

    //Posiciones más a la izquierda y más a la derecha que se va a poder mover el enemigo
    public Transform leftPoint, rightPoint;

    //Variable para conocer la dirección de movimiento del enemigo
    private bool movingRight;

    //Contadores para esperar un tiempo tras moverse y para saber cuanto tiempo se mueve el enemigo
    public float moveTime, waitTime;

    //Referencia al RigidBody del enemigo
    private Rigidbody2D theRB;
    //Referencia al SpriteRenderer del enemigo
    private SpriteRenderer theSR;
    //Referencia al Animator del enemigo
    private Animator anim;
    public float invincibleCounter;

    public GameObject a;

    public static EnemyController sharedInstance;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        moveSpeerAux = moveSpeed;
        //Inicializamos el Rigidbody del enemigo
        theRB = GetComponent<Rigidbody2D>();
        //Inicializamos el SpriteRenderer del enemigo teniendo en cuenta que está en el GO hijo
        theSR = GetComponentInChildren<SpriteRenderer>();
        //Inicializamos el Animator del enemigo
        anim = GetComponent<Animator>();

        //Sacamos el Leftpoint y el Rightpoint del objeto padre, para que no se muevan junto con este
        leftPoint.parent = null;//null es vacío o no tiene en este caso
        rightPoint.parent = null;

        //Iniciamos el movimiento hacia la derecha
        movingRight = true;
    }

    // Update is called once per frame
    void Update()
    {
        invincibleCounter -= Time.deltaTime;
        if (hp <= 0)
        {
            Instantiate(a, transform.position, transform.rotation);
            Debug.Log("muere");
            this.gameObject.SetActive(false);
        }

        if (isOnFire == true)
        {
            hp = hp - Time.deltaTime;
        }

        if (isFrozen == false)
        {
            moveSpeed = moveSpeerAux;

            //Si el enemigo se está moviendo hacia la derecha
            if (movingRight)
            {
                //Aplicamos una velocidad hacia la derecha al enemigo
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
            
                //Giramos en horizontal el sprite del enemigo
                theSR.flipX = true;
            
                //Si la posición en X del enemigo está más a la derecha que el RighPoint
                if (transform.position.x > rightPoint.position.x)
                {
                    //Ya no se moverá a la derecha sino a la izquierda
                    movingRight = false;
                }
            }
            //Si el enemigo se está moviendo hacia la izquierda
            else
            {
                //Aplicamos una velocidad hacia la izquierda al enemigo
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
            
                //Mantenemos la dirección hacia la que mira el sprite
                theSR.flipX = false;
            
                //Si la posición en X del enemigo está más a la izquierda que el LeftPoint
                if (transform.position.x < leftPoint.position.x)
                {
                    //Ya no se moverá a la izquierda sino a la derecha
                    movingRight = true;
                }
            }
        }

        else if (isFrozen == true)
        {
            moveSpeed = 0;
            theRB.velocity = new Vector2(0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("IceAxe") && !inmuneToIce)
        {
            if (isFrozen == true)
            {
                DamageEnemy(AxeController.sharedInstance.damage * 2);
                isFrozen = false;
            }

            else
            {
                DamageEnemy(AxeController.sharedInstance.damage);
                StartCoroutine(Timer(1f));
            }
        }
        
        else if (collision.CompareTag("FireAxe") && !inmuneToFire)
        {
            if (isFrozen == true)
            {
                DamageEnemy(AxeController.sharedInstance.damage * 2);
                isOnFire = true;
                isFrozen = false;
            }

            else
            {
                DamageEnemy(AxeController.sharedInstance.damage);
                isOnFire = true;
            }
        }

        else if (collision.CompareTag("LightHitbox"))
        {

            if (isFrozen == true)
            {
                DamageEnemy(10);
                isFrozen = false;
            }
            else
            {
                DamageEnemy(5);
            }
        }

        else if (collision.CompareTag("HeavyHitbox"))
        {

            if (isFrozen == true)
            {
                DamageEnemy(20);
                isFrozen = false;
            }
            else
            {
                DamageEnemy(10);
            }
        }

        else if (collision.CompareTag("FireHitbox") && !inmuneToFire)
        {

            if (isFrozen == true)
            {
                DamageEnemy(30);
                isFrozen = false;
                isOnFire = true;
            }
            else
            {
                DamageEnemy(15);
                isOnFire = true;
            }
        }

        else if (collision.CompareTag("IceHitbox") && !inmuneToIce)
        {
            if (isFrozen == true)
            {
                DamageEnemy(30);
                isFrozen = true;
            }
            else
            {
                DamageEnemy(15);
                isFrozen = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("FireAxe") || collision.CompareTag("IceAxe"))
        {
            collision.transform.position = this.transform.position;
            collision.attachedRigidbody.velocity = theRB.velocity;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FireAxe"))
        {
            StartCoroutine(StopFire());
        }
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        isFrozen = true;
    }

    private IEnumerator StopFire()
    {
        yield return new WaitForSeconds(2f);
        isOnFire = false;
    }

    public void DamageEnemy(float damage)
    {
        if (invincibleCounter <= 0)
        {
            hp = hp - damage;
            invincibleCounter = 0.2f;
        }     
    }
}
