using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class Bird : MonoBehaviour
{
    [SerializeField] float maxDragDistance = 2.0f;
    [SerializeField] float launchPower = 150.0f;
    [SerializeField] AudioClip buttonthunk;
    [SerializeField] AudioClip woosh;
    [SerializeField] AudioClip levelcomplete;
    AudioSource audioSource;
    LineRenderer lineRenderer;
    Vector3 startingPosition;
    public float resetDelay = 1.0f; // Delay in seconds before reset
    public Button resetButton;

    void Start()
    {
        resetButton.onClick.AddListener(ResetBirdPosition); // Link button to method
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.enabled = false;
        startingPosition = transform.position;
        audioSource = GetComponent<AudioSource>();
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(levelcomplete);
    }

    void OnMouseUp()
    {
        Vector3 directionAndMagnitude = startingPosition - transform.position;
        GetComponent<Rigidbody2D>().AddForce(directionAndMagnitude* launchPower);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        lineRenderer.enabled = false;
        audioSource.PlayOneShot(woosh);
    }

    void OnMouseDrag()
    {
        lineRenderer.enabled = true;
        Vector3 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        destination.z = 0;
        if(Vector2.Distance(destination, startingPosition) > maxDragDistance)
        {
            destination= Vector3.MoveTowards(startingPosition, destination, maxDragDistance);
        }
        transform.position = destination;
        lineRenderer.SetPosition(1, transform.position);

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GetComponent<Rigidbody2D>().gravityScale=1;
        } 

        if (FindAnyObjectByType<Enemy>(FindObjectsInactive.Exclude)== null)
        {

            Debug.Log("Level Complete");
            int levelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(levelToLoad);
        }
    }    

    void ResetBirdPosition()
    {
        Invoke(nameof(ResetToStartPositon), resetDelay);
        audioSource.PlayOneShot(buttonthunk);

    }
    void ResetToStartPositon()
    {
        transform.position = startingPosition;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
    }
}
