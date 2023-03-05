using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWall : MonoBehaviour
{
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
            StartCoroutine(Melt());
        }
    }

    private IEnumerator Melt()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(this.gameObject);
    }
}
