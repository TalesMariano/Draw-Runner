using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(Collider2D))]
public class TriggerEvent : MonoBehaviour
{

    [Tooltip("How long in seconds it has to wait to execute event")]
    public float waitTime = 0;

    [Tooltip("Event that happen when player enter area")]
    public UnityEvent unityEvent;

    /// <summary>
    /// If player enter trigger area, execute event after delay
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            StartCoroutine(WaitInvoke());
    }
    
    /// <summary>
    /// Wait waitTime before execute event
    /// </summary>
    IEnumerator WaitInvoke()
    {
        yield return new WaitForSeconds(waitTime);
        unityEvent.Invoke();
    }
}
