using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public GameObject first;
    public GameObject second;
    public int[] enemyCount;

    public GameObject portal;
    public GameObject bonfire;
    private void Start()
    {
        enemyCount[0] = first.transform.childCount;
        enemyCount[1] = second.transform.childCount;
    }

    public void DeadEnemy(int order)
    {
        enemyCount[order]--;
        CheckNowEnemyCount();
    }

    public void CheckNowEnemyCount()
    {
        for(int i = enemyCount.Length - 1; i  >= 0 ; i--)
        {
            if (enemyCount[i] <= 0)
            {
                NextSituation(i);
                return;
            }
        }
    }

    public void NextSituation(int order)
    {
        switch (order)
        {
            case 0:
                for (int i = 0; i < second.transform.childCount; i++)
                {
                    second.transform.GetChild(i).gameObject.SetActive(true);
                }
                break;

            case 1:
                portal.SetActive(true);
                bonfire.SetActive(true);
                break;
        }
    }
}
