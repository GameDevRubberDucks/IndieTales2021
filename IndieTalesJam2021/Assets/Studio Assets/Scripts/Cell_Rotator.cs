using UnityEngine;

public class Cell_Rotator : MonoBehaviour
{
    //--- Public Variables ---//
    public KeyCode m_keyLeftRot;
    public KeyCode m_keyRightRot;
    public float m_rotationAmount;



    //--- Unity Methods ---//
    private void Update()
    {
        // Rotate left or right depending on the inputs
        if (Input.GetKeyDown(m_keyLeftRot))
            transform.Rotate(0.0f, 0.0f, m_rotationAmount);
        else if (Input.GetKeyDown(m_keyRightRot))
            transform.Rotate(0.0f, 0.0f, -m_rotationAmount);
    }
}
