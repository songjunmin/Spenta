using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearSkill : MonoBehaviour
{

    public float gizmosX;
    public float gizmosY;

    public bool isActive;
    public bool isHit;

    public Bear bear;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(transform.position, new Vector3(gizmosX, gizmosY, 0));
    }


    void Start()
    {
        
    }

    void Update()
    {
        if (!isHit && isActive)
        {
            Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, new Vector3(gizmosX, gizmosY, 0), 0);

            foreach (Collider2D hit in hits)
            {
                if (hit.tag == "Player")
                {
                    hit.GetComponent<PlayerStatus>().Damaged(false, bear.attackPower, bear.dmg[1], bear.transform.position.x);
                    isHit = true;
                }
            }
        }
    }

    public void ActiveOn()
    {
        isActive = true;
    }

    public void ActiveOff()
    {
        isActive = false;
        isHit = false;
    }
}
