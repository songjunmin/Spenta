using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public enum SkillName
    {
        Bohuman, Cassatra, Asha, Armaity
    }


    public enum NonSkillName
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
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            // 막기 사용
            NonSkillUse(NonSkillName.Parrying);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            NonSkillUse(NonSkillName.Attack);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            NonSkillUse(NonSkillName.Dash);
        }

    }

    void SKillUse(SkillName skillName)
    {
        if (gameObject.GetComponent<PlayerStatus>().skillCanUse[(int)skillName])
        {
            gameObject.GetComponent<PlayerStatus>().Action(skillName);
            gameObject.GetComponentInChildren<PlayerSpine>().Action(skillName);
            gameObject.GetComponentInChildren<PlayerSkill>().Action(skillName);
        }

    }
    void NonSkillUse(NonSkillName nonSkillName)
    {
        if (gameObject.GetComponent<PlayerStatus>().nonSkillCanUse[(int)nonSkillName])
        {
            gameObject.GetComponent<PlayerStatus>().Action(nonSkillName);
            gameObject.GetComponentInChildren<PlayerSpine>().Action(nonSkillName);
            gameObject.GetComponentInChildren<PlayerSkill>().Action(nonSkillName);
        }

    }
}
