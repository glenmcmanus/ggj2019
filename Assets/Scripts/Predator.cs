using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Predator : Critter
{
    public bool chasePlayer = false;
    public int stamDamage = 10;
    public float fleeDistance = 15f;
    public float struggleThreshold = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chasePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            chasePlayer = false;
        }
    }

    private void Awake()
    {
        StartCoroutine(Wander());
    }

    IEnumerator Wander()
    {
        alive = true;
        float vMag;
        while (alive)
        {
            vMag = agent.velocity.magnitude;
            if (fleeing)
            {
                if (vMag < 0.01f)
                {
                    agent.SetDestination(Vector3.Cross(Player.instance.agent.velocity, Vector3.up) + transform.position);
                }

                if (Vector3.Distance(transform.position, Player.instance.transform.position) >= fleeDistance)
                    fleeing = false;

            }
            else if(chasePlayer)
            {
                if(Vector3.Distance(transform.position, Player.instance.transform.position) <= struggleThreshold)
                {
                    //TODO: Add delay before stamina reduction and fleeing

                    Player.instance.stamina -= stamDamage;
                    fleeing = true;
                }
                else if(vMag < 0.01f)
                {
                    agent.SetDestination(Player.instance.transform.position);
                }
            }
            else
            {
                if (vMag < 0.01f)
                {
                    yield return wanderWait;

                    agent.SetDestination(new Vector3(Random.Range(-nextBounds.x, nextBounds.x) + transform.position.x, transform.position.y, Random.Range(-nextBounds.y, nextBounds.y) + transform.position.z));
                }
            }


            if (vMag > 0.01f)
            {
                spriteRenderer.flipX = agent.velocity.x < 0 ? true : false;
                animator.SetBool("Move", true);
            }

            yield return null;
        }

        alive = false;

    }
}
