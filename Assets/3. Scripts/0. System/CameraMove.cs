using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public GameObject player;

    public float speed;

    private void Start()
    {
        player = GameManager.instance.Player;
    }

    void LateUpdate()
    {
        Vector2 vector2 = Vector2.Lerp(transform.position, player.transform.position, speed);
        transform.position = new Vector3(vector2.x,vector2.y + 0.05f, -10);
    }
}
