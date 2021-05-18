using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSetLegs : MonoBehaviour
{
    public UIDrawLine drawLine;


    public Line wheel1, wheel2;

    private void OnEnable()
    {
        drawLine.OnLineFinished += TestSetWhells;
    }

    private void OnDisable()
    {
        drawLine.OnLineFinished -= TestSetWhells;
    }


    void TestSetWhells(Vector2[] listPoints)
    {
        wheel1.ClearPoints();
        wheel2.ClearPoints();

        List<Vector2> pointsConvert = new List<Vector2>(listPoints);

        for (int i = 0; i < pointsConvert.Count; i++)
        {
            pointsConvert[i] /= 530 / 3;// windows side
        }


        for (int i = 0; i < pointsConvert.Count; i++)
        {
            wheel1.AddPoint(pointsConvert[i]);
            wheel2.AddPoint(pointsConvert[i]);
        }



        wheel1.SetPosZero();
        wheel2.SetPosZero();
    }
}
