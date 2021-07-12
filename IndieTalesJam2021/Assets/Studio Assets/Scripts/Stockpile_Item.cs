using UnityEngine;
using System.Collections;

public class Stockpile_Item : MonoBehaviour
{
    //--- Public Variables ---//
    public GameObject m_siblingTetronimo;
    public GameObject m_thisPhysicsTetronimoPrefab;



    //--- Private Variables ---//
    private Item_Type m_itemType;
    private Sprite m_itemSprite;
    private Item m_itemController;



    //--- Unity Methods ---//
    private void OnMouseOver()
    {
        m_itemController.UpdateColor(true);
    }

    private void OnMouseExit()
    {
        m_itemController.UpdateColor(false);
    }

    private void OnMouseDown()
    {
        // Destroy any existing items being placed on the main board
        var existingItem = FindObjectOfType<Item_Placer>();
        if (existingItem != null)
            Destroy(existingItem.gameObject);

        // Spawn a new item to be placed on the main board
        var sibling = Instantiate(m_siblingTetronimo);
        sibling.GetComponent<Item>().Init(m_itemType, m_itemSprite);
        sibling.GetComponent<Item_Placer>().SetSibling(this);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collided");
    }



    //--- Public Methods ---//
    public void Init(Item_Type _type, Sprite _itemSprite)
    {
        m_itemType = _type;
        m_itemSprite = _itemSprite;

        m_itemController = GetComponent<Item>();
        m_itemController.Init(m_itemType, m_itemSprite);
        m_itemController.UpdateColor(false);
    }

    public void RemoveFromStockpile()
    {
        //FindObjectOfType<Game_SaleTracker>().AddSale(this.m_itemType, this.m_itemSprite, m_itemController.m_itemShape);
        var fxObjs = GetComponentsInChildren<Tile_VFX>();
        foreach (var fx in fxObjs)
            fx.PlayFX(Tile_VFXType.Stockpile_Removal);

        Destroy(this.gameObject);
    }
}
