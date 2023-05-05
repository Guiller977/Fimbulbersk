using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    public GameObject windZone, windZoneReference;
    public GameObject FireAxe, FireAxeReference;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FireAxe"))
        {
            collision.attachedRigidbody.velocity = new Vector2(0, -5f);
            collision.GetComponent<SpriteRenderer>().sprite = null;
            collision.attachedRigidbody.freezeRotation = true;
            windZoneReference = Instantiate(windZone, collision.transform.position + new Vector3(0f, 2f, 0f), transform.rotation);
            FireAxeReference = Instantiate(FireAxe, collision.transform.position, Quaternion.Euler(0, 0, -90));
            AudioManager.sharedInstance.PlaySFX(5);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FireAxe"))
        {
            AudioManager.sharedInstance.soundEffects[5].Stop();
            Destroy(windZoneReference);
            Destroy(FireAxeReference);
        }
    }
}
