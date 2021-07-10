using UnityEngine;

public class Item_Tile : MonoBehaviour
{
    //--- Private Variables ---//
    private Item_Type m_itemType;
    private Grid_Cell m_attachedCell;



    //--- Public Methods ---//
    public void Init(Item_Type _itemType, Sprite _sprite)
    {
        // Store the new data
        m_itemType = _itemType;
        GetComponent<SpriteRenderer>().sprite = _sprite;
    }



    //--- Setters ---//
    public void SetAttachedCell(Grid_Cell _attachedCell)
    {
        m_attachedCell = _attachedCell;
    }
}
