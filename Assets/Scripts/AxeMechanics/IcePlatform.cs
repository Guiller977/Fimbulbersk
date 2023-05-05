using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlatform : MonoBehaviour
{
    public GameObject SolidIcePlatform, IcePlatformReference;
    private SpriteRenderer theSR;
    public Sprite baseSprite;
    public GameObject iceAxe, iceAxeReference;
    // Start is called before the first frame update
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("IceAxe"))
        {
            collision.attachedRigidbody.velocity = new Vector2(0,0);
            collision.gameObject.GetComponent<AxeController>().rotationSpeed = 0;
            IcePlatformReference = Instantiate(SolidIcePlatform, transform.position, transform.rotation);
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            AudioManager.sharedInstance.PlaySFX(1);
            theSR.sprite = null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("IceAxe"))
        {
            gameObject.transform.GetChild(0).gameObject.SetActive(true);
            StartCoroutine(Wait());
        }        
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(IcePlatformReference);
        AudioManager.sharedInstance.PlaySFX(4);
    }
}
