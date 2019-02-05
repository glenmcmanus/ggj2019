using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    public static Player instance;

    public NavMeshAgent agent;
    public Inventory inventory;

    public float maxStamina;
    private float stamina = 100;
    public float Stamina { get { return stamina; }
                           set { stamina = value;
                                 if (stamina <= 0)
                                    Die();          } }

    public float durationOfHarvest;

    public UnityEvent OnHarvestStart;
    public UnityEvent OnHarvestFinished;

    public UnityEvent OnHarvestCritterStart;
    public UnityEvent OnHarvestCritterFinished;

    public bool GoingToNewPosition { get; set; } = false;

    public bool drainStamina { get; set; } = false;
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
            //inventory = new Inventory();

            drainWait = new WaitForSeconds(drainStep);
        }
    }

    public void Start()
    {
        StartCoroutine(StaminaDrain());
    }

    public void Die()
    {
        Debug.Log("You are dead");
        GameOver.instance.OnGameOver();
    }

    public void Harvest(Harvestable selected, Action OnDone)
    {
        StartCoroutine(HarvestPrivate(selected, OnDone));
    }

    IEnumerator HarvestPrivate(Harvestable selected, Action OnDone)
    {
        if(selected is Critter)
        {
            OnHarvestCritterStart.Invoke();
            GetComponentInChildren<SpriteRenderer>().flipX = transform.position.x > selected.transform.position.x;
            yield return new WaitForSeconds(selected.durationOfHarvest);
            OnHarvestCritterFinished.Invoke();
            OnDone();
        }
        else
        {
            OnHarvestStart.Invoke();
            GetComponentInChildren<SpriteRenderer>().flipX = transform.position.x > selected.transform.position.x;
            yield return new WaitForSeconds(selected.durationOfHarvest);
            OnHarvestFinished.Invoke();
            OnDone();
        }
        
    }

    public void MoveTowards(float howCloseDoYouWannaGet, Vector3 Position, Action OnReached)
    {
        StartCoroutine(MoveChecks(howCloseDoYouWannaGet, Position, OnReached));
    }

    IEnumerator MoveChecks(float howCloseDoYouWannaGet, Vector3 Position, Action OnReach)
    {
        var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(Position);
        yield return null;
        GoingToNewPosition = false;
		
		while (!GoingToNewPosition)
        {
            if(transform.position.XZDifference(Position) <= howCloseDoYouWannaGet)
            {
                Debug.Log("Reached");
                OnReach();
                break;
            }
            yield return null;
        }
	}
	
    IEnumerator StaminaDrain()
    {
        while(Stamina > 0)
        {
            if(drainStamina) Stamina -= stamDrain;
            yield return drainWait;
        }

    }

}

[Serializable]
public class Inventory
{
    public int wood;
    public int food;
    public int medicine;
}