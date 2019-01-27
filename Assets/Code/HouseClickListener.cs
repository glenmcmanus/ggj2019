using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HouseClickListener : MonoBehaviour
{
    public UnityEvent houseReached;
    public float minDistanceToInteractWithHouse = 1.5f;

    public void OnMouseDown()
    {
        if(Player.instance.transform.position.XZDifference(transform.position) > minDistanceToInteractWithHouse)
        {
            Player.instance.MoveTowards(minDistanceToInteractWithHouse, transform.position, () =>
            {
                houseReached.Invoke();
            });
        } else
        {
            houseReached.Invoke();
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
