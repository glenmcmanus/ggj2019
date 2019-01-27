using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventThrower : MonoBehaviour
{
    public UnityEvent Event;

    public bool callOnStart = true;
    public void Start()
    {
        ThrowUnityEvent();
    }

    [ContextMenu("Throw Event")]
    public void ThrowUnityEvent()
    {
        Event.Invoke();
    }
}
