using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIController : MonoBehaviour
{
    public TextMeshProUGUI gemText;

    public Image heart1, heart2, heart3, fireBar, iceBar;

    public Sprite heartFull, heartEmpty, fireEmpty, fireFull, iceEmpty, iceFull;

    public static UIController sharedInstance;

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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateHealthDisplay()
    {
        switch (PlayerHealthController.sharedInstance.currentHealth)
        {
            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;

            case 2:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;

            case 1:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartFull;
                break;
            
            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;

            default:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;
        }
    }

    public void updateBars()
    {
        if (Playercontroller.sharedInstance.fireAttackCD <= 0)
        {
            fireBar.sprite = fireFull;
        }

        if (Playercontroller.sharedInstance.fireAttackCD > 0)
        {
            fireBar.sprite = fireEmpty;
        }

        if (Playercontroller.sharedInstance.iceAttackCD <= 0)
        {
            iceBar.sprite = iceFull;
        }

        if (Playercontroller.sharedInstance.iceAttackCD > 0)
        {
            iceBar.sprite = iceEmpty;
        }
    }

}
