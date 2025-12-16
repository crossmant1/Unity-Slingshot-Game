using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    [SerializeField] AudioClip realBackgroundMusic;
    [SerializeField][Range(0f, 1f)] float volume = 0.5f;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = realBackgroundMusic;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.Play();

    }
}