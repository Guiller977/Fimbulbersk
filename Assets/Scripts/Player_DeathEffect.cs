using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DeathEffect : MonoBehaviour
{
    private SpriteRenderer theDeathEffectSR;
    // Start is called before the first frame update
    void Start()
    {
        //Inizializar Sprite Renderer
        theDeathEffectSR = GetComponent<SpriteRenderer>();
        if (Playercontroller.sharedInstance.isLeft)
        {
            theDeathEffectSR.flipX = false;
        }
        else if (!Playercontroller.sharedInstance.isLeft)
        {
            theDeathEffectSR.flipX = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
