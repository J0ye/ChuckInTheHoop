using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopOnTrigger : MonoBehaviour
{
    public AudioSource audio;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            foreach(Transform go in other.gameObject.transform)
            {
                if(go.gameObject.name == "Ring")
                {
                    if(go.gameObject.activeSelf)
                    {                        
                        Destroy(go.gameObject);
                        audio.Play();
                    }
                }
            }
        }
    }
}
