using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrower : MonoBehaviour
{
    public GameObject iceAxe, fireAxe, iceAxeSprite, fireAxeSprite, iceAxeReference, fireAxeReference;
    public bool iceCanBeThrown, fireCanBeThrown;
    public float damage, cd, cdCounter;

    public static AxeThrower sharedInstance;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        iceCanBeThrown = true;
        fireCanBeThrown = true;
        anim.SetBool("hasIce", true);
        anim.SetBool("hasFire", true);

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        cdCounter -= Time.deltaTime;
        if (Playercontroller.sharedInstance.reference.isPaused == false)
        {
            //Lanzar hacha de hielo
            if (Input.GetKeyDown(KeyCode.Mouse0) && iceCanBeThrown && cdCounter <= 0)
            {
                //iceAxeSprite.SetActive(false);
                iceAxeReference = Instantiate(iceAxe, transform.position, transform.rotation);
                iceCanBeThrown = false;
                cdCounter = cd;
                AudioManager.sharedInstance.PlaySFX(11);
                anim.SetBool("hasIce", false);
                
            }
            //Recoger hacha de hielo
            else if (Input.GetKeyDown(KeyCode.Mouse0) && !iceCanBeThrown && cdCounter <= 0)
            {
                //iceAxeSprite.SetActive(true);
                iceCanBeThrown = true;
                Destroy(iceAxeReference);
                cdCounter = cd;
                AudioManager.sharedInstance.PlaySFX(15);
                anim.SetBool("hasIce", true);
            }
            //Lanzar hacha de fuego
            if (Input.GetKeyDown(KeyCode.Mouse1) && fireCanBeThrown && cdCounter <= 0)
            {
                //fireAxeSprite.SetActive(false);
                fireAxeReference = Instantiate(fireAxe, transform.position, transform.rotation);
                fireCanBeThrown = false;
                cdCounter = cd;
                AudioManager.sharedInstance.PlaySFX(11);
                anim.SetBool("hasFire", false);
            }
            //Recoger hacha de fuego
            else if (Input.GetKeyDown(KeyCode.Mouse1) && !fireCanBeThrown && cdCounter <= 0)
            {
                //fireAxeSprite.SetActive(true);
                fireCanBeThrown = true;
                Destroy(fireAxeReference);
                cdCounter = cd;
                AudioManager.sharedInstance.PlaySFX(15);
                anim.SetBool("hasFire", true);
            }
        }
    }
}
