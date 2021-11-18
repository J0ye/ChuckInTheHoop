using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectScript : MonoBehaviour
{
    [Range(1f, 50f)]
    public float force = 5f;
    public bool indestructable = false;

    [Header("Design Settings")]
    public AudioSource audio;

    private Rigidbody2D rb;
    private GameObject player;
    private GameObject shadow;
    private Vector2 pos;
    private Quaternion rot;
    private bool atPlayer = false;
    // Start is called before the first frame update
    void Awake()
    {
        shadow = transform.GetChild(0).gameObject;
        rb = GetComponent<Rigidbody2D>();
        pos = transform.localPosition;
        rot = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if(atPlayer && Input.GetKeyDown(KeyCode.Space))
        {
            Push();
        }
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0;
        rb.bodyType = RigidbodyType2D.Kinematic;
        transform.localPosition = pos;
        transform.rotation = rot;
        shadow.SetActive(true);
    }

    public void Push()
    {
        if(audio)
            audio.Play();

        if(player != null)
        {        
            rb.bodyType = RigidbodyType2D.Dynamic;    
            Vector2 dir = transform.position - player.transform.position;
            dir *= force;
            rb.AddForce(dir, ForceMode2D.Impulse);
            shadow.SetActive(false);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            atPlayer = true;
            player = collision.gameObject;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            atPlayer = false;
            player = null;
        }
    }
}
