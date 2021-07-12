using UnityEngine;
using System.Collections.Generic;
using DG.Tweening;

public class Grid_ItemClearer : MonoBehaviour
{
    private class SearchLists
    {
        public List<Grid_Cell> m_allSearchedCells = new List<Grid_Cell>();
        public List<Grid_Cell> m_allFlaggedCells = new List<Grid_Cell>();
        public List<Grid_Cell> m_allIslandCells = new List<Grid_Cell>();
    }



    //--- Private Variables ---//
    private Grid_Manager m_gridManager;
    private Game_Score m_gameScore;
    private Game_SaleTracker m_saleTracker;
    private static Dictionary<int, Vector2Int> m_directions = new Dictionary<int, Vector2Int>{{0, Vector2Int.up},
                                                                                            {1, Vector2Int.right},
                                                                                            {2, Vector2Int.down},
                                                                                            {3, Vector2Int.left}};



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_gridManager = GetComponent<Grid_Manager>();
        m_gameScore = FindObjectOfType<Game_Score>();
        m_saleTracker = FindObjectOfType<Game_SaleTracker>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //    ClearItems(Item_Type.Apples);
        //else if (Input.GetKeyDown(KeyCode.Alpha2))
        //    ClearItems(Item_Type.Bananas);
        //else if (Input.GetKeyDown(KeyCode.Alpha3))
        //    ClearItems(Item_Type.Oranges);
        //else if (Input.GetKeyDown(KeyCode.Alpha4))
        //    ClearItems(Item_Type.Grapes);
        //else if (Input.GetKeyDown(KeyCode.Alpha5))
        //    ClearItems(Item_Type.Limes);
    }



    //--- Public Methods ---//
    public void ClearItems(Item_Type _type)
    {
        // Keep track of all of the tiles throughout the search
        SearchLists searchLists = new SearchLists();

        // Search all of the tiles
        for (int x = 0; x < m_gridManager.m_gridDimensions.x; x++)
        {
            for (int y = 0; y < m_gridManager.m_gridDimensions.y; y++)
            {
                if (SearchCell(x, y, _type, searchLists))
                {
                    // We found a new island so we should clear it from the board
                    ClearIsland(searchLists.m_allIslandCells);
                }
            }
        }
    }

    private bool SearchCell(int _x, int _y, Item_Type _type, SearchLists _searchLists)
    {
        var thisCell = m_gridManager.GetCell(_x, _y, false);

        // Back out if this cell doesn't actually exist (ie: is a boundary)
        if (thisCell == null)
            return false;

        // Back out if this cell has already been searched
        if (_searchLists.m_allSearchedCells.Contains(thisCell))
            return false;

        // If this cell is a match, we should search its neighbours as well
        if (CheckForMatch(thisCell, _type, _searchLists))
        {
            for (int dir = 0; dir < 4; dir++)
            {
                Vector2Int coord = new Vector2Int(_x, _y);
                Vector2Int neighbourCoord = coord + m_directions[dir];
                SearchCell(neighbourCoord.x, neighbourCoord.y, _type, _searchLists);
            }

            return true;
        }

        // If this cell is NOT a match, back out of this branch of the search
        return false;
    }

    private bool CheckForMatch(Grid_Cell _cell, Item_Type _searchType, SearchLists _searchLists)
    {
        _searchLists.m_allSearchedCells.Add(_cell);

        if (_cell.AttachedItemTile != null)
        {
            if (_cell.AttachedItemTile.GetItemType() == _searchType)
            {
                _searchLists.m_allFlaggedCells.Add(_cell);
                _searchLists.m_allIslandCells.Add(_cell);
                return true;
            }
        }

        return false;
    }

    private void ClearIsland(List<Grid_Cell> _islandTiles)
    {
        m_gameScore.HandleIslandScore(_islandTiles);

        // Detach all of the tiles from the grid and then destroy them
        foreach (var cell in _islandTiles)
        {
            m_saleTracker.AddSale(cell.AttachedItemTile.GetItemType(), cell.AttachedItemTile.GetSprite());

            // TODO: play particles
            //Destroy(cell.AttachedItemTile.gameObject);
            cell.AttachedItemTile.GetComponent<Item_ExitAnimator>().StartAnimation();
            cell.AttachedItemTile = null;
        }

        // Finish by clearing the island list now
        _islandTiles.Clear();
    }

    //private void SearchForNeighbours(Grid_Cell _cell, Item_Type _type, Vector2Int _coord, SearchLists _lists)
    //{
    //    for (int dir = 0; dir < 4; dir++)
    //    {
    //        Vector2Int neighbourCoord = _coord + m_directions[dir];
    //        var cell = m_gridManager.GetCell(neighbourCoord.x, neighbourCoord.y);
    //        _lists.m_allSearchedCells.Add(cell);

    //        if (CheckForMatch(cell, _type))
    //        {
    //            _lists.m_allFlaggedCells.Add(cell);
    //            _lists.m_allIslandCells.Add(cell);
    //        }
    //    }
    //}

    //public void ClearItems(Item_Type _type)
    //{
    //    for(int x = 0; x < m_gridManager.m_gridDimensions.x; x++)
    //    {
    //        for (int y = 0; y < m_gridManager.m_gridDimensions.y; y++)
    //        {
    //            var cell = m_gridManager.GetCell(x, y);

    //            if (cell != null && cell.AttachedItemTile.GetItemType() == _type)
    //            {
    //                if (!m_allSearchedTiles.Contains(cell.AttachedItemTile))
    //                {
    //                    SearchForNeighbours(x, y, _type, cell, m_allTiles, m_currentIsland);
    //                }
    //            }
    //        }
    //    }
    //}

    //public void SearchForNeighbours(int _x, int _y, Item_Type _type, Grid_Cell _cell, List<Item_Tile> _allTiles, List<Item_Tile> _currentIsland)
    //{
    //    for (int dir = 0; dir < 4; dir++)
    //    {
    //        Vector2Int coord = new Vector2Int(_x, _y);

    //        switch(dir)
    //        {
    //            case 0:
    //                coord += Vector2Int.up;
    //                break;

    //            case 1:
    //                coord += Vector2Int.right;
    //                break;

    //            case 2:
    //                coord += Vector2Int.down;
    //                break;

    //            case 3:
    //            default:
    //                coord += Vector2Int.left;
    //                break;
    //        }

    //        var neighbourCell = m_gridManager.GetCell(coord.x, coord.y);

    //        if (neighbourCell != null && neighbourCell.AttachedItemTile.GetItemType() == _type)
    //        {
    //            if (!m_allTiles.Contains(cell.AttachedItemTile))
    //            {
    //                SearchForNeighbours(x, y, _type, cell, m_allTiles, m_currentIsland);
    //            }
    //        }
    //    }
    //}
}
