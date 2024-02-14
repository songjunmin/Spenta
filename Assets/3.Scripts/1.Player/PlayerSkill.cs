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
        Debug.Log(dmg);
    }
}
