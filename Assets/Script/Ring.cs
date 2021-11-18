using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ring : MonoBehaviour
{
    public float timeBetweenSnaps = 0.5f;

    private GameObject player;
    private bool ready = true;


    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" && ready)
        {
            player = other.gameObject;
            player.transform.parent = transform;
            player.transform.position = other.GetContact(0).point;
            player.transform.up = -(transform.position - player.transform.position);
            player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;            
            ready = false;
        }
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && player != null)
        {
            Release();
        }
    }

    public void Release()
    {
        player.transform.parent = null;
        player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;        
        player.transform.rotation = new Quaternion(0, 0, 0, 1);
        player = null;
        StartCoroutine(Pause());
    }

    IEnumerator Pause()
    {
        yield return new WaitForSeconds(timeBetweenSnaps);

        ready = true;
    } 
}
