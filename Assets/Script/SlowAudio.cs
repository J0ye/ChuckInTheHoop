using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowAudio : MonoBehaviour
{
    [Range(0.01f, 1f)]
    public float targetScale = 0.1f;

    void Update()
    {
        if(GameManager.Instance.player.GetComponent<TimeSlow>() && GetComponent<AudioSource>().pitch != targetScale)
        {
            GetComponent<AudioSource>().pitch = targetScale;
        } else if(!GameManager.Instance.player.GetComponent<TimeSlow>())
        {
            GetComponent<AudioSource>().pitch = 1;
        }
    }
}
