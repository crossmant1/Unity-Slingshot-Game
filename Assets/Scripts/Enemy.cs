using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float destroyImpactMagnitude = 5.0f;
    [SerializeField] AudioClip impact;
    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.LogWarning("AudioSource was missing on Enemy GameObject. Added one.", this);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool shouldDestroy = false;

        if (collision.collider.GetComponent<Bird>() != null)
        {
            shouldDestroy = true;
        }
        else if (collision.relativeVelocity.magnitude > destroyImpactMagnitude)
        {
            shouldDestroy = true;
        }

        if (shouldDestroy)
        {
            if (audioSource != null && impact != null)
            {
                GameObject audioObject = new GameObject("EnemyImpactAudio");
                AudioSource tempAudioSource = audioObject.AddComponent<AudioSource>();
                tempAudioSource.PlayOneShot(impact);
                Destroy(audioObject, impact.length); 
            }


            Destroy(gameObject); // Destroy enemy after playing sound
        }
    }
}
