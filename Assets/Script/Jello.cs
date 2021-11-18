using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
public class Jello : MonoBehaviour
{
    public float jelloPower = 10f;
    public AudioSource audio;

    private Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.GetComponent<Collider2D>())
        {
            Vector2 dir = (Vector2)transform.position - other.GetContact(0).point;
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * jelloPower, ForceMode2D.Impulse);

            anim.SetTrigger("Action");
            if(audio)
                audio.Stop();
                audio.Play();
        }
    }
}
