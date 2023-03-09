using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageEnemie : MonoBehaviour
{
    public float damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController.sharedInstance.DamageEnemy(damage/2);
            StartCoroutine(Stun());
        }
    }

    private IEnumerator Stun()
    {
        EnemyController.sharedInstance.isFrozen = true;
        yield return new WaitForSeconds(0.25f);
        EnemyController.sharedInstance.isFrozen = false;
    }
}
