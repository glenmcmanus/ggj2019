using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static Player instance;

    public Inventory inventory;

    public float maxStamina;
    public float stamina;

    public float durationOfHarvest;

    public UnityEvent OnHarvestStart;
    public UnityEvent OnHarvestFinished;

    public bool GoingToNewPosition { get; set; } = false;

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

    public void Harvest(Harvestable selected, Action OnDone)
    {
        StartCoroutine(HarvestPrivate(selected, OnDone));
    }

    IEnumerator HarvestPrivate(Harvestable selected, Action OnDone)
    {
        OnHarvestStart.Invoke();
        GetComponentInChildren<SpriteRenderer>().flipX = transform.position.x > selected.transform.position.x;
        yield return new WaitForSeconds(durationOfHarvest);
        OnHarvestFinished.Invoke();
        OnDone();
    }

    public void MoveTowards(Vector3 Position, Action OnReached)
    {
        StartCoroutine(MoveChecks(Position, OnReached));
    }

    IEnumerator MoveChecks(Vector3 Position, Action OnReach)
    {
        var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(Position);
        Vector3 dest = agent.pathEndPosition;
        yield return null;
        GoingToNewPosition = false;

        while (!GoingToNewPosition)
        {
            if((transform.position - Position).magnitude <= 1.25f)
            {
                Debug.Log("Reached");
                OnReach();
                break;
            }
            yield return null;
        }
    }
}

public class Inventory
{
    public int wood;
    public int food;
    public int medicine;
}