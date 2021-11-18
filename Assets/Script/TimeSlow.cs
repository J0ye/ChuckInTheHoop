using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TimeSlow : MonoBehaviour
{
    [Range(0.01f, 1f)]
    public float targetScale = 0.1f;
    public float duration = .3f;
    public bool destroyOnDone = false;

    void Awake()
    {        
        Time.timeScale = targetScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        ChangePitch(targetScale); 
        DOVirtual.Float(targetScale, 1f, duration, SetScale);
    }

    private void SetScale(float scale)
    {
        if(scale >= 1)
        {
            if(destroyOnDone)
            {
                Destroy(this);
            }
        }
    }

    void OnDestroy()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale; 
        ChangePitch(1f); 
    }

    void ChangePitch(float value)
    {
        GameObject[] allObjects = UnityEngine.Object.FindObjectsOfType<GameObject>() ;
        foreach(GameObject go in allObjects)
        {
            if (go.GetComponent<AudioSource>())
            {
                go.GetComponent<AudioSource>().pitch = value;
            }
        }
    }
}
