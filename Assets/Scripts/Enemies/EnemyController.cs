using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //Velocidad del enemigo
    public float moveSpeed;
    public float hp;
    public bool isFrozen;
    public bool isOnFire;

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

    // Start is called before the first frame update
    void Start()
    {
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
        if (hp <= 0)
        {
            EnemyDeath.sharedInstance.KillEnemy();
        }

        if (isOnFire == true)
        {
            StartCoroutine(FireDamage());
            Debug.Log(hp);
        }

        if (isFrozen == false)
        {

            //Si el enemigo se est� moviendo hacia la derecha
            if (movingRight)
            {
                //Aplicamos una velocidad hacia la derecha al enemigo
                theRB.velocity = new Vector2(moveSpeed, theRB.velocity.y);
            
                //Giramos en horizontal el sprite del enemigo
                theSR.flipX = true;
            
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
                theSR.flipX = false;
            
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
        if (collision.CompareTag("IceAxe"))
        {
            if (isFrozen == true)
            {
                hp = hp - (AxeController.sharedInstance.damage * 2);
            }

            else
            {
                hp = hp - AxeController.sharedInstance.damage;
                StartCoroutine(Timer());
            }
        }
        
        else if (collision.CompareTag("FireAxe"))
        {
            if (isFrozen == true)
            {
                hp = hp - (AxeController.sharedInstance.damage * 2);
                isOnFire = true;
            }

            else
            {
                hp = hp - AxeController.sharedInstance.damage;
                isOnFire = true;
            }
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1f);
        isFrozen = true;
    }

    private IEnumerator FireDamage()
    {
        hp--;
        yield return new WaitForSeconds(1f);
    }
}
