using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraMove : MonoBehaviour
{
    public GameObject player;

    public float speed;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    

    private void Start()
    {
        player = GameManager.instance.Player;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene , LoadSceneMode mode)
    {
        string sceneName = scene.name;

        switch (sceneName)
        {
            case "stage1":
                minX = -15;
                maxX = 25;
                minY = 3;
                maxY = 9;
                break;
            
            case "stage2":
                minX = 15;
                maxX = 75;
                minY = 4;
                maxY = 13;
                break;
            
            case "stage3":
                minX = 10;
                maxX = 55;
                minY = 0;
                maxY = 8;
                break;

            case "stage4":
                minX = 0;
                maxX = 60;
                minY = -2;
                maxY = 7;
                break;

            case "start":
                minX = 0;
                maxX = 15;
                minY = 1;
                maxY = 10;
                break;

            default:
                minX = -10;
                maxX = 100;
                minY = -10;
                maxY = 100;
                break;
        }
    }

    void LateUpdate()
    {
        Vector2 vector2 = Vector2.Lerp(transform.position, player.transform.position, speed);
        vector2 = new Vector2(Mathf.Clamp(vector2.x, minX, maxX), Mathf.Clamp(vector2.y, minY, maxY));
        transform.position = new Vector3(vector2.x,vector2.y + 0.05f, -10);
    }
}
