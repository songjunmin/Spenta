using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        audioSource.Stop();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "stage1":
            case "stage2":
            case "stage3":
            case "stage4":
                audioSource.Play();
                break;

            default:
                audioSource.Pause();
                break;
        }
    }

    public void ChangeSound(float v)
    {
        audioSource.volume = v;
    }



}
