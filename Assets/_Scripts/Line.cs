using UnityEngine;
using System.Collections.Generic;

public class Line : MonoBehaviour
{

    public LineRenderer lineRenderer;
    public EdgeCollider2D edgeCollider;
    public Rigidbody2D rigidBody;

     public List<Vector2> points = new List<Vector2>();
    [HideInInspector] public int pointsCount = 0;

    //The minimum distance between line's points.
    float pointsMinDistance = 0.1f;

    //Circle collider added to each line's point
    float circleColliderRadius;

    public void AddPoint(Vector2 newPoint)
    {
        //If distance between last point and new point is less than pointsMinDistance do nothing (return)
        if (pointsCount >= 1 && Vector2.Distance(newPoint, GetLastPoint()) < pointsMinDistance)
            return;

        points.Add(newPoint);
        pointsCount++;

        /*
        //Add Circle Collider to the Point
        CircleCollider2D circleCollider = this.gameObject.AddComponent<CircleCollider2D>();
        circleCollider.offset = newPoint;
        circleCollider.radius = circleColliderRadius;
        */

        //Line Renderer
        lineRenderer.positionCount = pointsCount;
        lineRenderer.SetPosition(pointsCount - 1, newPoint);

        //Edge Collider
        //Edge colliders accept only 2 points or more (we can't create an edge with one point :D )
        if (pointsCount > 1)
            edgeCollider.points = points.ToArray();
    }

    public Vector2 GetLastPoint()
    {
        return (Vector2)lineRenderer.GetPosition(pointsCount - 1);
    }

    public void UsePhysics(bool usePhysics)
    {
        // isKinematic = true  means that this rigidbody is not affected by Unity's physics engine
        rigidBody.isKinematic = !usePhysics;
    }

    public void SetLineColor(Gradient colorGradient)
    {
        lineRenderer.colorGradient = colorGradient;
    }

    public void SetPointsMinDistance(float distance)
    {
        pointsMinDistance = distance;
    }

    public void SetLineWidth(float width)
    {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        circleColliderRadius = width / 2f;

        edgeCollider.edgeRadius = circleColliderRadius;
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
        Vector3[] pointsV3 = new Vector3 [points.Count];

        for (int i = 0; i < pointsV3.Length; i++)
        {
            pointsV3[i] = points[i];
        }


        lineRenderer.SetPositions(pointsV3);

        edgeCollider.points = points.ToArray();

    }

    public void ClearPoints()
    {
        points.Clear();

        pointsCount = 0;


        //Remove Circle Collider to the Point
        CircleCollider2D[] cicles = GetComponents<CircleCollider2D>();
        for (int i = 0; i < cicles.Length; i++)
        {
            Destroy(cicles[i]);
        }

        //Line Renderer
        lineRenderer.positionCount = 0;

        //Edge Collider
        //Edge colliders accept only 2 points or more (we can't create an edge with one point :D )
        edgeCollider.Reset();
    }

}