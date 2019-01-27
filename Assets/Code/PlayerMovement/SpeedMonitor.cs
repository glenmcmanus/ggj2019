using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedMonitor : MonoBehaviour
{
    public Animator anim;
    public Vector3 Speed;

    public Vector3 LastPosition;

    // Start is called before the first frame update
    void Start()
    {
        LastPosition = transform.position;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Speed = transform.position - LastPosition;
        Speed.Normalize();
        LastPosition = transform.position;

        anim.SetFloat("HorizontalSpeed", Speed.x);

        anim.SetFloat("VerticalSpeed", Speed.z);

        anim.SetBool("HorizontalStronger", Mathf.Abs(Speed.x) > Mathf.Abs(Speed.z));

        GetComponent<SpriteRenderer>().flipX = Speed.x < 0;
    }
}
