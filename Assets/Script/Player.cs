using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Collision))]
[RequireComponent(typeof(BetterJumping))]
[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [Range(1f, 50f)]
    public float speed = 10f;
    [Range(1f, 50f)]
    public float jumpForce = 10f;
    [Range(1f, 50f)]
    public float dashForce = 10f;
    [Range(1f, 50f)]
    public float slideMultiplier = 10f;
    [Range(1f, 50f)]
    public float wallJumpLerp = 10;
    [Range(0.01f, 2f)]
    public float slowScale = 0.1f;
    public float timeBetweenClucks = 1f;

    [Space]

    public GameObject burst;
    public ParticleSystem fart;
    public TrailRenderer flair;
    public PrefabList clucks;
    public PrefabList farts;
    public GameObject flap;
    public GameObject plop;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Collision coll;
    private BetterJumping bj;
    private PrefabList eggList;
    private Animator anim;

    public bool wallJumped = false;
    private bool flutterReady = true;
    private bool cluckReady = true;
    private bool dashReady = true;
    private bool canMove = true; 
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<Collision>();
        bj = GetComponent<BetterJumping>();
        eggList = GetComponent<PrefabList>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if(h != 0) Walk(h);        
        if(rb.velocity == Vector2.zero) 
        {
            anim.SetInteger("State", 0);
        } else if(rb.velocity.y < 0 && !coll.onGround)
        { 
            anim.SetInteger("State", 3);
        }else if(rb.velocity.y > 0 && !coll.onGround)
        {
            anim.SetInteger("State", 2);
        }

        if((Input.GetKeyDown(KeyCode.Space)) && coll.CheckAny())
        {
            Jump();
        } else if((Input.GetKeyDown(KeyCode.Space)) && flutterReady)
        {
            flutterReady = false;
            SpawnEgg();
            Jump();
        }

        if(Input.GetKeyDown(KeyCode.LeftShift) && dashReady)
        {
            Dash(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        }else if(coll.onGround && !Input.GetKey(KeyCode.LeftShift)) 
        {
            dashReady = true;
        }

        if(Input.GetKeyDown(KeyCode.T))
        {
            if(GetComponent<TimeSlow>())
            {
                Destroy(GetComponent<TimeSlow>());
            } else {
                TimeSlow ts = (TimeSlow)gameObject.AddComponent(typeof(TimeSlow));
                ts.targetScale = slowScale;
            }
        }

        if(coll.onGround) GroundTouch();
        if(!sr.isVisible)
        {
            StartCoroutine(CheckOutOfBounds());
        }

        if(cluckReady) Cluck();
    }

    void GroundTouch()
    {
        flutterReady = true;
        dashReady = true;
        wallJumped = false;
    }

    #region Actions
    private void Walk(float h)
    {        
        if (!canMove)
            return;

        if(h < 0)
        {
            sr.flipX = true;
        }else
        {
            sr.flipX = false;
        }    
            
        if(!wallJumped)
        {
            rb.velocity = new Vector2(h * speed, rb.velocity.y);
        }else 
        {
            rb.velocity = Vector2.Lerp(rb.velocity, (new Vector2(h * speed, rb.velocity.y)), wallJumpLerp * Time.deltaTime);
        }
        anim.SetInteger("State", 1);
    }

    private void Jump()
    {
        Vector2 dir = Vector2.up;
        if (!canMove)
            return;

        if(coll.onWall && !coll.onGround)
        {
            wallJumped = true;
            Vector2 wallDir = coll.onRightWall ? Vector2.left : Vector2.right; // Direction to wall
            dir = dir / 1.5f + wallDir /1.5f;
        }   
        rb.velocity = Vector2.zero;
        rb.velocity += dir * jumpForce;
        Instantiate(flap, transform.position, Quaternion.identity);
        Instantiate(burst, transform.position, Quaternion.identity);
    }

    private void Dash(float x, float y)
    {
        if (!canMove)
            return;        

        dashReady = false;
        if(!coll.onGround)
        {
            Camera.main.transform.DOComplete();
            Camera.main.transform.DOShakePosition(.2f, .5f, 14, 90, false, true);
            rb.velocity = Vector2.zero;
            rb.velocity += new Vector2(x, y).normalized * dashForce;
        }else 
        {
            Slide(x);
        }

        fart.Clear();
        fart.Play();
        Instantiate(farts.GetRandom(), transform.position, Quaternion.identity);
        StartCoroutine(DashWait());
    }

    private void Slide(float x)
    {
        rb.velocity = Vector2.zero;
        rb.velocity += new Vector2(x, 0).normalized * (dashForce * slideMultiplier);
        Debug.Log("Slide");
    }

    private void SpawnEgg()
    {
        int rand = UnityEngine.Random.Range(0, 100);
        if(rand >= 90)
        {            
            GameObject egg = Instantiate(eggList.GetRandom(), transform.position, Quaternion.identity);
            if(rand >= 99)
            {
                egg.transform.localScale *= 2;
            }
            Instantiate(plop, transform.position, Quaternion.identity);
        }
    }

    private void Cluck()
    {
        int rand = UnityEngine.Random.Range(0, 100);
        if(rand >= 50)
        {
            Instantiate(clucks.GetRandom(), transform.position, Quaternion.identity);
            StartCoroutine(WaitAfterCluck());
        }
    }

    IEnumerator WaitAfterCluck()
    {
        cluckReady = false;

        yield return new WaitForSeconds(timeBetweenClucks);

        cluckReady = true;
    }
    #endregion

    IEnumerator DashWait()
    {
        DOVirtual.Float(14, 0, .8f, RigidbodyDrag);
        rb.gravityScale = 0;
        bj.enabled = false;

        yield return new WaitForSeconds(.3f);
        
        rb.gravityScale = 1;
        bj.enabled = true;
    }

    IEnumerator CheckOutOfBounds()
    {
        yield return new WaitForSeconds(1f);

        if(!sr.isVisible)
        {
            GameManager.Instance.Restart();
        }
    }

    void RigidbodyDrag(float x)
    {
        rb.drag = x;
    }

    #region Ring functions
    public bool GetSpriteOrientation()
    {
        return sr.flipX;
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }
    #endregion
}
