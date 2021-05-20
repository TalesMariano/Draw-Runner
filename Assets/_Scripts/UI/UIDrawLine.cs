using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;


/// <summary>
/// Must be an UI component of canvas
/// </summary>
public class UIDrawLine : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public UILine uiLine;   // line Class from the ui
    public Canvas canvas;   // PArent canvas from this object


    bool drawing = false;   // if the user is drawing right now
    bool insideDrawingArea; // if cursor is inside of the drawing area

    [Tooltip("Max number of segments of line. -1 for no limit")]
    public int limitSegments = -1;

    public Action<Vector2[]> OnLineFinished;    // Event for when line is finished drawing


    private void Start()
    {
        canvas = GetComponentInParent<Canvas>();    // Get Canvas raference
    }

    /// <summary>
    /// Every frame, check fo input and draw line 
    /// </summary>
    private void Update()
    {
#if UNITY_WEBGL

        if (Input.GetMouseButtonDown(0))
            BeginDraw();

        if (Input.GetMouseButtonUp(0))
            FinishedDrawing();


        if (drawing && insideDrawingArea)
            Draw();
#else
        // Mobile input
        if (Input.touchCount >= 1)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                BeginDraw();
            }

            if (Input.touches[0].phase == TouchPhase.Ended)
            {
                FinishedDrawing();
            }
        }

        if (drawing)
            Draw();

#endif
    }


    /// <summary>
    /// Start drawing line
    /// </summary>
    void BeginDraw()
    {
        drawing = true;

        uiLine.StartDrawing();
    }

    /// <summary>
    /// Every frame where the line is being draw
    /// </summary>
    void Draw()
    {
        // Stop function when the limit of segments is resched, if there is alimit
        if (limitSegments > 0 && limitSegments < uiLine.pointsCount)
        {
            return;
        }
        
        // Get input touch position and convert to canvas position
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);

        uiLine.AddPoint(pos);   // add point in the position
    }

    /// <summary>
    /// When the 
    /// </summary>
    void FinishedDrawing()
    {
        drawing = false;

        if(uiLine.points.Count != 0)    // Only invoke line finished if line is not empty
            OnLineFinished.Invoke(uiLine.points.ToArray());

        uiLine.ClearPoints();   // Clear ui line / make it invisible
    }



    /// <summary>
    /// When the pointer enter area
    /// </summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        insideDrawingArea = true;
    }

    /// <summary>
    /// When ponter exit image, stop drawing
    /// </summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        insideDrawingArea = false;

        if (drawing)
            FinishedDrawing();
    }
}