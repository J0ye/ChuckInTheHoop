using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityControl : MonoBehaviour
{
    public KeyCode actionButton = KeyCode.G;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(actionButton))
        {
            Physics2D.gravity = new Vector2(0, 9.81f);
        } else if(Input.GetKeyUp(actionButton))
        {
            Physics2D.gravity = new Vector2(0, -9.81f);
        }
    }
}
