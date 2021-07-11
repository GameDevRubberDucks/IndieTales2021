using UnityEngine;

public class Item_Placer : MonoBehaviour
{
    //--- Private Variables ---//
    private Grid_Validator[] m_validators;
    private GameObject m_siblingTetronimo;
    private Item m_itemComp;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_validators = GetComponentsInChildren<Grid_Validator>();
        m_itemComp = GetComponent<Item>();
    }

    private void Update()
    {
        // Try to place the item when pressing the mouse button
        // Or, cancel by pressing the right mouse button
        if (Input.GetMouseButtonDown(0))
            PlaceTiles();
        else if (Input.GetMouseButton(1))
            Destroy(this.gameObject);
    }



    //--- Public Methods ---//
    public void PlaceTiles()
    {
        // Start by checking if all of the child tiles are valid
        if (AreAllTilesValid())
        {
            // Change the colour of the item as feedback
            m_itemComp.UpdateColor(false);

            // Place the tiles into the grid
            PlaceTilesIntoGrid();

            // Destroy this parent object since all of the tiles have been placed
            Destroy(this.gameObject);
        }
    }



    //--- Setters ---//
    public void SetSibling(GameObject _sibling)
    {
        m_siblingTetronimo = _sibling;
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

        Destroy(m_siblingTetronimo);
    }
}
