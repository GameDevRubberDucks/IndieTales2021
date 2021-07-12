using UnityEngine;

public enum Item_Shape
{
    H,
    J,
    L,
    Line,
    Plus,
    Single,
    T,

    Count
}

public class Item : MonoBehaviour
{
    //--- Public Variables ---//
    public Color m_interactedColor;
    public Color m_staticColor;
    public Item_Shape m_itemShape;



    //--- Private Variables ---//
    private Item_Type m_itemType;
    private Item_Tile[] m_tiles;



    //--- Public Methods ---//
    public void Init(Item_Type _itemType, Sprite _tileSprite)
    {
        // Store the data
        m_itemType = _itemType;

        // Gather all of the children and set them up as well
        m_tiles = GetComponentsInChildren<Item_Tile>();
        foreach (var tile in m_tiles)
            tile.Init(_itemType, _tileSprite);
    }

    public void UpdateColor(bool _useInteractableColor)
    {
        foreach (var tile in m_tiles)
            tile.UpdateColor(_useInteractableColor ? m_interactedColor : m_staticColor);
    }
}
