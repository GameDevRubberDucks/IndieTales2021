using UnityEngine;

public class Item_Tile : MonoBehaviour
{
    //--- Private Variables ---//
    private Item_Type m_itemType;
    private Grid_Cell m_attachedCell;
    private SpriteRenderer m_spriteRend;



    //--- Public Methods ---//
    public void Init(Item_Type _itemType, Sprite _sprite)
    {
        // Store the new data
        m_itemType = _itemType;
        m_spriteRend = GetComponent<SpriteRenderer>();
        m_spriteRend.sprite = _sprite;
    }

    public void UpdateColor(Color _color)
    {
        m_spriteRend.color = _color;
    }



    //--- Setters and Getters ---//
    public void SetAttachedCell(Grid_Cell _attachedCell)
    {
        m_attachedCell = _attachedCell;
    }

    public Item_Type GetItemType()
    {
        return m_itemType;
    }

    public Sprite GetSprite()
    {
        return m_spriteRend.sprite;
    }
}
