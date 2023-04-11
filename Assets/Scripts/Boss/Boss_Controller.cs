using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Controller : MonoBehaviour
{
    private float cd, cdDuration = 2;
    public int hp, maxhp = 200;

    public GameObject shockwave, iceRay, preIceRay;
    private bool inmuneToFire, inmuneToIce;

    public Transform top, mid, bottom, shockwaveTransform;
    public int nextpos;

    public Health_Bar healthbar;
    // Start is called before the first frame update
    void Start()
    {
        hp = maxhp;
        healthbar.SetMaxHealth(maxhp);
    }

    // Update is called once per frame
    void FixedUpdate()
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
                nextpos = Random.Range(1, 4);

                if (nextpos == 1)
                {
                    StartCoroutine(IceRayTimer1());
                    cd = 5;
                }

                if (nextpos == 2)
                {
                    StartCoroutine(IceRayTimer2());
                    cd = 5;
                }

                if (nextpos == 3)
                {
                    StartCoroutine(IceRayTimer3());
                    cd = 5;
                }
            }
        }

        //Primera fase
        else
        {
            inmuneToFire = true;
            if (cd < 0)
            {
                Instantiate(shockwave, shockwaveTransform.position, transform.rotation);
                cd = cdDuration;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("IceAxe") && !inmuneToIce)
        {
            hp = hp - 5;
            healthbar.SetHealth(hp);
        }

        else if (collision.CompareTag("FireAxe") && !inmuneToFire)
        {
            hp = hp - 5;
            healthbar.SetHealth(hp);
        }

        //else if (collision.CompareTag("LightHitbox"))
        //{
        //    hp = hp - 5;
        //}

        //else if (collision.CompareTag("HeavyHitbox"))
        //{
        //    hp = hp - 10;
        //}

        //else if (collision.CompareTag("FireHitbox") && !inmuneToFire)
        //{
        //    hp = hp - 20;
        //}

        else if (collision.CompareTag("IceHitbox") && !inmuneToIce)
        {
            hp = hp - 20;
        }
    }

    private IEnumerator IceRayTimer1()
    {
        Instantiate(preIceRay, top.position, transform.rotation);
        yield return new WaitForSeconds(2f);
        Instantiate(iceRay, top.position, transform.rotation);
        yield return new WaitForSeconds(2f);
        StopCoroutine(IceRayTimer1());
    }

    private IEnumerator IceRayTimer2()
    {
        Instantiate(preIceRay, mid.position, transform.rotation);
        yield return new WaitForSeconds(2f);
        Instantiate(iceRay, mid.position, transform.rotation);
        yield return new WaitForSeconds(2f);
        StopCoroutine(IceRayTimer2());
    }

    private IEnumerator IceRayTimer3()
    {
        Instantiate(preIceRay, bottom.position, transform.rotation);
        yield return new WaitForSeconds(2f);
        Instantiate(iceRay, bottom.position, transform.rotation);
        yield return new WaitForSeconds(2f);
        StopCoroutine(IceRayTimer3());
    }
}
