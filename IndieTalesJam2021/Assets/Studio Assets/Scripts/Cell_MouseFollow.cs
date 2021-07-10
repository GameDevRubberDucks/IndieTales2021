using UnityEngine;

public class Cell_MouseFollow : MonoBehaviour
{
    //--- Public Variables ---//
    public float m_zPos;



    //--- Private Variables ---//
    private Camera m_mainCam;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_mainCam = Camera.main;
    }



    private void Update()
    {
        // Get the current mouse position in world space
        var mousePosScreen = Input.mousePosition;
        var mousePosWorld = m_mainCam.ScreenToWorldPoint(mousePosScreen);

        // Move the object to match the world position
        transform.position = new Vector3(mousePosWorld.x, mousePosWorld.y, m_zPos);
    }
}
