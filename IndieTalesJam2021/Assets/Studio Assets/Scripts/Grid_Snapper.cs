using UnityEngine;

public class Grid_Snapper : MonoBehaviour
{
    //--- Public Variables ---//
    public float m_zPos;



    //--- Private Variables ---//
    private Grid m_grid;
    private Vector3 m_initialOffset;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_grid = FindObjectOfType<Grid>();
        m_initialOffset = transform.localPosition;
    }

    private void LateUpdate()
    {
        // Move back to the correct position relative to the parent before snapping again
        // Prevents the object from getting stuck at a previous snap position
        transform.localPosition = m_initialOffset;

        // Find what cell the object falls into now
        var cellCoord = m_grid.WorldToCell(transform.position);

        // Snap the object to the center of the relevant cell
        var cellCenterWorld = m_grid.GetCellCenterWorld(cellCoord);
        transform.position = new Vector3(cellCenterWorld.x, cellCenterWorld.y, m_zPos);
    }
}
