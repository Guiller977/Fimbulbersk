using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : MonoBehaviour
{
    //Array de puntos por los que se mueve el enemigo
    public Transform[] points;
    //Velocidad de movimiento del enemigo
    public float moveSpeed;
    //Variable para conocer en que punto del recorrido se encuentra el enemigo
    public int currentPoint;

    public float hp;
    public bool isFrozen;
    public bool isOnFire;
    public bool inmuneToIce, inmuneToFire;
    public float invincibleCounter;

    //Una referencia al SpriteRenderer del enemigo
    private SpriteRenderer theSR;

    //Distancia del jugador para ser atacado, velocidad de persecución
    public float distanceToAttackPlayer, chaseSpeed;

    //Objetivo del enemigo
    private Vector3 attackTarget;

    //Tiempo entre ataques
    public float waitAfterAttack;
    //Contador de tiempo entre ataques
    private float attackCounter;

    // Start is called before the first frame update
    void Start()
    {
        //Inicializamos el SpriteRenderer del enemigo
        theSR = GetComponentInChildren<SpriteRenderer>();

        //Hago que los puntos entre los que se mueve el enemigo dejen de tener padre para que no lo sigan
        foreach (Transform p in points)
        {
            p.parent = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Si el contador de tiempo entre ataques aún está lleno
        if (attackCounter > 0)
        {
            //Hacemos que se vacíe el contador
            attackCounter -= Time.deltaTime;
        }
        //Si el contador de tiempo entre ataques ya está vacío
        else
        {
            //Si la distancia entre el jugador y el enemigo es la suficiente grande
            if (Vector3.Distance(transform.position, Playercontroller.sharedInstance.transform.position) > distanceToAttackPlayer)
            {
                //Reiniciamos el objetivo del ataque
                attackTarget = Vector3.zero;

                //Movemos al enemigo
                transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, moveSpeed * Time.deltaTime);

                //Si el enemigo prácticamente ha llegado a su punto de destino
                if (Vector3.Distance(transform.position, points[currentPoint].position) < 0.01f)
                {
                    //Pasamos al siguiente punto
                    currentPoint++;

                    //Comprobamos si hemos llegado al último punto del array
                    if (currentPoint >= points.Length)
                    {
                        //Reseteamos al primer punto del array
                        currentPoint = 0;
                    }
                }

                //Si el enemigo ha llegado al punto más a la izquierda
                if (transform.position.x < points[currentPoint].position.x)
                {
                    //Rotamos al enemigo para que mire en dirección contraria
                    theSR.flipX = true;
                }
                //Si el enemigo ha llegado al punto más a la derecha
                else if (transform.position.x > points[currentPoint].position.x)
                {
                    //Dejamos al enemigo mirando a donde estaba
                    theSR.flipX = false;
                }
            }
            //Si por el contrario el jugador está lo suficientemente cerca como para ser atacado
            else
            {
                //Si el objetivo del ataque está vacío
                if (attackTarget == Vector3.zero)
                {
                    //El objetivo del ataque será el jugador
                    attackTarget = Playercontroller.sharedInstance.transform.position;
                }

                //Movemos al enemigo hacia donde está el jugador
                transform.position = Vector3.MoveTowards(transform.position, attackTarget, chaseSpeed * Time.deltaTime);

                //Si el enemigo ha llegado prácticamente a la posición objetivo del ataque
                if (Vector3.Distance(transform.position, attackTarget) <= 0.1f)
                {
                    //Inicializamos el contador de tiempo entre ataques
                    attackCounter = waitAfterAttack;
                    //Reiniciamos el objetivo del ataque
                    attackTarget = Vector3.zero;
                }
            }
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
