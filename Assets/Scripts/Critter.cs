﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Critter : Harvestable
{
    public bool alive = true;
    public NavMeshAgent agent;
    public Animator animator;
    public bool fleeing;

    public GameObject corpse;

    [Header("Wander")]
    [Tooltip("Min/max offset range from current position to wander to next")]
    public Vector2 nextBounds = Vector2.one * 2;
    public float wanderDelay = 1f;
    public WaitForSeconds wanderWait;

    // Start is called before the first frame update
    void Start()
    {
        wanderWait = new WaitForSeconds(wanderDelay);
        StartCoroutine(Wander());
    }

    private void Update()
    {
        transform.rotation = Quaternion.identity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            fleeing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fleeing = false;
        }
    }

    IEnumerator Wander()
    {
        alive = true;
        float vMag;
        while(alive)
        {
            vMag = agent.velocity.magnitude;

            animator.SetBool("Move", vMag > 0.01f);

            if (vMag > 0.01f)
                spriteRenderer.flipX = agent.velocity.x < 0 ? true : false;

            if (fleeing)
            {
                if(vMag < 0.05f)
                {
                    agent.SetDestination(Vector3.Cross(Player.instance.agent.velocity, Vector3.up) + transform.position);
                    animator.SetBool("Move", true);
                    spriteRenderer.flipX = agent.destination.x < transform.position.x;
                }
            }
            else
            {
                if (vMag < 0.05f)
                {
                    animator.SetBool("Move", false);
                    yield return wanderWait;

                    if (gameObject == null) break;

                    agent.SetDestination(new Vector3(Random.Range(-nextBounds.x, nextBounds.x) + transform.position.x, transform.position.y, Random.Range(-nextBounds.y, nextBounds.y) + transform.position.z));
                    animator.SetBool("Move", true);
                    spriteRenderer.flipX = agent.destination.x < transform.position.x;
                }
            }

            yield return null;
        }

        alive = false;

    }

    public override void HarvestDone()
    {
        Instantiate(corpse, transform.position, Quaternion.identity);
        spriteGlow.enabled = false;
        Destroy(transform.parent.gameObject);
    }
}
