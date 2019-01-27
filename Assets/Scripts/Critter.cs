using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Critter : MonoBehaviour
{
    public bool alive = true;
    public NavMeshAgent agent;
    public Animator animator;
    public SpriteRenderer spriteRenderer;
    public bool fleeing;

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
            if (fleeing)
            {
                if(vMag > 0.01f)
                {
                    agent.SetDestination(Vector3.Cross(Player.instance.agent.velocity, Vector3.up) + transform.position);
                }
            }
            else
            {
                if (vMag < 0.01f)
                {
                    Debug.Log("rabbit stopped");
                    yield return wanderWait;

                    agent.SetDestination(new Vector3(Random.Range(-nextBounds.x, nextBounds.x) + transform.position.x, transform.position.y, Random.Range(-nextBounds.y, nextBounds.y) + transform.position.z));
                }
            }

            if(vMag > 0.01f)
            {
                spriteRenderer.flipX = agent.velocity.x < 0 ? true : false;
                animator.SetBool("Move", true);
            }

            yield return null;
        }

        alive = false;

    }
}
