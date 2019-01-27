using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtStuff : MonoBehaviour
{

    public float YDistance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(new Vector3(transform.position.x, YDistance, transform.position.z));
    }

    [ContextMenu("LookAt")]
    void LookAt()
    {
        transform.LookAt(new Vector3(transform.position.x, YDistance, transform.position.z));
    }
}
