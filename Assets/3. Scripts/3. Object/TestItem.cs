using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TestItem : MonoBehaviour
{
    public float angle;
    public float distance;

    public bool isStart;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Calculation();
        }
    }

    public void Calculation()
    {
        Vector2 vec2 = GameManager.instance.Player.transform.position - transform.position;

        angle = Mathf.Atan2(vec2.y, vec2.x);
        distance = Mathf.Sqrt(vec2.x * vec2.x + vec2.y * vec2.y);

        isStart = true;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        float remainTime = 1;
        float term = distance / 1;

        while (remainTime > 0)
        {
            remainTime -= Time.deltaTime;

            angle -= 2f * Time.deltaTime;
            distance -= term * Time.deltaTime;

            transform.position = GameManager.instance.Player.transform.position - new Vector3(Mathf.Cos(angle) * distance, Mathf.Sin(angle) * distance, 0);
            yield return null;
        }

       
    }

}


