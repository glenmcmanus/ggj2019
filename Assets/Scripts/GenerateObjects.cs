using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateObjects : MonoBehaviour
{
    public GameObject prefab;

    public float yPos = 0;
    public BoxCollider box;

    public int maxObjects = 50;
    [Tooltip("Minimum distance between objects")]
    public float minDist = 1f;

    private void Awake()
    {
        
    }

    [ContextMenu("Generate")]
    public void Generate()
    {
        List<GameObject> objects = new List<GameObject>();
        bool skip;
        for(int i = 0; i < maxObjects; i++)
        {
            skip = false;
            Vector3 pos = new Vector3(Random.Range(Mathf.Floor( -box.size.x / 2), Mathf.Ceil( box.size.x / 2) ) + transform.position.x,
                                      yPos,
                                      Random.Range(Mathf.Floor(-box.size.z / 2), Mathf.Ceil(box.size.z / 2) ) + transform.position.z);

            foreach(GameObject go in objects)
            {
                if(Vector3.Distance(go.transform.position, pos) < minDist)
                {
                    skip = true;
                    break;
                }
            }

            if (skip)
                continue;

            GameObject o = Instantiate(prefab, pos, prefab.transform.rotation);
            o.transform.SetParent(transform);
            objects.Add(o);
        }
    }
}
