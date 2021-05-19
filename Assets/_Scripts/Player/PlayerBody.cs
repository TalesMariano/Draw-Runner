using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof (Rigidbody2D))]
public class PlayerBody : MonoBehaviour
{
    [Tooltip("list of joint position")]
    public Vector2[] jointPos;


    /// <summary>
    /// Draw joint position on gizmos when object selected
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (var joint in jointPos)
        {
            Gizmos.DrawWireSphere(transform.position + (Vector3)joint, 0.5f);
        }
    }
}
