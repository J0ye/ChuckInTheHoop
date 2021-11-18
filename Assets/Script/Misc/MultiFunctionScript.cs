using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiFunctionScript : MonoBehaviour
{
    public List<Material> ColorTable = new List<Material>();

    private Material standardMaterial;
    // Start is called before the first frame update
    void Start()
    {
        if(GetComponent<Renderer>() != null)
        {
            standardMaterial = GetComponent<Renderer>().material;
        }
    }

    public void SpawnPrefab(GameObject prefab)
    {
        GameObject gb = Instantiate(prefab, transform);
    }

    public void ChangeColorTo(int index)
    {
        GetComponent<Renderer>().material = ColorTable[index];
    }

    public void ResetColor()
    {
        GetComponent<Renderer>().material = standardMaterial;
    }
}
