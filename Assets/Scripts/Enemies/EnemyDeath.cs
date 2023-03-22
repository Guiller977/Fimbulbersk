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

    //Método para la muerte del enemigo
    public void KillEnemy()
    {
        Debug.Log("sa matao");
        //Desactivamos al enemigo padre
        this.transform.gameObject.SetActive(false);
        //Instanciamos el efecto de muerte del enemigo en la posición del primer hijo
        //Instantiate(deathEffect, transform.GetChild(0).position, transform.GetChild(0).rotation);
    }
}
