using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen_Scale : MonoBehaviour
{
    public TextMeshProUGUI m_scoreText;

    private float m_scoreAnimDuration;
    private Game_SaleTracker m_saleTracker;
    private EndScreen_ItemSpawner m_itemSpawner;
    private float m_scoreAnimTimeSoFar;
    private float m_finalScore;
    private bool m_animateScore;

    // Start is called before the first frame update
    void Awake()
    {
        m_itemSpawner = FindObjectOfType<EndScreen_ItemSpawner>();
        m_scoreAnimTimeSoFar = 0.0f;
        // m_finalScore = 100000.0f; 
        m_animateScore = false;
    }

    private void Start()
    {
        m_saleTracker = FindObjectOfType<Game_SaleTracker>();
        m_finalScore = m_saleTracker.GetFinalScore();
        m_scoreAnimDuration = m_itemSpawner.GetNumToSpawn() * m_itemSpawner.m_spawnTimeInterval;
    }

    // Update is called once per frame
    void Update()
    {
        // Lerp the score upwards
        if (m_animateScore && m_scoreAnimTimeSoFar < m_scoreAnimDuration)
        {
            m_scoreAnimTimeSoFar += Time.deltaTime;
            float lerpT = Mathf.Clamp(m_scoreAnimTimeSoFar / m_scoreAnimDuration, 0.0f, 1.0f);
            float lerpedScore = Mathf.Lerp(0.0f, m_finalScore, lerpT);
            m_scoreText.text = "$ " + lerpedScore.ToString("F0");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_animateScore = true;
    }
}
