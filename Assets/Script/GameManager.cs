using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(PrefabList))]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [Range(0, 10f)]
    public float respawnTime = 1f;

    public GameObject deathParticle;

    [Space]
    public Room room;
    public GameObject player;
    public PrefabList screams;

    private PrefabList rooms; 

    void Awake()
    {
        if(Instance) Destroy(gameObject);
        if(!Instance) Instance = this;

        player = GameObject.FindWithTag("Player");
        rooms = GetComponent<PrefabList>();
        FocusRoom();
    }

    void Update()
    {
        if(Input.GetKeyUp(KeyCode.Return)) Restart();
    }

    public void Goal(bool usePath, GameObject target)
    {
        Vector3 point = room.snapPoint.position;
        Destroy(room.spawn.gameObject);
        Destroy(room.goal.gameObject);

        GameObject roomPrefab;
        if(!usePath)
        {
            roomPrefab = rooms.GetExclusive();
        } else 
        {
            roomPrefab = target;
        }
        GameObject roomObj = Instantiate(roomPrefab, room.transform.position, Quaternion.identity);
        roomObj.transform.position = point;
        room = roomObj.GetComponent<Room>();

        Restart();
        FocusRoom();
        Debug.Log("Reach the goal");
    }

    public void Restart()
    {
        Debug.Log("Restarting");
        ResetRoom();
        StartCoroutine(WaitUntilSpawn());
    }

    private void ResetRoom()
    {
        foreach (GameObject go in GetObjectList())
        {
            go.GetComponent<ObjectScript>().Reset();
        }
    }

    private void FocusRoom()
    {
        if(room)
        {
            Vector3 target = new Vector3(room.transform.position.x, room.transform.position.y, Camera.main.transform.position.z);
            Camera.main.transform.DOMove(target, 0.3f, false);
        }
    }

    private IEnumerator WaitUntilSpawn()
    {
        if(player.GetComponent<TimeSlow>())
            Destroy(player.GetComponent<TimeSlow>());

        if(player.transform.parent != null)
        {
            if(player.transform.parent.gameObject.GetComponent<Ring>())
            {
                player.transform.parent.gameObject.GetComponent<Ring>().Release();
            }
        }
        player.SetActive(false);
        player.GetComponent<Rigidbody2D>().gravityScale = 1f;
        Instantiate(screams.GetRandom(), transform.position, Quaternion.identity); // death scream
        Instantiate(deathParticle, player.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(respawnTime);
        
        player.SetActive(true);
        room.spawn.RespawnPlayer();
    }

    private List<GameObject> GetObjectList()
    {
        List<GameObject> objects = new List<GameObject>();           
        foreach (Transform go in room.transform)
        {
            if(go.GetComponent<ObjectScript>()) objects.Add(go.gameObject);
        }

        return objects;
    }
}
