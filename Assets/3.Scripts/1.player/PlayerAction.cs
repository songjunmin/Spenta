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
        // �ٸ� �ൿ ���� ���
        if (gameObject.GetComponentInChildren<PlayerSpine>().animName != "Stand" && gameObject.GetComponentInChildren<PlayerSpine>().animName != "Run")
        {
            return;
        }
        // ���� ���� ���(�ӽ�)
        if (gameObject.GetComponent<PlayerMove>().isJump != 0)
        {
            return;
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            // ���ĸ� ���
            SKillUse(SkillName.Bohuman);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            // ī��Ʈ�� ���
            SKillUse(SkillName.Cassatra);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // �ƻ� ���
            SKillUse(SkillName.Asha);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            // �Ƹ�����Ƽ ���
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
