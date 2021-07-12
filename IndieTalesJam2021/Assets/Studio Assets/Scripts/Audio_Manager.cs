using UnityEngine;

public class Audio_Manager : MonoBehaviour
{
    //--- Public Variables ---//
    [Header("Music")]
    public AudioSource m_musicSource;

    [Header("SFX")]
    public AudioSource m_sfxSource;
    public AudioClip[] m_collisionSounds;
    public int m_collisionSoundChance;
    public AudioClip m_placementSound;
    public AudioClip m_moneySound;



    //--- Private Variables ---//
    private static Audio_Manager m_instance;



    //--- Unity Methods ---//
    private void Awake()
    {
        // Make the audio persistent so it can stay through the screens
        if (m_instance != null && m_instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        // Start the background music right away
        m_musicSource.Play();
    }



    //--- Public Methods ---//
    public void PlayCollisionSound()
    {
        int collisionSoundRoll = Random.Range(0, 100);
        if (collisionSoundRoll < m_collisionSoundChance)
        {
            int collisionSFXIdx = Random.Range(0, m_collisionSounds.Length);
            m_sfxSource.PlayOneShot(m_collisionSounds[collisionSFXIdx], 0.25f);
        }
    }

    public void PlayPlacementSound()
    {
        m_sfxSource.PlayOneShot(m_placementSound, 1.0f);
    }

    public void PlayMoneySound()
    {
        m_sfxSource.PlayOneShot(m_moneySound, 0.6f);
    }
}
