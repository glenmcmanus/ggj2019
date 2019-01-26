using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerMovement : MonoBehaviour
{
    public UnityEventVector2 OnControllerMovement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private Vector2 ControllerDir;

    // Update is called once per frame
    void Update()
    {
        if((ControllerDir.x = Input.GetAxisRaw("Horizontal")) != 0 || (ControllerDir.y = Input.GetAxisRaw("Vertical")) != 0)
        {
            
        }
    }
}
