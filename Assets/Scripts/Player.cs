using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public Inventory inventory;

    public float stamina;

    public bool drainStamina;
    [Tooltip("Amount drained per drain tick")]
    public float stamDrain = 0.001f;
    [Tooltip("Seconds between stam drain ticks")]
    public float drainStep = 1f;
    WaitForSeconds drainWait;

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

            drainWait = new WaitForSeconds(drainStep);
        }
    }

    public void StartStaminaDrain()
    {
        StartCoroutine(StaminaDrain());
    }


    IEnumerator StaminaDrain()
    {
        drainStamina = true;

        while(drainStamina && stamina > 0)
        {
            stamina -= stamDrain;
            yield return drainWait;
        }

        Debug.Log("DEADZ");
        drainStamina = false;
    }

}

public class Inventory
{
    public int wood;
    public int food;
    public int medicine;
}