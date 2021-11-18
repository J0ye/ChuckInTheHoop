using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float speed = 1f;
    
    void Update()
    {
        transform.Rotate(0, 0, 180 * Time.deltaTime * speed, Space.World);
    }
}
