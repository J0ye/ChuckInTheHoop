using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingFlipper : MonoBehaviour
{
    public Player pl;

    private SpriteRenderer sr;

    // Update is called once per frame
    void Update()
    {
        if(sr == null)
        {
            sr = GetComponent<SpriteRenderer>();
        }

        if(pl.GetVelocity().x > 0)
        {
            transform.localPosition = new Vector3(-0.03f, transform.localPosition.y, 0);
        } else if(pl.GetVelocity().x > 0)
        {
            transform.localPosition = new Vector3(0.03f, transform.localPosition.y, 0);
        } else if(pl.GetVelocity().x == 0)
        {
            transform.localPosition = new Vector3(0f, transform.localPosition.y, 0);
        }

        sr.flipX = pl.GetSpriteOrientation();
    }
}
