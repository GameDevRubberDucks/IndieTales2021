using UnityEngine;

public enum Item_Type
{
    Apples,
    Bananas,
    Oranges,
    Grapes,
    Limes,

    Count,
    All
}

public class Item_Spawner : MonoBehaviour
{
    //--- Public Variables ---//
    public GameObject[] m_itemTetronimoPrefabs;
    public Sprite[] m_itemSprites;
    public Item_Type m_highestSpawnableItem;



    //--- Unity Methods ---//
    private void Update()
    {
        // DEBUG: Press space to spawn a new item
        if (Input.GetKeyDown(KeyCode.Space))
            SpawnItem();
    }



    //--- Public Methods ---//
    public void SpawnItem()
    {
        // If there is already an item currently being placed, back out
        if (FindObjectOfType<Item>() != null)
            return;

        // Spawn a random tetronimo
        int tetroIdx = Random.Range(0, m_itemTetronimoPrefabs.Length);
        GameObject tetroObj = Instantiate(m_itemTetronimoPrefabs[tetroIdx]);

        // Randomly determine the item type being generated
        Item_Type type = RandomizeType();

        // Init the tetronimo
        var itemComp = tetroObj.GetComponent<Item>();
        itemComp.Init(type, m_itemSprites[(int)type]);
    }



    //--- Private Methods ---//
    private Item_Type RandomizeType()
    {
        // Need to decide what the upper limit of the randomization should be
        // If the setting is ALL, then everything should be included
        // Otherwise, the setting should be included so we need to use the next one as an upper bound
        int upperBound = (m_highestSpawnableItem == Item_Type.All || m_highestSpawnableItem == Item_Type.Count) 
                            ? (int)Item_Type.Count 
                            : (int)(m_highestSpawnableItem + 1);
        return (Item_Type)(Random.Range(0, upperBound));
    }
}
