using UnityEngine;

public class Grid_Validator : MonoBehaviour
{
    //--- Public Variables ---//
    public Color m_colValidPlace;
    public Color m_colInvalidPlace;
    public Color m_colPlaced;



    //--- Private Variables ---//
    private Grid m_grid;
    private SpriteRenderer m_sprRend;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_grid = FindObjectOfType<Grid>();
        m_sprRend = GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {

    }
}
