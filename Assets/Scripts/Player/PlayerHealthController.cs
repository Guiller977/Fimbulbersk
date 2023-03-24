using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public int currentHealth, maxHealth = 3;

    public float invincibleLength;
    public float invincibleCounter;
    private SpriteRenderer theSR;

    public static PlayerHealthController sharedInstance;

    void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (invincibleCounter > 0)
        {
            Playercontroller.sharedInstance.isHurt = true;
            invincibleCounter -= Time.deltaTime;           
        }
        if (invincibleCounter <= 0)
        {
            theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, 1f);
            Playercontroller.sharedInstance.isHurt = false;
        }
    }

    public void DealWithDamage()
    {
        if (invincibleCounter <= 0)
        {
            currentHealth--;

            if (currentHealth <= 0)
            {
                currentHealth = 0;

                LevelManager.sharedInstance.RespawnPlayer();
            }

            else
            {
                invincibleCounter = invincibleLength;

                theSR.color = new Color(theSR.color.r, theSR.color.g, theSR.color.b, .5f);
                Playercontroller.sharedInstance.KnockBack();
            }
            UIController.sharedInstance.UpdateHealthDisplay();
        }
    }

    public void HealPlayer()
    {
        currentHealth++;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
            
        }
        UIController.sharedInstance.UpdateHealthDisplay();
    }
}
