using UnityEngine;
using System.Collections;

public class Stockpile_Item : MonoBehaviour
{
    //--- Public Variables ---//
    public GameObject m_siblingTetronimo;



    //--- Private Variables ---//
    private Item_Type m_itemType;
    private Sprite m_itemSprite;
    private Item m_itemController;



    //--- Public Methods ---//
    public void Init(Item_Type _type, Sprite _itemSprite)
    {
        m_itemType = _type;
        m_itemSprite = _itemSprite;

        m_itemController = GetComponent<Item>();
        m_itemController.Init(m_itemType, m_itemSprite);
    }

    private void OnMouseDown()
    {
        var sibling = Instantiate(m_siblingTetronimo);
        sibling.GetComponent<Item>().Init(m_itemType, m_itemSprite);
        sibling.GetComponent<Item_Placer>().SetSibling(this.gameObject);
    }
}
