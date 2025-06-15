using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float waitTime = 1f;
    [SerializeField] private AudioClip landSfx;
    [SerializeField] private AudioClip crashSfx;
    [SerializeField] private AudioSource sfxAudio;
    [SerializeField] private ParticleSystem landParticles;
    [SerializeField] private ParticleSystem crashParticles;

    private bool isControllable = true;
    private bool isCollidable = true;

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable || !isCollidable) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("START");
                break;
            case "Finish":
                LoadNextLevel();
                break;
            case "Fuel":
                Debug.Log("FUEL");
                break;
            default:
                StartCrash();
                break;
        }
    }

    private void StartCrash()
    {
        isControllable = false;
        sfxAudio.Stop();
        crashParticles.Play();
        sfxAudio.PlayOneShot(crashSfx);
        GetComponent<Movement>().enabled = false;
        Invoke("RestartLevel", waitTime);
    }

    private void RestartLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    private void CalculateLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }

        SceneManager.LoadScene(nextScene);
    }

    private void LoadNextLevel()
    {
        isControllable = false;
        sfxAudio.Stop();
        landParticles.Play();
        sfxAudio.PlayOneShot(landSfx);
        GetComponent<Movement>().enabled = false;
        Invoke("CalculateLevel", waitTime);
    }

    private void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }
}
