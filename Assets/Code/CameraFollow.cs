using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform ToFollow;

    public float xOffset;
    public float yOffset;
    public float zOffset;

    private void Awake()
    {
        Vector3 pos = ToFollow.position;
        pos.x += xOffset;
        pos.y += yOffset;
        pos.z += zOffset;
        transform.position = (pos + transform.position) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = ToFollow.position;
        pos.x += xOffset;
        pos.y += yOffset;
        pos.z += zOffset;
        transform.position = (pos + transform.position) / 2;

        //transform.LookAt(ToFollow);
    }

    [ContextMenu("Update")]
    void DoUpdate()
    {
        Update();
    }
}
