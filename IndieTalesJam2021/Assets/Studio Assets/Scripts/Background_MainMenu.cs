using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background_MainMenu : MonoBehaviour
{
    //Public Variables
    public List<GameObject> tetrominoes;
    public int spawnTotal;
    //Private Variables
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private int spawnRange;
    [SerializeField] private float m_spawnDelay;
    [SerializeField] private List<Sprite> m_itemSprites;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnBackgroundItem(spawnTotal));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator spawnBackgroundItem(int numToSpawn)
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            //Determine tetromino piece to spawn
            GameObject spawnPiece = tetrominoes[Random.Range(0, tetrominoes.Count)];

            //Determine spawnpoint
            Vector3 spawnLoc = new Vector3(Random.Range(spawnPoint.transform.position.x - spawnRange, spawnPoint.transform.position.x + spawnRange), spawnPoint.transform.position.y);

            //Determine random rotation + convert to Quaternion
            Quaternion spawnRot = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));

            var spawnedObject = Instantiate(spawnPiece, spawnLoc, spawnRot);

            var itemType = (Item_Type)Random.Range(0, (int)Item_Type.Count);
            spawnedObject.GetComponent<Item>().Init(itemType, m_itemSprites[(int)itemType]);
            Destroy(spawnedObject.GetComponent<Stockpile_Item>());


            // If not at the end of the final spawning, wait a second before spawning the next one
            if (i < numToSpawn - 1)
                yield return new WaitForSeconds(m_spawnDelay);
        }
    }
}

