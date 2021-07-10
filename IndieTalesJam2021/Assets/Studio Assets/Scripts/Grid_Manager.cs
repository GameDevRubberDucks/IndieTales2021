using UnityEngine;
using RubberDucks.Util;

public class Grid_Manager : MonoBehaviour
{
    //--- Public Variables ---//
    [Header("Grid Spawning")]
    public Vector2Int m_gridDimensions;
    public Vector2Int m_gridBoundaryDimensions;
    public int m_gridZPos;
    public Transform m_gridCellParent;
    public bool m_regenOnStart;

    [Header("Cells")]
    public GameObject m_gridCellPrefab;
    public GameObject m_gridBoundaryPrefab;



    //--- Private Variables ---//
    private Grid m_grid;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_grid = GetComponent<Grid>();
        var go = new GameObject();
    }

    private void Start()
    {
        // Generate the grid
        if (m_regenOnStart)
            GenerateGrid();
    }



    //--- Public Methods ---//
    [ContextMenu("Generate Grid")]
    public void GenerateGrid()

    {
        // First, clear any existing tiles
        ClearGrid();

        // Ensure the grid component has been assigned
        if (!m_grid)
            m_grid = GetComponent<Grid>();

        // Now, place the tiles into the world using the underlying grid information
        int halfWidth = m_gridDimensions.x / 2;
        int halfHeight = m_gridDimensions.y / 2;
        for(int row = (-halfHeight - m_gridBoundaryDimensions.y); row <= (halfHeight + m_gridBoundaryDimensions.y); row++)
        {
            for (int col = (-halfWidth - m_gridBoundaryDimensions.x); col <= (halfWidth + m_gridBoundaryDimensions.x) ; col++)
            {
                // Determine if the cell being spawned is actually one of the boundary blocks
                bool isBoundary = (row < -halfHeight || row > halfHeight) ||
                                   (col < -halfWidth || col > halfWidth);
                // Spawn the cell
                GameObject cellPrefab = (isBoundary) ? m_gridBoundaryPrefab : m_gridCellPrefab;
                var newCell = Instantiate(cellPrefab, m_gridCellParent);
                var cellNameStart = isBoundary ? "Boundary" : "Cell";
                newCell.name = cellNameStart + " [Row = " + row.ToString() + "][Col = " + col.ToString() + "]";
                newCell.transform.position = m_grid.GetCellCenterWorld(new Vector3Int(col, row, m_gridZPos));
            }
        }
    }

    public void ClearGrid()
    {
        // Delete all of the grid cells by removing any children from the grid parent
        Util_ClearChildren.ClearChildren(m_gridCellParent, true);
    }
}
