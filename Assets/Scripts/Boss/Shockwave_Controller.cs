using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shockwave_Controller : MonoBehaviour
{
    public float movspeed;
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector2(-1f, 0f) * movspeed * Time.deltaTime);
    }
}
