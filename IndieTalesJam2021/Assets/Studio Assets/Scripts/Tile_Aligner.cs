using UnityEngine;

public class Tile_Aligner : MonoBehaviour
{
    //--- Unity Methods ---//
    private void LateUpdate()
    {
        // Stay aligned no matter which direction the parent is rotated
        transform.right = Vector3.right;
    }
}
