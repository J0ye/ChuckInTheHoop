using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

[RequireComponent(typeof(SpriteRenderer))]
public class Simple_Button : MonoBehaviour
{
    public Color engaged = Color.green;
    public Color idle = Color.red;
    public GameObject knob;


    public UnityEvent onPress;
    public UnityEvent onRelease;

    private List<GameObject> objs = new List<GameObject>();
    private SpriteRenderer sr;
    private bool pause;

    void Start()
    {
        StartEvent(onPress);
        StartEvent(onRelease);

        sr = knob.GetComponent<SpriteRenderer>();
        sr.color = idle;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Rigidbody2D>() && !pause)
        {
            objs.Add(other.gameObject);
            if(objs.Count == 1)
            {
                Press();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<Rigidbody2D>() && !pause)
        {
            RemoveFromList(other.gameObject);
            if(objs.Count <= 0)
            {
                Release();
            }
        }
    }

    public void Press()
    {
        sr.color = engaged;
        onPress.Invoke();
        knob.transform.DOLocalMoveY(-0.05f, 0.3f, false);
    }

    public void Release()
    {
        sr.color = idle;
        onRelease.Invoke();
        knob.transform.DOLocalMoveY(0, 0.3f, false);
    }

    private bool RemoveFromList(GameObject target)
    {
        List<GameObject> temp = new List<GameObject>();
        foreach (GameObject obj in objs)
        {
            if(obj == target)
            {
                temp.Add(obj);               
            }
        }

        foreach (GameObject obj in temp)
        {
            objs.Remove(obj);
        }

        return true;
    }

    private IEnumerator Pause()
    {
        pause = true;

        yield return new WaitForSeconds(0.2f);

        pause = false;
    }

    private void StartEvent(UnityEvent eve)
    {
        if (eve == null)
        {
            eve = new UnityEvent();
        }
    }
}
