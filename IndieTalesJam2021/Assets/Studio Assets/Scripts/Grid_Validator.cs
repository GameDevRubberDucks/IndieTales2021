using UnityEngine;

public class Grid_Validator : MonoBehaviour
{
    //--- Public Variables ---//
    public GameObject m_validIndicator;
    public GameObject m_invalidIndicator;



    //--- Private Variables ---//
    private Grid_Manager m_gridManager;
    private bool m_isValid;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_gridManager = FindObjectOfType<Grid_Manager>();
    }

    private void LateUpdate()
    {
        // Check the grid to see if the current position is valid on the grid
        m_isValid = (m_gridManager.GetTileOpen(transform.position));

        // Enable the correct indicator to show the validation state
        m_validIndicator.SetActive(m_isValid);
        m_invalidIndicator.SetActive(!m_isValid);
    }



    //--- Public Methods ---//
    public void PlaceIntoGrid()
    {
        // Place the tile
        m_gridManager.PlaceTile(this.gameObject);

        // Disable this script since the tile no longer needs to be validated
        m_validIndicator.SetActive(false);
        m_invalidIndicator.SetActive(false);
        this.enabled = false;

        // Enable the tile grid snapper to ensure the tile stays put now
        GetComponent<Grid_Snapper>().enabled = true;

        // Play the placement particles
        GetComponent<Tile_VFX>().PlayFX(Tile_VFXType.Placement);
    }


    
    //--- Getters ---//
    public bool GetIsValid() { return m_isValid; }
}
