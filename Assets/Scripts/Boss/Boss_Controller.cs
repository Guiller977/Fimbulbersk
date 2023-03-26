using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Controller : MonoBehaviour
{
    private float cd, cdDuration = 3;
    public float hp, maxhp = 200;

    private GameObject shockwave, iceRay;
    private bool inmuneToFire, inmuneToIce;

    public Transform top, mid, bottom;
    public int nextpos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        cd -= Time.deltaTime;
        //Muerte
        if (hp <= 0)
        {
            Destroy(this.gameObject);
        }

        //Segunda fase
        if (hp < (maxhp / 2) && hp > 0)
        {
            inmuneToIce = true;
            inmuneToFire = false;
            if (cd < 0)
            {
                nextpos = Random.Range(1, 3);

                if (nextpos == 1)
                {
                    Instantiate(iceRay, top.position, transform.rotation);
                    cd = cdDuration;
                }

                if (nextpos == 2)
                {
                    Instantiate(iceRay, mid.position, transform.rotation);
                    cd = cdDuration;
                }

                if (nextpos == 3)
                {
                    Instantiate(iceRay, bottom.position, transform.rotation);
                    cd = cdDuration;
                }

            }
        }

        //Primera fase
        else
        {
            inmuneToFire = true;
            if (cd < 0)
            {
                Instantiate(shockwave, transform.position, transform.rotation);
                cd = cdDuration;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("IceAxe") && !inmuneToIce)
        {
            hp = hp - 5;
        }

        else if (collision.CompareTag("FireAxe") && !inmuneToFire)
        {
            hp = hp - 5;
        }

        else if (collision.CompareTag("LightHitbox"))
        {
            hp = hp - 5;
        }

        else if (collision.CompareTag("HeavyHitbox"))
        {
            hp = hp - 10;
        }

        else if (collision.CompareTag("FireHitbox") && !inmuneToFire)
        {
            hp = hp - 20;
        }

        else if (collision.CompareTag("IceHitbox") && !inmuneToIce)
        {
            hp = hp - 20;
        }
    }
}
