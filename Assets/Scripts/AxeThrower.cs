using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeThrower : MonoBehaviour
{
    private Camera mainCam;
    private Vector3 mousePos;
    public GameObject fireAxe, iceAxe;
    public Transform fireAxeTransform, iceAxeTransform;
    public bool canThrowIce = true, canThrowFire = true;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        Vector3 rotation = mousePos - transform.position;

        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        if (Input.GetMouseButton(0) && canThrowIce)
        {
            iceAxeTransform.parent = null;
            Instantiate(iceAxe, iceAxeTransform.position, Quaternion.identity);
            canThrowIce = false;
        }

        if (Input.GetMouseButton(1) && canThrowFire)
        {
            fireAxeTransform.parent = null;
            Instantiate(fireAxe, fireAxeTransform.position, Quaternion.identity);
            canThrowFire = false;
        }
    }
}
