using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoreHP : MonoBehaviour
{
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
        if (collision.CompareTag("Player"))
        {
            LevelManager.sharedInstance.MoreHP = true;
            PlayerHealthController.sharedInstance.maxHealth++;
            PlayerHealthController.sharedInstance.currentHealth = PlayerHealthController.sharedInstance.maxHealth;
            UIController.sharedInstance.UpdateHealthDisplay();
            Destroy(this.gameObject);
            AudioManager.sharedInstance.PlaySFX(16);
        }
    }
}
