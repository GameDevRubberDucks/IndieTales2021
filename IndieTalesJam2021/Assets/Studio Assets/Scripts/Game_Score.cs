using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Game_Score : MonoBehaviour
{
    //--- Public Variables ---//
    public float m_scorePerItem;
    public float m_islandBonusPerItem;
    public Text m_txtScore;



    //--- Private Variables ---//
    private float m_score;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Init the private variables
        m_score = 0.0f;
    }



    //--- Public Methods ---//
    public void HandleIslandScore(List<Grid_Cell> _islandCells)
    {
        // Calculate the score increase
        // Use the equation: N x (I + (B(N - 1))
        // N = number of items in island, I = base score per item, B = bonus for additional items in chain
        float islandSize = (float)_islandCells.Count;
        float scoreIncrease = islandSize * (m_scorePerItem + ((islandSize - 1.0f) * m_islandBonusPerItem));

        // Update the score
        m_score += scoreIncrease;
        m_txtScore.text = "Score: " + m_score.ToString("F0");
    }
}
