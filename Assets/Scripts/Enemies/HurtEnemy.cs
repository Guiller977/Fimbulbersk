using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtEnemy : MonoBehaviour
{
    public float damage, stun;
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
        if (EnemyController.sharedInstance.isFrozen == true)
        {
            EnemyController.sharedInstance.DamageEnemy(damage);
            StartCoroutine(Stun());
        }

        else
        {
            EnemyController.sharedInstance.DamageEnemy(damage / 2);
            StartCoroutine(Stun());
        }    
    }

    private IEnumerator Stun()
    {
        EnemyController.sharedInstance.isFrozen = true;
        yield return new WaitForSeconds(stun);
        EnemyController.sharedInstance.isFrozen = false;
    }
}
