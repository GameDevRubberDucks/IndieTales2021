using UnityEngine;

public class Item_Placer : MonoBehaviour
{
    //--- Private Variables ---//
    private Grid_Validator[] m_validators;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_validators = GetComponentsInChildren<Grid_Validator>();
    }

    private void Update()
    {
        // Try to place the item when pressing the mouse button
        if (Input.GetMouseButtonDown(0))
            PlaceTiles();
    }



    //--- Public Methods ---//
    public void PlaceTiles()
    {
        // Start by checking if all of the child tiles are valid
        if (AreAllTilesValid())
        {
            // Place the tiles into the grid
            PlaceTilesIntoGrid();

            // Destroy this parent object since all of the tiles have been placed
            Destroy(this.gameObject);
        }
    }



    //--- Private Utility Methods ---//
    private bool AreAllTilesValid()
    {
        foreach(var validator in m_validators)
        {
            if (!validator.GetIsValid())
                return false;
        }

        return true;
    }

    private void PlaceTilesIntoGrid()
    {
        foreach(var validator in m_validators)
            validator.PlaceIntoGrid();
    }
}
