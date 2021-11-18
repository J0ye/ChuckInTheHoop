using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public float spawnForce = 10f;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Respawn(other.gameObject);  
        } else 
        {
            Destroy(other.gameObject);
        }  
    }

    public void RespawnPlayer()
    {
        Respawn(GameManager.Instance.player);
        GameManager.Instance.player.GetComponent<BetterJumping>().enabled = true;
    }

    private void Respawn(GameObject target)
    {
        target.transform.position = transform.position;
        target.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        target.GetComponent<Rigidbody2D>().velocity += (Vector2)transform.up * spawnForce;
    }
}
