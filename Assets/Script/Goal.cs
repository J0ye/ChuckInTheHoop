using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public GameObject nextRoom;
    public bool isEnd = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(isEnd) SceneManager.LoadScene("Game");
            
            if(nextRoom == null)
            {
                GameManager.Instance.Goal(false, null);
            } else
            {
                GameManager.Instance.Goal(true, nextRoom);
            }
        }
    }
}
