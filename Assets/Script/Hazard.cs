using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.Instance.Restart();
        } else if(collision.gameObject.GetComponent<ObjectScript>())
        {
            // Only destroy object if destructable
            if(!collision.gameObject.GetComponent<ObjectScript>().indestructable)
                collision.gameObject.GetComponent<ObjectScript>().Reset();

        } else if(collision.gameObject.GetComponent<Rigidbody2D>())
        {
            Destroy(collision.gameObject);
        }
    }
}
