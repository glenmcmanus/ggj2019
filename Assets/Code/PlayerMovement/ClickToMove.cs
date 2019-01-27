using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ClickToMove : MonoBehaviour
{
    public Camera MainCamera;
    public float MaxCastDistance = 200;

    public UnityEventVector3 OnClickHit;

    public bool IsEnabled { get; set; } = true;

    // Update is called once per frame
    void Update()
    {
        if(IsEnabled && Input.GetMouseButtonDown(0))
        {
            var ray = MainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                OnClickHit.Invoke(hit.point);
            }

        }
    }
}
