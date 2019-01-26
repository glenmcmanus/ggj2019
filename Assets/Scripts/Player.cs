using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public Inventory inventory;

    public float maxStamina;
    public float stamina;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            inventory = new Inventory();
        }
    }


}

public class Inventory
{
    public int wood;
    public int food;
    public int medicine;
}