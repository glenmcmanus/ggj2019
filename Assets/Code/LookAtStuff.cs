using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtStuff : MonoBehaviour
{

    public Transform ThingToLookAt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(ThingToLookAt);
        transform.LookAt(new Vector3(transform.position.x, -7, transform.position.z));
    }
}
