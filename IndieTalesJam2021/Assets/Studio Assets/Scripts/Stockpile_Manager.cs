using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stockpile_Manager : MonoBehaviour
{
    //Public Variables
    public List<GameObject> tetrominoes;

    public int spawnMax;

    //Private Variables
    [SerializeField] private BoxCollider2D ceilingCollider;
    [SerializeField] private GameObject spawnPoint;
    [Tooltip("+/- this value to spawnpoint X value for spawning range")]
    [SerializeField] private int spawnRange;
    [SerializeField] private bool aboveLimit = false;

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

    public void SpawnShipment(List<Item_Type> _itemsToSpawn)
    {
        //int numToSpawn = Random.Range(1, spawnMax + 1);
        int numToSpawn = _itemsToSpawn.Count;

        for (int i = 0; i < numToSpawn; i++)
        {
            //Determine tetromino piece to spawn
            GameObject spawnPiece = tetrominoes[Random.Range(0, tetrominoes.Count)];

            //Determine spawnpoint
            Vector3 spawnLoc = new Vector3(Random.Range(spawnPoint.transform.position.x - spawnRange, spawnPoint.transform.position.x + spawnRange), spawnPoint.transform.position.y);

            //Determine random rotation + convert to Quaternion
            Quaternion spawnRot = Quaternion.Euler(0, 0, Random.Range(0.0f, 360.0f));


            Instantiate(spawnPiece, spawnLoc, spawnRot);

            Debug.Log("New piece spawned: " + spawnPiece.name + ". Spawning at " + spawnLoc + " with rotation " + spawnRot);
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
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PhysicsObjects"))
            aboveLimit = false;
    }
}
