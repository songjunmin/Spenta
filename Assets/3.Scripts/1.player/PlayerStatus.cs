using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class PlayerStatus : MonoBehaviour
{
    public float hp;
    public float maxHp;

    // ���ĸ� , ī��Ʈ�� , �ƻ� , �Ƹ�����Ƽ

    public float[] coolTime = new float[4];
    public float[] curTime = new float[4];
    public bool[] canUse = new bool[4];
    public float[] skillDmg = new float[3];

    [Serializable]
    public class List
    {
        public int[] range = new int[2];

        public void ChangeRnage(int minusNum, int plusNum)
        {
            range[0] -= minusNum;
            range[1] += plusNum;
        }
    }
    public List[] skillRange = new List[3];

    /*
    // ���ĸ� ��Ÿ��
    public float bohumanCoolTime;
    public float bohumanCurTime;
    public bool bohumanSkill;

    // ī��Ʈ�� ��Ÿ��
    public float cassatraCoolTime;
    public float cassatraCurTime;
    public bool cassatraSkill;

    // �ƻ� ��Ÿ��
    public float ashaCoolTime;
    public float ashaCurTime;
    public bool ashaSkill;

    // �Ƹ�����Ƽ ��Ÿ��
    public float armaityCoolTime;
    public float armaityCurTime;
    public bool armaitySkill;
    */

    void Start()
    {
        
    }

    void Update()
    {
        CoolTimeMng();
    }

    public void CoolTimeMng()
    {
        for (int i = 0; i < 4; i++)
        {
            curTime[i] -= Time.deltaTime;

            if (curTime[i] < 0 && canUse[i] == false)
            {
                canUse[i] = true;
            }
        }
    }
    public void UseSkill(PlayerAction.SkillName skillName)
    {
        curTime[(int)skillName] = coolTime[(int)skillName];
        canUse[(int)skillName] = false;
    }
}
