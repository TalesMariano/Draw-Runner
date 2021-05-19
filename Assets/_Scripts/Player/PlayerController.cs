using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerBody playerBody;


    [Header("References")]
    //[SerializeField] Line leg;

    [SerializeField] UIDrawLine drawLine;



    [Header("Settings")]
    public bool doubleLeg = true;
    public float motorSpeed = 250;
    public float motorForce = 100;



    // Motor
    private LineMotor[] motors;
    private ILineDraw[] legsVisual;

    #region Subscriptions

    private void OnEnable()
    {
        drawLine.OnLineFinished += DrawLeg;
    }

    private void OnDisable()
    {
        drawLine.OnLineFinished -= DrawLeg;
    }


    #endregion


    private void Start()
    {
        motors = GetComponentsInChildren<LineMotor>();
        legsVisual = GetComponentsInChildren<ILineDraw>();

        // Set motor settings
        foreach (var motor in motors)
        {
            motor.SetForce(motorForce);
            motor.SetSpeed(motorSpeed);
        }

    }

    /// <summary>
    /// Draw leg, visual and physics
    /// </summary>
    /// <param name="legNodes">Array of leg nodes that will shape leg</param>
    void DrawLeg(Vector2[] legNodes)
    {
        if (doubleLeg)
            DoubleLegOneLine(legNodes);
        else
            OneLegOneLine(legNodes);

    }

    void OneLegOneLine(Vector2[] legNodes)
    {
        // Convert point position from UI do world size
        for (int i = 0; i < legNodes.Length; i++)
        {
            legNodes[i] /= 530 / 3;// windows side
        }

        // Normalize pos
        legNodes = NormalizeList(legNodes);

        //Update Visuals
        foreach (var leg in legsVisual)
        {
            leg.SetPoints(legNodes);
        }

        // Update motor collider
        foreach (var motor in motors)
        {
            motor.SetPoints(legNodes);
        }

    }


    /// <summary>
    /// Draw two oposites legs on the same motor
    /// This is necessary so both legs 
    /// </summary>
    /// <param name="legNodes"></param>
    void DoubleLegOneLine(Vector2[] legNodes)
    {
        // Convert point position from UI do world size
        for (int i = 0; i < legNodes.Length; i++)
        {
            legNodes[i] /= 530 / 3;// windows side
        }

        // Normalize pos
        legNodes = NormalizeList(legNodes);

        // Test List
        List<Vector2> doubleLegList = new List<Vector2>();
        for (int i = legNodes.Length-1; i >=0; i--)
        {
            doubleLegList.Add((legNodes[i]) * -1);
        }
        for (int i = 0; i < legNodes.Length; i++)
        {
            doubleLegList.Add((legNodes[i]));
        }

        //Update Visuals
        foreach (var leg in legsVisual)
        {
            leg.SetPoints(doubleLegList.ToArray());
        }

        // Update motor collider
        foreach (var motor in motors)
        {
            motor.SetPoints(doubleLegList.ToArray());
        }

    }

    /// <summary>
    /// Normalize vector list so first element position is 0,0
    /// </summary>
    /// <param name="vectors">Array of vectors</param>
    /// <returns>Array of vectors with position 0 with value 0,0</returns>
    Vector2[] NormalizeList(Vector2[] vectors)
    {
        if (vectors.Length <= 1)
            return new Vector2[] { Vector2.zero };

        Vector2[] result = new Vector2[vectors.Length];
        Vector2 firstElement = vectors[0];

        for (int i = 0; i < vectors.Length; i++)
        {
            result[i] = vectors[i] - firstElement; 
        }

        return result;
    }
}
