using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public bool isGem, isPotion;

    public bool isCollected;

    public GameObject pickupEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isCollected)
        {
            if (isPotion)
            {
                if (PlayerHealthController.sharedInstance.currentHealth != PlayerHealthController.sharedInstance.maxHealth)
                {
                    PlayerHealthController.sharedInstance.HealPlayer();
                    isCollected = true;
                    Instantiate(pickupEffect, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
            }
        }
    }
}
