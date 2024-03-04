using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshaFire : MonoBehaviour
{
    int leftRange;
    int rightRange;

    private void Start()
    {
        leftRange = GameManager.instance.Player.GetComponent<PlayerStatus>().skillRange[2].GetRange1();
        rightRange = GameManager.instance.Player.GetComponent<PlayerStatus>().skillRange[2].GetRange2();

        Damage();
        if (!GameManager.instance.Player.GetComponent<PlayerStatus>().isAhsaEnforce)
        {
            
            Destroy(gameObject,0.3f);

        }
        else
        {
            Destroy(gameObject, 3.3f);
        }
    }

    float coolT = 1f;
    int loot = 3;

    void Update()
    {
        coolT -= Time.deltaTime;

        if (coolT <= 0f && loot > 0)
        {
            coolT = 1f;
            loot--;
            Damage();
        }

    }
    
    public void Damage()
    {
        Collider2D[] hits = Physics2D.OverlapAreaAll(new Vector2(transform.position.x + leftRange, transform.position.y - 8f), new Vector2(transform.position.x + rightRange, transform.position.y));

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject.layer == 7)
            {
                hit.GetComponent<EnemyStatus>().Damaged(GameManager.instance.Player.GetComponent<PlayerStatus>().attackPower, GameManager.instance.Player.GetComponent<PlayerStatus>().skillDmg[2], 1f);

            }
        }
    }

}
