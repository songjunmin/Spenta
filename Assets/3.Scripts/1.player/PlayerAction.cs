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
            // ���ĸ� ���
            SKillUse(SkillName.bohuman);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // ī��Ʈ�� ���
            SKillUse(SkillName.cassatra);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // �ƻ� ���
            SKillUse(SkillName.asha);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            // �Ƹ�����Ƽ ���
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
