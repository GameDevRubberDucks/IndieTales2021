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

    [Header("Tiles")]
    public Transform m_tileParent;
    public string m_tileSortingLayer;



    //--- Private Variables ---//
    private Grid m_grid;
    private Grid_Cell[,] m_gridCells;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_grid = GetComponent<Grid>();
        m_gridCells = new Grid_Cell[m_gridDimensions.x, m_gridDimensions.y];
        for (int x = 0; x < m_gridDimensions.x; x++)
        {
            for (int y = 0; y < m_gridDimensions.y; y++)
                m_gridCells[x, y] = new Grid_Cell();
        }
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

    public void PlaceTile(GameObject _tile)
    {
        // Attach it to the grid cell
        var cell = GetCell(_tile.transform.position);
        var tileComp = _tile.GetComponent<Item_Tile>();
        tileComp.SetAttachedCell(cell);
        cell.AttachedItemTile = tileComp;

        // Detach the tile from its parent and re-parent it here instead
        _tile.transform.parent = this.m_tileParent;

        // Update the layering so that it now appears behind new tiles being placed
        _tile.GetComponent<SpriteRenderer>().sortingLayerName = m_tileSortingLayer;
    }



    //--- Setters and Getters ---//
    public bool SetTile(int _gridX, int _gridY, Item_Tile _tile)
    {
        // If the cell is open, attach the tile to it. Otherwise, return false to say it is already filled
        var gridCell = QueryGridArrays(_gridX, _gridY);
        if (gridCell != null && gridCell.IsOpen)
        {
            gridCell.AttachedItemTile = _tile;
            return true;
        }
        else
            return false;
    }

    public bool SetTile(Vector3 _worldPos, Item_Tile _tile)
    {
        // Convert the world position to a grid position
        var gridPos = m_grid.WorldToCell(_worldPos);

        // Use the grid position to set the tile normally
        return SetTile(gridPos.x, gridPos.y, _tile);
    }

    public Grid_Cell GetCell(int _gridX, int _gridY, bool _transformCoords = true)
    {
        return QueryGridArrays(_gridX, _gridY, _transformCoords);
    }

    public Grid_Cell GetCell(Vector3 _worldPos, bool _transformCoords = true)
    {
        var gridPos = m_grid.WorldToCell(_worldPos);
        return GetCell(gridPos.x, gridPos.y, _transformCoords);
    }

    public bool GetCellOpen(int _gridX, int _gridY, bool _transformCoords = true)
    {
        var cell = GetCell(_gridX, _gridY, _transformCoords);
        return (cell != null && cell.AttachedItemTile == null);
    }

    public bool GetTileOpen(Vector3 _worldPos, bool _transformCoords = true)
    {
        var cell = GetCell(_worldPos, _transformCoords);
        return (cell != null && cell.AttachedItemTile == null);
    }



    //--- Utility Methods ---//
    private Grid_Cell QueryGridArrays(int _gridCoordX, int _gridCoordY, bool _transformCoords = true)
    {
        // May need to convert the coordinates from [-halfWidth, halfWidth] to [0, width]
        // This way, the arrays can be accessed correctly, without having negative indices
        int arrayIndexX = (_transformCoords) ? _gridCoordX + (m_gridDimensions.x / 2) : _gridCoordX;
        int arrayIndexY = (_transformCoords) ? _gridCoordY + (m_gridDimensions.y / 2) : _gridCoordY;

        // Ensure the indices are actual in the bounds of the grid
        if (arrayIndexX < 0 || arrayIndexX >= m_gridDimensions.x ||
            arrayIndexY < 0 || arrayIndexY >= m_gridDimensions.y)
            return null;

        // Return the tile at the given position
        return m_gridCells[arrayIndexX, arrayIndexY];
    }
}
