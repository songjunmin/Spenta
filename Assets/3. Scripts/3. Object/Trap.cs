using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    float dmg;
    float coolTime = 0.3f;
    public float curTime;
    private void Start()
    {
        dmg = float.Parse(gameObject.name.Split("_")[1]);
    }

    private void Update()
    {
        curTime -= Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 3)
        {
            if (curTime < 0)
            {
                collision.gameObject.GetComponent<PlayerStatus>().Damaged(true, dmg, 1f, transform.position.x);
                curTime = coolTime;
            }

        }
    }
}
