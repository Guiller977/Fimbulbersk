using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public float damage, stun;
    public bool canFreeze, canBurn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (EnemyController.sharedInstance.isFrozen == true && canFreeze == false)
        {
            EnemyController.sharedInstance.DamageEnemy(damage * 2);
            StartCoroutine(Stun());
            if (canFreeze == true)
            {
                EnemyController.sharedInstance.isFrozen = true;
            }

            if (canBurn == true)
            {
                EnemyController.sharedInstance.isOnFire = true;
            }
        }

        else if (EnemyController.sharedInstance.isFrozen == false)
        {
            EnemyController.sharedInstance.DamageEnemy(damage);
            
            if (canFreeze == true)
            {
                EnemyController.sharedInstance.isFrozen = true;
            }

            if (canBurn == true)
            {
                EnemyController.sharedInstance.isOnFire = true;
                StartCoroutine(Stun());
            }
        }
    }

    private IEnumerator Stun()
    {
        EnemyController.sharedInstance.isFrozen = true;
        yield return new WaitForSeconds(stun);
        EnemyController.sharedInstance.isFrozen = false;
    }
}
