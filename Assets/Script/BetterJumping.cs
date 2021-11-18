using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class BetterJumping : MonoBehaviour
{    
    [Range(0f, 20f)]
    public float extraGravity = 8f;

    private Rigidbody2D rb;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {        
        if(rb.velocity.y < 0)   // If character is falling
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (extraGravity - 1) * Time.deltaTime;
        } else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (extraGravity - 1.5f) * Time.deltaTime;
        }
    }
}
