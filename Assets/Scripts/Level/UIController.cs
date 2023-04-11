using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIController : MonoBehaviour
{
    public TextMeshProUGUI gemText;

    public Image heart1, heart2, heart3, heart0, fireBar, iceBar;

    public Sprite heartFull, heartEmpty, fireEmpty, fireFull, iceEmpty, iceFull;

    public static UIController sharedInstance;

    public Image fadeScreen;

    public float fadeSpeed;

    private bool shouldFadeToBlack, shouldFadeFromBlack;

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
        UpdateHealthDisplay();

        FadeFromBlack();
    }

    // Update is called once per frame
    void Update()
    {
        //Si hay que hacer fundido a negro
        if (shouldFadeToBlack)
        {
            //Cambiar la transparencia del color a opaco
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            //Mathf.MoveTowards (Moverse hacia) -> el valor que queremos cambiar, valor al que lo queremos cambiar, velocidad a la que lo queremos cambiar
            //Si el color ya es totalmente opaco
            if (fadeScreen.color.a == 1f)
            {
                //Paramos de hacer fundido a negro
                shouldFadeToBlack = false;
            }
        }
        //Si hay que hacer fundido a transparente
        if (shouldFadeFromBlack)
        {
            //Cambiar la transparencia del color a transparente
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            //Mathf.MoveTowards (Moverse hacia) -> el valor que queremos cambiar, valor al que lo queremos cambiar, velocidad a la que lo queremos cambiar
            //Si el color ya es totalmente transparente
            if (fadeScreen.color.a == 0f)
            {
                //Paramos de hacer fundido a transparente
                shouldFadeFromBlack = false;
            }
        }
    }

    public void UpdateHealthDisplay()
    {
        if (PlayerHealthController.sharedInstance.maxHealth == 3)
        {
            heart0.gameObject.SetActive(false);
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

        if (PlayerHealthController.sharedInstance.maxHealth == 4)
        {
            heart0.gameObject.SetActive(true);
            switch (PlayerHealthController.sharedInstance.currentHealth)
            {
                case 4:
                    heart0.sprite = heartFull;
                    heart1.sprite = heartFull;
                    heart2.sprite = heartFull;
                    heart3.sprite = heartFull;
                    break;

                case 3:
                    heart0.sprite = heartEmpty;
                    heart1.sprite = heartFull;
                    heart2.sprite = heartFull;
                    heart3.sprite = heartFull;
                    break;

                case 2:
                    heart0.sprite = heartEmpty;
                    heart1.sprite = heartEmpty;
                    heart2.sprite = heartFull;
                    heart3.sprite = heartFull;
                    break;

                case 1:
                    heart0.sprite = heartEmpty;
                    heart1.sprite = heartEmpty;
                    heart2.sprite = heartEmpty;
                    heart3.sprite = heartFull;
                    break;

                case 0:
                    heart0.sprite = heartEmpty;
                    heart1.sprite = heartEmpty;
                    heart2.sprite = heartEmpty;
                    heart3.sprite = heartEmpty;
                    break;

                default:
                    heart0.sprite = heartFull;
                    heart1.sprite = heartFull;
                    heart2.sprite = heartFull;
                    heart3.sprite = heartFull;
                    break;
            }
        }

        switch (PlayerHealthController.sharedInstance.currentHealth)
        {
            case 4:
                heart0.sprite = heartFull;
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;

            case 3:
                heart0.sprite = heartEmpty;
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;

            case 2:
                heart0.sprite = heartEmpty;
                heart1.sprite = heartEmpty;
                heart2.sprite = heartFull;
                heart3.sprite = heartFull;
                break;

            case 1:
                heart0.sprite = heartEmpty;
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartFull;
                break;
            
            case 0:
                heart0.sprite = heartEmpty;
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                break;

            default:
                heart0.sprite = heartFull;
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
    public void FadeToBlack()
    {
        //Activamos la booleana de fundido a negro
        shouldFadeToBlack = true;
        //Desactivamos la booleana de fundido a transparente
        shouldFadeFromBlack = false;
    }

    //Método para hacer fundido a transparente
    public void FadeFromBlack()
    {
        //Activamos la booleana de fundido a transparente
        shouldFadeFromBlack = true;
        //Desactivamos la booleana de fundido a negro
        shouldFadeToBlack = false;
    }
}

