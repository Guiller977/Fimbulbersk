using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    public static EnemyDeath sharedInstance;
    //Referencia al objeto de efecto de muerte del enemigo
    public GameObject deathEffect;

    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }

    //M�todo para la muerte del enemigo
    public void KillEnemy()
    {
        //Desactivamos al enemigo padre
        transform.gameObject.SetActive(false);
        //Instanciamos el efecto de muerte del enemigo en la posici�n del primer hijo
        Instantiate(deathEffect, transform.GetChild(0).position, transform.GetChild(0).rotation);
    }
}
