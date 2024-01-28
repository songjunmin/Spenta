using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public enum SkillName
    {
        bohuman, cassatra, asha, armaity
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
            SKillUse(SkillName.bohuman);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // 카사트라 사용
            SKillUse(SkillName.cassatra);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // 아샤 사용
            SKillUse(SkillName.asha);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            // 아르마이티 사용
            SKillUse(SkillName.armaity);
        }

    }

    void SKillUse(SkillName skillName)
    {
        if (gameObject.GetComponent<PlayerStatus>().canUse[(int)skillName])
        {
            gameObject.GetComponent<PlayerStatus>().UseSkill((int)skillName);
        }
    }
}
