using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Controller : MonoBehaviour
{
    private float cd, cdDuration = 2;
    public int hp, maxhp = 200;

    public int counter;

    public GameObject shockwave, bigShockwave, iceRay, preIceRay,vIceRay,vPreIceRay, plat1, plat2;
    private bool inmuneToFire, inmuneToIce;

    public Transform top, mid, bottom, shockwaveTransform, bigShockwaveTransform;
    public int nextpos, nextpos2, typeOfAttack;

    public Transform pos1, pos2, pos3;

    public Health_Bar healthbar;
    // Start is called before the first frame update
    void Start()
    {
        cd = 1.5f;
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
        if (hp < ((maxhp / 2) + 2) && hp > 0)
        {
            plat1.SetActive(true);
            plat2.SetActive(true);
            inmuneToIce = true;
            inmuneToFire = false;
            if (cd < 0)
            {
                typeOfAttack = Random.Range(1, 3);

                //Horizontal
                if(typeOfAttack == 1)
                {
                    nextpos = Random.Range(1, 4);
                    nextpos2 = Random.Range(1, 4);

                    if (nextpos == nextpos2)
                    {
                        nextpos = Random.Range(1, 4);
                        nextpos2 = Random.Range(1, 4);
                    }

                    else if (nextpos != nextpos2)
                    {
                        if (nextpos == 1 || nextpos2 == 1)
                        {
                            StartCoroutine(IceRayTimer1());
                            cd = 5;
                        }

                        if (nextpos == 2 || nextpos2 == 2)
                        {
                            StartCoroutine(IceRayTimer2());
                            cd = 5;
                        }

                        if (nextpos == 3 || nextpos2 == 3)
                        {
                            StartCoroutine(IceRayTimer3());
                            cd = 5;
                        }
                    }
                }
                
                //Vertical
                if (typeOfAttack == 2)
                {
                    StartCoroutine(IceRayTimerVertical());
                    cd = 5;
                }
            }
        }

        //Primera fase
        else
        {
            inmuneToFire = true;
            if (cd < 0 && counter < 2)
            {
                Instantiate(shockwave, shockwaveTransform.position, transform.rotation);
                cd = cdDuration;
                counter++;
            }

            if (cd < 0 && counter >= 2)
            {
                Instantiate(bigShockwave, bigShockwaveTransform.position, transform.rotation);
                cd = cdDuration;
                counter = 0;
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

    private IEnumerator IceRayTimerVertical()
    {
        pos1.transform.position = new Vector3(Random.Range(-4, 11), 4, 0);
        pos2.transform.position = new Vector3(Random.Range(-4, 11), 4, 0);
        pos3.transform.position = new Vector3(Random.Range(-4, 11), 4, 0);
        if (pos1.transform.position == pos2.transform.position || pos1.transform.position == pos3.transform.position || pos3.transform.position == pos2.transform.position)
        {
            pos1.transform.position = new Vector3(Random.Range(-4, 11), 4, 0);
            pos2.transform.position = new Vector3(Random.Range(-4, 11), 4, 0);
            pos3.transform.position = new Vector3(Random.Range(-4, 11), 4, 0);
        }

        else
        {
            Instantiate(vPreIceRay, pos1.position, vPreIceRay.transform.rotation);
            Instantiate(vPreIceRay, pos2.position, vPreIceRay.transform.rotation);
            Instantiate(vPreIceRay, pos3.position, vPreIceRay.transform.rotation);
            yield return new WaitForSeconds(2f);
            Instantiate(vIceRay, pos1.position, vIceRay.transform.rotation);
            Instantiate(vIceRay, pos2.position, vIceRay.transform.rotation);
            Instantiate(vIceRay, pos3.position, vIceRay.transform.rotation);
            yield return new WaitForSeconds(2f);
            StopCoroutine(IceRayTimerVertical());
        }
        
    }
}
