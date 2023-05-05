using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateIceZone : MonoBehaviour
{
    public GameObject IceZone, IceZoneReference;
    public GameObject IceAxe, IceAxeReference;
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
            collision.attachedRigidbody.velocity = new Vector2(0, -5f);
            collision.GetComponent<SpriteRenderer>().sprite = null;
            collision.attachedRigidbody.freezeRotation = true;
            IceZoneReference = Instantiate(IceZone, collision.transform.position - new Vector3 (0, 0.90f, 0), transform.rotation);
            IceAxeReference = Instantiate(IceAxe, collision.transform.position, Quaternion.Euler(0,0,-90));
            AudioManager.sharedInstance.PlaySFX(0);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("IceAxe"))
        {
            Destroy(IceZoneReference);
            Destroy(IceAxeReference);
            StartCoroutine(Timer());
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(0.5f);
        Playercontroller.sharedInstance.moveSpeed = 8;
    }
}
