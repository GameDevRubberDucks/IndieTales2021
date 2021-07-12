using UnityEngine;

public class EndScreen_ItemSpawner : MonoBehaviour
{
    //--- Public Variables ---//
    public Transform m_spawnPoint;
    public Transform m_spawnParent;
    public float m_spawnRangeX;
    public float m_spawnTimeInterval;
    public GameObject[] m_itemPrefabs;



    //--- Private Variables ---//
    private Game_SaleTracker m_saleTracker;
    private float m_timeSinceLastSpawn;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_saleTracker = FindObjectOfType<Game_SaleTracker>();
        m_timeSinceLastSpawn = m_spawnTimeInterval;
    }

    private void Update()
    {
        // If enough time has passed, spawn the next item
        if (m_saleTracker.HasMoreSales())
        {
            m_timeSinceLastSpawn += Time.deltaTime;
            if (m_timeSinceLastSpawn >= m_spawnTimeInterval)
            {
                SpawnNextItem();
                m_timeSinceLastSpawn = 0.0f;
            }
        }
    }



    //--- Public Methods ---//
    public void SpawnNextItem()
    {
        // Get the information about the next sale
        var saleInfo = m_saleTracker.GetNextSale();

        // Determine where to spawn the obejct
        Vector3 spawnLoc = new Vector3(Random.Range(m_spawnPoint.position.x - m_spawnRangeX, m_spawnPoint.position.x + m_spawnRangeX), m_spawnPoint.position.y, 0.0f);

        // Spawn the new object and configure it to match the sale made
        var newObj = Instantiate(m_itemPrefabs[(int)saleInfo.m_itemShape], m_spawnParent);
        newObj.GetComponent<Stockpile_Item>().Init(saleInfo.m_itemType, saleInfo.m_itemSprite);
        
        // Also, disable the updates on the component so that it doesn't interact with the mouse like in the main game
        newObj.GetComponent<Stockpile_Item>().enabled = false;
    }
}
