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

    //Posiciones m�s a la izquierda y m�s a la derecha que se va a poder mover el enemigo
    public Transform leftPoint, rightPoint;

    //Variable para conocer la direcci�n de movimiento del enemigo
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

    public GameObject fireAxe, fireAxeReference, iceAxe, iceAxeReference;

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
        //Inicializamos el SpriteRenderer del enemigo teniendo en cuenta que est� en el GO hijo
        theSR = GetComponentInChildren<SpriteRenderer>();
        //Inicializamos el Animator del enemigo
        anim = GetComponent<Animator>();

        //Sacamos el Leftpoint y el Rightpoint del objeto padre, para que no se muevan junto con este
        leftPoint.parent = null;//null es vac�o o no tiene en este caso
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
            AudioManager.sharedInstance.PlaySFX(14);
            this.gameObject.SetActive(false);
        }

        if (isOnFire == true)
        {
            hp = hp - Time.deltaTime;
        }

        if (isFrozen == false)
        {
            moveSpeed = moveSpeerAux;

            //Si el enemigo se est� moviendo hacia la derecha
            if (movingRight)
            {
                //Aplicamos una velocidad hacia la derecha al enemigo
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
            
                //Giramos en horizontal el sprite del enemigo
                theSR.flipX = false;
            
                //Si la posici�n en X del enemigo est� m�s a la derecha que el RighPoint
                if (transform.position.x > rightPoint.position.x)
                {
                    //Ya no se mover� a la derecha sino a la izquierda
                    movingRight = false;
                }
            }
            //Si el enemigo se est� moviendo hacia la izquierda
            else
            {
                //Aplicamos una velocidad hacia la izquierda al enemigo
                theRB.velocity = new Vector2(-moveSpeed, theRB.velocity.y);
            
                //Mantenemos la direcci�n hacia la que mira el sprite
                theSR.flipX = true;
            
                //Si la posici�n en X del enemigo est� m�s a la izquierda que el LeftPoint
                if (transform.position.x < leftPoint.position.x)
                {
                    //Ya no se mover� a la izquierda sino a la derecha
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
            iceAxeReference = Instantiate(iceAxe, collision.transform.position, Quaternion.Euler(0, 0, -45));
            if (isFrozen == true)
            {
                DamageEnemy(AxeController.sharedInstance.damage * 2);
                isFrozen = false;
                AudioManager.sharedInstance.PlaySFX(10);
            }

            else
            {
                DamageEnemy(AxeController.sharedInstance.damage);
                StartCoroutine(Timer(1f));
                AudioManager.sharedInstance.PlaySFX(10);
            }
        }
        
        else if (collision.CompareTag("FireAxe") && !inmuneToFire)
        {
            fireAxeReference = Instantiate(fireAxe, collision.transform.position, Quaternion.Euler(0, 0, -45));
            if (isFrozen == true)
            {
                DamageEnemy(AxeController.sharedInstance.damage * 2);
                isOnFire = true;
                isFrozen = false;
                AudioManager.sharedInstance.PlaySFX(10);
            }

            else
            {
                DamageEnemy(AxeController.sharedInstance.damage);
                isOnFire = true;
                AudioManager.sharedInstance.PlaySFX(10);
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
                AudioManager.sharedInstance.PlaySFX(10);
            }
            else
            {
                DamageEnemy(15);
                isOnFire = true;
                AudioManager.sharedInstance.PlaySFX(10);
            }
        }

        else if (collision.CompareTag("IceHitbox") && !inmuneToIce)
        {
            if (isFrozen == true)
            {
                DamageEnemy(30);
                isFrozen = true;
                AudioManager.sharedInstance.PlaySFX(10);
            }
            else
            {
                DamageEnemy(15);
                isFrozen = true;
                AudioManager.sharedInstance.PlaySFX(10);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("FireAxe"))
        {
            collision.GetComponent<SpriteRenderer>().sprite = null;
            collision.attachedRigidbody.freezeRotation = true;
            collision.transform.position = this.transform.position;
            collision.attachedRigidbody.velocity = theRB.velocity;
            fireAxeReference.transform.position = this.transform.position;
        }

        else if (collision.CompareTag("IceAxe"))
        {
            collision.GetComponent<SpriteRenderer>().sprite = null;
            collision.attachedRigidbody.freezeRotation = true;
            collision.transform.position = this.transform.position;
            collision.attachedRigidbody.velocity = theRB.velocity;
            iceAxeReference.transform.position = this.transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FireAxe"))
        {
            Destroy(fireAxeReference);
            StartCoroutine(StopFire());
        }

        if (collision.CompareTag("IceAxe"))
        {
            Destroy(iceAxeReference);
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
