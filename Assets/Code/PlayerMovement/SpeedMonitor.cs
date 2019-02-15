using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedMonitor : MonoBehaviour
{
    public Animator anim;
    public bool harvesting;
    public Vector3 Speed;

    public Vector3 LastPosition;


    public bool IsEnabled { get; set; } = true;
    // Start is called before the first frame update
    void Start()
    {
        LastPosition = transform.position;
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsEnabled) return; 

        transform.rotation = Quaternion.identity;
        Speed = transform.position - LastPosition;
        Speed.Normalize();
        LastPosition = transform.position;

        anim.SetFloat("HorizontalSpeed", Speed.x);

        anim.SetFloat("VerticalSpeed", Speed.z);

        anim.SetBool("HorizontalStronger", Mathf.Abs(Speed.x) > Mathf.Abs(Speed.z));

        if (Speed.x == 0 || harvesting == true)
            return;

        GetComponent<SpriteRenderer>().flipX = Speed.x < 0;
    }

    public void StartHarvesting()
    {
        harvesting = true;
        //IsEnabled = false;
        anim.SetBool("Chopping", true);
    }

    public void StopHarvesting()
    {
        harvesting = false;
        //IsEnabled = true;
        anim.SetBool("Chopping", false);
    }

    public void HarvestCritter()
    {
        harvesting = true;
        //IsEnabled = false;
        anim.SetTrigger("Shoot");
    }

    public void StopHarvestCritter()
    {
        harvesting = false;
        IsEnabled = true;
    }
}
