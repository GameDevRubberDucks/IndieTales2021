using UnityEngine;
using System.Collections.Generic;

public class Game_SaleDesc
{
    public Game_SaleDesc(Item_Type _type, Sprite _sprite)
    {
        m_itemType = _type;
        m_itemSprite = _sprite;
    }

    public Item_Type m_itemType;
    public Sprite m_itemSprite;
}

public class Game_SaleTracker : MonoBehaviour
{
    //--- Private Variables ---//
    private static Game_SaleTracker m_instance;
    public Queue<Game_SaleDesc> m_allSales;
    private float m_finalScore;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Make the tracker persistent so it can stay through to the end screen
        if (m_instance != null && m_instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
            m_allSales = new Queue<Game_SaleDesc>();
            DontDestroyOnLoad(this.gameObject);
        }
    }



    //--- Public Methods ---//
    public void ResetSaleList()
    {
        m_instance.m_allSales = new Queue<Game_SaleDesc>();
    }

    public void AddSale(Item_Type _type, Sprite _sprite)
    {
        m_instance.m_allSales.Enqueue(new Game_SaleDesc(_type, _sprite));
    }



    //--- Setters and Getters ---//
    public bool HasMoreSales()
    {
        return m_instance.m_allSales.Count > 0;
    }

    public Game_SaleDesc GetNextSale()
    {
        return m_instance.m_allSales.Dequeue();
    }

    public void SetFinalScore(float _finalScore)
    {
        m_instance.m_finalScore = _finalScore;
    }

    public float GetFinalScore()
    {
        return m_instance.m_finalScore;
    }

    public int GetNumSales()
    {
        return m_instance.m_allSales.Count;
    }
}
