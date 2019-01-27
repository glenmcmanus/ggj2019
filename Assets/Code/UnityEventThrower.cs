using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnityEventThrower : MonoBehaviour
{
    public UnityEvent Event;

    [ContextMenu("Throw Event")]
    public void ThrowUnityEvent()
    {
        Event.Invoke();
    }
}
