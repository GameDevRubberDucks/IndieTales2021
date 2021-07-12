using UnityEngine;
using System.Collections;

public class StockpileItem_Collision : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        FindObjectOfType<Audio_Manager>().PlayCollisionSound();
    }
}
