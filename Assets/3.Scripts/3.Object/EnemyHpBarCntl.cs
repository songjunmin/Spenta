using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHpBarCntl : MonoBehaviour
{
    Vector3 scale;

    public GameObject enemy;
    void Start()
    {
        scale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy.transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(-1 * scale.x, scale.y, scale.z);
        }
        else
        {
            transform.localScale = scale;
        }
    }
}
