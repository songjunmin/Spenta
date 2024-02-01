using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public enum SkillName
    {
        Bohuman, Cassatra, Asha, Armaity
    }


    public enum ActionName
    {
         Dash, Parrying, Attack
    }

    void Start()
    {
        
    }

    void Update()
    {
        SkillMng();
    }

    void SkillMng()
    {
        // 다른 행동 중일 경우
        if (gameObject.GetComponentInChildren<PlayerSpine>().animName != "Stand" && gameObject.GetComponentInChildren<PlayerSpine>().animName != "Run")
        {
            return;
        }
        // 점프 중일 경우(임시)
        if (gameObject.GetComponent<PlayerMove>().isJump != 0)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            // 보후만 사용
            SKillUse(SkillName.Bohuman);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // 카사트라 사용
            SKillUse(SkillName.Cassatra);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // 아샤 사용
            SKillUse(SkillName.Asha);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            // 아르마이티 사용
            SKillUse(SkillName.Armaity);
        }

    }

    void SKillUse(SkillName skillName)
    {
        if (gameObject.GetComponent<PlayerStatus>().canUse[(int)skillName])
        {
            gameObject.GetComponent<PlayerStatus>().UseSkill(skillName);
            gameObject.GetComponentInChildren<PlayerSpine>().SetAnimState(skillName);
        }
    }
}
