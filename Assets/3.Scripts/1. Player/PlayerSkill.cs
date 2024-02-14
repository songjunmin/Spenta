using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    public Animator animator;
    
    PlayerStatus playerStatus;

    private void Start()
    {
        playerStatus = gameObject.GetComponentInParent<PlayerStatus>();
    }
    public void UseSkill(PlayerAction.SkillName skillName)
    {
        switch (skillName)
        {
            case PlayerAction.SkillName.Bohuman:

                break;

            case PlayerAction.SkillName.Cassatra:

                break;

            case PlayerAction.SkillName.Asha:

                break;

               
        }


    }

    public void Asha()
    {
        float dmg = playerStatus.attackPower * playerStatus.skillDmg[2];

        Vector2 point1 = new Vector2(transform.position.x + playerStatus.skillRange[2].GetRange1() , transform.position.y + 10f);
        Vector2 point2 = new Vector2(transform.position.x + playerStatus.skillRange[2].GetRange2(), transform.position.y);

        Collider2D[] colls = Physics2D.OverlapAreaAll(point1, point2);

        foreach(Collider2D coll in colls)
        {
            if (coll.tag == "Enemy")
            {
                coll.GetComponent<EnemyStatus>().Damaged(playerStatus.attackPower, playerStatus.skillDmg[2]);
            }
        }

        Debug.Log(dmg);
    }
}
