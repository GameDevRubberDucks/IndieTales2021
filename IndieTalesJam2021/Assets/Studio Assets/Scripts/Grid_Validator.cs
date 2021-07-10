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


    
    //--- Getters ---//
    public bool GetIsValid() { return m_isValid; }
}
