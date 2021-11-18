using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapOnTrigger : MonoBehaviour
{
    public GameObject actiavte;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            actiavte.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
