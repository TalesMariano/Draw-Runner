using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class UILine : MonoBehaviour
{
    public UILineRenderer lineRenderer; // UI line renderer

    public List<Vector2> points = new List<Vector2>();
    public int pointsCount = 0;

    public float pointsMinDistance = 0.1f;

    public void AddPoint(Vector2 newPoint)
    {
        //If distance between last point and new point is less than pointsMinDistance do nothing (return)
        if (pointsCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < pointsMinDistance)
            return;

        points.Add(newPoint);
        pointsCount++;

        //Line Renderer
        AddNewPoint(newPoint.x, newPoint.y);
        /*old
        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);*/

    }

    void AddNewPoint(float x, float y)
    {
        var point = new Vector2(x, y);
        var pointlist = new List<Vector2>(lineRenderer.Points);
        pointlist.Add(point);
        lineRenderer.Points = pointlist.ToArray();
    }

    public Vector2 GetLastPoint()
    {
        return lineRenderer.Points[pointsCount - 1];
    }

    /*
    public void SetLineColor(UnityEngine.Gradient colorGradient)
    {
        lineRenderer.colorGradient = colorGradient;
    }*/

    public void SetPointsMinDistance(float distance)
    {
        pointsMinDistance = distance;
    }

    public void SetLineWidth(float width)
    {
        /*
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        circleColliderRadius = width / 2f;

        edgeCollider.edgeRadius = circleColliderRadius;*/
    }

    [ContextMenu("SetPosZero")]
    public void SetPosZero()
    {
        if (points.Count == 0)
            return;


        Vector2 initialPos = points[0];

        for (int i = 0; i < points.Count; i++)
        {
            points[i] -= initialPos;
        }

        //array 
        Vector3[] pointsV3 = new Vector3[points.Count];

        for (int i = 0; i < pointsV3.Length; i++)
        {
            pointsV3[i] = points[i];
        }


        lineRenderer.Points = new Vector2[0];
    }

    /// <summary>
    /// when 
    /// </summary>
    public void StartDrawing()
    {
        lineRenderer.Points = new Vector2[0];
    }

    public void ClearPoints()
    {
        points.Clear();

        pointsCount = 0;


        //Line Renderer
        lineRenderer.Points = new Vector2[] { Vector2.zero }; //new Vector2[0];

    }
}
