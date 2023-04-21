using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IcePlatform : MonoBehaviour
{
    public GameObject SolidIcePlatform, IcePlatformReference;
    private SpriteRenderer theSR;
    public Sprite baseSprite;
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
            collision.attachedRigidbody.velocity = new Vector2(0, 0);
            IcePlatformReference = Instantiate(SolidIcePlatform, transform.position - new Vector3(0, 3, 0), transform.rotation);
            theSR.sprite = null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("IceAxe"))
        {
            StartCoroutine(Wait());
        }        
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(IcePlatformReference);
        theSR.sprite = baseSprite;
    }
}
