using UnityEngine;
using TMPro;

public class EndScreen_ItemSpawner : MonoBehaviour
{
    //--- Public Variables ---//
    public Transform m_spawnPoint;
    public Transform m_spawnParent;
    public float m_spawnRangeX;
    public float m_spawnTimeInterval;
    public GameObject[] m_itemPrefabs;
    public GameObject m_boxPrefab;
    public int m_maxSpawnCount;



    //--- Private Variables ---//
    private Game_SaleTracker m_saleTracker;
    private float m_timeSinceLastSpawn;
    private int m_numSpawned;
    private int m_numToSpawn;
    



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_timeSinceLastSpawn = m_spawnTimeInterval;
        m_numSpawned = 0;

        //for (int i = 0; i < m_maxSpawnCount; i++)
        //    m_saleTracker.AddSale(Item_Type.Apples, null);
    }

    private void Start()
    {
        m_saleTracker = FindObjectOfType<Game_SaleTracker>();
        //m_saleTracker.ResetSaleList();
        m_numToSpawn = (m_saleTracker.GetNumSales() > m_maxSpawnCount) ? m_maxSpawnCount : m_saleTracker.GetNumSales();
    }

    private void Update()
    {
        // If enough time has passed, spawn the next item
        if (m_saleTracker.HasMoreSales() && m_numSpawned < m_maxSpawnCount)
        {
            m_timeSinceLastSpawn += Time.deltaTime;
            if (m_timeSinceLastSpawn >= m_spawnTimeInterval)
            {
                SpawnNextItem();
                m_timeSinceLastSpawn = 0.0f;
                m_numSpawned++;
            }
        }
    }



    //--- Public Methods ---//
    public void SpawnNextItem()
    {
        // Get the information about the next sale
        var saleInfo = m_saleTracker.GetNextSale();

        // Determine where to spawn the object
        Vector3 spawnLoc = new Vector3(Random.Range(m_spawnPoint.position.x - m_spawnRangeX, m_spawnPoint.position.x + m_spawnRangeX), m_spawnPoint.position.y, 0.0f);

        // Spawn the new object and configure it to match the sale made
        //var newObj = Instantiate(m_itemPrefabs[(int)saleInfo.m_itemShape], m_spawnParent);
        var newObj = Instantiate(m_boxPrefab, spawnLoc, Quaternion.Euler(0.0f, 0.0f, Random.Range(0.0f, 360.0f)), m_spawnParent);
        newObj.GetComponent<Stockpile_Item>().Init(saleInfo.m_itemType, saleInfo.m_itemSprite);

        // Also, disable the updates on the component so that it doesn't interact with the mouse like in the main game
        //newObj.GetComponent<Stockpile_Item>().enabled = false;
        Destroy(newObj.GetComponent<Stockpile_Item>());
    }

    public int GetNumToSpawn()
    {
        return m_numToSpawn;
    }
}
