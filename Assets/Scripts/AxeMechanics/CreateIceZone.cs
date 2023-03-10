using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateIceZone : MonoBehaviour
{
    public GameObject IceZone, IceZoneReference;
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
        if (collision.CompareTag("IceAxe"))
        {
            collision.attachedRigidbody.velocity = new Vector2(0, 0);
            IceZoneReference = Instantiate(IceZone, collision.transform.position, transform.rotation);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("IceAxe"))
        {
            Destroy(IceZoneReference);
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.5f);
        Playercontroller.sharedInstance.moveSpeed = 8;
    }
}
