using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stockpile_Manager : MonoBehaviour
{
    //Public Variables
    public List<GameObject> tetrominoes;
    public Sprite[] m_itemSprites;

    public int spawnMax;

    //Private Variables
    [SerializeField] private BoxCollider2D ceilingCollider;
    [SerializeField] private GameObject spawnPoint;
    private Game_Manager m_gameManager;
    [Tooltip("+/- this value to spawnpoint X value for spawning range")]
    [SerializeField] private int spawnRange;
    [SerializeField] private bool aboveLimit = false;
    [SerializeField] private float m_spawnDelay;

    private void Awake()
    {
        m_gameManager = FindObjectOfType<Game_Manager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    CheckCeiling();

        //    if (!aboveLimit)
        //        SpawnShipment();
        //    else
        //        Debug.Log("<color=red>Too many shipments - fail.</color>");
        //}     
    }

    public IEnumerator SpawnShipment(List<Item_Type> _itemsToSpawn)
    {
        if (aboveLimit)
        {
            Debug.Log("<color=red>Too many shipments - fail.</color>");
            m_gameManager.EndGame();
        }

        //int numToSpawn = Random.Range(1, spawnMax + 1);
        int numToSpawn = _itemsToSpawn.Count;

        for (int i = 0; i < numToSpawn; i++)
        //foreach(var itemType in _itemsToSpawn)
        {
            //Determine tetromino piece to spawn
            GameObject spawnPiece = tetrominoes[Random.Range(0, tetrominoes.Count)];

            //Determine spawnpoint
            Vector3 spawnLoc = new Vector3(Random.Range(spawnPoint.transform.position.x - spawnRange, spawnPoint.transform.position.x + spawnRange), spawnPoint.transform.position.y);

            //Determine random rotation + convert to Quaternion
            Quaternion spawnRot = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));

            var spawnedObject = Instantiate(spawnPiece, spawnLoc, spawnRot);

            // Init the tetronimo visuals
            var itemType = _itemsToSpawn[i];
            var itemComp = spawnedObject.GetComponent<Stockpile_Item>();
            itemComp.Init(itemType, m_itemSprites[(int)itemType]);

            // If not at the end of the final spawning, wait a second before spawning the next one
            if (i < numToSpawn - 1)
                yield return new WaitForSeconds(m_spawnDelay);
        }
    }

    public void CheckCeiling()
    {
        if (aboveLimit)
        { 
            //Set fail state
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PhysicsObjects"))
            aboveLimit = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TriggerEntered");

        if (collision.CompareTag("PhysicsObjects"))
        { 
            aboveLimit = true;
        
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("TriggerExited");
        if (collision.CompareTag("PhysicsObjects"))
            aboveLimit = false;
    }
}
