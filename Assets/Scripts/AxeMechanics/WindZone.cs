using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindZone : MonoBehaviour
{
    public GameObject windZone, windZoneReference;
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
            collision.attachedRigidbody.velocity = new Vector2(0, 0);
            windZoneReference = Instantiate(windZone, collision.transform.position + new Vector3(0f, 2f, 0f), transform.rotation);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("FireAxe"))
        {
            Destroy(windZoneReference);
        }
    }
}
