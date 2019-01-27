using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtStuff : MonoBehaviour
{

    public Transform LookAtTransform;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(LookAtTransform);
        transform.rotation = Quaternion.identity;
    }

    [ContextMenu("LookAt")]
    void LookAt()
    {
        transform.LookAt(LookAtTransform);
    }
}
