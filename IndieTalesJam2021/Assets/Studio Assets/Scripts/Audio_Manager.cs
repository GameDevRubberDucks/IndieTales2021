using UnityEngine;

public enum Audio_SFX
{
    Item_Landing,
    Item_Sell,
    Item_Placed,
    Game_Over
}  

public class Audio_Manager : MonoBehaviour
{
    //--- Public Variables ---//
    [Header("Music")]
    public AudioSource m_musicSource;

    [Header("SFX")]
    public AudioSource m_sfxSource;
    public AudioClip[] m_sfxClips;



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
    public void PlayAudio(Audio_SFX _sfx)
    {
        m_sfxSource.PlayOneShot(m_sfxClips[(int)_sfx]);
    }
}
