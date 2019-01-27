using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public Transform navAgent;
    public Vector3 offset;
    public float trackLag;
    public Transform parent;
    public Footstep prefab;
    public Tracks tracks;
    public SpeedMonitor monitor;
    public float minTrackDistance = 0.25f;

    private void Awake()
    {
        tracks.Initialize();
        StartLaying();
    }

    public void StartLaying()
    {
        StartCoroutine(PollVelocity());
    }

    IEnumerator PollVelocity()
    {
        Vector3 lastTrackPos = monitor.LastPosition;
        while(true)
        {
            while(monitor.Speed.magnitude < 1)
                yield return null;

            if(Vector3.Distance(lastTrackPos, monitor.LastPosition) >= minTrackDistance)
            {
                LayStep(navAgent);
                lastTrackPos = monitor.LastPosition;
            }

            yield return null;
        }
    }

    public void LayStep(Transform t)
    {
        Footstep f = Instantiate(prefab, t.position + offset - monitor.Speed.normalized * trackLag, prefab.transform.rotation, parent);
        f.transform.Rotate(new Vector3(90, t.rotation.eulerAngles.y));
        f.spriteRenderer.sprite = tracks.sprite;
        f.spriteRenderer.material.color = Color.white;
        f.StartCoroutine(Duration(f));
    }

    IEnumerator Duration(Footstep footstep)
    {
        float t = 0;
        while (footstep.mat.color.a > 0)
        {
            footstep.spriteRenderer.material.color = Vector4.Lerp(Color.white, Color.clear, t);
            t += tracks.fadeStep;

            yield return tracks.fadeWait;
        }

        Destroy(footstep.gameObject);

    }
}
