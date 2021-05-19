using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineVisual : MonoBehaviour, ILineDraw
{
    private LineRenderer lineRenderer;

    private void Start()
    {
        // Get components
        lineRenderer = GetComponent<LineRenderer>();
    }
    /// <summary>
    /// Set line renderer points
    /// </summary>
    /// <param name="points">List of poits positions</param>
    public void SetPoints(Vector2[] points)
    {
        Vector3[] v3Points = new Vector3[points.Length];

        for (int i = 0; i < v3Points.Length; i++)
        {
            v3Points[i] = (Vector3)points[i];
        }

        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(v3Points);
    }

    /// <summary>
    /// Set line renderer points
    /// </summary>
    /// <param name="points">List of poits positions</param>
    public void SetPoints(Vector3[] points)
    {
        lineRenderer.positionCount = points.Length;
        lineRenderer.SetPositions(points);
    }
}
