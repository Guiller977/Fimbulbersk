using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrower : MonoBehaviour
{
    public GameObject iceAxe, fireAxe, iceAxeSprite, fireAxeSprite, iceAxeReference, fireAxeReference;
    public bool iceCanBeThrown, fireCanBeThrown;
    public float damage;

    public static AxeThrower sharedInstance;

    // Start is called before the first frame update
    void Start()
    {
        iceCanBeThrown = true;
        fireCanBeThrown = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Playercontroller.sharedInstance.reference.isPaused == false)
        {
            //Lanzar hacha de hielo
            if (Input.GetKeyDown(KeyCode.Mouse0) && iceCanBeThrown)
            {
                iceAxeSprite.SetActive(false);
                iceAxeReference = Instantiate(iceAxe, transform.position, transform.rotation);
                iceCanBeThrown = false;
            }
            //Recoger hacha de hielo
            else if (Input.GetKeyDown(KeyCode.Mouse0) && !iceCanBeThrown)
            {
                iceAxeSprite.SetActive(true);
                iceCanBeThrown = true;
                Destroy(iceAxeReference);
            }
            //Lanzar hacha de fuego
            if (Input.GetKeyDown(KeyCode.Mouse1) && fireCanBeThrown)
            {
                fireAxeSprite.SetActive(false);
                fireAxeReference = Instantiate(fireAxe, transform.position, transform.rotation);
                fireCanBeThrown = false;
            }
            //Recoger hacha de fuego
            else if (Input.GetKeyDown(KeyCode.Mouse1) && !fireCanBeThrown)
            {
                fireAxeSprite.SetActive(true);
                fireCanBeThrown = true;
                Destroy(fireAxeReference);
            }
        }
    }
}
