using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance = null;

    [SerializeField] private AudioClip mainMenuMusic;
    [SerializeField] private AudioClip level1Music;
    [SerializeField] private AudioClip level2Music;
    [SerializeField] private AudioClip level3Music;
    private AudioSource audioSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            audioSource = GetComponent<AudioSource>();
        }
        else
        {
            if (this != instance)
                Destroy(this.gameObject);
        }
    }

    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;

        // Check and change music depending on the scene
        if (sceneName == "MainMenu")
        {
            ChangeMusic(mainMenuMusic);
        }
        else if (sceneName == "Level1")
        {
            ChangeMusic(level1Music);
        }
        else if (sceneName == "Level2")
        {
            ChangeMusic(level2Music);
        }
        else if (sceneName == "Level3")
        {
            ChangeMusic(level3Music);
        }
       
    }

    void ChangeMusic(AudioClip newMusic)
    {
        if (audioSource.clip != newMusic)
        {
            audioSource.clip = newMusic;
            audioSource.Play();
        }
    }
}
