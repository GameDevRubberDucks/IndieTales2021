using UnityEngine;

public class Grid_MouseSnapper : MonoBehaviour
{
    //--- Public Variables ---//
    public float m_zPos;



    //--- Private Variables ---//
    private Camera m_mainCam;
    private Grid m_grid;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_mainCam = Camera.main;
        m_grid = FindObjectOfType<Grid>();
    }



    private void Update()
    {
        // Get the current mouse position in world space
        var mousePosScreen = Input.mousePosition;
        var mousePosWorld = m_mainCam.ScreenToWorldPoint(mousePosScreen);

        // Move the object to match the world position
        transform.position = new Vector3(mousePosWorld.x, mousePosWorld.y, m_zPos);

        // Find what cell the object falls into now
        var cellCoord = m_grid.WorldToCell(transform.position);

        // Snap the object to the center of the relevant cell
        var cellCenterWorld = m_grid.GetCellCenterWorld(cellCoord);
        transform.position = new Vector3(cellCenterWorld.x, cellCenterWorld.y, m_zPos);
    }
}
