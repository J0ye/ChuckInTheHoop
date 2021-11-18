using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Pan : MonoBehaviour
{
    public Vector2 A, B;
    public float duration = 1f;
    public bool flip = false;
    public bool flipOnStart = false;

    private Spin spin;
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<Spin>())
        {
            spin = GetComponent<Spin>();
            if(flipOnStart) spin.speed = -spin.speed;
        }
        transform.DOLocalMove(A, duration, false);
    }

    // Update is called once per frame
    void Update()
    {
        if((Vector2)transform.localPosition == A)
        {
            transform.DOLocalMove(B, duration, false);
            if(flip) spin.speed = -spin.speed;
        } else if((Vector2)transform.localPosition == B)
        {
            transform.DOLocalMove(A, duration, false);
            if(flip) spin.speed = -spin.speed;
        }
    }
}
