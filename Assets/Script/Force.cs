using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    public float force = 10f;

    public void OnTriggerStay2D(Collider2D other)
    {
        if(other.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            rb.AddForce(transform.up * force, ForceMode2D.Force);
        }
    }
}
