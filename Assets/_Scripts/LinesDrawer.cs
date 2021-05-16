using UnityEngine;

public class LinesDrawer : MonoBehaviour
{

    public GameObject linePrefab;
    public LayerMask cantDrawOverLayer;
    int cantDrawOverLayerIndex;

    [Space(30f)]
    public Gradient lineColor;
    public float linePointsMinDistance;
    public float lineWidth;

    Line currentLine;

    Camera cam;


    [Header("Test Whell")]
    public Line wheel1, wheel2;

    void Start()
    {
        cam = Camera.main;
        cantDrawOverLayerIndex = LayerMask.NameToLayer("CantDrawOver");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            BeginDraw();

        if (currentLine != null)
            Draw();

        if (Input.GetMouseButtonUp(0))
            EndDraw();
    }

    // Begin Draw ----------------------------------------------
    void BeginDraw()
    {
        Debug.Log("Draiwng");

        currentLine = Instantiate(linePrefab, this.transform).GetComponent<Line>();

        //Set line properties
        currentLine.UsePhysics(false);
        currentLine.SetLineColor(lineColor);
        currentLine.SetPointsMinDistance(linePointsMinDistance);
        currentLine.SetLineWidth(lineWidth);

    }
    // Draw ----------------------------------------------------
    void Draw()
    {
        Vector2 mousePosition = cam.ScreenToViewportPoint(Input.mousePosition); //cam.ScreenToWorldPoint(Input.mousePosition);

        //Check if mousePos hits any collider with layer "CantDrawOver", if true cut the line by calling EndDraw( )
        RaycastHit2D hit = Physics2D.CircleCast(mousePosition, lineWidth / 3f, Vector2.zero, 1f, cantDrawOverLayer);

        if (hit)
            EndDraw();
        else
            currentLine.AddPoint(mousePosition);
    }
    // End Draw ------------------------------------------------
    void EndDraw()
    {
        SetWhells();

        if (currentLine != null)
        {
            Destroy(currentLine.gameObject);
            /*
            if (currentLine.pointsCount < 2)
            {
                //If line has one point
                Destroy(currentLine.gameObject);
            }
            else
            {
                //Add the line to "CantDrawOver" layer
                currentLine.gameObject.layer = cantDrawOverLayerIndex;

                //Activate Physics on the line
                currentLine.UsePhysics(true);

                currentLine = null;
            }*/
        }
    }

    void SetWhells()
    {
        wheel1.ClearPoints();
        wheel2.ClearPoints();

        for (int i = 0; i < currentLine.points.Count; i++)
        {
            wheel1.AddPoint(currentLine.points[i]);
            wheel2.AddPoint(currentLine.points[i]);
        }

        wheel1.SetPosZero();
        wheel2.SetPosZero();
    }
}